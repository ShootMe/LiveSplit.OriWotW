using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace LiveSplit.OriWotW {
    public sealed class Grid : DataGridView {
        private ContextMenuStrip cMenu;
        private IContainer components;
        private SaveFileDialog saveFile;
        private ToolStripMenuItem exportItem;
        private bool IsEditOnEnter, readOnly;
        private bool? allowUpdate, allowNew, allowDelete;

        public Grid() {
            InitializeComponent();
            AllowUserToAddRows = false;
            AllowUserToOrderColumns = true;
            AllowUserToResizeRows = false;
            EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            BackgroundColor = Color.FromArgb(234, 242, 251);
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            BorderStyle = BorderStyle.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RowHeadersWidth = 20;
            ContextMenuStrip = cMenu;
            readOnly = false;
            EnableHeadersVisualStyles = false;
            ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Cyan;
            ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
        }
        public DataGridViewRow CloneWithValues(DataGridViewRow row) {
            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
            for (int i = 0; i < row.Cells.Count; i++) {
                clonedRow.Cells[i].Value = row.Cells[i].Value;
                clonedRow.Cells[i].Tag = row.Cells[i].EditedFormattedValue;
            }
            return clonedRow;
        }
        protected override void OnDataSourceChanged(EventArgs e) {
            Columns.Clear();
            IsEditOnEnter = EditMode == DataGridViewEditMode.EditOnEnter;
            base.OnDataSourceChanged(e);
        }
        protected override void OnPaint(PaintEventArgs e) {
            if (DesignMode) {
                Graphics g = e.Graphics;
                g.Clear(BackgroundColor);
                Pen bp = new Pen(Color.DarkGray, 1);
                bp.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                int i = 0;
                g.DrawLine(bp, 0, 0, 0, Height);
                g.DrawLine(bp, Width - 1, 0, Width - 1, Height);
                int header = (int)ColumnHeadersDefaultCellStyle.Font.Size * 2 + 1;
                g.FillRectangle(new SolidBrush(ColumnHeadersDefaultCellStyle.BackColor), 1, 1, Width - 2, header - 1);
                TextRenderer.DrawText(g, "Header", ColumnHeadersDefaultCellStyle.Font, new Point(10, 2), ColumnHeadersDefaultCellStyle.ForeColor);
                g.FillRectangle(new SolidBrush(DefaultCellStyle.BackColor), 1, header, Width - 2, Height - header - 1);
                g.DrawLine(bp, 0, i, Width, i);
                i += header;
                int row = 1;
                while (i < Height) {
                    g.DrawLine(bp, 0, i, Width, i);
                    TextRenderer.DrawText(g, "Row " + row++, DefaultCellStyle.Font, new Point(10, i + (RowTemplate.Height / 4)), DefaultCellStyle.ForeColor);
                    i += RowTemplate.Height;
                }
                g.DrawLine(bp, 0, Height - 1, Width, Height - 1);
                bp.Dispose();
            } else {
                base.OnPaint(e);
            }
        }
        protected override void OnCellClick(DataGridViewCellEventArgs e) {
            base.OnCellClick(e);
            if (!IsEditOnEnter) { return; }
            if (e.ColumnIndex == -1) {
                EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                EndEdit();
            } else if (EditMode != DataGridViewEditMode.EditOnEnter) {
                EditMode = DataGridViewEditMode.EditOnEnter;
                BeginEdit(false);
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue(false)]
        public bool Disabled {
            get { return readOnly; }
            set {
                readOnly = value;
                if (readOnly) {
                    allowDelete = AllowUserToDeleteRows;
                    allowNew = AllowUserToAddRows;
                    allowUpdate = ReadOnly;
                    AllowUserToAddRows = false;
                    AllowUserToDeleteRows = false;
                    ReadOnly = true;
                } else if (allowNew.HasValue) {
                    AllowUserToAddRows = allowNew.Value;
                    AllowUserToDeleteRows = allowDelete.Value;
                    ReadOnly = allowUpdate.Value;
                }
            }
        }
        protected override void OnLeave(EventArgs e) {
            if (IsEditOnEnter) {
                EditMode = DataGridViewEditMode.EditOnEnter;
            }
            base.OnLeave(e);
        }
        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e) {
            try {
                DataGridViewCell cell = this.CurrentCell;
                cell.Tag = e.Control;
                DataGridViewComboBoxEditingControl cbo = e.Control as DataGridViewComboBoxEditingControl;
                if (cbo != null) {
                    cbo.DropDownStyle = ComboBoxStyle.DropDown;
                    if (cell.Value == DBNull.Value) {
                        cbo.Text = string.Empty;
                        cbo.SelectedIndex = -1;
                        cbo.SelectedIndex = -1;
                    }
                }
            } catch (Exception ex) {
                ControlErrors.HandleException(this, ex, false);
            }
            base.OnEditingControlShowing(e);
        }
        protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e) {
            try {
                DataGridViewCombo cbo = this.Columns[e.ColumnIndex] as DataGridViewCombo;
                if (cbo != null) {
                    DataGridViewComboEditingControl editor = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as DataGridViewComboEditingControl;
                    if (editor != null) {
                        object newValue = editor.GetItemInList(e.FormattedValue.ToString()) ?? string.Empty;
                        if (!editor.Text.ToString().Equals(newValue)) {
                            editor.Text = newValue.ToString();
                        }
                    }
                }
            } catch (Exception ex) {
                ControlErrors.HandleException(this, ex, false);
            }
            this.Columns[e.ColumnIndex].Tag = e.FormattedValue;
            if (!this.IsCurrentCellDirty) { return; }
            base.OnCellValidating(e);
        }
        protected override void OnCellValidated(DataGridViewCellEventArgs e) {
            try {
                DataGridViewCombo cbo = this.Columns[e.ColumnIndex] as DataGridViewCombo;
                object value = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (cbo != null) {
                    DataGridViewComboEditingControl editor = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as DataGridViewComboEditingControl;
                    if (editor != null) {
                        object newValue = editor.GetItemInList(cbo.Tag.ToString()) ?? string.Empty;
                        if (!editor.Text.ToString().Equals(newValue) || (string.IsNullOrEmpty(cbo.Tag.ToString()) && value != DBNull.Value)) {
                            editor.Text = newValue.ToString();
                            if (string.IsNullOrEmpty(newValue.ToString())) {
                                this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                ControlErrors.HandleException(this, ex, false);
            }
            base.OnCellValidated(e);
        }
        protected override void OnRowValidating(DataGridViewCellCancelEventArgs e) {
            if (!this.IsCurrentRowDirty || this.CurrentRow == null || this.CurrentRow.IsNewRow) { return; }
            base.OnRowValidating(e);
        }
        protected override void OnRowEnter(DataGridViewCellEventArgs e) {
            try {
                if (this.Rows[e.RowIndex].Tag == null || ((DataGridViewRow)this.Rows[e.RowIndex].Tag).Cells.Count != this.Rows[e.RowIndex].Cells.Count) {
                    this.Rows[e.RowIndex].Tag = this.CloneWithValues(this.Rows[e.RowIndex]);
                }
            } catch (Exception ex) {
                ControlErrors.HandleException(this, ex, false);
            }
            base.OnRowEnter(e);
        }
        [DefaultValue(typeof(DataGridViewRowHeadersWidthSizeMode), "DisableResizing")]
        public new DataGridViewRowHeadersWidthSizeMode RowHeadersWidthSizeMode {
            get { return base.RowHeadersWidthSizeMode; }
            set { base.RowHeadersWidthSizeMode = value; }
        }
        [DefaultValue(20)]
        public new int RowHeadersWidth {
            get { return base.RowHeadersWidth; }
            set { base.RowHeadersWidth = value; }
        }
        [DefaultValue(DataGridViewAutoSizeColumnsMode.AllCells)]
        public new DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode {
            get { return base.AutoSizeColumnsMode; }
            set { base.AutoSizeColumnsMode = value; }
        }
        [DefaultValue(false)]
        public new bool AllowUserToAddRows {
            get { return base.AllowUserToAddRows; }
            set { base.AllowUserToAddRows = value; }
        }
        [DefaultValue(true)]
        public new bool AllowUserToOrderColumns {
            get { return base.AllowUserToOrderColumns; }
            set { base.AllowUserToOrderColumns = value; }
        }
        [DefaultValue(false)]
        public new bool AllowUserToResizeRows {
            get { return base.AllowUserToResizeRows; }
            set { base.AllowUserToResizeRows = value; }
        }
        [DefaultValue(DataGridViewEditMode.EditOnKeystrokeOrF2)]
        public new DataGridViewEditMode EditMode {
            get { return base.EditMode; }
            set { base.EditMode = value; }
        }
        [DefaultValue(typeof(Color), "234,242,251")]
        public new Color BackgroundColor {
            get { return base.BackgroundColor; }
            set { base.BackgroundColor = value; }
        }
        public override string ToString() {
            return "Grid(" + Name + ") " + Text;
        }
        public bool HasFocus(Control activeControl) {
            while (activeControl != null) {
                if (activeControl == this) {
                    return true;
                }
                activeControl = activeControl.Parent;
            }
            return false;
        }
        private void exportItem_Click(object sender, EventArgs e) {
            try {
                saveFile.Filter = "CSV files|*.csv";
                if (saveFile.ShowDialog() == DialogResult.OK) {
                    Encoding enc = Encoding.GetEncoding("windows-1252");
                    using (FileStream fs = new FileStream(saveFile.FileName, FileMode.Create)) {
                        StringBuilder sb = new StringBuilder();
                        foreach (DataGridViewColumn col in this.Columns) {
                            if (col.ValueType != null && col.Visible) {
                                sb.Append(col.Name).Append(",");
                            }
                        }
                        if (sb.Length > 0) { sb.Length = sb.Length - 1; }
                        sb.AppendLine();
                        byte[] bytes = enc.GetBytes(sb.ToString());
                        fs.Write(bytes, 0, bytes.Length);
                        foreach (DataGridViewRow row in this.Rows) {
                            sb.Length = 0;
                            foreach (DataGridViewColumn col in this.Columns) {
                                if (!col.Visible) { continue; }
                                if (col.ValueType == typeof(string)) {
                                    sb.Append("\"").Append(row.Cells[col.Name].Value.ToString()).Append("\",");
                                } else if (col.ValueType != null) {
                                    sb.Append(row.Cells[col.Name].Value.ToString()).Append(",");
                                }
                            }
                            if (sb.Length > 0) { sb.Length = sb.Length - 1; }
                            sb.AppendLine();
                            bytes = enc.GetBytes(sb.ToString());
                            fs.Write(bytes, 0, bytes.Length);
                        }
                        fs.Flush();
                        fs.Close();
                    }
                }
            } catch (Exception ex) {
                ControlErrors.HandleException(this, ex, false);
            }
        }
        private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            e.ThrowException = false;
        }
        protected override bool ProcessDialogKey(Keys keyData) {
            if (ProcessKeyStroke(keyData)) {
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }
        protected override bool ProcessDataGridViewKey(KeyEventArgs e) {
            if (ProcessKeyStroke(e.KeyData)) {
                return true;
            }

            return base.ProcessDataGridViewKey(e);
        }
        private bool ProcessKeyStroke(Keys keyData) {
            if (keyData == Keys.Tab) {
                int iCol = CurrentCell.ColumnIndex + 1;
                while (iCol < Columns.Count) {
                    DataGridViewColumn col = Columns[iCol];
                    if (!col.ReadOnly && col.Visible) {
                        break;
                    }
                    iCol = iCol + 1;
                }

                if (iCol < Columns.Count) {
                    CurrentCell = Rows[CurrentCell.RowIndex].Cells[iCol];
                } else {
                    for (iCol = 0; iCol <= CurrentCell.ColumnIndex; iCol++) {
                        DataGridViewColumn col = Columns[iCol];
                        if (!col.ReadOnly && col.Visible) {
                            break;
                        }
                    }

                    if (iCol <= CurrentCell.ColumnIndex) {
                        if (CurrentCell.RowIndex + 1 < Rows.Count) {
                            CurrentCell = Rows[CurrentCell.RowIndex + 1].Cells[iCol];
                        } else {
                            CurrentCell = Rows[0].Cells[iCol];
                        }
                    }
                }
                return true;
            }
            return false;
        }
        public void Setup(string column, int index, int width = -1, string header = null, DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleCenter) {
            if (Columns == null || Columns[column] == null) { return; }
            Columns[column].Visible = true;
            Columns[column].DisplayIndex = index;
            Columns[column].SortMode = DataGridViewColumnSortMode.Automatic;
            Columns[column].DefaultCellStyle.Alignment = align;
            if (width > 0) {
                Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Columns[column].Width = width;
            } else if (width == 0) {
                Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            } else if (width < 0) {
                Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            Columns[column].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (header != null) {
                Columns[column].HeaderText = header;
            }
        }
        private void InitializeComponent() {
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Grid));
            this.cMenu = new ContextMenuStrip(this.components);
            this.exportItem = new ToolStripMenuItem();
            this.saveFile = new SaveFileDialog();
            this.cMenu.SuspendLayout();
            ((ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // cMenu
            // 
            this.cMenu.Items.AddRange(new ToolStripItem[] { this.exportItem });
            this.cMenu.Name = "contextMenu";
            this.cMenu.Size = new Size(135, 48);
            // 
            // exportItem
            // 
            this.exportItem.Image = global::LiveSplit.OriWotW.Properties.Resources.save;
            this.exportItem.Name = "exportItem";
            this.exportItem.Size = new Size(134, 22);
            this.exportItem.Text = "&Export to CSV";
            this.exportItem.ShowShortcutKeys = true;
            this.exportItem.ShortcutKeys = Keys.Control | Keys.S;
            this.exportItem.Click += new EventHandler(this.exportItem_Click);
            // 
            // saveFile
            // 
            this.saveFile.Filter = "CSV files|*.csv";
            this.saveFile.Title = "Save Results";
            // 
            // Grid
            // 
            this.DataError += new DataGridViewDataErrorEventHandler(this.Grid_DataError);
            this.cMenu.ResumeLayout(false);
            ((ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
        public static DataTable Convert(IEnumerable array, params string[] columns) {
            object rec = null;
            foreach (object o in array) { rec = o; break; }
            DataTable dt = new DataTable();
            if (rec == null) { return dt; }
            PropertyInfo[] properties = rec.GetType().GetProperties();
            dt.Columns.Add(".", rec.GetType());
            foreach (PropertyInfo pi in properties) {
                Type type = pi.PropertyType;
                if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                    type = pi.PropertyType.GetGenericArguments()[0];
                }
                bool addColumn = true;
                if (columns != null && columns.Length > 0) {
                    bool found = false;
                    for (int i = columns.Length - 1; i >= 0; i--) {
                        if (pi.Name.Equals(columns[i], StringComparison.OrdinalIgnoreCase)) {
                            found = true;
                            break;
                        }
                    }
                    addColumn = found;
                }
                if (addColumn) {
                    dt.Columns.Add(pi.Name, type);
                }
            }
            foreach (object o in array) {
                DataRow dr = dt.NewRow();
                dr["."] = o;
                foreach (PropertyInfo pi in properties) {
                    if (columns == null || columns.Length == 0 || dt.Columns.Contains(pi.Name)) {
                        dr[pi.Name] = pi.GetValue(o, null) ?? DBNull.Value;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
    public class DataGridViewCombo : DataGridViewComboBoxColumn {
        [DefaultValue("")]
        public string ColumnDefinitions { get; set; }

        public DataGridViewCombo() {
            CellTemplate = new DataGridViewComboCell();
        }
        public override object Clone() {
            DataGridViewCombo cbo = (DataGridViewCombo)base.Clone();
            cbo.ColumnDefinitions = this.ColumnDefinitions;
            return cbo;
        }
    }
    public class DataGridViewComboCell : DataGridViewComboBoxCell {
        public override Type EditType {
            get {
                return typeof(DataGridViewComboEditingControl);
            }
        }
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle) {
            try {
                base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
                DataGridViewComboEditingControl ctrl = DataGridView.EditingControl as DataGridViewComboEditingControl;
                DataGridViewCombo combo = (DataGridViewCombo)DataGridView.CurrentCell.OwningColumn;
                ctrl.ColumnDefinitions = combo.ColumnDefinitions;
            } catch { }
        }
    }
    public class DataGridViewComboEditingControl : DataGridViewComboBoxEditingControl {
        [DefaultValue("")]
        public string ColumnDefinitions { get; set; }

        public DataGridViewComboEditingControl()
            : base() {
            this.DropDownStyle = ComboBoxStyle.DropDown;
        }

        public object GetItemInList(string item, bool getObject = false) {
            for (int i = base.Items.Count - 1; i >= 0; i += -1) {
                object ob = base.Items[i];
                if ((!string.IsNullOrEmpty(DisplayMember) && ob is DataRowView && ((DataRowView)ob)[DisplayMember].ToString().Equals(item, StringComparison.OrdinalIgnoreCase)))
                    return getObject ? ob : ((DataRowView)ob)[DisplayMember];
                else if (string.IsNullOrEmpty(DisplayMember) && ob.ToString().Equals(item, StringComparison.OrdinalIgnoreCase))
                    return getObject ? ob : ob.ToString();
                else if (!string.IsNullOrEmpty(DisplayMember)) {
                    PropertyInfo[] pi = ob.GetType().GetProperties();
                    foreach (PropertyInfo p in pi) {
                        if (p.Name.ToLower() == DisplayMember.ToLower()) {
                            object value = p.GetValue(ob, null);
                            if (value != null && value.ToString().Equals(item, StringComparison.OrdinalIgnoreCase)) {
                                return getObject ? ob : value;
                            }
                        }
                    }
                }
            }
            return null;
        }

        const int WM_USER = 0x0400, WM_REFLECT = WM_USER + 0x1C00, WM_COMMAND = 0x0111, CBN_DROPDOWN = 7;
        protected override void WndProc(ref Message msg) {
            if (msg.Msg == 0x0201 || msg.Msg == 0x0203 || (msg.Msg == 335 && (int)msg.WParam == -1)) {
                if (!string.IsNullOrEmpty(ColumnDefinitions)) {
                    Form parent = this.FindForm();
                    parent.Invoke((Action)delegate () {
                        ComboSelection selection = new ComboSelection(this, ColumnDefinitions);
                        selection.Show(parent);
                    });
                    return;
                }
            }
            base.WndProc(ref msg);
        }
    }
    public class ComboSelection : Form {
        private Container components = null;
        private Grid grid;
        private ComboBox combo;
        private string columnDefinitions;
        private StringBuilder typedString;
        private DateTime lastTyped;
        private bool loading, hasActive, showActive;
        private int firstColumn;
        private object selected;

        public ComboSelection(ComboBox combo, string columnDefinitions) {
            InitializeComponent();
            typedString = new StringBuilder();
            lastTyped = DateTime.MinValue;
            this.combo = combo;
            this.columnDefinitions = columnDefinitions;
            loading = true;
            firstColumn = -1;
            selected = combo.SelectedItem;
            object ds = combo.DataSource;
            if (ds is DataTable) {
                DataTable dt = ((DataTable)ds).Clone();
                dt.Columns.Add(".", typeof(DataRowView));
                DataTable old = (DataTable)ds;
                foreach (DataRowView row in old.DefaultView) {
                    DataRow newrow = dt.NewRow();
                    newrow["."] = row;
                    foreach (DataColumn col in old.Columns) {
                        newrow[col.ColumnName] = row[col.ColumnName];
                    }
                    dt.Rows.Add(newrow);
                }
                grid.DataSource = dt;
            } else if (ds is IEnumerable) {
                grid.DataSource = Grid.Convert((IEnumerable)ds);
            } else {
                throw new Exception("DataSource is not a valid type.");
            }

            Form parent = combo.FindForm();
            Point p = combo.PointToScreen(new Point(0, 0));
            Location = new Point(p.X, p.Y + combo.Height);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent() {
            this.grid = new Grid();
            this.SuspendLayout();

            this.grid.BorderStyle = BorderStyle.FixedSingle;
            this.grid.Dock = DockStyle.Fill;
            this.grid.TabIndex = 0;
            this.grid.KeyDown += grid_KeyDown;
            this.grid.Click += grid_Click;
            this.grid.DataSourceChanged += grid_DataSourceChanged;
            this.grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.grid.ReadOnly = true;
            this.grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.Location = new Point(0, 0);
            this.grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.RowHeadersWidth = 20;
            this.grid.RowHeadersVisible = false;
            this.grid.MouseDown += grid_MouseDown;
            this.grid.Sorted += grid_Sorted;

            this.AutoScaleBaseSize = new Size(5, 13);
            this.ClientSize = new Size(292, 88);
            this.ControlBox = false;
            this.Controls.AddRange(new Control[] { grid });
            this.FormBorderStyle = FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ComboSelection";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.KeyDown += this_KeyDown;
            this.Leave += this_Leave;
            this.Deactivate += this_Deactivate;
            this.FormClosed += this_FormClosed;
            this.Shown += this_Shown;
            this.ResumeLayout(false);
        }

        void this_Shown(object sender, EventArgs e) {
            try {
                foreach (DataGridViewRow row in grid.Rows) {
                    object value = grid.Columns.Contains(".") ? row.Cells["."].Value : row.DataBoundItem;
                    if (value == selected) {
                        grid.CurrentCell = row.Cells[firstColumn];
                        row.Selected = true;
                        grid.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    } else if (row.Selected) {
                        row.Selected = false;
                    }
                }
            } catch { }
            loading = false;
        }
        void this_FormClosed(object sender, EventArgs e) {
            Form parent = combo.FindForm();
            parent.Focus();
            combo.Focus();
        }
        private void this_Deactivate(object sender, EventArgs e) {
            this.Close();
        }
        private void this_Leave(object sender, EventArgs e) {
            this.Close();
        }
        private void this_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Escape) {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            } else if (!string.IsNullOrEmpty(combo.DisplayMember) && e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.NumPad9 && (e.KeyCode < Keys.LWin || e.KeyCode > Keys.Sleep)) {
                try {
                    if ((DateTime.Now - lastTyped).TotalSeconds > 1) {
                        typedString.Length = 0;
                    }
                    lastTyped = DateTime.Now;
                    typedString.Append((char)(e.KeyCode > Keys.Sleep ? (int)e.KeyCode - 48 : (int)e.KeyCode));

                    foreach (DataGridViewRow row in grid.Rows) {
                        if (row.Cells[combo.DisplayMember].Value.ToString().IndexOf(typedString.ToString(), StringComparison.OrdinalIgnoreCase) == 0) {
                            row.Selected = true;
                            grid.CurrentCell = row.Cells[combo.DisplayMember];
                            grid.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }
                } catch { }
            }
        }
        private void grid_DataSourceChanged(object sender, EventArgs e) {
            try {
                string[] definitions = columnDefinitions.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                hasActive = false;
                foreach (DataGridViewColumn col in grid.Columns) {
                    col.Visible = false;
                    if (col.Name.Equals("Active", StringComparison.OrdinalIgnoreCase) && col.ValueType == typeof(bool)) {
                        hasActive = true;
                    }
                }
                int totalWidth = 0;
                showActive = false;
                for (int i = definitions.Length - 1; i >= 0; i--) {
                    string[] info = definitions[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (info[0].Equals("Active", StringComparison.OrdinalIgnoreCase)) {
                        showActive = true;
                    }
                    if (grid.Columns.Count <= 0 || !grid.Columns.Contains(info[0])) {
                        continue;
                    }
                    grid.Columns[info[0]].Visible = true;
                    grid.Columns[info[0]].DisplayIndex = i;
                    grid.Columns[info[0]].SortMode = DataGridViewColumnSortMode.Automatic;
                    if (firstColumn < 0) { firstColumn = grid.Columns[info[0]].Index; }
                    int width = int.Parse(info[1]);
                    grid.Columns[info[0]].Width = width;
                    grid.Columns[info[0]].HeaderText = info[2];
                    totalWidth += width;
                }

                if (hasActive && !showActive) {
                    ((DataTable)grid.DataSource).DefaultView.RowFilter = "Active = 1";
                }
                int count = grid.Rows.Count;
                totalWidth += 3 + (count > 15 ? SystemInformation.VerticalScrollBarWidth : 0);
                count = count > 15 ? 15 : count;
                int height = count * (count == 0 ? 18 : grid.Rows[0].Height) + 2;
                if (totalWidth < combo.Width) {
                    string[] info = definitions[definitions.Length - 1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    grid.Columns[info[0]].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    totalWidth = combo.Width;
                }
                ClientSize = new Size(totalWidth, grid.ColumnHeadersHeight + height);
            } catch { }
        }
        private void SetSelection() {
            if (this.DialogResult == DialogResult.Cancel || grid.SelectedRows.Count == 0 || combo == null || loading) { return; }
            object value = grid.Columns.Contains(".") ? grid.SelectedRows[0].Cells["."].Value : grid.SelectedRows[0].DataBoundItem;
            foreach (object row in combo.Items) {
                if (row == value) {
                    combo.SelectedItem = row;
                    break;
                }
            }
        }
        void grid_Sorted(object sender, EventArgs e) {
            foreach (DataGridViewRow row in grid.Rows) {
                object value = grid.Columns.Contains(".") ? row.Cells["."].Value : row.DataBoundItem;
                if (value == selected) {
                    grid.CurrentCell = row.Cells[firstColumn];
                    row.Selected = true;
                    grid.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }
        void grid_MouseDown(object sender, MouseEventArgs e) {
            foreach (DataGridViewRow row in grid.Rows) {
                if (row.Selected) {
                    selected = grid.Columns.Contains(".") ? row.Cells["."].Value : row.DataBoundItem;
                    break;
                }
            }
        }
        private void grid_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab) {
                SetSelection();
                this.Close();
            }
        }
        private void grid_Click(object sender, EventArgs e) {
            Point p = grid.PointToClient(MousePosition);
            DataGridView.HitTestInfo test = grid.HitTest(p.X, p.Y);
            if (grid.SelectedRows.Count > 0 && test.RowIndex >= 0 && grid.Rows[test.RowIndex].Selected) {
                SetSelection();
                this.Close();
            }
        }
    }
    public static class ControlErrors {
        public static event Action<object, Exception> Error;
        internal static Exception HandleException(object sender, Exception ex, bool rethrow = true) {
            if (!ex.Data.Contains("Handled"))
                ex.Data.Add("Handled", false);
            if (!ex.Data.Contains("Thread"))
                ex.Data.Add("Thread", Thread.CurrentThread.Name);
            if (string.IsNullOrEmpty(ex.Message))
                ex.Data["Handled"] = true;
            if (rethrow)
                throw ex;

            if (!(bool)ex.Data["Handled"]) {
                if (Error != null) {
                    Error(sender, ex);
                } else {
                    MessageBox.Show(ex.Message);
                }
                ex.Data["Handled"] = true;
            }
            return ex;
        }
    }
}