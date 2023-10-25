/**
 * 
 * Author       :: Basilius Bias Astho Christyono
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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
        private readonly IWinReg _winreg;
        private readonly IConverter _converter;
        private readonly IBerkas _berkas;
        private readonly IStream _stream;
        private readonly IQrBar _qrBar;
        private readonly ISurel _surel;

        private CMainForm mainForm;

        private bool isAutoRun = false;
        private bool isInitialized = false;

        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        private int waitTimeQrEmail = 15 * 60;
        private int countDownSecondsQrEmail = 0;

        private int waitTimeJsonByte = 60 * 60;
        private int countDownSecondsJsonByte = 0;

        /* NP* Header */

        private List<MNpLog> listNpLogPendingQrEmail = null;
        private BindingList<MNpLog> bindNpLogPendingQrEmail = null;

        private List<MNpLog> listNpLogGagalQrEmail = null;
        private BindingList<MNpLog> bindNpLogGagalQrEmail = null;

        private List<MNpLog> listNpLogPendingJsonByte = null;
        private BindingList<MNpLog> bindNpLogPendingJsonByte = null;

        public CMainPanel(
            IApp app,
            ILogger logger,
            IDb db,
            IConfig config,
            IWinReg winreg,
            IConverter converter,
            IBerkas berkas,
            IStream stream,
            IQrBar qrBar,
            ISurel surel
        ) {
            _app = app;
            _logger = logger;
            _db = db;
            _config = config;
            _winreg = winreg;
            _converter = converter;
            _berkas = berkas;
            _stream = stream;
            _qrBar = qrBar;
            _surel = surel;

            InitializeComponent();
            OnInit();
        }

        public Label LabelStatus => lblStatus;

        public ProgressBar ProgressBarStatus => prgrssBrStatus;

        private void OnInit() {
            Dock = DockStyle.Fill;

            listNpLogPendingQrEmail = new List<MNpLog>();
            bindNpLogPendingQrEmail = new BindingList<MNpLog>(listNpLogPendingQrEmail);

            listNpLogGagalQrEmail = new List<MNpLog>();
            bindNpLogGagalQrEmail = new BindingList<MNpLog>(listNpLogGagalQrEmail);

            listNpLogPendingJsonByte = new List<MNpLog>();
            bindNpLogPendingJsonByte = new BindingList<MNpLog>(listNpLogPendingJsonByte);

            txtBxDaysRetentionFiles.Value = _berkas.MaxOldRetentionDay;
        }

        private void ImgDomar_Click(object sender, EventArgs e) {
            mainForm.Width = 800;
            mainForm.Height = 600;
        }

        private async void CMainPanel_Load(object sender, EventArgs e) {
            if (!isInitialized) {

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
                userInfo.Text = $".: {dcKode} - {namaDc} :: {_db.LoggedInUsername} :.";

                bool windowsStartup = _config.Get<bool>("WindowsStartup", bool.Parse(_app.GetConfig("windows_startup")));
                chkWindowsStartup.Checked = windowsStartup;

                isAutoRun = _config.Get<bool>("AutoRun", bool.Parse(_app.GetConfig("auto_run")));

                waitTimeQrEmail = _config.Get<int>("WaitTimeQrEmail", _app.GetConfig("wait_time_qr_email"));
                waitTimeJsonByte = _config.Get<int>("WaitTimeJsonByte", _app.GetConfig("wait_json_byte"));

                FirstSingleRunOnlyAsync();
                // TestingQr();

                isInitialized = true;
            }

            SetIdleBusyStatus(_app.IsIdle);
        }

        private void ChkWindowsStartup_CheckedChanged(object sender, EventArgs e) {
            CheckBox cb = (CheckBox) sender;
            _config.Set("WindowsStartup", cb.Checked);
            _winreg.SetWindowsStartup(cb.Checked);
        }

        private void TestingQr() {
            //
            // Testing Buat QR & Baca QR & Bandingin Isi QR
            //
            // Image bmp1 = Image.FromFile("D:/_data/Bifeldy/_sources/KirimNPFileQR/bifeldy-sd3-wf-452/bin/Debug/Backup_Files/6379586_NPBG001TRIY20230830152154_DETAIL_01-05.JPG");
            // string txt1 = _qrBar.ReadTextFromQrBarCode(bmp1);
            // MessageBox.Show(txt1);
            // 
            // Image img1 = _qrBar.GenerateQrCodeDots(txt1);
            // Image bmp2 = Image.FromFile("D:/_data/Bifeldy/_sources/KirimNPFileQR/bifeldy-sd3-wf-452/Images/domar.gif");
            // Image img2 = _qrBar.AddQrLogo(img1, bmp2);
            // 
            // string urutan = $"{1:00#}-{5:00#}";
            // Image img3 = _qrBar.AddQrCaption(img2, $"6379586_NPBG001TRIY20230830152154_DETAIL_{urutan}.JPG");
            // img3.Save($"D:/_data/Bifeldy/_sources/KirimNPFileQR/bifeldy-sd3-wf-452/bin/Debug/Backup_Files/6379586_NPBG001TRIY20230830152154_DETAIL_{urutan}_NEW.JPG");
            // 
            // Image bmp3 = Image.FromFile($"D:/_data/Bifeldy/_sources/KirimNPFileQR/bifeldy-sd3-wf-452/bin/Debug/Backup_Files/6379586_NPBG001TRIY20230830152154_DETAIL_{urutan}_NEW.JPG");
            // string txt2 = _qrBar.ReadTextFromQrBarCode(bmp3);
            // MessageBox.Show((txt1 == txt2).ToString());
            //
            // string txt1 = "COBA GAMBAR PAKAI BACKGROUND";
            // for(int i = 0; i < 20; i++) {
            //     txt1 += $"{Environment.NewLine} :: COBA GAMBAR PAKAI BACKGROUND";
            // }
            // Image img1 = Image.FromFile("C:/Users/USER/Downloads/ji.jpg");
            // Image img2 = Image.FromFile(Path.Combine(_app.AppLocation, "Images", "domar.ico"));
            // Image img3 = _qrBar.GenerateQrCodeDots(txt1);
            // img3 = _qrBar.AddBackground(img3, img1);
            // img3 = _qrBar.AddQrLogo(img3, img2);
            // img3 = _qrBar.AddQrCaption(img3, txt1);
            // img3.Save("C:/Users/USER/Downloads/_coba.jpg");
            // string txt2 = _qrBar.ReadTextFromQrBarCode(img3);
            // MessageBox.Show((txt1 == txt2).ToString());
            //
        }

        public void SetIdleBusyStatus(bool isIdle) {
            _app.IsIdle = isIdle;
            LabelStatus.Text = $"Program {(isIdle ? "Idle" : "Sibuk")} ...";
            ProgressBarStatus.Style = isIdle ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee;
            EnableDisableControl(Controls);
            if (isAutoRun && !_app.DebugMode) {
                chkWindowsStartup.Enabled = false;
                // btnStartStopQrEmail.Enabled = false;
                btnStartStopJsonByte.Enabled = false;
            }
        }

        private void EnableDisableControl(ControlCollection controls) {
            foreach (Control control in controls) {
                if (control is Button || control is CheckBox || control is DateTimePicker || control is DataGridView) {
                    control.Enabled = _app.IsIdle;
                }
                else {
                    EnableDisableControl(control.Controls);
                }
            }
        }

        private void TxtBxDaysRetentionFiles_ValueChanged(object sender, EventArgs e) {
            _berkas.MaxOldRetentionDay = (int) txtBxDaysRetentionFiles.Value;
            _config.Set("MaxOldRetentionDay", _berkas.MaxOldRetentionDay);
        }

        private async void FirstSingleRunOnlyAsync() {
            SetIdleBusyStatus(false);
            countDownSecondsQrEmail = waitTimeQrEmail;
            countDownSecondsJsonByte = waitTimeJsonByte;
            try {
                await RefreshDataTableQrEmail();
                await RefreshDataTableJsonByte();
            }
            catch (Exception ex) {
                _logger.WriteError(ex);
            }
            if (isAutoRun) {
                // btnStartStopQrEmail_Click(this, EventArgs.Empty);
                BtnStartStopJsonByte_Click(this, EventArgs.Empty);
            }
            SetIdleBusyStatus(true);
        }

        private void btnStartStopQrEmail_Click(object sender, EventArgs e) {
            if (tmrQrEmail.Enabled) {
                tmrQrEmail.Stop();
                btnStartStopQrEmail.Text = "Start";
            }
            else {
                tmrQrEmail.Start();
                btnStartStopQrEmail.Text = "Stop";
            }
        }

        private void BtnStartStopJsonByte_Click(object sender, EventArgs e) {
            if (tmrJsonByte.Enabled) {
                tmrJsonByte.Stop();
                btnStartStopJsonByte.Text = "Start";
            }
            else {
                tmrJsonByte.Start();
                btnStartStopJsonByte.Text = "Stop";
            }
        }

        private async void TmrQrEmail_Tick(object sender, EventArgs e) {
            TimeSpan t = TimeSpan.FromSeconds(countDownSecondsQrEmail);
            lblCountDownQrEmail.Text = $"{t.Hours.ToString().PadLeft(2, '0')}:{t.Minutes.ToString().PadLeft(2, '0')}:{t.Seconds.ToString().PadLeft(2, '0')}";
            countDownSecondsQrEmail--;
            if (countDownSecondsQrEmail < 0) {
                tmrQrEmail.Stop();

                await _lock.WaitAsync(-1);

                SetIdleBusyStatus(false);
                await RefreshDataTableQrEmail();
                await ProsesNPFileQrEmail();
                await RefreshDataTableQrEmail();
                countDownSecondsQrEmail = waitTimeQrEmail;
                SetIdleBusyStatus(true);

                _lock.Release();

                tmrQrEmail.Start();
            }
        }

        private async void TmrJsonByte_Tick(object sender, EventArgs e) {
            TimeSpan t = TimeSpan.FromSeconds(countDownSecondsJsonByte);
            lblCountDownJsonByte.Text = $"{t.Hours.ToString().PadLeft(2, '0')}:{t.Minutes.ToString().PadLeft(2, '0')}:{t.Seconds.ToString().PadLeft(2, '0')}";
            countDownSecondsJsonByte--;
            if (countDownSecondsJsonByte< 0) {
                tmrJsonByte.Stop();

                await _lock.WaitAsync(-1);

                SetIdleBusyStatus(false);
                await RefreshDataTableJsonByte();
                await ProsesNPFileJsonByte();
                await RefreshDataTableJsonByte();
                countDownSecondsJsonByte= waitTimeJsonByte;
                SetIdleBusyStatus(true);

                _lock.Release();

                tmrJsonByte.Start();
            }
        }

        private void DtGrd_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            // --
        }

        private void EnableCustomColumnOnly(DataGridView dtGrdVw, List<string> visibleColumn) {
            List<string> cols = visibleColumn.Select(c => c.ToUpper()).ToList();
            foreach (DataGridViewColumn dtGrdCol in dtGrdVw.Columns) {
                if (!cols.Contains(dtGrdCol.Name.ToUpper())) {
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

        private async Task RefreshDataTableQrEmail() {
            try {
                listNpLogPendingQrEmail.Clear();
                listNpLogGagalQrEmail.Clear();
                DataTable dtNpLogHeader = new DataTable();
                await Task.Run(async () => {
                    dtNpLogHeader = await _db.GetNpLogHeaderQrEmail();
                });
                if (dtNpLogHeader.Rows.Count > 0) {
                    // Program Not Responding
                    // Jangan Di Masukkin Ke Thread
                    List<MNpLog> lsNpLog = _converter.DataTableToList<MNpLog>(dtNpLogHeader);
                    // Sekalian Buat Nahan Window Message Queuenya
                    // Biar Timer Ke Tunda
                    foreach (MNpLog npLog in lsNpLog) {
                        if (string.IsNullOrEmpty(npLog.STATUS_KIRIM_EMAIL)) {
                            listNpLogPendingQrEmail.Add(npLog);
                        }
                        else {
                            listNpLogGagalQrEmail.Add(npLog);
                        }
                    }
                    List<string> columnToShow = new List<string> {
                        "LOG_DCKODE",
                        "LOG_TOK_KODE",
                        "LOG_NAMAFILE",
                        "TOK_NAME",
                        "TOK_KIRIM",
                        "TOK_EMAIL",
                        "LOG_TYPEFILE",
                        "LOG_JENIS"
                    };
                    dtGrdNpPendingQrEmail.DataSource = bindNpLogPendingQrEmail;
                    dtGrdNpGagalQrEmail.DataSource = bindNpLogGagalQrEmail;
                    EnableCustomColumnOnly(dtGrdNpPendingQrEmail, columnToShow);
                    EnableCustomColumnOnly(dtGrdNpGagalQrEmail, columnToShow);
                }
                bindNpLogPendingQrEmail.ResetBindings();
                bindNpLogGagalQrEmail.ResetBindings();
                dtGrdNpPendingQrEmail.ClearSelection();
                dtGrdNpGagalQrEmail.ClearSelection();
                dtGrdNpPendingQrEmail.Refresh();
                dtGrdNpGagalQrEmail.Refresh();
            }
            catch (Exception ex) {
                _logger.WriteError(ex);
            }
        }

        private async Task RefreshDataTableJsonByte() {
            try {
                listNpLogPendingJsonByte.Clear();
                DataTable dtNpLogHeader = new DataTable();
                await Task.Run(async () => {
                    dtNpLogHeader = await _db.GetNpLogHeaderJsonByte();
                });
                if (dtNpLogHeader.Rows.Count > 0) {
                    // Program Not Responding
                    // Jangan Di Masukkin Ke Thread
                    List<MNpLog> lsNpLog = _converter.DataTableToList<MNpLog>(dtNpLogHeader);
                    // Sekalian Buat Nahan Window Message Queuenya
                    // Biar Timer Ke Tunda
                    foreach (MNpLog npLog in lsNpLog) {
                        listNpLogPendingJsonByte.Add(npLog);
                    }
                    List<string> columnToShow = new List<string> {
                        "LOG_DCKODE",
                        "LOG_TOK_KODE",
                        "LOG_NAMAFILE",
                        "TOK_NAME",
                        "TOK_KIRIM",
                        "LOG_TYPEFILE",
                        "LOG_JENIS"
                    };
                    dtGrdNpPendingJsonByte.DataSource = bindNpLogPendingJsonByte;
                    EnableCustomColumnOnly(dtGrdNpPendingJsonByte, columnToShow);
                }
                bindNpLogPendingJsonByte.ResetBindings();
                dtGrdNpPendingJsonByte.ClearSelection();
                dtGrdNpPendingJsonByte.Refresh();
            }
            catch (Exception ex) {
                _logger.WriteError(ex);
            }
        }

        private async Task ProsesNPFileQrEmail() {
            bool kirimUlangGagal = chkKirimSemuaNpQrEmail.Checked;
            await Task.Run(async () => {
                _berkas.DeleteOldFilesInFolder(_berkas.TempFolderPath, 0);
                List<MNpLog> listNpLogHeader = listNpLogPendingQrEmail;
                if (kirimUlangGagal) {
                    listNpLogHeader.AddRange(listNpLogGagalQrEmail);
                }

                int maxQrChar = 1853;
                string zipPassword = "PernahKejepit2XOuch!!";
                string lastCharDetail = "*";
                string lastCharHeader = "-";
                int versionQrHeader = 17;
                int versionQrDetail = 25;
                // string imageQrBgPath = Path.Combine(_app.AppLocation, "Images", "qrBgImg.jpg");
                // string imageQrLogoPath = Path.Combine(_app.AppLocation, "Images", "qrLogoImg.png");

                List<decimal> lsLogSeqNo = new List<decimal>();

                foreach (MNpLog npLogHeader in listNpLogHeader) {
                    try {
                        if (!string.IsNullOrEmpty(npLogHeader.TOK_EMAIL)) {

                            lsLogSeqNo.Clear();
                            List<string> lsAttachmentPath = new List<string>();

                            DataTable dtNpLogDetail = await _db.GetNpLogDetailQrEmail(npLogHeader.LOG_NAMAFILE);
                            List<MNpLog> lsNpLogDetail = _converter.DataTableToList<MNpLog>(dtNpLogDetail);
                            if (lsNpLogDetail.Count <= 0) {
                                throw new Exception($"Data {npLogHeader.LOG_NAMAFILE} Tidak Tersedia / Sudah Sukses Dari Service Sebelumnya");
                            }

                            /* ** Create Ulang Qr Code ** */

                            foreach (MNpLog npLogDetail in lsNpLogDetail) {
                                if (!lsLogSeqNo.Contains(npLogDetail.LOG_SEQNO)) {
                                    lsLogSeqNo.Add(npLogDetail.LOG_SEQNO);
                                }

                                // -- Detail
                                string detailCreateUlangQrCodeFileName = $"{npLogDetail.LOG_SEQNO}_{npLogDetail.LOG_NAMAFILE}_DETAIL";
                                DataTable dtNpCreateUlangQrCodeDetail = await _db.GetNpCreateUlangQrCodeDetail(npLogDetail.LOG_SEQNO);
                                // -- Detail CSV
                                if (!_berkas.DataTable2CSV(dtNpCreateUlangQrCodeDetail, $"{detailCreateUlangQrCodeFileName}.CSV", "|")) {
                                    throw new Exception($"Gagal Membuat {detailCreateUlangQrCodeFileName}.CSV");
                                }
                                // -- Detail ZIP
                                _berkas.ZipListFileInFolder(
                                    $"{detailCreateUlangQrCodeFileName}.ZIP",
                                    new List<string> { $"{detailCreateUlangQrCodeFileName}.CSV" },
                                    password: zipPassword
                                );
                                string detailCreateUlangQrCodePathZip = Path.Combine(_berkas.ZipFolderPath, $"{detailCreateUlangQrCodeFileName}.ZIP");
                                byte[] detailCreateUlangQrCodeByteZip = null;
                                using (MemoryStream ms = _stream.ReadFileAsBinaryByte(detailCreateUlangQrCodePathZip)) {
                                    detailCreateUlangQrCodeByteZip = ms.ToArray();
                                }
                                string detailCreateUlangQrCodeHex = _converter.ByteToString(detailCreateUlangQrCodeByteZip);
                                TextDevider txtDvdr = new TextDevider(detailCreateUlangQrCodeHex, maxQrChar - 9 - 1);
                                txtDvdr.Devide();
                                // -- Header
                                string headerCreateUlangQrCodeFileName = $"{npLogDetail.LOG_SEQNO}_{npLogDetail.LOG_NAMAFILE}_HEADER";
                                // -- Header CSV
                                StringBuilder sb = new StringBuilder();
                                sb.AppendLine("TOKO|KIRIM|GEMBOK|NOSJ|NORANG|JMLPART|JMLRECORD");
                                DataTable dtNpCreateUlangQrCodeHeader = await _db.GetNpCreateUlangQrCodeHeader(npLogDetail.LOG_JENIS, npLogDetail.LOG_NO_NPB, npLogDetail.LOG_TGL_NPB);
                                MNpCreateUlangQrCodeHeader npCreateUlangQrCodeHeader = _converter.DataTableToList<MNpCreateUlangQrCodeHeader>(dtNpCreateUlangQrCodeHeader).First();
                                sb.AppendLine($"{npLogDetail.LOG_TOK_KODE}|{await _db.GetKodeDc()}|{npCreateUlangQrCodeHeader.NOKUNCI}|{npCreateUlangQrCodeHeader.NOSJ}|{npCreateUlangQrCodeHeader.NORANG}|{txtDvdr.JumlahPart}|{dtNpCreateUlangQrCodeDetail.Rows.Count}");
                                string headerCreateUlangQrCodePathCsv = Path.Combine(_berkas.TempFolderPath, $"{headerCreateUlangQrCodeFileName}.CSV");
                                File.WriteAllText(headerCreateUlangQrCodePathCsv, sb.ToString());
                                // -- Header ZIP
                                _berkas.ZipListFileInFolder(
                                    $"{headerCreateUlangQrCodeFileName}.ZIP",
                                    new List<string> { $"{headerCreateUlangQrCodeFileName}.CSV" },
                                    password: zipPassword
                                );
                                string headerCreateUlangQrCodePathZip = Path.Combine(_berkas.ZipFolderPath, $"{headerCreateUlangQrCodeFileName}.ZIP");
                                byte[] headerCreateUlangQrCodeByteZip = null;
                                using (MemoryStream ms = _stream.ReadFileAsBinaryByte(headerCreateUlangQrCodePathZip)) {
                                    headerCreateUlangQrCodeByteZip = ms.ToArray();
                                }
                                string headerCreateUlangQrCodeHex = _converter.ByteToString(headerCreateUlangQrCodeByteZip) + lastCharHeader;
                                // -- QR Header
                                Image headerCreateUlangQrCodeQr = _qrBar.GenerateQrCodeDots(headerCreateUlangQrCodeHex, versionQrHeader);
                                // headerCreateUlangQrCodeQr = _qrBar.AddBackground(headerCreateUlangQrCodeQr, Image.FromFile(imageQrBgPath));
                                // headerCreateUlangQrCodeQr = _qrBar.AddQrLogo(headerCreateUlangQrCodeQr, Image.FromFile(imageQrLogoPath));
                                headerCreateUlangQrCodeQr = _qrBar.AddQrCaption(headerCreateUlangQrCodeQr, $"{headerCreateUlangQrCodeFileName}.JPG");
                                string headerCreateUlangQrCodeQrImgPath = Path.Combine(_berkas.TempFolderPath, $"{headerCreateUlangQrCodeFileName}.JPG");
                                headerCreateUlangQrCodeQr.Save(headerCreateUlangQrCodeQrImgPath, ImageFormat.Jpeg);
                                lsAttachmentPath.Add(headerCreateUlangQrCodeQrImgPath);
                                // -- QR Detail
                                int totalQr = txtDvdr.JumlahPart;
                                for (int i = 0; i < totalQr; i++) {
                                    // 2 Digit Dengan Awal 0
                                    string idx = (i + 1).ToString("0#");
                                    string saltDetailHex = $"{idx}{npCreateUlangQrCodeHeader.NOSJ}{txtDvdr.ArrDevidedText[i]}{lastCharDetail}";
                                    string urutan = $"{idx}-{totalQr:0#}";
                                    Image detailCreateUlangQrCodeQr = _qrBar.GenerateQrCodeDots(saltDetailHex, versionQrDetail);
                                    // detailCreateUlangQrCodeQr = _qrBar.AddBackground(detailCreateUlangQrCodeQr, Image.FromFile(imageQrBgPath));
                                    // detailCreateUlangQrCodeQr = _qrBar.AddQrLogo(detailCreateUlangQrCodeQr, Image.FromFile(imageQrLogoPath));
                                    detailCreateUlangQrCodeQr = _qrBar.AddQrCaption(detailCreateUlangQrCodeQr, $"{detailCreateUlangQrCodeFileName}_{urutan}.JPG");
                                    string detailCreateUlangQrCodeQrImgPath = Path.Combine(_berkas.TempFolderPath, $"{detailCreateUlangQrCodeFileName}_{urutan}.JPG");
                                    detailCreateUlangQrCodeQr.Save(detailCreateUlangQrCodeQrImgPath, ImageFormat.Jpeg);
                                    lsAttachmentPath.Add(detailCreateUlangQrCodeQrImgPath);
                                }
                            }

                            /* ** Create Ulang File NP ** */

                            // -- File1 CSV -- Sama Sesuai Log
                            string createUlangFileNp1 = npLogHeader.LOG_NAMAFILE;
                            DataTable dtNpCreateUlangFileNp1 = await _db.GetNpCreateUlangFileNp1(npLogHeader.LOG_JENIS, lsLogSeqNo.ToArray(), npLogHeader.LOG_DCKODE);
                            if (!_berkas.DataTable2CSV(dtNpCreateUlangFileNp1, $"{createUlangFileNp1}.CSV", "|")) {
                                throw new Exception($"Gagal Membuat {createUlangFileNp1}.CSV");
                            }
                            // -- File2 CSV -- Beda Huruf Depan
                            string createUlangFileNp2 = npLogHeader.LOG_NAMAFILE;
                            if (npLogHeader.LOG_JENIS == "NPR") {
                                createUlangFileNp2 = "X" + createUlangFileNp2.Substring(1);
                            }
                            else {
                                createUlangFileNp2 = "R" + createUlangFileNp2.Substring(1);
                            }
                            DataTable dtNpCreateUlangFileNp2 = await _db.GetNpCreateUlangFileNp2(npLogHeader.LOG_JENIS, lsLogSeqNo.ToArray(), npLogHeader.LOG_TOK_KODE, npLogHeader.LOG_TYPEFILE);
                            if (!_berkas.DataTable2CSV(dtNpCreateUlangFileNp2, $"{createUlangFileNp2}.CSV", "|")) {
                                throw new Exception($"Gagal Membuat {createUlangFileNp2}.CSV");
                            }

                            // -- File3 CSV ZIP
                            _berkas.ZipListFileInFolder(
                                $"{npLogHeader.LOG_NAMAFILE}.ZIP",
                                new List<string> {
                                    $"{createUlangFileNp1}.CSV",
                                    $"{createUlangFileNp2}.CSV"
                                }
                            );
                            string createUlangFileNp1Path = Path.Combine(_berkas.ZipFolderPath, $"{createUlangFileNp1}.ZIP");
                            lsAttachmentPath.Add(createUlangFileNp1Path);

                            /* ** Kirim Email ** */

                            if (lsAttachmentPath.Count <= 0) {
                                throw new Exception("Data Attachment E-Mail Tidak Tersedia");
                            }

                            // Email
                            string title = $"{npLogHeader.LOG_JENIS} TOKO :: {npLogHeader.LOG_TOK_KODE}";
                            string[] to = new string[] { };
                            string[] cc = new string[] { };
                            string[] bcc = new string[] { };
                            /* WARNING ** SPAM */
                            if (_app.DebugMode) {
                                title = "[SIMULASI] " + title;
                                to = new string[] {
                                    "edwin@indomaret.co.id",
                                    "bias@indomaret.co.id"
                                };
                                cc = new string[] {
                                    "nova.nujula@indomaret.co.id",
                                    "teguh.widi@indomaret.co.id",
                                    "hanani@indomaret.co.id",
                                    "rofiedmaindra@indomaret.co.id"
                                };
                                bcc = new string[] {
                                    "bayu.agastiya@indomaret.co.id",
                                    "chandrianto@indomaret.co.id"
                                };
                            }
                            else {
                                to = new string[] {
                                    npLogHeader.TOK_EMAIL.Trim()
                                };
                            }
                            await _surel.CreateAndSend(
                                title,
                                $"{npLogHeader.LOG_TOK_KODE} :: {npLogHeader.TOK_NAME}",
                                _surel.CreateEmailAddress("sd3@indomaret.co.id", $"[SD3_BOT] 📧 {_app.AppName} v{_app.AppVersion}"),
                                _surel.CreateEmailAddress(to),
                                _surel.CreateEmailAddress(cc),
                                _surel.CreateEmailAddress(bcc),
                                attachments: _surel.CreateEmailAttachment(lsAttachmentPath.ToArray())
                            );
                            await _db.UpdateAfterSendEmail(lsLogSeqNo.ToArray());

                        }
                    }
                    catch (Exception ex) {
                        if (lsLogSeqNo.Count > 0) {
                            await _db.UpdateAfterSendEmail(lsLogSeqNo.ToArray(), ex.Message);
                        }
                        _logger.WriteError(ex);
                    }
                    finally {
                        Thread.Sleep(250);
                    }
                }

                _berkas.CleanUp();
            });
            chkKirimSemuaNpQrEmail.Checked = false;
        }

        private async Task ProsesNPFileJsonByte() {
            await Task.Run(async () => {
                decimal isWrcMainTenis = await _db.CheckWrcMainTenis();
                if (isWrcMainTenis <= 0) {
                    List<decimal> lsLogSeqNo = new List<decimal>();
                    foreach (MNpLog npLogHeader in listNpLogPendingJsonByte) {
                        try {
                            lsLogSeqNo.Clear();
                            string sysDateKodeDc = string.Empty;
                            decimal count = await _db.CheckCreateUlangJsonByte(npLogHeader.LOG_DCKODE, npLogHeader.LOG_TOK_KODE, npLogHeader.LOG_NAMAFILE);
                            if (count == 0) {
                                string url = await _db.GetURLWRC(npLogHeader.LOG_TOK_KODE);
                                if (!string.IsNullOrEmpty(url)) {
                                    DataTable dtNpHeader = await _db.GetNpHeaderJsonByte(npLogHeader.LOG_DCKODE, npLogHeader.LOG_TOK_KODE, npLogHeader.LOG_NAMAFILE);
                                    if (dtNpHeader.Rows.Count > 0) {
                                        List<MNpCreateUlangJsonByteHeader> lsNpHeader = _converter.DataTableToList<MNpCreateUlangJsonByteHeader>(dtNpHeader);
                                        sysDateKodeDc = lsNpHeader.First().SYSDATEKODEDC;
                                        List<Dictionary<string, object>> lsDictHdr = new List<Dictionary<string, object>>();
                                        foreach (MNpCreateUlangJsonByteHeader npHeader in lsNpHeader) {
                                            if (!lsLogSeqNo.Contains(npHeader.LOG_SEQNO)) {
                                                lsLogSeqNo.Add(npHeader.LOG_SEQNO);
                                            }
                                            Dictionary<string, object> dictHeader = npHeader.GetType()
                                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                     .ToDictionary(prop => prop.Name, prop => {
                                                         var val = prop.GetValue(npHeader, null);
                                                         // SD7 Menyamakan NULL Dari Database Sebagai "" String Kosong :: Wkwk
                                                         if (val == null) {
                                                             val = "";
                                                         }
                                                         return val;
                                                     });
                                            dictHeader.Remove("LOG_SEQNO");
                                            dictHeader.Remove("SYSDATEKODEDC");
                                            List<Dictionary<string, object>> lsDictDtl = new List<Dictionary<string, object>>();
                                            DataTable dtNpDetail = await _db.GetNpDetailJsonByte(npHeader.LOG_SEQNO);
                                            if (dtNpDetail.Rows.Count > 0) {
                                                List<MNpCreateUlangJsonByteDetail> lsNpDetail = _converter.DataTableToList<MNpCreateUlangJsonByteDetail>(dtNpDetail);
                                                foreach (MNpCreateUlangJsonByteDetail npDetail in lsNpDetail) {
                                                    Dictionary<string, object> dictDetail = npDetail.GetType()
                                                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                             .ToDictionary(prop => prop.Name, prop => {
                                                                 var val = prop.GetValue(npDetail, null);
                                                                 // SD7 Menyamakan NULL Dari Database Sebagai "" String Kosong :: Wkwk
                                                                 if (val == null) {
                                                                     val = "";
                                                                 }
                                                                 return val;
                                                             });
                                                    dictDetail.Remove("LOG_FK_SEQNO");
                                                    lsDictDtl.Add(dictDetail);
                                                }
                                            }
                                            dictHeader.Add("DC_NPBTOKO_FILE", lsDictDtl);
                                            lsDictHdr.Add(dictHeader);
                                        }
                                        string jsonText = _converter.ObjectToJson(lsDictHdr);
                                        _logger.WriteInfo("[JSON_TEXT]", jsonText);
                                        byte[] textByte = _stream.GZipCompressString(jsonText);
                                        _logger.WriteInfo("[TEXT_BYTE]", BitConverter.ToString(textByte).Replace("-", ""));
                                        await _db.UpdateBeforeSendWebService(lsLogSeqNo.ToArray());
                                        // --
                                        // string byteText = _stream.GZipDecompressString(textByte);
                                        // bool test = jsonText == byteText;
                                        // --
                                        wsNPLtoko.NPB_Service ws = new wsNPLtoko.NPB_Service {
                                            Url = url,
                                            Timeout = 30 * 1000 // 30 detik
                                        };
                                        string res = ws.Receive(textByte, sysDateKodeDc);
                                        _logger.WriteInfo($"[wsNPLtoko.NPB_Service] {sysDateKodeDc}", res, force: true);
                                        if (_app.DebugMode) {
                                            MessageBox.Show(jsonText, "SIMULASI :: jsonText", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            MessageBox.Show(res, "SIMULASI :: wsNPLtoko.NPB_Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        if (res.ToUpper().Contains("SUKSES") || res.ToUpper().Contains("BERHASIL") || res.ToUpper().Contains("SELESAI")) {
                                            await _db.UpdateAfterSendWebService(lsLogSeqNo.ToArray());
                                        }
                                        else {
                                            string[] rez = res.Split('|');
                                            await _db.UpdateAfterSendWebService(lsLogSeqNo.ToArray(), $"{rez[0]} - {rez[1]}");
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex) {
                            if (lsLogSeqNo.Count > 0) {
                                await _db.UpdateBeforeSendWebService(lsLogSeqNo.ToArray(), ex.Message);
                            }
                            _logger.WriteError(ex);
                        }
                        finally {
                            Thread.Sleep(250);
                        }
                    }
                }

                _berkas.CleanUp();
            });
        }

        private async void BtnReFreshQrEmail_Click(object sender, EventArgs e) {
            SetIdleBusyStatus(false);
            await RefreshDataTableQrEmail();
            SetIdleBusyStatus(true);
        }

        private async void BtnReFreshJsonByte_Click(object sender, EventArgs e) {
            SetIdleBusyStatus(false);
            await RefreshDataTableJsonByte();
            SetIdleBusyStatus(true);
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e) {
            Process.Start(new ProcessStartInfo { Arguments = _berkas.BackupFolderPath, FileName = "explorer.exe" });
        }

    }

}
