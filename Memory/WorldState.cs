namespace LiveSplit.OriWotW {
    public enum WorldState {
        WaterPurified = 0x6,
        MistLifted = 0x9,
        DarknessLifted = 0xe,
        KwolokDead = 0xf,
        FindKuQuest = 0x10,
        WatermillQuest,
        KwolokNpc,
        TheElderQuest = 0x14,
        MouldwoodWispQuest,
        LagoonWispQuest,
        DesertWispQuest,
        WinterForestWispQuest,
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