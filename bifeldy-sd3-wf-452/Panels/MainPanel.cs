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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using bifeldy_sd3_lib_452.Utilities;

using KirimNPFileQR.Forms;
using KirimNPFileQR.Handlers;
using KirimNPFileQR.Models;
using KirimNPFileQR.Utilities;

namespace KirimNPFileQR.Panels {

    public sealed partial class CMainPanel : UserControl {

        private readonly IApp _app;
        private readonly ILogger _logger;
        private readonly IDb _db;
        private readonly IConfig _config;
        private readonly IConverter _converter;

        private CMainForm mainForm;

        bool timerBusy = false;

        int waitTime = 15 * 60;
        int countDownSeconds = 0;

        /* NP* Header */

        private List<MNpHeader> listNpHeader = null;
        private BindingList<MNpHeader> bindNpHeader = null;

        public CMainPanel(IApp app, ILogger logger, IDb db, IConfig config, IConverter converter) {
            _app = app;
            _logger = logger;
            _db = db;
            _config = config;
            _converter = converter;

            InitializeComponent();
            OnInit();
        }

        public Label LabelStatus => lblStatus;

        public ProgressBar ProgressBarStatus => prgrssBrStatus;

        private void OnInit() {
            Dock = DockStyle.Fill;

            listNpHeader = new List<MNpHeader>();
            bindNpHeader = new BindingList<MNpHeader>(listNpHeader);
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
                if (control is Button || control is CheckBox || control is DateTimePicker || control is DataGridView) {
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
                await RefreshDataTable();
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
                countDownSeconds = waitTime;
                SetIdleBusyStatus(false);
                await ProsesNPFile();
                SetIdleBusyStatus(true);
                timerBusy = false;
                ReStartTimer();
            }
        }

        private void dtGrd_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            // --
        }

        private void EnableCustomColumnOnly(DataGridView dtGrdVw, List<string> visibleColumn) {
            foreach (DataGridViewColumn dtGrdCol in dtGrdVw.Columns) {
                if (!visibleColumn.Contains(dtGrdCol.Name)) {
                    dtGrdCol.Visible = chkSemuaKolom.Checked;
                }
                if (dtGrdCol.GetType() != typeof(DataGridViewButtonColumn) && dtGrdCol.GetType() != typeof(DataGridViewComboBoxColumn)) {
                    dtGrdCol.ReadOnly = true;
                }
                else {
                    dtGrdCol.ReadOnly = false;
                }
            }
        }

        private async Task RefreshDataTable() {
            try {
                listNpHeader.Clear();
                DataTable dtNpHeader = new DataTable();
                await Task.Run(async () => {
                    dtNpHeader = await _db.GetNpHeader();
                });
                if (dtNpHeader.Rows.Count > 0) {
                    List<MNpHeader> lsNpHeader = _converter.DataTableToList<MNpHeader>(dtNpHeader);
                    foreach (MNpHeader npHeader in lsNpHeader) {
                        listNpHeader.Add(npHeader);
                    }
                    dtGrdNp.DataSource = bindNpHeader;
                    EnableCustomColumnOnly(dtGrdNp, new List<string> {
                        "LOG_SEQNO",
                        "LOG_DCKODE",
                        "LOG_TOK_KODE",
                        "LOG_NO_NPB",
                        "LOG_TGL_NPB",
                        "LOG_NAMAFILE",
                        "LOG_ITEM",
                        "TOK_NAME",
                        "TOK_KIRIM",
                        "TOK_EMAIL"
                    });
                }
                bindNpHeader.ResetBindings();
                dtGrdNp.ClearSelection();
            }
            catch (Exception ex) {
                _logger.WriteError(ex);
            }
        }

        private async Task ProsesNPFile() {
            await Task.Run(() => {
                try {
                    // TODO ::
                    Thread.Sleep(10000);
                    throw new Exception("Coba Throw Error ProsesNPFile !!");
                }
                catch (Exception ex) {
                    _logger.WriteError(ex);
                }
            });
        }

        private async void btbReFresh_Click(object sender, EventArgs e) {
            SetIdleBusyStatus(false);
            await RefreshDataTable();
            SetIdleBusyStatus(true);
        }
    }

}
