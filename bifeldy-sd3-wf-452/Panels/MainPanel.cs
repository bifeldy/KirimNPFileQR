/**
 * 
 * Author       :: Basilius Bias Astho Christyono
 * Mail         :: bias@indomaret.co.id
 * Phone        :: (+62) 889 236 6466
 * 
 * Department   :: IT SD 03
 * Mail         :: bias@indomaret.co.id
 * 
 * Catatan      :: Halaman Awal
 *              :: Harap Didaftarkan Ke DI Container
 * 
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using bifeldy_sd3_lib_452.Utilities;

using KirimNPFileQR.Forms;
using KirimNPFileQR.Handlers;
using KirimNPFileQR.Utilities;

namespace KirimNPFileQR.Panels {

    public sealed partial class CMainPanel : UserControl {

        private readonly IApp _app;
        private readonly ILogger _logger;
        private readonly IDb _db;
        private readonly IConfig _config;

        private CMainForm mainForm;

        bool timerBusy = false;

        int waitTime = 15;
        int countDownSeconds = 0;

        public CMainPanel(IApp app, ILogger logger, IDb db, IConfig config) {
            _app = app;
            _logger = logger;
            _db = db;
            _config = config;

            InitializeComponent();
            OnInit();
        }

        public Label LabelStatus => lblStatus;

        public ProgressBar ProgressBarStatus => prgrssBrStatus;

        private void OnInit() {
            Dock = DockStyle.Fill;
        }

        private void ImgDomar_Click(object sender, EventArgs e) {
            mainForm.Width = 800;
            mainForm.Height = 600;
        }

        private async void CMainPanel_Load(object sender, EventArgs e) {
            mainForm = (CMainForm) Parent.Parent;
            mainForm.FormBorderStyle = FormBorderStyle.Sizable;
            mainForm.MaximizeBox = true;
            mainForm.MinimizeBox = true;

            appInfo.Text = _app.AppName;
            string dcKode = null;
            string namaDc = null;
            await Task.Run(async () => {
                dcKode = await _db.GetKodeDc();
                namaDc = await _db.GetNamaDc();
            });

            bool bypassLogin = _config.Get<bool>("BypassLogin", bool.Parse(_app.GetConfig("bypass_login")));
            if (bypassLogin) {
                _db.LoggedInUsername = "ANONYMOUS";
            }
            userInfo.Text = $".: {dcKode} - {namaDc} :: {_db.LoggedInUsername} :.";

            if (!timerBusy) {
                SetIdleBusyStatus(true);
            }

            ReStartTimer();
        }

        public void SetIdleBusyStatus(bool isIdle) {
            LabelStatus.Text = $"Program {(isIdle ? "Idle" : "Sibuk")} ...";
            ProgressBarStatus.Style = isIdle ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee;
            EnableDisableControl(Controls, isIdle);
        }

        private void EnableDisableControl(ControlCollection controls, bool isIdle) {
            foreach (Control control in controls) {
                if (control is Button || control is CheckBox || control is DateTimePicker) {
                    control.Enabled = isIdle;
                }
                else {
                    EnableDisableControl(control.Controls, isIdle);
                }
            }
        }

        private async void ReStartTimer() {
            if (!tmrCountDown.Enabled && !timerBusy) {
                timerBusy = true;
                countDownSeconds = waitTime;
                SetIdleBusyStatus(false);
                await Task.Run(() => {
                    try {
                        RefreshDataTable();
                    }
                    catch (Exception ex) {
                        _logger.WriteError(ex);
                    }
                });
                SetIdleBusyStatus(true);
                tmrCountDown.Start();
            }
        }

        private async void tmrCountDown_Tick(object sender, EventArgs e) {
            TimeSpan t = TimeSpan.FromSeconds(countDownSeconds);
            lblCountDown.Text = $"{t.Hours.ToString().PadLeft(2, '0')}:{t.Minutes.ToString().PadLeft(2, '0')}:{t.Seconds.ToString().PadLeft(2, '0')}";
            countDownSeconds--;
            if (countDownSeconds < 0) {
                tmrCountDown.Stop();
                SetIdleBusyStatus(false);
                await Task.Run(() => {
                    try {
                        countDownSeconds = waitTime;
                        ProsesNPFile();
                    }
                    catch (Exception ex) {
                        _logger.WriteError(ex);
                    }
                });
                SetIdleBusyStatus(true);
                timerBusy = false;
                ReStartTimer();
            }
        }

        private void RefreshDataTable() {
            // TODO ::
            Thread.Sleep(10000);
            throw new Exception("Coba Throw Error RefreshDataTable !!");
        }

        private void ProsesNPFile() {
            // TODO ::
            Thread.Sleep(10000);
            throw new Exception("Coba Throw Error ProsesNPFile !!");
        }

    }

}
