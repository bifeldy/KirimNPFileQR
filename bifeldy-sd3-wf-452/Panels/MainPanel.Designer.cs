
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
            this.chkSemuaKolom = new System.Windows.Forms.CheckBox();
            this.userInfo = new System.Windows.Forms.Label();
            this.imgDomar = new System.Windows.Forms.PictureBox();
            this.prgrssBrStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.appInfo = new System.Windows.Forms.Label();
            this.dtGrdNp = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCountDown = new System.Windows.Forms.Label();
            this.tmrCountDown = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btbReFresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxDaysRetentionFiles = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgDomar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBxDaysRetentionFiles)).BeginInit();
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
            // dtGrdNp
            // 
            this.dtGrdNp.AllowUserToAddRows = false;
            this.dtGrdNp.AllowUserToDeleteRows = false;
            this.dtGrdNp.AllowUserToOrderColumns = true;
            this.dtGrdNp.AllowUserToResizeRows = false;
            this.dtGrdNp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGrdNp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtGrdNp.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtGrdNp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdNp.Location = new System.Drawing.Point(31, 179);
            this.dtGrdNp.Name = "dtGrdNp";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdNp.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGrdNp.RowHeadersVisible = false;
            this.dtGrdNp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdNp.Size = new System.Drawing.Size(740, 350);
            this.dtGrdNp.TabIndex = 6;
            this.dtGrdNp.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dtGrd_DataError);
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
            this.tmrCountDown.Tick += new System.EventHandler(this.tmrCountDown_Tick);
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
            this.btbReFresh.Click += new System.EventHandler(this.btbReFresh_Click);
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
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // CMainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxDaysRetentionFiles);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btbReFresh);
            this.Controls.Add(this.lblCountDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtGrdNp);
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
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdNp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBxDaysRetentionFiles)).EndInit();
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
        private System.Windows.Forms.DataGridView dtGrdNp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCountDown;
        private System.Windows.Forms.Timer tmrCountDown;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btbReFresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtBxDaysRetentionFiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOpenFolder;
    }

}
