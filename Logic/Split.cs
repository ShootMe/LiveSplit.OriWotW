namespace LiveSplit.OriWotW {
    public enum SplitType {
        ManualSplit,
        FileSelect,
        Skill,
        Ability,
        Health,
        Energy,
        AbilityCell,
        Area,
        Event
    }
    public class Split {
        public string Name { get; set; }
        public SplitType Type { get; set; }
        public object Value { get; set; }

        public override string ToString() {
            string value = Value == null ? string.Empty : Value.ToString();
            return $"{Type.ToString()}|{value}";
        }
    }
}