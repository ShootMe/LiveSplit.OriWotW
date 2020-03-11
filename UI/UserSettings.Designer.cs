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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridSplits = new LiveSplit.OriWotW.Grid();
            this.btnLog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplits)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSplits
            // 
            this.gridSplits.AllowUserToDeleteRows = false;
            this.gridSplits.AllowUserToOrderColumns = false;
            this.gridSplits.AllowUserToResizeColumns = false;
            this.gridSplits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSplits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.gridSplits.BackgroundColor = System.Drawing.Color.Gray;
            this.gridSplits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSplits.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSplits.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridSplits.ColumnHeadersHeight = 20;
            this.gridSplits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridSplits.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridSplits.EnableHeadersVisualStyles = false;
            this.gridSplits.Location = new System.Drawing.Point(0, 33);
            this.gridSplits.MultiSelect = false;
            this.gridSplits.Name = "gridSplits";
            this.gridSplits.RowHeadersVisible = false;
            this.gridSplits.Size = new System.Drawing.Size(452, 591);
            this.gridSplits.TabIndex = 0;
            this.gridSplits.DataSourceChanged += new System.EventHandler(this.gridSplits_DataSourceChanged);
            this.gridSplits.SelectionChanged += new System.EventHandler(this.gridSplits_SelectionChanged);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(374, 4);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(75, 23);
            this.btnLog.TabIndex = 1;
            this.btnLog.Text = "Debug Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.gridSplits);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserSettings";
            this.Size = new System.Drawing.Size(452, 624);
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSplits)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Grid gridSplits;
        private System.Windows.Forms.Button btnLog;
    }
}
