/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Model Tabel DC_NPBTOKO_FILE
 *              :: Seharusnya Tidak Untuk Didaftarkan Ke DI Container
 * 
 */

namespace KirimNPFileQR.Models {

    public interface DC_NPBTOKO_FILE {
        string RECID { get; set; }
        string RTYPE { get; set; }
        string DOCNO { get; set; }
        decimal SEQNO { get; set; }
        decimal PICNO { get; set; }
        string PICNOT { get; set; }
        string PICTGL { get; set; }
        string PRDCD { get; set; }
        string NAMA { get; set; }
        string DIV { get; set; }
        decimal QTY { get; set; }
        decimal SJ_QTY { get; set; }
        decimal PRICE { get; set; }
        decimal GROSS { get; set; }
        decimal PPNRP { get; set; }
        decimal HPP { get; set; }
        string TOKO { get; set; }
        string KETER { get; set; }
        string TANGGAL1 { get; set; }
        string TANGGAL2 { get; set; }
        string DOCNO2 { get; set; }
        string LT { get; set; }
        string RAK { get; set; }
        string BAR { get; set; }
        string KIRIM { get; set; }
        string DUS_NO { get; set; }
        decimal LOG_FK_SEQNO { get; set; }
        string TGLEXP { get; set; }
        decimal PPN_RATE { get; set; }
        string BKP { get; set; }
        string SUB_BKP { get; set; }
        decimal NL_QTY { get; set; }
    }

}
