/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Cek Versi, IP, etc.
 *              :: Harap Didaftarkan Ke DI Container
 * 
 */

using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using bifeldy_sd3_lib_452.Utilities;

using KirimNPFileQR.Forms;
using KirimNPFileQR.Handlers;
using KirimNPFileQR.Utilities;

namespace KirimNPFileQR.Panels {

    public sealed partial class CCekProgram : UserControl {

        private readonly IApp _app;
        private readonly IDb _db;
        private readonly IConfig _config;

        private CMainForm mainForm;

        public CCekProgram(IApp app, IDb db, IConfig config) {
            _app = app;
            _db = db;
            _config = config;

            InitializeComponent();
            OnInit();
        }

        public Label LoadingInformation => loadingInformation;

        private void OnInit() {
            loadingInformation.Text = "Sedang Mengecek Program ...";
            Dock = DockStyle.Fill;
        }

        private void CCekProgram_Load(object sender, EventArgs e) {
            mainForm = (CMainForm) Parent.Parent;
            CheckProgram();
        }

        private async void CheckProgram() {
            mainForm.StatusStripContainer.Items["statusStripDbName"].Text = _db.DbName;

            // First DB Run + Check Connection
            bool dbAvailable = false;
            // Check Jenis DC
            string jenisDc = null;
            await Task.Run(async () => {
                try {
                    jenisDc = await _db.GetJenisDc();
                    dbAvailable = true;
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message, "Program Checker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
            if (dbAvailable) {
                if (_app.ListDcCanUse.Contains(jenisDc)) {

                    // Check Version
                    string responseCekProgram = null;
                    await Task.Run(async () => {
                        responseCekProgram = await _db.CekVersi();
                    });
                    if (responseCekProgram == "OKE") {
                        bool bypassLogin = _config.Get<bool>("BypassLogin", bool.Parse(_app.GetConfig("bypass_login")));
                        if (bypassLogin) {
                            ShowMainPanel();
                        }
                        else {
                            ShowLoginPanel();
                        }
                    }
                    else {
                        MessageBox.Show(responseCekProgram, "Program Checker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _app.Exit();
                    }
                }
                else {
                    MessageBox.Show(
                        $"Program Hanya Dapat Di Jalankan Di DC {Environment.NewLine}{string.Join(", ", _app.ListDcCanUse.ToArray())}",
                        "Program Checker",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    _app.Exit();
                }
            }
            else {
                _app.Exit();
            }
        }

        private void ShowLoginPanel() {

            // Create & Show `Login` Panel
            try {
                if (!mainForm.PanelContainer.Controls.ContainsKey("CLogin")) {
                    mainForm.PanelContainer.Controls.Add(CProgram.Bifeldyz.ResolveClass<CLogin>());
                }
                mainForm.PanelContainer.Controls["CLogin"].BringToFront();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Terjadi Kesalahan! (｡>﹏<｡)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowMainPanel() {

            // Change Window Size & Position To Middle Screen
            mainForm.Width = 800;
            mainForm.Height = 600;
            mainForm.SetDesktopLocation((_app.ScreenWidth - mainForm.Width) / 2, (_app.ScreenHeight - mainForm.Height) / 2);

            // Change Panel To Fully Windowed Mode And Go To `MainPanel`
            mainForm.HideLogo();
            mainForm.PanelContainer.Dock = DockStyle.Fill;

            // Create & Show `MainPanel`
            try {
                if (!mainForm.PanelContainer.Controls.ContainsKey("CMainPanel")) {
                    mainForm.PanelContainer.Controls.Add(CProgram.Bifeldyz.ResolveClass<CMainPanel>());
                }
                mainForm.PanelContainer.Controls["CMainPanel"].BringToFront();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Terjadi Kesalahan! (｡>﹏<｡)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Remove All Other Unused Panels
            mainForm.PanelContainer.Controls.RemoveByKey("CDbSelector");
            mainForm.PanelContainer.Controls.RemoveByKey("CCekProgram");
        }

    }

}
