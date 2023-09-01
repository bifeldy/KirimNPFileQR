
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
            this.chkSemuaKolom = new System.Windows.Forms.CheckBox();
            this.userInfo = new System.Windows.Forms.Label();
            this.imgDomar = new System.Windows.Forms.PictureBox();
            this.prgrssBrStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.appInfo = new System.Windows.Forms.Label();
            this.dtGrdNpPending = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCountDown = new System.Windows.Forms.Label();
            this.tmrCountDown = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btbReFresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxDaysRetentionFiles = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.tbNp = new System.Windows.Forms.TabControl();
            this.tbPending = new System.Windows.Forms.TabPage();
            this.tbGagal = new System.Windows.Forms.TabPage();
            this.dtGrdNpGagal = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.imgDomar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNpPending)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBxDaysRetentionFiles)).BeginInit();
            this.tbNp.SuspendLayout();
            this.tbPending.SuspendLayout();
            this.tbGagal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNpGagal)).BeginInit();
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
            // dtGrdNpPending
            // 
            this.dtGrdNpPending.AllowUserToAddRows = false;
            this.dtGrdNpPending.AllowUserToDeleteRows = false;
            this.dtGrdNpPending.AllowUserToOrderColumns = true;
            this.dtGrdNpPending.AllowUserToResizeRows = false;
            this.dtGrdNpPending.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtGrdNpPending.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtGrdNpPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdNpPending.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrdNpPending.Location = new System.Drawing.Point(3, 3);
            this.dtGrdNpPending.Name = "dtGrdNpPending";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdNpPending.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGrdNpPending.RowHeadersVisible = false;
            this.dtGrdNpPending.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdNpPending.Size = new System.Drawing.Size(726, 320);
            this.dtGrdNpPending.TabIndex = 6;
            this.dtGrdNpPending.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DtGrd_DataError);
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
            // lblCountDown
            // 
            this.lblCountDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountDown.AutoSize = true;
            this.lblCountDown.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblCountDown.Location = new System.Drawing.Point(706, 133);
            this.lblCountDown.Name = "lblCountDown";
            this.lblCountDown.Size = new System.Drawing.Size(65, 20);
            this.lblCountDown.TabIndex = 8;
            this.lblCountDown.Text = "88:88:88";
            this.lblCountDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrCountDown
            // 
            this.tmrCountDown.Interval = 1000;
            this.tmrCountDown.Tick += new System.EventHandler(this.TmrCountDown_Tick);
            // 
            // btbReFresh
            // 
            this.btbReFresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btbReFresh.Location = new System.Drawing.Point(625, 133);
            this.btbReFresh.Name = "btbReFresh";
            this.btbReFresh.Size = new System.Drawing.Size(75, 23);
            this.btbReFresh.TabIndex = 9;
            this.btbReFresh.Text = "ReFresh";
            this.btbReFresh.UseVisualStyleBackColor = true;
            this.btbReFresh.Click += new System.EventHandler(this.BtbReFresh_Click);
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
            // tbNp
            // 
            this.tbNp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNp.Controls.Add(this.tbPending);
            this.tbNp.Controls.Add(this.tbGagal);
            this.tbNp.Location = new System.Drawing.Point(31, 175);
            this.tbNp.Name = "tbNp";
            this.tbNp.SelectedIndex = 0;
            this.tbNp.Size = new System.Drawing.Size(740, 352);
            this.tbNp.TabIndex = 43;
            // 
            // tbPending
            // 
            this.tbPending.Controls.Add(this.dtGrdNpPending);
            this.tbPending.Location = new System.Drawing.Point(4, 22);
            this.tbPending.Name = "tbPending";
            this.tbPending.Padding = new System.Windows.Forms.Padding(3);
            this.tbPending.Size = new System.Drawing.Size(732, 326);
            this.tbPending.TabIndex = 0;
            this.tbPending.Text = "PENDING KIRIM";
            this.tbPending.UseVisualStyleBackColor = true;
            // 
            // tbGagal
            // 
            this.tbGagal.Controls.Add(this.dtGrdNpGagal);
            this.tbGagal.Location = new System.Drawing.Point(4, 22);
            this.tbGagal.Name = "tbGagal";
            this.tbGagal.Padding = new System.Windows.Forms.Padding(3);
            this.tbGagal.Size = new System.Drawing.Size(732, 326);
            this.tbGagal.TabIndex = 1;
            this.tbGagal.Text = "GAGAL KIRIM";
            this.tbGagal.UseVisualStyleBackColor = true;
            // 
            // dtGrdNpGagal
            // 
            this.dtGrdNpGagal.AllowUserToAddRows = false;
            this.dtGrdNpGagal.AllowUserToDeleteRows = false;
            this.dtGrdNpGagal.AllowUserToOrderColumns = true;
            this.dtGrdNpGagal.AllowUserToResizeRows = false;
            this.dtGrdNpGagal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtGrdNpGagal.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtGrdNpGagal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdNpGagal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrdNpGagal.Location = new System.Drawing.Point(3, 3);
            this.dtGrdNpGagal.Name = "dtGrdNpGagal";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdNpGagal.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtGrdNpGagal.RowHeadersVisible = false;
            this.dtGrdNpGagal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdNpGagal.Size = new System.Drawing.Size(726, 320);
            this.dtGrdNpGagal.TabIndex = 7;
            // 
            // CMainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbNp);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxDaysRetentionFiles);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btbReFresh);
            this.Controls.Add(this.lblCountDown);
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
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNpPending)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBxDaysRetentionFiles)).EndInit();
            this.tbNp.ResumeLayout(false);
            this.tbPending.ResumeLayout(false);
            this.tbGagal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNpGagal)).EndInit();
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
        private System.Windows.Forms.DataGridView dtGrdNpPending;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCountDown;
        private System.Windows.Forms.Timer tmrCountDown;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btbReFresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtBxDaysRetentionFiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.TabControl tbNp;
        private System.Windows.Forms.TabPage tbPending;
        private System.Windows.Forms.TabPage tbGagal;
        private System.Windows.Forms.DataGridView dtGrdNpGagal;
    }

}
