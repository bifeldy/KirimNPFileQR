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
        Task<DataTable> GetNpCreateUlangQrCodeDetail(decimal log_seqno);
        Task<DataTable> GetNpCreateUlangQrCodeHeader(string log_jenis, decimal log_no_npb, DateTime log_tgl_npb);
        Task<DataTable> GetNpCreateUlangFileNp1(string log_jenis, decimal log_seqno, string tbl_dc_kode);
        Task<DataTable> GetNpCreateUlangFileNp2(string log_jenis, decimal log_seqno, string log_tok_kode, string log_typefile, DateTime log_tgl_npb);
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
                            AND a.LOG_JENIS IN ( 'NPB', 'NPL', 'NPR', 'NPX' )
                            AND a.LOG_TYPEFILE = 'WEB'
                        ORDER BY
                            d.hdr_nosj ASC,
                            a.log_tgl_npb ASC
                    ) logs
                    WHERE ROWNUM <= 60
                "
            );
        }

        public async Task<DataTable> GetNpCreateUlangQrCodeDetail(decimal log_seqno) {
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

        public async Task<DataTable> GetNpCreateUlangQrCodeHeader(string log_jenis, decimal log_no_npb, DateTime log_tgl_npb) {
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

        public async Task<DataTable> GetNpCreateUlangFileNp1(string log_jenis, decimal log_seqno, string log_dckode) {
            string query = string.Empty;
            List<CDbQueryParamBind> param = new List<CDbQueryParamBind>();
            switch (log_jenis.ToUpper()) {
                case "NPB":
                    query = $@"
                        SELECT
                            recid, rtype, docno, seqno, picno, picnot, pictgl, prdcd, nama, div, qty, sj_qty,
                            TRUNC (price, 6) price,
                            TRUNC (gross, 6) gross,
                            TRUNC (ppnrp, 6) ppnrp,
                            TRUNC (hpp, 6) hpp,
                            toko, keter, tanggal1, tanggal2, docno2, lt, rak, bar, kirim, dus_no, tglexp, ppn_rate, bkp, sub_bkp
                        FROM
                            dc_npbtoko_file a
                        WHERE
                            log_fk_seqno = :log_seqno
                        ORDER BY
                            docno ASC,
                            seqno ASC
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    break;
                case "NPL":
                    query = $@"
                        SELECT
                            recid, rtype, docno, seqno, picno, picnot, pictgl, prdcd, nama, div, qty, sj_qty,
                            TRUNC (price, 6) price,
                            TRUNC (gross, 6) gross,
                            TRUNC (ppnrp, 6) ppnrp,
                            TRUNC (hpp, 6) hpp,
                            toko, keter, tanggal1, tanggal2, docno2, lt, rak, bar, kirim, dus_no,
                            tglexp AS tgl_exp,
                            ppn_rate, bkp, sub_bkp
                        FROM
                            dc_npbtoko_file a 
                        WHERE
                            log_fk_seqno = :log_seqno
                        ORDER BY
                            docno ASC,
                            seqno ASC
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    break;
                case "NPR":
                    query = $@"
                        SELECT
                            RECID, RTYPE, DOCNO, SEQNO, PICNO, PICNOT, PICTGL, PRDCD, NAMA, DIV, QTY, SJ_QTY,
                            ROUND(PRICE, 3) AS PRICE,
                            ROUND(GROSS, 3) AS GROSS,
                            ROUND(PPNRP, 3) AS PPNRP,
                            ROUND(HPP, 3) AS HPP,
                            TOKO, KETER, TANGGAL1, TANGGAL2, DOCNO2, LT, RAK, BAR, KIRIM, DUS_NO, PPN_RATE, BKP, SUB_BKP
                        FROM
                            DC_NPBTOKO_FILE
                        WHERE
                            LOG_FK_SEQNO = :log_seqno
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    break;
                case "NPX":
                    query = $@"
                        SELECT
                            a.recid, a.rtype, a.docno, a.seqno, a.picno, a.picnot, a.pictgl, a.prdcd, a.nama, div, a.qty,
                            a.sj_qty, a.price, a.gross, a.ppnrp, a.hpp, a.toko, a.keter, tanggal1, a.tanggal2, a.docno2,
                            a.lt, a.rak, a.bar, a.kirim, a.dus_no, PPN_RATE, BKP, SUB_BKP, a.nl_qty
                        FROM
                            dc_npbtoko_file a, dc_barang_dc_v b, dc_barang_t d
                        WHERE
                            a.prdcd = b.mbr_fk_pluid
                            AND a.prdcd = d.mbr_pluid
                            AND b.tbl_dc_kode = :log_dckode
                            AND log_fk_seqno = :log_seqno
                            AND (a.sj_qty <> 0)
                            AND (a.sj_qty IS NOT NULL)
                        ORDER BY
                            a.prdcd ASC
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    param.Add(new CDbQueryParamBind { NAME = "log_dckode", VALUE = log_dckode });
                    break;
                default:
                    throw new Exception($"Jenis {log_jenis} Belum Tersedia !!");
            }
            return await OraPg_GetDataTable(query, param);
        }

        public async Task<DataTable> GetNpCreateUlangFileNp2(string log_jenis, decimal log_seqno, string log_tok_kode, string log_typefile, DateTime log_tgl_npb) {
            string query = string.Empty;
            List<CDbQueryParamBind> param = new List<CDbQueryParamBind>();
            switch (log_jenis.ToUpper()) {
                case "NPB": // RPB
                    query = $@"
                        SELECT
                            docno AS DocNo,
                            tanggal1 AS doc_date,
                            toko,
                            kirim AS gudang,
                            COUNT(*) AS item,
                            SUM(sj_qty) AS qty, 
                            TRUNC(SUM(gross), 6) AS gross,
                            NULL AS koli,
                            NULL AS kubikasi,
                            NULL AS LPG
                        FROM
                            dc_npbtoko_file a
                        WHERE
                            log_fk_seqno = :log_seqno
                        GROUP BY
                            docno,
                            tanggal1,
                            toko,
                            kirim
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    break;
                case "NPL": // RPL
                    query = $@"
                        SELECT
                            docno AS DocNo,
                            tanggal1 AS doc_date,
                            toko,
                            kirim AS gudang,
                            COUNT(*) AS item,
                            SUM(sj_qty) AS qty,
                            TRUNC(SUM(gross), 6) AS gross,
                            NULL AS koli,
                            NULL AS kubikasi,
                            NULL AS LPG
                        FROM
                            dc_npbtoko_file a
                        WHERE
                            log_fk_seqno = :log_seqno
                        GROUP BY
                            docno,
                            tanggal1,
                            toko,
                            kirim
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    break;
                case "NPR": // XPR
                    query = $@"
                        SELECT
                            LOG_NO_NPB AS DOCNO,
                            TO_CHAR(LOG_TGL_NPB, 'dd-MM-yyyy') AS DOC_DATE,
                            LOG_TOK_KODE AS TOKO,
                            LOG_DCKODE AS GUDANG,
                            LOG_ITEM AS ITEM,
                            LOG_QTY AS QTY,
                            ROUND(LOG_GROSS, 3) AS GROSS,
                            ROUND(LOG_KUBIKASI, 3) AS KUBIKASI,
                            NULL AS LPG
                        FROM
                            DC_NPBTOKO_LOG
                        WHERE
                            LOG_TOK_KODE = :log_tok_kode
                            AND TRUNC(LOG_TGL_NPB) = TO_DATE(:log_tgl_npb, 'dd/MM/yyyy')
                            AND LOG_TYPEFILE = :log_typefile
                            AND LOG_JENIS = :log_jenis
                            AND LOG_SEQNO = :log_seqno
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    param.Add(new CDbQueryParamBind { NAME = "log_tok_kode", VALUE = log_tok_kode });
                    param.Add(new CDbQueryParamBind { NAME = "log_jenis", VALUE = log_jenis });
                    param.Add(new CDbQueryParamBind { NAME = "log_typefile", VALUE = log_typefile });
                    param.Add(new CDbQueryParamBind { NAME = "log_tgl_npb", VALUE = $"{log_tgl_npb:dd/MM/yyyy}" });
                    break;
                case "NPX": // RPX
                    query = $@"
                        SELECT
                            log_no_npb AS docno,
                            TO_CHAR(log_tgl_npb, 'dd-MM-yyyy') AS doc_date,
                            log_tok_kode AS toko,
                            log_dckode AS gudang,
                            log_item AS item,
                            log_qty AS qty,
                            TRUNC(log_gross, 7) AS gross,
                            log_kubikasi AS kubikasi,
                            NULL AS lpg
                        FROM
                            dc_npbtoko_log
                        WHERE
                            log_tok_kode = :log_tok_kode
                            AND TRUNC(LOG_TGL_NPB) = TO_DATE(:log_tgl_npb, 'dd/MM/yyyy')
                            AND LOG_TYPEFILE = :log_typefile
                            AND LOG_JENIS = :log_jenis
                            AND log_no_npb IS NOT NULL
                            AND LOG_SEQNO = :log_seqno
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    param.Add(new CDbQueryParamBind { NAME = "log_tok_kode", VALUE = log_tok_kode });
                    param.Add(new CDbQueryParamBind { NAME = "log_jenis", VALUE = log_jenis });
                    param.Add(new CDbQueryParamBind { NAME = "log_typefile", VALUE = log_typefile });
                    param.Add(new CDbQueryParamBind { NAME = "log_tgl_npb", VALUE = $"{log_tgl_npb:dd/MM/yyyy}" });
                    break;
                default:
                    throw new Exception($"Jenis {log_jenis} Belum Tersedia !!");
            }
            return await OraPg_GetDataTable(query, param);
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
