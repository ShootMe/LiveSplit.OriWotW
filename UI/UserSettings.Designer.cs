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
            this.SuspendLayout();
            // 
            // btnLog
            // 
            this.btnLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLog.Location = new System.Drawing.Point(392, 4);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(75, 23);
            this.btnLog.TabIndex = 1;
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
            this.flowMain.Size = new System.Drawing.Size(470, 591);
            this.flowMain.TabIndex = 2;
            this.flowMain.WrapContents = false;
            this.flowMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowMain_DragDrop);
            this.flowMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowMain_DragEnter);
            this.flowMain.DragOver += new System.Windows.Forms.DragEventHandler(this.flowMain_DragOver);
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowMain);
            this.Controls.Add(this.btnLog);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserSettings";
            this.Size = new System.Drawing.Size(470, 624);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.FlowLayoutPanel flowMain;
    }
}
