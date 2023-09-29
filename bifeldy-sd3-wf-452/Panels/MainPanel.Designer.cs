
namespace KirimNPFileQR.Panels {

    public sealed partial class CMainPanel {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMainPanel));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chkSemuaKolom = new System.Windows.Forms.CheckBox();
            this.userInfo = new System.Windows.Forms.Label();
            this.imgDomar = new System.Windows.Forms.PictureBox();
            this.prgrssBrStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.appInfo = new System.Windows.Forms.Label();
            this.dtGrdNpPendingQrEmail = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCountDownQrEmail = new System.Windows.Forms.Label();
            this.tmrQrEmail = new System.Windows.Forms.Timer(this.components);
            this.btnReFreshQrEmail = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxDaysRetentionFiles = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.tbNpQrEmail = new System.Windows.Forms.TabControl();
            this.tbPending = new System.Windows.Forms.TabPage();
            this.tbGagal = new System.Windows.Forms.TabPage();
            this.dtGrdNpGagalQrEmail = new System.Windows.Forms.DataGridView();
            this.chkKirimSemuaNpQrEmail = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnReFreshJsonByte = new System.Windows.Forms.Button();
            this.lblCountDownJsonByte = new System.Windows.Forms.Label();
            this.dtGrdNpPendingJsonByte = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnStartStopQrEmail = new System.Windows.Forms.Button();
            this.tmrJsonByte = new System.Windows.Forms.Timer(this.components);
            this.btnStartStopJsonByte = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgDomar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNpPendingQrEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBxDaysRetentionFiles)).BeginInit();
            this.tbNpQrEmail.SuspendLayout();
            this.tbPending.SuspendLayout();
            this.tbGagal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNpGagalQrEmail)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNpPendingJsonByte)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkSemuaKolom
            // 
            this.chkSemuaKolom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSemuaKolom.AutoSize = true;
            this.chkSemuaKolom.Location = new System.Drawing.Point(572, 69);
            this.chkSemuaKolom.Name = "chkSemuaKolom";
            this.chkSemuaKolom.Size = new System.Drawing.Size(143, 17);
            this.chkSemuaKolom.TabIndex = 0;
            this.chkSemuaKolom.Text = "Tampilkan Semua Kolom";
            this.chkSemuaKolom.UseVisualStyleBackColor = true;
            // 
            // userInfo
            // 
            this.userInfo.AutoSize = true;
            this.userInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(157)))), ((int)(((byte)(88)))));
            this.userInfo.Location = new System.Drawing.Point(51, 62);
            this.userInfo.Name = "userInfo";
            this.userInfo.Size = new System.Drawing.Size(343, 21);
            this.userInfo.TabIndex = 1;
            this.userInfo.Text = ".: {{ KodeDc }} - {{ NamaDc }} :: {{ UserName }} :.";
            this.userInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imgDomar
            // 
            this.imgDomar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgDomar.Image = ((System.Drawing.Image)(resources.GetObject("imgDomar.Image")));
            this.imgDomar.Location = new System.Drawing.Point(720, 30);
            this.imgDomar.Name = "imgDomar";
            this.imgDomar.Size = new System.Drawing.Size(51, 56);
            this.imgDomar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgDomar.TabIndex = 2;
            this.imgDomar.TabStop = false;
            this.imgDomar.Click += new System.EventHandler(this.ImgDomar_Click);
            // 
            // prgrssBrStatus
            // 
            this.prgrssBrStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.prgrssBrStatus.ForeColor = System.Drawing.Color.GreenYellow;
            this.prgrssBrStatus.Location = new System.Drawing.Point(571, 55);
            this.prgrssBrStatus.MarqueeAnimationSpeed = 25;
            this.prgrssBrStatus.Name = "prgrssBrStatus";
            this.prgrssBrStatus.Size = new System.Drawing.Size(139, 10);
            this.prgrssBrStatus.Step = 1;
            this.prgrssBrStatus.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgrssBrStatus.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(64)))), ((int)(((byte)(129)))));
            this.lblStatus.Location = new System.Drawing.Point(568, 33);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(142, 23);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Program {{ Idle }} ...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // appInfo
            // 
            this.appInfo.AutoSize = true;
            this.appInfo.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(133)))), ((int)(((byte)(244)))));
            this.appInfo.Location = new System.Drawing.Point(26, 27);
            this.appInfo.Name = "appInfo";
            this.appInfo.Size = new System.Drawing.Size(342, 30);
            this.appInfo.TabIndex = 5;
            this.appInfo.Text = "{{ BoilerPlate-Net452-WinForm }}";
            this.appInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtGrdNpPendingQrEmail
            // 
            this.dtGrdNpPendingQrEmail.AllowUserToAddRows = false;
            this.dtGrdNpPendingQrEmail.AllowUserToDeleteRows = false;
            this.dtGrdNpPendingQrEmail.AllowUserToOrderColumns = true;
            this.dtGrdNpPendingQrEmail.AllowUserToResizeRows = false;
            this.dtGrdNpPendingQrEmail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtGrdNpPendingQrEmail.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtGrdNpPendingQrEmail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdNpPendingQrEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrdNpPendingQrEmail.Location = new System.Drawing.Point(3, 3);
            this.dtGrdNpPendingQrEmail.Name = "dtGrdNpPendingQrEmail";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdNpPendingQrEmail.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGrdNpPendingQrEmail.RowHeadersVisible = false;
            this.dtGrdNpPendingQrEmail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdNpPendingQrEmail.Size = new System.Drawing.Size(706, 242);
            this.dtGrdNpPendingQrEmail.TabIndex = 6;
            this.dtGrdNpPendingQrEmail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DtGrd_DataError);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(68)))), ((int)(((byte)(55)))));
            this.label1.Location = new System.Drawing.Point(27, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Dokumen NP* Yang Perlu Di Kirim Ke Toko";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCountDownQrEmail
            // 
            this.lblCountDownQrEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountDownQrEmail.AutoSize = true;
            this.lblCountDownQrEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountDownQrEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblCountDownQrEmail.Location = new System.Drawing.Point(652, 17);
            this.lblCountDownQrEmail.Name = "lblCountDownQrEmail";
            this.lblCountDownQrEmail.Size = new System.Drawing.Size(65, 20);
            this.lblCountDownQrEmail.TabIndex = 8;
            this.lblCountDownQrEmail.Text = "88:88:88";
            this.lblCountDownQrEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrQrEmail
            // 
            this.tmrQrEmail.Interval = 1000;
            this.tmrQrEmail.Tick += new System.EventHandler(this.TmrQrEmail_Tick);
            // 
            // btnReFreshQrEmail
            // 
            this.btnReFreshQrEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReFreshQrEmail.Location = new System.Drawing.Point(571, 17);
            this.btnReFreshQrEmail.Name = "btnReFreshQrEmail";
            this.btnReFreshQrEmail.Size = new System.Drawing.Size(75, 23);
            this.btnReFreshQrEmail.TabIndex = 9;
            this.btnReFreshQrEmail.Text = "ReFresh";
            this.btnReFreshQrEmail.UseVisualStyleBackColor = true;
            this.btnReFreshQrEmail.Click += new System.EventHandler(this.BtnReFreshQrEmail_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(157)))), ((int)(((byte)(88)))));
            this.label2.Location = new System.Drawing.Point(547, 551);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 23);
            this.label2.TabIndex = 41;
            this.label2.Text = "Hari Terakhir";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBxDaysRetentionFiles
            // 
            this.txtBxDaysRetentionFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxDaysRetentionFiles.Location = new System.Drawing.Point(471, 552);
            this.txtBxDaysRetentionFiles.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.txtBxDaysRetentionFiles.Name = "txtBxDaysRetentionFiles";
            this.txtBxDaysRetentionFiles.Size = new System.Drawing.Size(70, 20);
            this.txtBxDaysRetentionFiles.TabIndex = 40;
            this.txtBxDaysRetentionFiles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBxDaysRetentionFiles.ThousandsSeparator = true;
            this.txtBxDaysRetentionFiles.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.txtBxDaysRetentionFiles.Value = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.txtBxDaysRetentionFiles.ValueChanged += new System.EventHandler(this.TxtBxDaysRetentionFiles_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.label4.Location = new System.Drawing.Point(31, 549);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(434, 26);
            this.label4.TabIndex = 39;
            this.label4.Text = "Max Umur Retention Berkas File";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFolder.Location = new System.Drawing.Point(651, 552);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(120, 23);
            this.btnOpenFolder.TabIndex = 42;
            this.btnOpenFolder.Text = "Buka Folder Backup";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.BtnOpenFolder_Click);
            // 
            // tbNpQrEmail
            // 
            this.tbNpQrEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNpQrEmail.Controls.Add(this.tbPending);
            this.tbNpQrEmail.Controls.Add(this.tbGagal);
            this.tbNpQrEmail.Location = new System.Drawing.Point(6, 46);
            this.tbNpQrEmail.Name = "tbNpQrEmail";
            this.tbNpQrEmail.SelectedIndex = 0;
            this.tbNpQrEmail.Size = new System.Drawing.Size(720, 274);
            this.tbNpQrEmail.TabIndex = 43;
            // 
            // tbPending
            // 
            this.tbPending.Controls.Add(this.dtGrdNpPendingQrEmail);
            this.tbPending.Location = new System.Drawing.Point(4, 22);
            this.tbPending.Name = "tbPending";
            this.tbPending.Padding = new System.Windows.Forms.Padding(3);
            this.tbPending.Size = new System.Drawing.Size(712, 248);
            this.tbPending.TabIndex = 0;
            this.tbPending.Text = "PENDING KIRIM";
            this.tbPending.UseVisualStyleBackColor = true;
            // 
            // tbGagal
            // 
            this.tbGagal.Controls.Add(this.dtGrdNpGagalQrEmail);
            this.tbGagal.Location = new System.Drawing.Point(4, 22);
            this.tbGagal.Name = "tbGagal";
            this.tbGagal.Padding = new System.Windows.Forms.Padding(3);
            this.tbGagal.Size = new System.Drawing.Size(712, 248);
            this.tbGagal.TabIndex = 1;
            this.tbGagal.Text = "GAGAL KIRIM";
            this.tbGagal.UseVisualStyleBackColor = true;
            // 
            // dtGrdNpGagalQrEmail
            // 
            this.dtGrdNpGagalQrEmail.AllowUserToAddRows = false;
            this.dtGrdNpGagalQrEmail.AllowUserToDeleteRows = false;
            this.dtGrdNpGagalQrEmail.AllowUserToOrderColumns = true;
            this.dtGrdNpGagalQrEmail.AllowUserToResizeRows = false;
            this.dtGrdNpGagalQrEmail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtGrdNpGagalQrEmail.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtGrdNpGagalQrEmail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdNpGagalQrEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrdNpGagalQrEmail.Location = new System.Drawing.Point(3, 3);
            this.dtGrdNpGagalQrEmail.Name = "dtGrdNpGagalQrEmail";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdNpGagalQrEmail.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtGrdNpGagalQrEmail.RowHeadersVisible = false;
            this.dtGrdNpGagalQrEmail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdNpGagalQrEmail.Size = new System.Drawing.Size(706, 242);
            this.dtGrdNpGagalQrEmail.TabIndex = 7;
            // 
            // chkKirimSemuaNpQrEmail
            // 
            this.chkKirimSemuaNpQrEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkKirimSemuaNpQrEmail.AutoSize = true;
            this.chkKirimSemuaNpQrEmail.Location = new System.Drawing.Point(290, 20);
            this.chkKirimSemuaNpQrEmail.Name = "chkKirimSemuaNpQrEmail";
            this.chkKirimSemuaNpQrEmail.Size = new System.Drawing.Size(194, 17);
            this.chkKirimSemuaNpQrEmail.TabIndex = 44;
            this.chkKirimSemuaNpQrEmail.Text = "Paksa Kirim Ulang Lagi Yang Gagal";
            this.chkKirimSemuaNpQrEmail.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(31, 175);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(740, 352);
            this.tabControl1.TabIndex = 45;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnStartStopJsonByte);
            this.tabPage1.Controls.Add(this.btnReFreshJsonByte);
            this.tabPage1.Controls.Add(this.lblCountDownJsonByte);
            this.tabPage1.Controls.Add(this.dtGrdNpPendingJsonByte);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(732, 326);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Json Byte";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnReFreshJsonByte
            // 
            this.btnReFreshJsonByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReFreshJsonByte.Location = new System.Drawing.Point(571, 17);
            this.btnReFreshJsonByte.Name = "btnReFreshJsonByte";
            this.btnReFreshJsonByte.Size = new System.Drawing.Size(75, 23);
            this.btnReFreshJsonByte.TabIndex = 11;
            this.btnReFreshJsonByte.Text = "ReFresh";
            this.btnReFreshJsonByte.UseVisualStyleBackColor = true;
            this.btnReFreshJsonByte.Click += new System.EventHandler(this.BtnReFreshJsonByte_Click);
            // 
            // lblCountDownJsonByte
            // 
            this.lblCountDownJsonByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountDownJsonByte.AutoSize = true;
            this.lblCountDownJsonByte.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountDownJsonByte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblCountDownJsonByte.Location = new System.Drawing.Point(652, 17);
            this.lblCountDownJsonByte.Name = "lblCountDownJsonByte";
            this.lblCountDownJsonByte.Size = new System.Drawing.Size(65, 20);
            this.lblCountDownJsonByte.TabIndex = 10;
            this.lblCountDownJsonByte.Text = "88:88:88";
            this.lblCountDownJsonByte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtGrdNpPendingJsonByte
            // 
            this.dtGrdNpPendingJsonByte.AllowUserToAddRows = false;
            this.dtGrdNpPendingJsonByte.AllowUserToDeleteRows = false;
            this.dtGrdNpPendingJsonByte.AllowUserToOrderColumns = true;
            this.dtGrdNpPendingJsonByte.AllowUserToResizeRows = false;
            this.dtGrdNpPendingJsonByte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGrdNpPendingJsonByte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtGrdNpPendingJsonByte.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtGrdNpPendingJsonByte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdNpPendingJsonByte.Location = new System.Drawing.Point(3, 57);
            this.dtGrdNpPendingJsonByte.Name = "dtGrdNpPendingJsonByte";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdNpPendingJsonByte.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtGrdNpPendingJsonByte.RowHeadersVisible = false;
            this.dtGrdNpPendingJsonByte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdNpPendingJsonByte.Size = new System.Drawing.Size(726, 266);
            this.dtGrdNpPendingJsonByte.TabIndex = 6;
            this.dtGrdNpPendingJsonByte.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DtGrd_DataError);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnStartStopQrEmail);
            this.tabPage2.Controls.Add(this.tbNpQrEmail);
            this.tabPage2.Controls.Add(this.chkKirimSemuaNpQrEmail);
            this.tabPage2.Controls.Add(this.btnReFreshQrEmail);
            this.tabPage2.Controls.Add(this.lblCountDownQrEmail);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(732, 326);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "QR Email";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnStartStopQrEmail
            // 
            this.btnStartStopQrEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStopQrEmail.Location = new System.Drawing.Point(490, 17);
            this.btnStartStopQrEmail.Name = "btnStartStopQrEmail";
            this.btnStartStopQrEmail.Size = new System.Drawing.Size(75, 23);
            this.btnStartStopQrEmail.TabIndex = 45;
            this.btnStartStopQrEmail.Text = "Start";
            this.btnStartStopQrEmail.UseVisualStyleBackColor = true;
            this.btnStartStopQrEmail.Click += new System.EventHandler(this.btnStartStopQrEmail_Click);
            // 
            // tmrJsonByte
            // 
            this.tmrJsonByte.Interval = 1000;
            this.tmrJsonByte.Tick += new System.EventHandler(this.TmrJsonByte_Tick);
            // 
            // btnStartStopJsonByte
            // 
            this.btnStartStopJsonByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStopJsonByte.Location = new System.Drawing.Point(490, 17);
            this.btnStartStopJsonByte.Name = "btnStartStopJsonByte";
            this.btnStartStopJsonByte.Size = new System.Drawing.Size(75, 23);
            this.btnStartStopJsonByte.TabIndex = 46;
            this.btnStartStopJsonByte.Text = "Start";
            this.btnStartStopJsonByte.UseVisualStyleBackColor = true;
            this.btnStartStopJsonByte.Click += new System.EventHandler(this.BtnStartStopJsonByte_Click);
            // 
            // CMainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxDaysRetentionFiles);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSemuaKolom);
            this.Controls.Add(this.userInfo);
            this.Controls.Add(this.imgDomar);
            this.Controls.Add(this.prgrssBrStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.appInfo);
            this.Name = "CMainPanel";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.CMainPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgDomar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNpPendingQrEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBxDaysRetentionFiles)).EndInit();
            this.tbNpQrEmail.ResumeLayout(false);
            this.tbPending.ResumeLayout(false);
            this.tbGagal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNpGagalQrEmail)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNpPendingJsonByte)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSemuaKolom;
        private System.Windows.Forms.Label userInfo;
        private System.Windows.Forms.PictureBox imgDomar;
        private System.Windows.Forms.ProgressBar prgrssBrStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label appInfo;
        private System.Windows.Forms.DataGridView dtGrdNpPendingQrEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCountDownQrEmail;
        private System.Windows.Forms.Timer tmrQrEmail;
        private System.Windows.Forms.Button btnReFreshQrEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtBxDaysRetentionFiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.TabControl tbNpQrEmail;
        private System.Windows.Forms.TabPage tbPending;
        private System.Windows.Forms.TabPage tbGagal;
        private System.Windows.Forms.DataGridView dtGrdNpGagalQrEmail;
        private System.Windows.Forms.CheckBox chkKirimSemuaNpQrEmail;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dtGrdNpPendingJsonByte;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnReFreshJsonByte;
        private System.Windows.Forms.Label lblCountDownJsonByte;
        private System.Windows.Forms.Timer tmrJsonByte;
        private System.Windows.Forms.Button btnStartStopQrEmail;
        private System.Windows.Forms.Button btnStartStopJsonByte;
    }

}
