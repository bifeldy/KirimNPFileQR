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
using bifeldy_sd3_lib_452.Utilities;

using KirimNPFileQR.Utilities;

namespace KirimNPFileQR.Handlers {

    public interface IDb : IDbHandler {
        Task<decimal> CheckWrcMainTenis();
        Task<DataTable> GetNpLogHeaderQrEmail();
        Task<DataTable> GetNpLogHeaderJsonByte();
        Task<DataTable> GetNpLogDetailQrEmail(string log_namafile);
        Task<DataTable> GetNpCreateUlangQrCodeDetail(decimal log_seqno);
        Task<DataTable> GetNpCreateUlangQrCodeHeader(string log_jenis, decimal log_no_npb, DateTime log_tgl_npb);
        Task<DataTable> GetNpCreateUlangFileNp1(string log_jenis, decimal[] log_seqno, string tbl_dc_kode);
        Task<DataTable> GetNpCreateUlangFileNp2(string log_jenis, decimal[] log_seqno, string log_tok_kode, string log_typefile);
        Task<bool> UpdateAfterSendEmail(decimal[] log_seqno, string errMessage = null);
        Task<decimal> CheckCreateUlangJsonByte(string log_dckode, string log_tok_kode, string log_namafile);
        Task<string> GetURLWRC(string log_tok_kode);
        Task<DataTable> GetNpHeaderJsonByte(string log_dckode, string log_tok_kode, string log_namafile);
        Task<DataTable> GetNpDetailJsonByte(decimal log_seqno);
        Task<bool> UpdateAfterSendWebService(decimal[] log_seqno, string errMessage = null);
        Task<bool> UpdateBeforeSendWebService(decimal[] log_seqno, string errMessage = null);
        Task<bool> UpdateSudahDiProsesDuluan(string log_namafile, decimal log_no_npb);
    }

    public sealed class CDb : CDbHandler, IDb {

        private readonly IApp _app;

        public CDb(
            IApp app,
            IConfig config,
            IOracle oracle,
            IPostgres postgres,
            IMsSQL mssql,
            ISqlite sqlite
        ) : base(app, config, oracle, postgres, mssql, sqlite) {
            _app = app;
        }

        public async Task<decimal> CheckWrcMainTenis() {
            return await OraPg.ExecScalarAsync<decimal>(
                _app.IsUsingPostgres ? $@"
                    SELECT
                        COALESCE(TAB.delayy, 0) AS DELAYY, t1.KETER
                    FROM
                        dc_npweb_delay_v AS t1
                        LEFT JOIN (
                            SELECT
                                COUNT(1) AS DELAYY, keter
                            FROM
                                dc_npweb_delay_v AS t2
                            WHERE
                                jam_db BETWEEN jam_awal AND jam_akhir
                            GROUP BY keter
                        ) TAB ON t1.keter = TAB.keter
                " : $@"
                    SELECT
                        COUNT(1) AS DELAYY, keter
                    FROM
                        dc_npweb_delay_v
                    WHERE
                        jam_db BETWEEN jam_awal AND jam_akhir
                "
            );
        }

        public async Task<DataTable> GetNpLogHeaderQrEmail() {
            return await OraPg.GetDataTableAsync(
                $@"
                    SELECT
                    DISTINCT
                        a.log_dckode,
                        a.log_tok_kode,
                        a.log_namafile,
                        b.tok_name,
                        b.tok_kirim,
                        b.tok_email,
                        a.LOG_TYPEFILE,
                        a.LOG_JENIS,
                        a.log_stat_rcv
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
                        AND a.LOG_TYPEFILE = 'WEB'
                        AND a.LOG_JENIS IN ( 'NPB', 'NPL', 'NPR', 'NPX' )
                "
            );
        }

