using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
namespace LiveSplit.OriWotW {
    public partial class UserSettings : UserControl {
        public SplitterSettings Settings { get; set; }
        private LiveSplitState State;
        private LogManager Log;

        public UserSettings(LiveSplitState state, LogManager log) {
            InitializeComponent();
            Settings = new SplitterSettings();
            State = state;
            Log = log;
            Dock = DockStyle.Fill;
            gridSplits.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Insert Above", global::LiveSplit.OriWotW.Properties.Resources.upAdd, InsertAbove, Keys.Shift | Keys.Alt | Keys.Up));
            gridSplits.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Insert Below", global::LiveSplit.OriWotW.Properties.Resources.downAdd, InsertBelow, Keys.Shift | Keys.Alt | Keys.Down));
            gridSplits.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Move Up", global::LiveSplit.OriWotW.Properties.Resources.up, MoveUp, Keys.Alt | Keys.Up));
            gridSplits.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Move Down", global::LiveSplit.OriWotW.Properties.Resources.down, MoveDown, Keys.Alt | Keys.Down));
            gridSplits.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Remove Segment", global::LiveSplit.OriWotW.Properties.Resources.remove, RemoveSegment, Keys.Shift | Keys.Alt | Keys.Delete));
            gridSplits.ContextMenuStrip.Items.RemoveAt(0);
            gridSplits.ContextMenuStrip.Opening += ContextMenu_Opening;
        }

