/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Model Tabel NP* Header & Detail
 *              :: Seharusnya Tidak Untuk Didaftarkan Ke DI Container
 * 
 */

using System;

namespace KirimNPFileQR.Models {

    public sealed class MNpLog : DC_NPBTOKO_LOG, DC_TOKO_T, DC_NPBTOKO_HDR {

        /* DC_NPBTOKO_HDR */
        public string HDR_DCKODE { get; set; }
        public decimal HDR_LOG_ALOKASID { get; set; }
        public string HDR_NOSJ { get; set; }
        public DateTime HDR_TGLSJ { get; set; }
        public string HDR_TOK_KODE { get; set; }
        public decimal HDR_ID { get; set; }
        public DateTime HDR_TGL { get; set; }
        public string HDR_KETER { get; set; }
        public decimal HDR_RECID { get; set; }
        public string HDR_TYPE { get; set; }
        public string HDR_JENIS { get; set; }
        public decimal HDR_TOTREC_NPB { get; set; }

        /* DC_NPBTOKO_LOG */
        public string LOG_DCKODE { get; set; }
        public decimal LOG_LOKID { get; set; }
        public decimal LOG_ALOKASIID { get; set; }
        public string LOG_TOK_KODE { get; set; }
        public decimal LOG_NO_NPB { get; set; }
        public DateTime LOG_TGL_NPB { get; set; }
        public string LOG_NO_INV { get; set; }
        public DateTime LOG_TGL_INV { get; set; }
        public decimal LOG_HDRID { get; set; }
        public decimal LOG_ITEM { get; set; }
        public decimal LOG_QTY { get; set; }
        public decimal LOG_GROSS { get; set; }
        public decimal LOG_KOLI { get; set; }
        public decimal LOG_KUBIKASI { get; set; }
        public decimal LOG_SEQNO { get; set; }
        public string LOG_STAT_CREATE { get; set; }
        public string LOG_STAT_SEND { get; set; }
        public string LOG_STAT_RCV { get; set; }
        public string LOG_NAMAFILE { get; set; }
        public decimal LOG_FK_ID { get; set; }
        public string LOG_TYPEFILE { get; set; }
        public string LOG_JENIS { get; set; }
        public string LOG_STAT_GET { get; set; }
        public string LOG_STAT_PROSES { get; set; }
        public string LOG_NOBPB { get; set; }
        public decimal LOG_JML_ITEMBPB { get; set; }
        public DateTime LOG_KIRIM { get; set; }
        public string LOG_IP_WEBREKAP { get; set; }
        public string LOG_CABANG { get; set; }
        public decimal LOG_RE_TRY { get; set; }
        public string LOG_IP_IISKIRIM { get; set; }
        public DateTime KIRIM_EMAIL { get; set; }
        public string STATUS_KIRIM_EMAIL { get; set; }
        public string KODE_STAT_KRIM_MAIL { get; set; }

        /* DC_TOKO_T */
        public decimal TOK_ID { get; set; }
        public string TOK_CODE { get; set; }
        public string TOK_NAME { get; set; }
        public string TOK_KIRIM { get; set; }
        public string TOK_JENIS_TOKO { get; set; }
        public DateTime TOK_TGL_BUKA { get; set; }
        public DateTime TOK_TGL_TUTUP { get; set; }
        public string TOK_RECID { get; set; }
        public string TOK_UPDREC_ID { get; set; }
        public DateTime TOK_UPDREC_DATE { get; set; }
        public DateTime TOK_TGL_PKM { get; set; }
        public string TOK_BMGR_CODE { get; set; }
        public string TOK_BMGR_NAME { get; set; }
        public string TOK_AMGR_CODE { get; set; }
        public string TOK_AMGR_NAME { get; set; }
        public string TOK_ASPV_CODE { get; set; }
        public string TOK_ASPV_NAME { get; set; }
        public string TOK_TYPE_TOKO { get; set; }
        public string TOK_KATEGORI { get; set; }
        public string TOK_ALAMAT { get; set; }
        public string TOK_TELP_1 { get; set; }
        public decimal TOK_FREKKIRIM_HARI { get; set; }
        public string TOK_JADWAL_BUAH { get; set; }
        public string TOK_JADWAL_ELPIJI { get; set; }
        public string TOK_TYPE_RAK { get; set; }
        public decimal TOK_JARAKDC_KM { get; set; }
        public string TOK_KOTA { get; set; }
        public string TOK_KODE_POS { get; set; }
        public string TOK_FAX_1 { get; set; }
        public string TOK_PKP { get; set; }
        public string TOK_NPWP { get; set; }
        public string TOK_SKP { get; set; }
        public DateTime TOK_TGL_SKP { get; set; }
        public string TOK_FLAG_REGULER { get; set; }
        public decimal TOK_MARKUP { get; set; }
        public string TOK_CID_CODE { get; set; }
        public DateTime TOK_TGL_BERLAKU { get; set; }
        public string TOK_OLD_CODE { get; set; }
        public string TOK_NEW_CODE { get; set; }
        public string TOK_CABANG { get; set; }
        public string TOK_KIRIM_KONV { get; set; }
        public string TOK_JADWAL_KONV { get; set; }
        public string TOK_KIRIM_BUAH { get; set; }
        public string TOK_KIRIM_LPG { get; set; }
        public string TOK_KIRIM_SEKUNDER { get; set; }
        public DateTime TOK_TGLSEK_AWAL { get; set; }
        public DateTime TOK_TGLSEK_AKHIR { get; set; }
        public DateTime TOK_TGL_8DIGIT { get; set; }
        public string TOK_8DIGIT { get; set; }
        public string TOK_IMOBILE { get; set; }
        public string TOK_EMAIL { get; set; }
        public string TOK_KODEPOS { get; set; }
        public string TOK_EVENT { get; set; }
        public DateTime TOK_TGL_REOPENING { get; set; }
        public string TOK_PERDA { get; set; }
        public DateTime TOK_TGL_AWAL_PERDA { get; set; }
        public DateTime TOK_TGL_AKHIR_PERDA { get; set; }
        public string TOK_KIRIM_WH { get; set; }
        public DateTime TOK_TGL_MULTIRATES { get; set; }
        public string TOK_KIRIM_IGR { get; set; }
    }

    public sealed class MNpCreateUlangQrCodeHeader {
        public decimal NOKUNCI { get; set; }
        public string NORANG { get; set; }
        public decimal NOSJ { get; set; }
    }

}