        public async Task<DataTable> GetNpLogHeaderJsonByte() {
            return await OraPg.GetDataTableAsync(
                $@"
                    SELECT
                    DISTINCT
                        a.log_dckode,
                        a.log_tok_kode,
                        a.log_namafile,
                        b.tok_name,
                        b.tok_kirim,
                        a.LOG_TYPEFILE,
                        a.LOG_JENIS,
                        a.log_stat_rcv,
                        a.LOG_STAT_GET,
                        a.LOG_STAT_PROSES
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
                        AND (
                            a.log_stat_rcv IS NOT NULL
                            AND UPPER(a.log_stat_rcv) NOT LIKE '%SUKSES%'
                            AND a.log_stat_rcv NOT LIKE '%- 00 -%'
                            AND a.log_stat_rcv NOT LIKE '%- 01 -%'
                        )
                        /*
                            AND NOT EXISTS (
                                SELECT *
                                FROM dc_npbtoko_log e
                                WHERE
                                    e.log_typefile = 'CSV'
                                    AND e.log_dckode = a.log_dckode
                                    AND e.log_tok_kode = a.log_tok_kode
                                    AND e.log_namafile = a.log_namafile
                            )
                        */
                        AND a.LOG_TYPEFILE = 'WEB'
                "
            );
        }

