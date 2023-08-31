/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Mail         :: bias@indomaret.co.id
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Model Tabel DC_NPBTOC_TOKO_TKO_LOG
 *              :: Seharusnya Tidak Untuk Didaftarkan Ke DI Container
 * 
 */

using System;

namespace KirimNPFileQR.Models {

    public interface DC_TOKO_T {
        decimal TOK_ID { get; set; }
        string TOK_CODE { get; set; }
        string TOK_NAME { get; set; }
        string TOK_KIRIM { get; set; }
        string TOK_JENIS_TOKO { get; set; }
        DateTime TOK_TGL_BUKA { get; set; }
        DateTime TOK_TGL_TUTUP { get; set; }
        string TOK_RECID { get; set; }
        string TOK_UPDREC_ID { get; set; }
        DateTime TOK_UPDREC_DATE { get; set; }
        DateTime TOK_TGL_PKM { get; set; }
        string TOK_BMGR_CODE { get; set; }
        string TOK_BMGR_NAME { get; set; }
        string TOK_AMGR_CODE { get; set; }
        string TOK_AMGR_NAME { get; set; }
        string TOK_ASPV_CODE { get; set; }
        string TOK_ASPV_NAME { get; set; }
        string TOK_TYPE_TOKO { get; set; }
        string TOK_KATEGORI { get; set; }
        string TOK_ALAMAT { get; set; }
        string TOK_TELP_1 { get; set; }
        decimal TOK_FREKKIRIM_HARI { get; set; }
        string TOK_JADWAL_BUAH { get; set; }
        string TOK_JADWAL_ELPIJI { get; set; }
        string TOK_TYPE_RAK { get; set; }
        decimal TOK_JARAKDC_KM { get; set; }
        string TOK_KOTA { get; set; }
        string TOK_KODE_POS { get; set; }
        string TOK_FAX_1 { get; set; }
        string TOK_PKP { get; set; }
        string TOK_NPWP { get; set; }
        string TOK_SKP { get; set; }
        DateTime TOK_TGL_SKP { get; set; }
        string TOK_FLAG_REGULER { get; set; }
        decimal TOK_MARKUP { get; set; }
        string TOK_CID_CODE { get; set; }
        DateTime TOK_TGL_BERLAKU { get; set; }
        string TOK_OLD_CODE { get; set; }
        string TOK_NEW_CODE { get; set; }
        string TOK_CABANG { get; set; }
        string TOK_KIRIM_KONV { get; set; }
        string TOK_JADWAL_KONV { get; set; }
        string TOK_KIRIM_BUAH { get; set; }
        string TOK_KIRIM_LPG { get; set; }
        string TOK_KIRIM_SEKUNDER { get; set; }
        DateTime TOK_TGLSEK_AWAL { get; set; }
        DateTime TOK_TGLSEK_AKHIR { get; set; }
        DateTime TOK_TGL_8DIGIT { get; set; }
        string TOK_8DIGIT { get; set; }
        string TOK_IMOBILE { get; set; }
        string TOK_EMAIL { get; set; }
        string TOK_KODEPOS { get; set; }
        string TOK_EVENT { get; set; }
        DateTime TOK_TGL_REOPENING { get; set; }
        string TOK_PERDA { get; set; }
        DateTime TOK_TGL_AWAL_PERDA { get; set; }
        DateTime TOK_TGL_AKHIR_PERDA  { get; set; }
        string TOK_KIRIM_WH { get; set; }
        DateTime TOK_TGL_MULTIRATES { get; set; }
        string TOK_KIRIM_IGR { get; set; }
    }

}
