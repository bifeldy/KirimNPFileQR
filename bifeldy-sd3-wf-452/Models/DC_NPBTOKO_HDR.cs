/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Mail         :: bias@indomaret.co.id
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Model Tabel DC_NPBTOKO_HDR
 *              :: Seharusnya Tidak Untuk Didaftarkan Ke DI Container
 * 
 */

using System;

namespace KirimNPFileQR.Models {

    public interface DC_NPBTOKO_HDR {
        string HDR_DCKODE { get; set; }
        decimal HDR_LOG_ALOKASID { get; set; }
        string HDR_NOSJ { get; set; }
        DateTime HDR_TGLSJ { get; set; }
        string HDR_TOK_KODE { get; set; }
        decimal HDR_ID { get; set; }
        DateTime HDR_TGL { get; set; }
        string HDR_KETER { get; set; }
        decimal HDR_RECID { get; set; }
        string HDR_TYPE { get; set; }
        string HDR_JENIS { get; set; }
        decimal HDR_TOTREC_NPB { get; set; }
    }

}