        public async Task<DataTable> GetNpLogDetailQrEmail(string log_namafile) {
            return await OraPg.GetDataTableAsync(
                $@"
                    SELECT

                        /* DC_NPBTOKO_HDR */
                        d.HDR_DCKODE,
                        d.HDR_LOG_ALOKASID,
                        d.HDR_NOSJ,
                        d.HDR_TGLSJ,
                        d.HDR_TOK_KODE,
                        d.HDR_ID,
                        d.HDR_TGL,
                        d.HDR_KETER,
                        d.HDR_RECID,
                        d.HDR_TYPE,
                        d.HDR_JENIS,
                        d.HDR_TOTREC_NPB,

                        /* DC_NPBTOKO_LOG */
                        a.LOG_DCKODE,
                        a.LOG_LOKID,
                        a.LOG_ALOKASIID,
                        a.LOG_TOK_KODE,
                        a.LOG_NO_NPB,
                        a.LOG_TGL_NPB,
                        a.LOG_NO_INV,
                        a.LOG_TGL_INV,
                        a.LOG_HDRID,
                        a.LOG_ITEM,
                        a.LOG_QTY,
                        ROUND(a.LOG_GROSS, 6) LOG_GROSS,
                        a.LOG_KOLI,
                        a.LOG_KUBIKASI,
                        a.LOG_SEQNO,
                        a.LOG_STAT_CREATE,
                        a.LOG_STAT_SEND,
                        a.LOG_STAT_RCV,
                        a.LOG_NAMAFILE,
                        a.LOG_FK_ID,
                        a.LOG_TYPEFILE,
                        a.LOG_JENIS,
                        a.LOG_STAT_GET,
                        a.LOG_STAT_PROSES,
                        a.LOG_NOBPB,
                        a.LOG_JML_ITEMBPB,
                        a.LOG_KIRIM,
                        a.LOG_IP_WEBREKAP,
                        a.LOG_CABANG,
                        a.LOG_RE_TRY,
                        a.LOG_IP_IISKIRIM,
                        a.KIRIM_EMAIL,
                        a.STATUS_KIRIM_EMAIL,
                        a.KODE_STAT_KRIM_MAIL,

                        /* DC_TOKO_T */
                        b.TOK_ID,
                        b.TOK_CODE,
                        b.TOK_NAME,
                        b.TOK_KIRIM,
                        b.TOK_JENIS_TOKO,
                        b.TOK_TGL_BUKA,
                        b.TOK_TGL_TUTUP,
                        b.TOK_RECID,
                        b.TOK_UPDREC_ID,
                        b.TOK_UPDREC_DATE,
                        b.TOK_TGL_PKM,
                        b.TOK_BMGR_CODE,
                        b.TOK_BMGR_NAME,
                        b.TOK_AMGR_CODE,
                        b.TOK_AMGR_NAME,
                        b.TOK_ASPV_CODE,
                        b.TOK_ASPV_NAME,
                        b.TOK_TYPE_TOKO,
                        b.TOK_KATEGORI,
                        b.TOK_ALAMAT,
                        b.TOK_TELP_1,
                        b.TOK_FREKKIRIM_HARI,
                        b.TOK_JADWAL_BUAH,
                        b.TOK_JADWAL_ELPIJI,
                        b.TOK_TYPE_RAK,
                        b.TOK_JARAKDC_KM,
                        b.TOK_KOTA,
                        b.TOK_KODE_POS,
                        b.TOK_FAX_1,
                        b.TOK_PKP,
                        b.TOK_NPWP,
                        b.TOK_SKP,
                        b.TOK_TGL_SKP,
                        b.TOK_FLAG_REGULER,
                        b.TOK_MARKUP,
                        b.TOK_CID_CODE,
                        b.TOK_TGL_BERLAKU,
                        b.TOK_OLD_CODE,
                        b.TOK_NEW_CODE,
                        b.TOK_CABANG,
                        b.TOK_KIRIM_KONV,
                        b.TOK_JADWAL_KONV,
                        b.TOK_KIRIM_BUAH,
                        b.TOK_KIRIM_LPG,
                        b.TOK_KIRIM_SEKUNDER,
                        b.TOK_TGLSEK_AWAL,
                        b.TOK_TGLSEK_AKHIR,
                        b.TOK_TGL_8DIGIT,
                        b.TOK_8DIGIT,
                        b.TOK_IMOBILE,
                        b.TOK_EMAIL,
                        b.TOK_KODEPOS,
                        b.TOK_EVENT,
                        b.TOK_TGL_REOPENING,
                        b.TOK_PERDA,
                        b.TOK_TGL_AWAL_PERDA,
                        b.TOK_TGL_AKHIR_PERDA,
                        b.TOK_KIRIM_WH,
                        b.TOK_TGL_MULTIRATES,
                        b.TOK_KIRIM_IGR

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
                        AND log_namafile = :log_namafile
                    ORDER BY
                        d.hdr_nosj ASC,
                        a.log_tgl_npb ASC
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_namafile", VALUE = log_namafile }
                }
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
                        ROUND(price, 6) AS price,
                        ROUND(ppnrp, 6) AS ppnrp,
                        ROUND(hpp, 6) AS hpp,
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
                        pass || LPAD({(_app.IsUsingPostgres ? "nonpb::TEXT" : "TO_CHAR(nonpb)")}, '6', '0') || TO_CHAR(tglnpb, 'ddMMyyyy') as norang,
                        nosj
                    FROM
                        {tblName1}
                    WHERE
                        nonpb = :log_no_npb
                        AND TRUNC(tglnpb) = TRUNC(:log_tgl_npb)
                    UNION
                    SELECT
                        nokunci,
                        pass || LPAD({(_app.IsUsingPostgres ? "nonpb::TEXT" : "TO_CHAR(nonpb)")}, '6', '0') || TO_CHAR(tglnpb, 'ddMMyyyy') as norang,
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

        public async Task<DataTable> GetNpCreateUlangFileNp1(string log_jenis, decimal[] log_seqno, string log_dckode) {
            string query = string.Empty;
            List<CDbQueryParamBind> param = new List<CDbQueryParamBind>();
            switch (log_jenis.ToUpper()) {
                case "NPB":
                    query = $@"
                        SELECT
                            recid, rtype, docno, seqno, picno, picnot, pictgl, prdcd, nama, div, qty, sj_qty,
                            ROUND(price, 6) price,
                            ROUND(gross, 6) gross,
                            ROUND(ppnrp, 6) ppnrp,
                            ROUND(hpp, 6) hpp,
                            toko, keter, tanggal1, tanggal2, docno2, lt, rak, bar, kirim, dus_no, tglexp, ppn_rate, bkp, sub_bkp
                        FROM
                            dc_npbtoko_file a
                        WHERE
                            log_fk_seqno IN (:log_seqno)
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
                            ROUND(price, 6) price,
                            ROUND(gross, 6) gross,
                            ROUND(ppnrp, 6) ppnrp,
                            ROUND(hpp, 6) hpp,
                            toko, keter, tanggal1, tanggal2, docno2, lt, rak, bar, kirim, dus_no,
                            tglexp AS tgl_exp,
                            ppn_rate, bkp, sub_bkp
                        FROM
                            dc_npbtoko_file a
                        WHERE
                            log_fk_seqno IN (:log_seqno)
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
                            ROUND(PRICE, 6) AS PRICE,
                            ROUND(GROSS, 6) AS GROSS,
                            ROUND(PPNRP, 6) AS PPNRP,
                            ROUND(HPP, 6) AS HPP,
                            TOKO, KETER, TANGGAL1, TANGGAL2, DOCNO2, LT, RAK, BAR, KIRIM, DUS_NO, PPN_RATE, BKP, SUB_BKP
                        FROM
                            DC_NPBTOKO_FILE
                        WHERE
                            LOG_FK_SEQNO IN (:log_seqno)
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    break;
                case "NPX":
                    query = $@"
                        SELECT
                            a.recid, a.rtype, a.docno, a.seqno, a.picno, a.picnot, a.pictgl, a.prdcd, a.nama, div, a.qty, a.sj_qty,
                            ROUND(price, 6) price,
                            ROUND(gross, 6) gross,
                            ROUND(ppnrp, 6) ppnrp,
                            ROUND(hpp, 6) hpp,
                            a.toko, a.keter, tanggal1, a.tanggal2, a.docno2, a.lt, a.rak, a.bar, a.kirim, a.dus_no, PPN_RATE, BKP, SUB_BKP, a.nl_qty
                        FROM
                            dc_npbtoko_file a, dc_barang_dc_v b, dc_barang_t d
                        WHERE
                            a.prdcd = b.mbr_fk_pluid
                            AND a.prdcd = d.mbr_pluid
                            AND b.tbl_dc_kode = :log_dckode
                            AND log_fk_seqno IN (:log_seqno)
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

        public async Task<DataTable> GetNpCreateUlangFileNp2(string log_jenis, decimal[] log_seqno, string log_tok_kode, string log_typefile) {
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
                            ROUND(SUM(gross), 6) AS gross,
                            NULL AS koli,
                            NULL AS kubikasi,
                            NULL AS LPG
                        FROM
                            dc_npbtoko_file a
                        WHERE
                            log_fk_seqno IN (:log_seqno)
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
                            ROUND(SUM(gross), 6) AS gross,
                            NULL AS koli,
                            NULL AS kubikasi,
                            NULL AS LPG
                        FROM
                            dc_npbtoko_file a
                        WHERE
                            log_fk_seqno IN (:log_seqno)
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
                            ROUND(LOG_GROSS, 6) AS GROSS,
                            ROUND(LOG_KUBIKASI, 6) AS KUBIKASI,
                            NULL AS LPG
                        FROM
                            DC_NPBTOKO_LOG
                        WHERE
                            LOG_TOK_KODE = :log_tok_kode
                            AND LOG_TYPEFILE = :log_typefile
                            AND LOG_JENIS = :log_jenis
                            AND LOG_SEQNO IN (:log_seqno)
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    param.Add(new CDbQueryParamBind { NAME = "log_tok_kode", VALUE = log_tok_kode });
                    param.Add(new CDbQueryParamBind { NAME = "log_jenis", VALUE = log_jenis });
                    param.Add(new CDbQueryParamBind { NAME = "log_typefile", VALUE = log_typefile });
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
                            ROUND(log_gross, 6) AS gross,
                            log_kubikasi AS kubikasi,
                            NULL AS lpg
                        FROM
                            dc_npbtoko_log
                        WHERE
                            log_tok_kode = :log_tok_kode
                            AND LOG_TYPEFILE = :log_typefile
                            AND LOG_JENIS = :log_jenis
                            AND log_no_npb IS NOT NULL
                            AND LOG_SEQNO IN (:log_seqno)
                    ";
                    param.Add(new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno });
                    param.Add(new CDbQueryParamBind { NAME = "log_tok_kode", VALUE = log_tok_kode });
                    param.Add(new CDbQueryParamBind { NAME = "log_jenis", VALUE = log_jenis });
                    param.Add(new CDbQueryParamBind { NAME = "log_typefile", VALUE = log_typefile });
                    break;
                default:
                    throw new Exception($"Jenis {log_jenis} Belum Tersedia !!");
            }
            return await OraPg_GetDataTable(query, param);
        }

        public async Task<bool> UpdateAfterSendEmail(decimal[] log_seqno, string errMessage = null) {
            return await OraPg.ExecQueryAsync(
                $@"
                    UPDATE DC_NPBTOKO_LOG
                    SET
                        KIRIM_EMAIL = {(_app.IsUsingPostgres ? "NOW()" : "SYSDATE")},
                        {(
                            string.IsNullOrEmpty(errMessage) ? $@"
                                STATUS_KIRIM_EMAIL = 'SUKSES',
                                KODE_STAT_KRIM_MAIL = '00'
                            " : $@"
                                STATUS_KIRIM_EMAIL = 'ERROR - {(errMessage.Length <= 90 ? errMessage : errMessage.Substring(0, 90))}',
                                KODE_STAT_KRIM_MAIL = '-1'
                            "
                        )}
                    WHERE
                        LOG_SEQNO IN (:log_seqno)
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno }
                }
            );
        }

