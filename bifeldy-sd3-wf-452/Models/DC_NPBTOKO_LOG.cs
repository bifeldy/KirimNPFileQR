/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Model Tabel DC_NPBTOKO_LOG
 *              :: Seharusnya Tidak Untuk Didaftarkan Ke DI Container
 * 
 */

using System;

namespace KirimNPFileQR.Models {

    public interface DC_NPBTOKO_LOG {
        string LOG_DCKODE { get; set; }
        decimal LOG_LOKID { get; set; }
        decimal LOG_ALOKASIID { get; set; }
        string LOG_TOK_KODE { get; set; }
        decimal LOG_NO_NPB { get; set; }
        DateTime LOG_TGL_NPB { get; set; }
        string LOG_NO_INV { get; set; }
        DateTime LOG_TGL_INV { get; set; }
        decimal LOG_HDRID { get; set; }
        decimal LOG_ITEM { get; set; }
        decimal LOG_QTY { get; set; }
        decimal LOG_GROSS { get; set; }
        decimal LOG_KOLI { get; set; }
        decimal LOG_KUBIKASI { get; set; }
        decimal LOG_SEQNO { get; set; }
        string LOG_STAT_CREATE { get; set; }
        string LOG_STAT_SEND { get; set; }
        string LOG_STAT_RCV { get; set; }
        string LOG_NAMAFILE { get; set; }
        decimal LOG_FK_ID { get; set; }
        string LOG_TYPEFILE { get; set; }
        string LOG_JENIS { get; set; }
        string LOG_STAT_GET { get; set; }
        string LOG_STAT_PROSES { get; set; }
        string LOG_NOBPB { get; set; }
        decimal LOG_JML_ITEMBPB { get; set; }
        DateTime LOG_KIRIM { get; set; }
        string LOG_IP_WEBREKAP { get; set; }
        string LOG_CABANG { get; set; }
        decimal LOG_RE_TRY { get; set; }
        string LOG_IP_IISKIRIM { get; set; }
        DateTime KIRIM_EMAIL { get; set; }
        string STATUS_KIRIM_EMAIL { get; set; }
        string KODE_STAT_KRIM_MAIL { get; set; }
    }

}
