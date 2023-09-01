/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Turunan `CDatabase`
 *              :: Harap Didaftarkan Ke DI Container
 *              :: Instance Semua Database Bridge
 * 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using bifeldy_sd3_lib_452.Databases;
using bifeldy_sd3_lib_452.Handlers;
using bifeldy_sd3_lib_452.Models;

using KirimNPFileQR.Utilities;

namespace KirimNPFileQR.Handlers {

    public interface IDb : IDbHandler {
        Task<DataTable> GetNpLog();
        Task<DataTable> GetNpDetail(decimal log_seqno);
        Task<DataTable> GetNpHeader(string log_jenis, decimal log_no_npb, DateTime log_tgl_npb);
        Task UpdateAfterSendEmail(decimal log_seqno, string errMessage = null);
    }

    public sealed class CDb : CDbHandler, IDb {

        private readonly IApp _app;

        public CDb(IApp app, IOracle oracle, IPostgres postgres, IMsSQL mssql) : base(app, oracle, postgres, mssql) {
            _app = app;
        }

        public async Task<DataTable> GetNpLog() {
            return await OraPg.GetDataTableAsync(
                $@"
                    SELECT *
                    FROM (
                        SELECT
                            d.*, a.*, b.*
                            /*
                                d.hdr_nosj,
                                a.log_seqno,
                                a.log_dckode,
                                a.log_tok_kode,
                                a.log_no_npb,
                                a.log_tgl_npb,
                                a.log_namafile,
                                a.log_item,
                                a.log_stat_rcv,
                                a.log_jenis,
                                b.tok_name,
                                b.tok_kirim,
                                b.tok_email
                            */
                        FROM
                            DC_NPBTOKO_LOG a,
                            DC_TOKO_T b,
                            DC_TABEL_DC_T c,
                            DC_NPBTOKO_HDR d
                        WHERE
                            a.LOG_TOK_KODE = b.TOK_CODE 
                            AND a.LOG_DCKODE = c.TBL_DC_KODE
                            AND a.log_fk_id = d.hdr_id 
                            AND b.tok_recid IS NULL
                            AND b.tok_email IS NOT NULL
                            AND (
                                a.log_stat_rcv IS NOT NULL
                                AND UPPER(a.log_stat_rcv) NOT LIKE '%SUKSES%'
                                AND a.log_stat_rcv NOT LIKE '%- 00 -%'
                                AND a.log_stat_rcv NOT LIKE '%- 01 -%'
                            )
                            AND (
                                a.status_kirim_email IS NULL
                                OR (
                                    a.status_kirim_email NOT LIKE '%SUKSES%'
                                    AND a.kode_stat_krim_mail NOT LIKE '%00%'
                                )
                            )
                            AND LOG_JENIS IN ( 'NPB', 'NPL', 'NPR', 'NPX' )
                        ORDER BY
                            d.hdr_nosj ASC,
                            a.log_tgl_npb ASC
                    ) logs
                    WHERE ROWNUM <= 60
                "
            );
        }

        public async Task<DataTable> GetNpDetail(decimal log_seqno) {
            return await OraPg_GetDataTable(
                $@"
                    SELECT 
                        docno,
                        picno,
                        pictgl,
                        prdcd,
                        sj_qty,
                        TRUNC(price, 10) AS price,
                        TRUNC(ppnrp, 10) AS ppnrp,
	                    TRUNC(hpp, 10) AS hpp,
	                    keter,
	                    tanggal1,
                        tanggal2,
	                    docno2,
	                    dus_no,
	                    tglexp,
	                    ppn_rate,
	                    bkp,
	                    sub_bkp
                    FROM
                        dc_npbtoko_file
                    WHERE 
                         log_fk_seqno = :log_seqno
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno }
                }
            );
        }

        public async Task<DataTable> GetNpHeader(string log_jenis, decimal log_no_npb, DateTime log_tgl_npb) {
            string tblName1;
            string tblName2;
            switch (log_jenis.ToUpper()) {
                case "NPB":
                    tblName1 = "cluster_tbbackup";
                    tblName2 = "cluster_tbbackup_h";
                    break;
                case "NPL":
                    tblName1 = "cluster_canvas";
                    tblName2 = "cluster_canvas_h";
                    break;
                case "NPR":
                    tblName1 = "cluster_tbbackuproti";
                    tblName2 = "cluster_tbbackuproti_h";
                    break;
                case "NPX":
                    tblName1 = "cluster_tbbackupbuah";
                    tblName2 = "cluster_tbbackupbuah_h";
                    break;
                default:
                    throw new Exception($"Jenis {log_jenis} Belum Tersedia !!");
            }
            return await OraPg_GetDataTable(
                $@"
                    SELECT
                        nokunci,
                        pass || LPAD(TO_CHAR(nonpb), '6', '0') || TO_CHAR(tglnpb, 'ddmmyyyy') as norang,
                        nosj
                    FROM
                        {tblName1}
                    WHERE
                        nonpb = :log_no_npb
                        AND TRUNC(tglnpb) = TRUNC(:log_tgl_npb)
                    UNION
                    SELECT
                        nokunci,
                        pass || LPAD(TO_CHAR(nonpb), '6', '0') || TO_CHAR(tglnpb, 'ddmmyyyy') as norang,
                        nosj
                    FROM
                        {tblName2}
                    WHERE
                        nonpb = :log_no_npb
                        AND TRUNC(tglnpb) = TRUNC(:log_tgl_npb)
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_no_npb", VALUE = log_no_npb },
                    new CDbQueryParamBind { NAME = "log_tgl_npb", VALUE = log_tgl_npb }
                }
            );
        }

        public Task UpdateAfterSendEmail(decimal log_seqno, string errMessage = null) {
            return OraPg.ExecQueryAsync(
                $@"
                    UPDATE DC_NPBTOKO_LOG
                    SET
                        KIRIM_EMAIL = {(_app.IsUsingPostgres ? "NOW()" : "SYSDATE")},
                        {(
                            string.IsNullOrEmpty(errMessage) ? $@"
                                STATUS_KIRIM_EMAIL = 'SUKSES',
                                KODE_STAT_KRIM_MAIL = '00' 
                            " : $@"
                                STATUS_KIRIM_EMAIL = '{(errMessage.Length < 100 ? errMessage : errMessage.Substring(0, 99))}',
                                KODE_STAT_KRIM_MAIL = '-1'
                            "
                        )}
                    WHERE
                        LOG_SEQNO = :log_seqno
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno }
                }
            );
        }

    }

}
