/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Mail         :: bias@indomaret.co.id
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Pengaturan Aplikasi
 *              :: Harap Didaftarkan Ke DI Container
 * 
 */

using System.Collections.Generic;
using System.Windows.Forms;

using bifeldy_sd3_lib_452.Utilities;

namespace KirimNPFileQR.Utilities {

    public interface IApp : IApplication {
        int ScreenWidth { get; }
        int ScreenHeight { get; }
        void Exit();
        List<string> ListDcCanUse { get; }
    }

    public sealed class CApp : CApplication, IApp {

        public int ScreenWidth { get; }
        public int ScreenHeight { get; }

        public List<string> ListDcCanUse { get; }

        public CApp() {
            ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            ListDcCanUse = new List<string> { "HO", "INDUK", "DEPO", "SEWA" };
        }

        public void Exit() => Application.Exit();

    }

}
