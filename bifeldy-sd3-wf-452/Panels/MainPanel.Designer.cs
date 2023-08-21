
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMainPanel));
            this.chkSemuaKolom = new System.Windows.Forms.CheckBox();
            this.userInfo = new System.Windows.Forms.Label();
            this.imgDomar = new System.Windows.Forms.PictureBox();
            this.prgrssBrStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.appInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgDomar)).BeginInit();
            this.SuspendLayout();
            // 
            // chkSemuaKolom
            // 
            this.chkSemuaKolom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSemuaKolom.AutoSize = true;
            this.chkSemuaKolom.Location = new System.Drawing.Point(576, 66);
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
            this.imgDomar.Location = new System.Drawing.Point(724, 27);
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
            this.prgrssBrStatus.Location = new System.Drawing.Point(575, 52);
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
            this.lblStatus.Location = new System.Drawing.Point(572, 30);
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
            // CMainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
    }

}
