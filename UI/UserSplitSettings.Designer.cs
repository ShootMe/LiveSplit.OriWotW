namespace LiveSplit.OriWotW {
	partial class UserSplitSettings {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSplitSettings));
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.picHandle = new System.Windows.Forms.PictureBox();
            this.lblSegment = new System.Windows.Forms.Label();
            this.cboValue = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picHandle)).BeginInit();
            this.SuspendLayout();
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(247, 1);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(54, 20);
            this.txtValue.TabIndex = 2;
            this.txtValue.Validating += new System.ComponentModel.CancelEventHandler(this.txtValue_Validating);
            // 
            // cboType
            // 
            this.cboType.DisplayMember = "Item2";
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(145, 1);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(100, 21);
            this.cboType.TabIndex = 1;
            this.cboType.ValueMember = "Item1";
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            this.cboType.Validating += new System.ComponentModel.CancelEventHandler(this.cboType_Validating);
            // 
            // picHandle
            // 
            this.picHandle.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.picHandle.Image = ((System.Drawing.Image)(resources.GetObject("picHandle.Image")));
            this.picHandle.Location = new System.Drawing.Point(124, 2);
            this.picHandle.Name = "picHandle";
            this.picHandle.Size = new System.Drawing.Size(21, 20);
            this.picHandle.TabIndex = 6;
            this.picHandle.TabStop = false;
            this.picHandle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHandle_MouseDown);
            this.picHandle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHandle_MouseMove);
            // 
            // lblSegment
            // 
            this.lblSegment.AutoEllipsis = true;
            this.lblSegment.Location = new System.Drawing.Point(2, 4);
            this.lblSegment.Name = "lblSegment";
            this.lblSegment.Size = new System.Drawing.Size(116, 15);
            this.lblSegment.TabIndex = 0;
            this.lblSegment.Text = "This is a really slong split name";
            this.lblSegment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboValue
            // 
            this.cboValue.DisplayMember = "Item2";
            this.cboValue.FormattingEnabled = true;
            this.cboValue.Location = new System.Drawing.Point(247, 1);
            this.cboValue.Name = "cboValue";
            this.cboValue.Size = new System.Drawing.Size(202, 21);
            this.cboValue.TabIndex = 3;
            this.cboValue.ValueMember = "Item1";
            this.cboValue.SelectedIndexChanged += new System.EventHandler(this.cboValue_SelectedIndexChanged);
            this.cboValue.Validating += new System.ComponentModel.CancelEventHandler(this.cboValue_Validating);
            // 
            // UserSplitSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSegment);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.picHandle);
            this.Controls.Add(this.cboValue);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserSplitSettings";
            this.Size = new System.Drawing.Size(450, 23);
            ((System.ComponentModel.ISupportInitialize)(this.picHandle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.PictureBox picHandle;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.ComboBox cboValue;
        private System.Windows.Forms.Label lblSegment;
    }
}
