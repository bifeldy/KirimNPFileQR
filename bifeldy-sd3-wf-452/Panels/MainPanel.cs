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
using System.Text;
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
        private readonly IBerkas _berkas;
        private readonly IStream _stream;
        private readonly IQrBar _qrBar;
        private readonly ISurel _surel;

        private CMainForm mainForm;

        bool timerBusy = false;

        readonly int waitTime = 1 * 60;
        int countDownSeconds = 0;

        /* NP* Header */

        private List<MNpLog> listNpLogPending = null;
        private BindingList<MNpLog> bindNpLogPending = null;

        private List<MNpLog> listNpLogGagal = null;
        private BindingList<MNpLog> bindNpLogGagal = null;

        public CMainPanel(
            IApp app,
            ILogger logger,
            IDb db,
            IConfig config,
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

            listNpLogPending = new List<MNpLog>();
            bindNpLogPending = new BindingList<MNpLog>(listNpLogPending);

            listNpLogGagal = new List<MNpLog>();
            bindNpLogGagal = new BindingList<MNpLog>(listNpLogGagal);

            txtBxDaysRetentionFiles.Value = _berkas.MaxOldRetentionDay;
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

            //
            // Testing Buat QR & Baca QR & Bandingin Isi QR
            //
            // Image bmp1 = Image.FromFile("D:/_data/Bifeldy/_sources/KirimNPFileQR/bifeldy-sd3-wf-452/bin/Debug/Backup_Files/6379586_NPBG001TRIY20230830152154_DETAIL_01-05.JPG");
            // string txt1 = _qrBar.ReadTextFromQrBarCode(bmp1);
            // MessageBox.Show(txt1);
            // 
            // Image img1 = _qrBar.GenerateQrCode(txt1);
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

            CheckTableColumn();
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

        private void TxtBxDaysRetentionFiles_ValueChanged(object sender, EventArgs e) {
            _berkas.MaxOldRetentionDay = (int)txtBxDaysRetentionFiles.Value;
            _config.Set("MaxOldRetentionDay", _berkas.MaxOldRetentionDay);
        }

        private async void CheckTableColumn() {
            try {
                await Task.Run(async () => {
                    await _db.OraPg_AlterTable_AddColumnIfNotExist("DC_NPBTOKO_LOG", "KIRIM_EMAIL", "DATE");
                    await _db.OraPg_AlterTable_AddColumnIfNotExist("DC_NPBTOKO_LOG", "STATUS_KIRIM_EMAIL", $"VARCHAR{(_app.IsUsingPostgres ? "" : "2")}(100)");
                    await _db.OraPg_AlterTable_AddColumnIfNotExist("DC_NPBTOKO_LOG", "KODE_STAT_KRIM_MAIL", $"VARCHAR{(_app.IsUsingPostgres ? "" : "2")}(10)");
                });
                ReStartTimer();
            }
            catch (Exception ex) {
                _logger.WriteError(ex);
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

        private async void TmrCountDown_Tick(object sender, EventArgs e) {
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

        private async Task RefreshDataTable() {
            try {
                listNpLogPending.Clear();
                listNpLogGagal.Clear();
                DataTable dtNpLog = new DataTable();
                await Task.Run(async () => {
                    dtNpLog = await _db.GetNpLog();
                });
                if (dtNpLog.Rows.Count > 0) {
                    // Program Not Responding
                    // Jangan Di Masukkin Ke Thread
                    List<MNpLog> lsNpLog = _converter.DataTableToList<MNpLog>(dtNpLog);
                    // Sekalian Buat Nahan Window Message Queuenya
                    // Biar Timer Ke Tunda
                    foreach (MNpLog npLog in lsNpLog) {
                        if (string.IsNullOrEmpty(npLog.STATUS_KIRIM_EMAIL)) {
                            listNpLogPending.Add(npLog);
                        }
                        else {
                            listNpLogGagal.Add(npLog);
                        }
                    }
                    List<string> columnToShow = new List<string> {
                        "HDR_NOSJ",
                        "LOG_SEQNO",
                        "LOG_DCKODE",
                        "LOG_TOK_KODE",
                        "LOG_NO_NPB",
                        "LOG_TGL_NPB",
                        "LOG_NAMAFILE",
                        "LOG_ITEM",
                        "LOG_STAT_RCV",
                        "LOG_JENIS",
                        "TOK_NAME",
                        "TOK_KIRIM",
                        "TOK_EMAIL"
                    };
                    dtGrdNpPending.DataSource = bindNpLogPending;
                    EnableCustomColumnOnly(dtGrdNpPending, columnToShow);
                    dtGrdNpGagal.DataSource = bindNpLogGagal;
                    EnableCustomColumnOnly(dtGrdNpGagal, columnToShow);
                }
                bindNpLogPending.ResetBindings();
                dtGrdNpPending.ClearSelection();
                bindNpLogGagal.ResetBindings();
                dtGrdNpGagal.ClearSelection();
            }
            catch (Exception ex) {
                _logger.WriteError(ex);
            }
        }

        private async Task ProsesNPFile() {
            bool kirimUlangGagal = chkKirimSemuaNp.Checked;
            await Task.Run(async () => {
                _berkas.DeleteOldFilesInFolder(_berkas.TempFolderPath, 0);
                List<MNpLog> listNpLog = listNpLogPending;
                if (kirimUlangGagal) {
                    listNpLog.AddRange(listNpLogGagal);
                }
                foreach (MNpLog npLog in listNpLog) {
                    try {
                        int maxQrChar = 1853;
                        string zipPassword = "PernahKejepit2XOuch!!";
                        string lastCharDetail = "*";
                        string lastCharHeader = "-";
                        int versionQrHeader = 17;
                        int versionQrDetail = 25;
                        // string imageQrLogoPath = Path.Combine(_app.AppLocation, "Images", "domar.gif");
                        List<string> lsAttachmentPath = new List<string>();

                        /* ** Create Ulang Qr Code ** */

                        // -- Detail
                        string detailCreateUlangQrCodeFileName = $"{npLog.LOG_SEQNO}_{npLog.LOG_NAMAFILE}_DETAIL";
                        DataTable dtNpCreateUlangQrCodeDetail = await _db.GetNpCreateUlangQrCodeDetail(npLog.LOG_SEQNO);
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
                        string headerCreateUlangQrCodeFileName = $"{npLog.LOG_SEQNO}_{npLog.LOG_NAMAFILE}_HEADER";
                        // -- Header CSV
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("TOKO|KIRIM|GEMBOK|NOSJ|NORANG|JMLPART|JMLRECORD");
                        DataTable dtNpCreateUlangQrCodeHeader = await _db.GetNpCreateUlangQrCodeHeader(npLog.LOG_JENIS, npLog.LOG_NO_NPB, npLog.LOG_TGL_NPB);
                        MNpCreateUlangQrCodeHeader npCreateUlangQrCodeHeader = _converter.DataTableToList<MNpCreateUlangQrCodeHeader>(dtNpCreateUlangQrCodeHeader).First();
                        sb.AppendLine($"{npLog.LOG_TOK_KODE}|{await _db.GetKodeDc()}|{npCreateUlangQrCodeHeader.NOKUNCI}|{npCreateUlangQrCodeHeader.NOSJ}|{npCreateUlangQrCodeHeader.NORANG}|{txtDvdr.JumlahPart}|{dtNpCreateUlangQrCodeDetail.Rows.Count}");
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
                        Image headerCreateUlangQrCodeQr = _qrBar.GenerateQrCode(headerCreateUlangQrCodeHex, version: versionQrHeader);
                        // headerCreateUlangQrCodeQr = _qrBar.AddQrLogo(headerCreateUlangQrCodeQr, Image.FromFile(imageQrLogoPath));
                        headerCreateUlangQrCodeQr = _qrBar.AddQrCaption(headerCreateUlangQrCodeQr, $"{headerCreateUlangQrCodeFileName}.JPG");
                        string headerCreateUlangQrCodeQrImgPath = Path.Combine(_berkas.TempFolderPath, $"{headerCreateUlangQrCodeFileName}.JPG");
                        headerCreateUlangQrCodeQr.Save(headerCreateUlangQrCodeQrImgPath, ImageFormat.Jpeg);
                        lsAttachmentPath.Add(headerCreateUlangQrCodeQrImgPath);
                        // -- QR Detail
                        int totalQr = txtDvdr.JumlahPart;
                        for (int i = 0; i < totalQr; i++) {
                            // 2 Digit Dengan Awal 0
                            string idx = (i + 1).ToString("00#");
                            string saltDetailHex = $"{idx}{npCreateUlangQrCodeHeader.NOSJ}{txtDvdr.ArrDevidedText[i]}{lastCharDetail}";
                            string urutan = $"{idx}-{totalQr:00#}";
                            Image detailCreateUlangQrCodeQr = _qrBar.GenerateQrCode(saltDetailHex, version: versionQrDetail);
                            // detailCreateUlangQrCodeQr = _qrBar.AddQrLogo(detailCreateUlangQrCodeQr, Image.FromFile(imageQrLogoPath));
                            detailCreateUlangQrCodeQr = _qrBar.AddQrCaption(detailCreateUlangQrCodeQr, $"{detailCreateUlangQrCodeFileName}_{urutan}.JPG");
                            string detailCreateUlangQrCodeQrImgPath = Path.Combine(_berkas.TempFolderPath, $"{detailCreateUlangQrCodeFileName}_{urutan}.JPG");
                            detailCreateUlangQrCodeQr.Save(detailCreateUlangQrCodeQrImgPath, ImageFormat.Jpeg);
                            lsAttachmentPath.Add(detailCreateUlangQrCodeQrImgPath);
                        }

                        /* ** Create Ulang File NP ** */

                        // -- File1 CSV -- Sama Sesuai Log
                        string createUlangFileNp1 = $"{npLog.LOG_SEQNO}_{npLog.LOG_NAMAFILE}";
                        DataTable dtNpCreateUlangFileNp1 = await _db.GetNpCreateUlangFileNp1(npLog.LOG_JENIS, npLog.LOG_SEQNO, npLog.LOG_DCKODE);
                        if (!_berkas.DataTable2CSV(dtNpCreateUlangFileNp1, $"{createUlangFileNp1}.CSV", "|")) {
                            throw new Exception($"Gagal Membuat {createUlangFileNp1}.CSV");
                        }
                        // -- File2 CSV -- Beda Huruf Depan
                        string createUlangFileNp2 = npLog.LOG_NAMAFILE;
                        if (npLog.LOG_NAMAFILE == "NPR") {
                            createUlangFileNp2 = npLog.LOG_SEQNO + "_X" + createUlangFileNp2.Substring(1);
                        }
                        else {
                            createUlangFileNp2 = npLog.LOG_SEQNO + "_R" + createUlangFileNp2.Substring(1);
                        }
                        DataTable dtNpCreateUlangFileNp2 = await _db.GetNpCreateUlangFileNp2(npLog.LOG_JENIS, npLog.LOG_SEQNO, npLog.LOG_TOK_KODE, npLog.LOG_TYPEFILE, npLog.LOG_TGL_NPB);
                        if (!_berkas.DataTable2CSV(dtNpCreateUlangFileNp2, $"{createUlangFileNp2}.CSV", "|")) {
                            throw new Exception($"Gagal Membuat {createUlangFileNp2}.CSV");
                        }
                        // -- File3 ZIP
                        _berkas.ZipListFileInFolder(
                            $"{createUlangFileNp1}.ZIP",
                            new List<string> {
                                $"{createUlangFileNp1}.CSV",
                                $"{createUlangFileNp2}.CSV"
                            }
                        );
                        string createUlangFileNp1Path = Path.Combine(_berkas.ZipFolderPath, $"{createUlangFileNp1}.ZIP");
                        lsAttachmentPath.Add(createUlangFileNp1Path);

                        /* ** Kirim Email ** */

                        // Email
                        string title = $"{npLog.HDR_JENIS} TOKO :: {npLog.LOG_TOK_KODE}";
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
                                npLog.TOK_EMAIL.Trim()
                            };
                        }
                        await _surel.CreateAndSend(
                            title,
                            $"INV :: {npLog.LOG_NO_INV}",
                            _surel.CreateEmailAddress("sd3@indomaret.co.id", $"[SD3_BOT] 📧 {_app.AppName} v{_app.AppVersion}"),
                            _surel.CreateEmailAddress(to),
                            _surel.CreateEmailAddress(cc),
                            _surel.CreateEmailAddress(bcc),
                            attachments: _surel.CreateEmailAttachment(lsAttachmentPath.ToArray())
                        );
                        await _db.UpdateAfterSendEmail(npLog.LOG_SEQNO);
                    }
                    catch (Exception ex) {
                        await _db.UpdateAfterSendEmail(npLog.LOG_SEQNO, ex.Message);
                        _logger.WriteError(ex);
                    }
                }
                _berkas.CleanUp();
            });
            chkKirimSemuaNp.Checked = false;
        }

        private async void BtbReFresh_Click(object sender, EventArgs e) {
            SetIdleBusyStatus(false);
            await RefreshDataTable();
            SetIdleBusyStatus(true);
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e) {
            Process.Start(new ProcessStartInfo { Arguments = _berkas.BackupFolderPath, FileName = "explorer.exe" });
        }
    }

}
