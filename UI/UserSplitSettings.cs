using System;
using System.ComponentModel;
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
        public void UpdateControls(bool updateType = false, bool updateValue = true) {
            if (updateType) {
                isLoading = true;
                cboType.DataSource = Utility.GetSortedEnumList<SplitType>();
                cboType.SelectedIndex = -1;
                cboType.SelectedIndex = -1;
                isLoading = false;
                cboType.SelectedValue = UserSplit.Type;
            }

            isLoading = true;
            if (updateValue) {
                switch (UserSplit.Type) {
                    case SplitType.AreaEnter:
                    case SplitType.AreaLeave:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitArea>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitArea>(UserSplit.Value);
                        break;
                    case SplitType.Ability:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitAbility>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitAbility>(UserSplit.Value);
                        break;
                    case SplitType.Boss:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitBoss>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitBoss>(UserSplit.Value);
                        break;
                    case SplitType.Map:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitMap>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitMap>(UserSplit.Value);
                        break;
                    case SplitType.RaceState:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitRace>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitRace>(UserSplit.Value);
                        break;
                    case SplitType.Seed:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitSeed>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitSeed>(UserSplit.Value);
                        break;
                    case SplitType.Shard:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitShard>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitShard>(UserSplit.Value);
                        break;
                    case SplitType.SpiritTrial:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitSpiritTrial>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitSpiritTrial>(UserSplit.Value);
                        break;
                    case SplitType.Teleporter:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitTeleporter>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitTeleporter>(UserSplit.Value);
                        break;
                    case SplitType.Wisp:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitWisp>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitWisp>(UserSplit.Value);
                        break;
                    case SplitType.WorldEvent:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitWorldEvent>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitWorldEvent>(UserSplit.Value);
                        break;
                    case SplitType.KeystoneDoor:
                        cboValue.DataSource = Utility.GetSortedEnumList<SplitKeystoneDoor>();
                        cboValue.SelectedValue = Utility.GetEnumValue<SplitKeystoneDoor>(UserSplit.Value);
                        break;
                    default:
                        txtValue.Text = UserSplit.Value;
                        break;
                }
            }
            lblSegment.Text = UserSplit.Name;
            isLoading = false;
        }
        private void cboType_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboType.SelectedValue == null || isLoading) { return; }

            SplitType nextControlType = (SplitType)cboType.SelectedValue;
            if (nextControlType == SplitType.ManualSplit || nextControlType == SplitType.GameStart || nextControlType == SplitType.GameEndCrawl || nextControlType == SplitType.FinalCutsceneStarted) {
                txtValue.Visible = false;
                cboValue.Visible = false;
                UserSplit.Value = string.Empty;
            } else if (nextControlType == SplitType.HealthCell
                || nextControlType == SplitType.EnergyCell
                || nextControlType == SplitType.Keystone
                || nextControlType == SplitType.Ore
                || nextControlType == SplitType.CreepHeart) {

                if (nextControlType != UserSplit.Type) {
                    UserSplit.Value = "1";
                }
                txtValue.Width = 54;
                txtValue.Visible = true;
                cboValue.Visible = false;
            } else if (nextControlType == SplitType.Hitbox) {
                if (nextControlType != UserSplit.Type) {
                    UserSplit.Value = "X, Y, Width, Height";
                }
                txtValue.Width = 202;
                txtValue.Visible = true;
                cboValue.Visible = false;
            } else if (nextControlType == SplitType.UberState) {
                if (nextControlType != UserSplit.Type) {
                    UserSplit.Value = "34543|11226";
                }
                txtValue.Width = 202;
                txtValue.Visible = true;
                cboValue.Visible = false;
            } else if (nextControlType == SplitType.UberStateValue) {
                if (nextControlType != UserSplit.Type) {
                    UserSplit.Value = "UberGroup, UberId, Comparison, Value | 8246,62310,>,500.0";
                }
                txtValue.Width = 202;
                txtValue.Visible = true;
                cboValue.Visible = false;
            } else {
                if (nextControlType != UserSplit.Type) {
                    switch (nextControlType) {
                        case SplitType.AreaEnter:
                        case SplitType.AreaLeave: DefaultValue = SplitArea.InkwaterMarsh; break;
                        case SplitType.Ability: DefaultValue = SplitAbility.DoubleJump; break;
                        case SplitType.Boss: DefaultValue = SplitBoss.HowlEnd; break;
                        case SplitType.KeystoneDoor: DefaultValue = SplitKeystoneDoor.OpenedKeystoneDoor; break;
                        case SplitType.Map: DefaultValue = SplitMap.InkwaterMarsh; break;
                        case SplitType.RaceState: DefaultValue = SplitRace.RaceHasStarted; break;
                        case SplitType.Seed: DefaultValue = SplitSeed.Wellspring; break;
                        case SplitType.Shard: DefaultValue = SplitShard.Reckless; break;
                        case SplitType.SpiritTrial: DefaultValue = SplitSpiritTrial.KwoloksHollowActivate; break;
                        case SplitType.Teleporter: DefaultValue = SplitTeleporter.KwoloksHollowActivated; break;
                        case SplitType.Wisp: DefaultValue = SplitWisp.VoiceOfTheForest; break;
                        case SplitType.WorldEvent: DefaultValue = SplitWorldEvent.WaterPurified; break;
                    }
                    UserSplit.Value = DefaultValue.ToString();
                }
                txtValue.Visible = false;
                cboValue.Visible = true;
            }
            UserSplit.Type = nextControlType;

            UpdateControls();
        }
        private void cboType_Validating(object sender, CancelEventArgs e) {
            if (cboType.SelectedIndex < 0 && !isLoading) {
                cboType.SelectedValue = SplitType.ManualSplit;
            }
        }
        private void cboValue_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboValue.Visible && cboValue.SelectedItem != null && !isLoading) {
                UserSplit.Value = cboValue.SelectedValue.ToString();
            }
        }
        private void cboValue_Validating(object sender, CancelEventArgs e) {
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