        public async Task<decimal> CheckCreateUlangJsonByte(string log_dckode, string log_tok_kode, string log_namafile) {
            return await OraPg.ExecScalarAsync<decimal>(
                $@"
                    SELECT
                        COUNT(1)
                    FROM
                        dc_npbtoko_log
                    WHERE
                        LOG_TYPEFILE = 'CSV'
                        AND log_dckode = :log_dckode
                        AND log_tok_kode = :log_tok_kode
                        AND log_namafile = :log_namafile
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_dckode", VALUE = log_dckode },
                    new CDbQueryParamBind { NAME = "log_tok_kode", VALUE = log_tok_kode },
                    new CDbQueryParamBind { NAME = "log_namafile", VALUE = log_namafile }
                }
            );
        }

        public async Task<string> GetURLWRC(string log_tok_kode) {
            return await OraPg.ExecScalarAsync<string>(
                $@"
                    SELECT
                        URL
                    FROM
                        TOKO_WSSETTING
                    WHERE
                        TIPE = 'NPL'
                        AND {(_app.IsUsingPostgres ? "COALESCE" : "NVL")}(
                            TBL_DC_KODE, (
                                SELECT
                                    TBL_DC_KODE
                                FROM
                                    DC_TABEL_DC_T
                            )
                        ) = (
                            SELECT
                                {(_app.IsUsingPostgres ? "COALESCE" : "NVL")}(TOK_KIRIM_SEKUNDER, TOK_KIRIM)
                            FROM
                                DC_TOKO_T
                            WHERE
                                TOK_CODE = :log_tok_kode
                        )
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_tok_kode", VALUE = log_tok_kode }
                }
            );
        }

        public async Task<DataTable> GetNpHeaderJsonByte(string log_dckode, string log_tok_kode, string log_namafile) {
            return await OraPg.GetDataTableAsync(
                $@"
                    SELECT
                        a.log_seqno,
                        a.log_dckode AS KIRIM,
                        a.log_tok_kode AS toko,
                        a.log_no_npb AS DOCNO,
                        TO_CHAR(a.log_tgl_npb, 'dd-MM-yyyy') AS PICTGL,
                        a.log_namafile AS NAMAFILE,
                        a.log_item AS ITEM,
                        TO_CHAR({(_app.IsUsingPostgres ? "NOW()" : "SYSDATE")}, 'yyyyMMdd') || {(_app.IsUsingPostgres ? "COALESCE" : "NVL")}(b.TOK_KIRIM_SEKUNDER, {(_app.IsUsingPostgres ? "COALESCE" : "NVL")}(c.TBL_DC_INDUK, c.TBL_DC_KODE)) AS sysDatekodeDC
                    FROM
                        DC_NPBTOKO_LOG a,
                        DC_TOKO_T b,
                        DC_TABEL_DC_T c
                    WHERE
                        a.LOG_TOK_KODE = b.TOK_CODE
                        AND a.LOG_DCKODE = c.TBL_DC_KODE
                        AND (
                            a.log_stat_rcv IS NOT NULL
                            AND UPPER(a.log_stat_rcv) NOT LIKE '%SUKSES%'
                            AND a.log_stat_rcv NOT LIKE '%- 00 -%'
                            AND a.log_stat_rcv NOT LIKE '%- 01 -%'
                        )
                        AND a.LOG_TYPEFILE = 'WEB'
                        AND a.log_dckode = :log_dckode
                        AND a.log_tok_kode = :log_tok_kode
                        AND a.log_namafile = :log_namafile
                    ORDER BY
                        a.log_seqno ASC
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_dckode", VALUE = log_dckode },
                    new CDbQueryParamBind { NAME = "log_tok_kode", VALUE = log_tok_kode },
                    new CDbQueryParamBind { NAME = "log_namafile", VALUE = log_namafile }
                }
            );
        }

        public async Task<DataTable> GetNpDetailJsonByte(decimal log_seqno) {
            return await OraPg.GetDataTableAsync(
                $@"
                    SELECT
                        RECID,
                        RTYPE,
                        DOCNO,
                        SEQNO,
                        PICNO,
                        PICNOT,
                        PICTGL,
                        {(_app.IsUsingPostgres ? "PRDCD::TEXT" : "TO_CHAR(PRDCD)")} AS PRDCD,
                        NAMA,
                        DIV,
                        QTY,
                        SJ_QTY,
                        ROUND(PRICE, 3) AS PRICE,
                        ROUND(GROSS, 3) AS GROSS,
                        ROUND(PPNRP, 3) AS PPNRP,
                        ROUND(HPP, 3) AS HPP,
                        TOKO,
                        KETER,
                        TANGGAL1,
                        TANGGAL2,
                        DOCNO2,
                        LT,
                        RAK,
                        BAR,
                        KIRIM,
                        DUS_NO,
                        TGLEXP,
                        PPN_RATE,
                        BKP,
                        SUB_BKP
                    FROM
                        DC_NPBTOKO_FILE
                    WHERE
                        log_fk_seqno = :log_seqno
                    ORDER BY
                        log_fk_seqno ASC,
                        seqno ASC
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno }
                }
            );
        }

        public async Task<bool> UpdateAfterSendWebService(decimal[] log_seqno, string errMessage = null) {
            return await OraPg.ExecQueryAsync(
                $@"
                    UPDATE
                        DC_NPBTOKO_LOG
                    SET
                        LOG_STAT_RCV = TO_CHAR({(_app.IsUsingPostgres ? "NOW()" : "SYSDATE")}, 'dd/MM/yyyy HH24:mi:ss') || ' - AutoResend - {(
                            string.IsNullOrEmpty(errMessage) ?
                                "00 - Sukses." : $@"ERROR :: {(
                                    errMessage.Length < 40 ?
                                        errMessage : errMessage.Substring(0, 40)
                                )}"
                        )}'
                    WHERE
                        LOG_SEQNO IN (:log_seqno)
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno }
                }
            );
        }

        public async Task<bool> UpdateBeforeSendWebService(decimal[] log_seqno, string errMessage = null) {
            return await OraPg.ExecQueryAsync(
                $@"
                    UPDATE
                        DC_NPBTOKO_LOG
                    SET
                        LOG_STAT_SEND = TO_CHAR({(_app.IsUsingPostgres ? "NOW()" : "SYSDATE")}, 'dd/MM/yyyy HH24:mi:ss') || ' - AutoResend - {(
                            string.IsNullOrEmpty(errMessage) ?
                                "OK" : $@"ERROR :: {(
                                    errMessage.Length < 40 ?
                                        errMessage : errMessage.Substring(0, 40)
                                )}"
                        )}'
                    WHERE
                        LOG_SEQNO IN (:log_seqno)
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_seqno", VALUE = log_seqno }
                }
            );
        }

        public async Task<bool> UpdateSudahDiProsesDuluan(string log_namafile, decimal log_no_npb) {
            return await OraPg.ExecQueryAsync(
                $@"
                    UPDATE
                        DC_NPBTOKO_LOG a
                    SET
                        LOG_STAT_RCV = TO_CHAR({(_app.IsUsingPostgres ? "NOW()" : "SYSDATE")}, 'dd/MM/yyyy HH24:mi:ss') || ' - AutoResend - 00 - Sukses.'
                    WHERE
                        LOG_NAMAFILE = :log_namafile
                        AND LOG_NO_NPB = :log_no_npb
                        AND LOG_TYPEFILE = 'WEB'
                        AND EXISTS
                        (
                            SELECT 1
                            FROM DC_NPBTOKO_LOG b 
                            WHERE
                                a.log_namafile = b.log_namafile 
                                AND a.log_no_npb = b.log_no_npb 
                                AND (
                                    LOG_STAT_GET IS NOT NULL
                                    AND LOG_STAT_PROSES  IS NOT NULL
                                )
                        )
                ",
                new List<CDbQueryParamBind> {
                    new CDbQueryParamBind { NAME = "log_namafile", VALUE = log_namafile },
                    new CDbQueryParamBind { NAME = "log_no_npb", VALUE = log_no_npb }
                }
            );
        }

    }

}
