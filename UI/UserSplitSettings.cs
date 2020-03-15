using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
namespace LiveSplit.OriWotW {
    public partial class UserSplitSettings : UserControl {
        public Split UserSplit;
        private object DefaultValue = null;
        private bool isDragging = false;
        private int mX = 0;
        private int mY = 0;
        private bool isLoading = false;
        public UserSplitSettings() {
            InitializeComponent();
        }
        public void UpdateControls(bool updateType = false) {
            if (updateType) {
                isLoading = true;
                cboType.DataSource = Utility.GetEnumList<SplitType>();
                cboType.SelectedIndex = -1;
                cboType.SelectedIndex = -1;
                isLoading = false;
                cboType.SelectedValue = UserSplit.Type;
            }

            isLoading = true;
            switch (UserSplit.Type) {
                case SplitType.AreaEnter:
                case SplitType.AreaLeave:
                    cboValue.DataSource = Utility.GetEnumList<GameWorldAreaID>();
                    cboValue.SelectedValue = Utility.GetEnumValue<GameWorldAreaID>(UserSplit.Value);
                    break;
                //case SplitType.Event:
                //    cboValue.DataSource = GetEnumList<WorldState>();
                //    cboValue.SelectedValue = GetEnumValue<WorldState>(UserSplit.Value);
                //    break;
                case SplitType.Ability:
                    cboValue.DataSource = Utility.GetEnumList<AbilityType>();
                    cboValue.SelectedValue = Utility.GetEnumValue<AbilityType>(UserSplit.Value);
                    break;
                case SplitType.Shard:
                    cboValue.DataSource = Utility.GetEnumList<ShardType>();
                    cboValue.SelectedValue = Utility.GetEnumValue<ShardType>(UserSplit.Value);
                    break;
                default:
                    txtValue.Text = UserSplit.Value;
                    break;
            }
            lblSegment.Text = UserSplit.Name;
            isLoading = false;
        }
        private void cboType_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboType.SelectedValue == null || isLoading) { return; }

            SplitType nextControlType = (SplitType)cboType.SelectedValue;
            if (nextControlType == SplitType.ManualSplit || nextControlType == SplitType.GameStart) {
                txtValue.Visible = false;
                cboValue.Visible = false;
                UserSplit.Value = string.Empty;
            } else if (nextControlType == SplitType.HealthCell || nextControlType == SplitType.EnergyCell) {
                if (nextControlType != UserSplit.Type) {
                    UserSplit.Value = "1";
                }
                txtValue.Visible = true;
                cboValue.Visible = false;
            } else {
                if (nextControlType != UserSplit.Type) {
                    switch (nextControlType) {
                        case SplitType.AreaEnter:
                        case SplitType.AreaLeave: DefaultValue = GameWorldAreaID.InkwaterMarsh; break;
                        //case SplitType.Event: DefaultValue = WorldState.WatermillQuest; break;
                        case SplitType.Ability: DefaultValue = AbilityType.DoubleJump; break;
                        case SplitType.Shard: DefaultValue = ShardType.Magnet; break;
                    }
                    UserSplit.Value = DefaultValue.ToString();
                }
                txtValue.Visible = false;
                cboValue.Visible = true;
            }
            UserSplit.Type = nextControlType;

            UpdateControls();
        }
        private void cboType_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            if (cboType.SelectedIndex < 0 && !isLoading) {
                cboType.SelectedValue = SplitType.ManualSplit;
            }
        }
        private void cboValue_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboValue.Visible && cboValue.SelectedItem != null && !isLoading) {
                UserSplit.Value = cboValue.SelectedValue.ToString();
            }
        }
        private void cboValue_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            if (cboValue.Visible && cboValue.SelectedIndex < 0) {
                cboValue.SelectedValue = DefaultValue;
            }
        }
        private void txtValue_Validating(object sender, CancelEventArgs e) {
            if (txtValue.Visible) {
                UserSplit.Value = txtValue.Text;
            }
        }
        private void picHandle_MouseMove(object sender, MouseEventArgs e) {
            if (!isDragging) {
                if (e.Button == MouseButtons.Left) {
                    int num1 = mX - e.X;
                    int num2 = mY - e.Y;
                    if (((num1 * num1) + (num2 * num2)) > 20) {
                        DoDragDrop(this, DragDropEffects.All);
                        isDragging = true;
                        return;
                    }
                }
            }
        }
        private void picHandle_MouseDown(object sender, MouseEventArgs e) {
            mX = e.X;
            mY = e.Y;
            isDragging = false;
        }
    }
}