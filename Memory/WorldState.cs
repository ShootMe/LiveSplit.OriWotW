using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum WorldState {
        [Description("Water Purified")]
        WaterPurified = 0x6,
        [Description("Mist Lifted")]
        MistLifted = 0x9,
        [Description("Darkness Lifted")]
        DarknessLifted = 0xe,
        //[Description("Kwolok Dead")]
        //KwolokDead,
        [Description("Find Ku")]
        FindKuQuest = 0x10,
        [Description("Watermill")]
        WatermillQuest,
        [Description("Kwolok")]
        KwolokNpc,
        [Description("Elder")]
        TheElderQuest = 0x14,
        [Description("Mouldwood Wisp")]
        MouldwoodWispQuest,
        [Description("Lagoon Wisp")]
        LagoonWispQuest,
        [Description("Desert Wisp")]
        DesertWispQuest,
        [Description("Winter Forest Wisp")]
        WinterForestWispQuest,
        [Description("Baur")]
        BaurNpc
    }
    public class WorldStateValue {
        public WorldState State;
        public int Value;
        public string Description;

        public override string ToString() {
            return $"{State} = {Value} ({Description})";
        }
    }
}