        private void UpdateSettings() {
            XmlDocument document = new XmlDocument();
            XmlElement autoSplitterSettings = document.CreateElement("AutoSplitterSettings");
            autoSplitterSettings.InnerXml = UpdateSettings(document).InnerXml;
            autoSplitterSettings.Attributes.Append(SettingsHelper.ToAttribute(document, "gameName", State.Run.GameName));
            State.Run.AutoSplitterSettings = autoSplitterSettings;
        }
        public void ControlChanged(object sender, EventArgs e) {
            UpdateSplits();
        }
        public void UpdateSplits() {

        }
        public XmlNode UpdateSettings(XmlDocument document) {
            XmlElement xmlSettings = document.CreateElement("Settings");

            //XmlElement xmlSetting = document.CreateElement("SplitOnEnterPickup");
            //xmlSetting.InnerText = true.ToString();
            //xmlSettings.AppendChild(xmlSetting);

            XmlElement xmlSplits = document.CreateElement("Splits");
            xmlSettings.AppendChild(xmlSplits);

            foreach (Split split in Settings.Autosplits) {
                XmlElement xmlSplit = document.CreateElement("Split");
                xmlSplit.InnerText = split.ToString();

                xmlSplits.AppendChild(xmlSplit);
            }

            return xmlSettings;
        }
        public void InitializeSettings(XmlNode node) {
            Settings.Autosplits.Clear();

            XmlNodeList splitNodes = node.SelectNodes(".//Splits/Split");
            foreach (XmlNode splitNode in splitNodes) {
                string[] splitValues = splitNode.InnerText.Split('|');
                if (splitValues.Length == 2) {
                    SplitType type = SplitType.ManualSplit;
                    if (Enum.TryParse<SplitType>(splitValues[0], out type)) {
                        Settings.Autosplits.Add(new Split() { Type = type, Value = splitValues[1] });
                    }
                }
            }

            if (Settings.Autosplits.Count == 0) {
                Settings.Autosplits.Add(new Split() { Name = "Auto Start", Type = SplitType.FileSelect });
            }

            gridSplits.DataSource = Settings.Autosplits;
            FixSplits();
        }
        private void gridSplits_DataSourceChanged(object sender, EventArgs e) {
            int columnIndex = 0;
            gridSplits.Columns["Type"].Visible = false;
            gridSplits.Columns.Add(new DataGridViewCombo() { DataPropertyName = "Type", Name = "TypeCombo", DisplayMember = "Item2", ValueMember = "Item1", ColumnDefinitions = "Item2,200,Type", DataSource = GetEnumList<SplitType>() });
            gridSplits.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            int size = gridSplits.Columns["Name"].Width + 20;
            if (size > 150) {
                size = 150;
            }
            gridSplits.Setup("Name", columnIndex++, size, "Segment Name", DataGridViewContentAlignment.MiddleLeft);
            gridSplits.Columns["Name"].DefaultCellStyle.BackColor = Color.FromArgb(200, 200, 200);
            gridSplits.Columns["Name"].ReadOnly = true;
            gridSplits.Setup("TypeCombo", columnIndex++, 120, "Type", DataGridViewContentAlignment.MiddleLeft);
            gridSplits.Setup("Value", columnIndex++, 0, "Value", DataGridViewContentAlignment.MiddleRight);
        }
        private void gridSplits_SelectionChanged(object sender, EventArgs e) {
            ContextMenu_ShowHide();
        }
        private void ContextMenu_Opening(object sender, CancelEventArgs e) {
            Point clientPoint = gridSplits.PointToClient(MousePosition);
            DataGridView.HitTestInfo info = gridSplits.HitTest(clientPoint.X, clientPoint.Y);
            if (info.Type == DataGridViewHitTestType.Cell) {
                gridSplits.Rows[info.RowIndex].Cells[info.ColumnIndex].Selected = true;
            }
        }
        private void ContextMenu_ShowHide() {
            if (gridSplits.SelectedCells.Count == 0) {
                gridSplits.ContextMenuStrip.Items[0].Enabled = false;
                gridSplits.ContextMenuStrip.Items[1].Enabled = false;
                gridSplits.ContextMenuStrip.Items[2].Enabled = false;
                gridSplits.ContextMenuStrip.Items[3].Enabled = false;
                gridSplits.ContextMenuStrip.Items[4].Enabled = false;
            } else {
                DataGridViewCell cell = gridSplits.SelectedCells[0];
                gridSplits.ContextMenuStrip.Items[0].Enabled = cell.RowIndex > 0;
                gridSplits.ContextMenuStrip.Items[1].Enabled = true;
                gridSplits.ContextMenuStrip.Items[2].Enabled = cell.RowIndex > 1;
                gridSplits.ContextMenuStrip.Items[3].Enabled = cell.RowIndex > 0 && cell.RowIndex + 1 < gridSplits.RowCount;
                gridSplits.ContextMenuStrip.Items[4].Enabled = cell.RowIndex > 0;
            }
        }
        private void Settings_Load(object sender, EventArgs e) {
            Form form = FindForm();
            form.Text = "Will of the Wisps Autosplitter v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

            gridSplits.DataSource = Settings.Autosplits;
            FixSplits();
        }
        private void InsertAbove(object sender, EventArgs e) {
            DataGridViewCell cell = gridSplits.SelectedCells[0];
            Settings.Autosplits.Insert(cell.RowIndex, new Split() { Name = "New Split", Type = SplitType.ManualSplit });
            gridSplits.Rows[cell.RowIndex - 1].Cells["Name"].Selected = true;
            FixSplits();
        }
        private void InsertBelow(object sender, EventArgs e) {
            DataGridViewCell cell = gridSplits.SelectedCells[0];
            Settings.Autosplits.Insert(cell.RowIndex + 1, new Split() { Name = "New Split", Type = SplitType.ManualSplit });
            gridSplits.Rows[cell.RowIndex + 1].Cells["Name"].Selected = true;
            FixSplits();
        }
        private void MoveUp(object sender, EventArgs e) {
            DataGridViewCell cell = gridSplits.SelectedCells[0];
            SwitchSegments(cell.RowIndex - 1);
            gridSplits.Rows[cell.RowIndex - 1].Cells["Name"].Selected = true;
            FixSplits();
        }
        private void MoveDown(object sender, EventArgs e) {
            DataGridViewCell cell = gridSplits.SelectedCells[0];
            SwitchSegments(cell.RowIndex);
            gridSplits.Rows[cell.RowIndex + 1].Cells["Name"].Selected = true;
            FixSplits();
        }
        private void RemoveSegment(object sender, EventArgs e) {
            DataGridViewCell cell = gridSplits.SelectedCells[0];
            int index = cell.RowIndex;
            Settings.Autosplits.RemoveAt(index);
            if (index < Settings.Autosplits.Count) {
                gridSplits.Rows[index].Cells["Name"].Selected = true;
            } else {
                gridSplits.Rows[index - 1].Cells["Name"].Selected = true;
            }
            FixSplits();
        }
        private void SwitchSegments(int segIndex) {
            Split temp = Settings.Autosplits[segIndex];
            Settings.Autosplits[segIndex] = Settings.Autosplits[segIndex + 1];
            Settings.Autosplits[segIndex + 1] = temp;
        }
        private void FixSplits() {
            int index = 1;
            Settings.Autosplits[0].Name = "Auto Start";
            Color normalColor = Color.FromArgb(200, 200, 200);
            Color missingColor = Color.FromArgb(255, 100, 100);
            foreach (ISegment segment in State.Run) {
                if (index < Settings.Autosplits.Count) {
                    DataGridViewCell cell = gridSplits.Rows[index].Cells["Name"];
                    if (cell.Style.BackColor != normalColor) {
                        cell.Style.BackColor = normalColor;
                    }
                    Split split = Settings.Autosplits[index++];
                    if (split.Name != segment.Name) {
                        split.Name = segment.Name;
                    }
                } else {
                    index++;
                    Settings.Autosplits.Add(new Split() { Name = segment.Name, Type = SplitType.ManualSplit });
                }
            }

            while (index < Settings.Autosplits.Count) {
                DataGridViewCell cell = gridSplits.Rows[index].Cells["Name"];
                if (cell.Style.BackColor != missingColor) {
                    cell.Style.BackColor = missingColor;
                }
                Split split = Settings.Autosplits[index++];
                string needsAdded = "-- Needs Added --";
                if (split.Name != needsAdded) {
                    split.Name = needsAdded;
                }
            }

            gridSplits.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            int size = gridSplits.Columns["Name"].Width + 20;
            if (size > 150) {
                size = 150;
            }
            gridSplits.Setup("Name", 0, size, "Segment Name", DataGridViewContentAlignment.MiddleLeft);
        }
        private List<Tuple<string, string>> GetEnumList<T>() where T : struct {
            List<Tuple<string, string>> returnValue = new List<Tuple<string, string>>();
            foreach (T value in Enum.GetValues(typeof(T))) {
                MemberInfo info = typeof(T).GetMember(value.ToString())[0];
                DescriptionAttribute[] descriptions = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptions == null || descriptions.Length == 0) {
                    returnValue.Add(new Tuple<string, string>(value.ToString(), value.ToString()));
                } else {
                    returnValue.Add(new Tuple<string, string>(value.ToString(), descriptions[0].Description));
                }
            }
            return returnValue;
        }
        private void btnLog_Click(object sender, EventArgs e) {
            DataTable dt = new DataTable();
            dt.Columns.Add("Event", typeof(string));
            for (int i = 0; i < Log.LogEntries.Count; i++) {
                ILogEntry logEntry = Log.LogEntries[i];
                dt.Rows.Add(logEntry.ToString());
            }
            using (LogViewer logViewer = new LogViewer() { DataSource = dt }) {
                logViewer.ShowDialog(this);
            }
        }
    }
}