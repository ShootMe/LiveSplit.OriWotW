using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum SplitType {
        [Description("Manual Split")]
        ManualSplit,
        [Description("Game Start")]
        GameStart,
        Ability,
        Shard,
        Keystone,
        [Description("Health Cell")]
        HealthCell,
        [Description("Energy Cell")]
        EnergyCell,
        [Description("Area (Enter)")]
        AreaEnter,
        [Description("Area (Leave)")]
        AreaLeave
    }
    public class Split {
        public string Name { get; set; }
        public SplitType Type { get; set; }
        public string Value { get; set; }

        public override string ToString() {
            return $"{Type}|{Value}";
        }
    }
}