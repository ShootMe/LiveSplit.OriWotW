namespace LiveSplit.OriWotW {
    partial class UserSettings {
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
            this.btnLog = new System.Windows.Forms.Button();
            this.flowMain = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.chkNoPause = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnLog
            // 
            this.btnLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLog.Location = new System.Drawing.Point(329, 4);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(69, 23);
            this.btnLog.TabIndex = 1;
            this.btnLog.TabStop = false;
            this.btnLog.Text = "Debug Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // flowMain
            // 
            this.flowMain.AllowDrop = true;
            this.flowMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowMain.AutoScroll = true;
            this.flowMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMain.Location = new System.Drawing.Point(0, 33);
            this.flowMain.Name = "flowMain";
            this.flowMain.Size = new System.Drawing.Size(470, 300);
            this.flowMain.TabIndex = 0;
            this.flowMain.WrapContents = false;
            this.flowMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowMain_DragEnter);
            this.flowMain.DragOver += new System.Windows.Forms.DragEventHandler(this.flowMain_DragOver);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(404, 4);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(63, 23);
            this.btnClearLog.TabIndex = 3;
            this.btnClearLog.TabStop = false;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // chkLog
            // 
            this.chkLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLog.AutoSize = true;
            this.chkLog.Location = new System.Drawing.Point(261, 8);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(65, 17);
            this.chkLog.TabIndex = 1;
            this.chkLog.Text = "Log Info";
            this.chkLog.UseVisualStyleBackColor = true;
            // 
            // chkNoPause
            // 
            this.chkNoPause.AutoSize = true;
            this.chkNoPause.Location = new System.Drawing.Point(3, 8);
            this.chkNoPause.Name = "chkNoPause";
            this.chkNoPause.Size = new System.Drawing.Size(73, 17);
            this.chkNoPause.TabIndex = 0;
            this.chkNoPause.Text = "No Pause";
            this.chkNoPause.UseVisualStyleBackColor = true;
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkNoPause);
            this.Controls.Add(this.chkLog);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.flowMain);
            this.Controls.Add(this.btnLog);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserSettings";
            this.Size = new System.Drawing.Size(470, 333);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.FlowLayoutPanel flowMain;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.CheckBox chkLog;
        private System.Windows.Forms.CheckBox chkNoPause;
    }
}
