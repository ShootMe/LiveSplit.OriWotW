using System.Runtime.InteropServices;
namespace LiveSplit.OriWotW {
    public enum UberStateType {
        BooleanUberState,
        ByteUberState,
        IntUberState,
        SerializedBooleanUberState,
        SerializedFloatUberState,
        SerializedIntUberState,
        SerializedByteUberState,
        CountUberState,
        SavePedestalUberState,
        ConditionUberState,
        PlayerUberStateDescriptor
    }
    public static class UberStateDefaults {
        public static UberState cleanseWellspringQuestUberState = new UberState() { Name = "cleanseWellspringQuestUberState", ID = 34641, GroupName = "kwolokGroupDescriptor", GroupID = 937, Type = UberStateType.SerializedIntUberState };
        public static UberState finishedWatermillEscape = new UberState() { Name = "finishedWatermillEscape", ID = 12379, GroupName = "waterMillStateGroupDescriptor", GroupID = 37858, Type = UberStateType.SerializedBooleanUberState };
        public static UberState nightCrawlerDefeated = new UberState() { Name = "nightCrawlerDefeated", ID = 40322, GroupName = "swampStateGroup", GroupID = 21786, Type = UberStateType.SerializedBooleanUberState };
        public static UberState findToadQuestUberState = new UberState() { Name = "findToadQuestUberState", ID = 48794, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedIntUberState };
        public static UberState kwolokNpcState = new UberState() { Name = "kwolokNpcState", ID = 10071, GroupName = "kwolokGroupDescriptor", GroupID = 937, Type = UberStateType.SerializedIntUberState };
        public static UberState petrifiedOwlBossState = new UberState() { Name = "petrifiedOwlBossState", ID = 47278, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedByteUberState };
        public static UberState kwolokDropRace = new UberState() { Name = "kwolokDropRace", ID = 25545, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState wellspringRace = new UberState() { Name = "wellspringRace", ID = 11512, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState silentWoodlandRace = new UberState() { Name = "silentWoodlandRace", ID = 22703, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState baursReachWindTunnelRace = new UberState() { Name = "baursReachWindTunnelRace", ID = 23661, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState mouldwoodDepthsRace = new UberState() { Name = "mouldwoodDepthsRace", ID = 28552, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState desertRace = new UberState() { Name = "desertRace", ID = 30767, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState inkwaterMarshRace = new UberState() { Name = "inkwaterMarshRace", ID = 45951, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState lumaPoolsRace = new UberState() { Name = "lumaPoolsRace", ID = 54686, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState winterForestWispQuestUberState = new UberState() { GroupID = 14019, GroupName = "questUberStateGroup", ID = 8973, Name = "winterForestWispQuestUberState", Type = UberStateType.SerializedBooleanUberState };
        public static UberState mouldwoodDepthsWispQuestUberState = new UberState() { Name = "mouldwoodDepthsWispQuestUberState", ID = 45931, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedIntUberState };
        public static UberState lagoonWispQuestUberState = new UberState() { Name = "lagoonWispQuestUberState", ID = 35087, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedIntUberState };
        public static UberState desertWispQuestUberState = new UberState() { Name = "desertWispQuestUberState", ID = 35399, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedIntUberState };
        public static UberState elevatorCompleteState = new UberState() { Name = "elevatorCompleteState", ID = 23584, GroupName = "corruptedPeakGroup", GroupID = 36153, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineAClear = new UberState() { Name = "vineAClear", ID = 42976, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineBClear = new UberState() { Name = "vineBClear", ID = 54940, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineCClear = new UberState() { Name = "vineCClear", ID = 24290, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineDClear = new UberState() { Name = "vineDClear", ID = 28478, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineEClear = new UberState() { Name = "vineEClear", ID = 3588, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineFClear = new UberState() { Name = "vineFClear", ID = 65277, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineGClear = new UberState() { Name = "vineGClear", ID = 41488, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineHClear = new UberState() { Name = "vineHClear", ID = 60752, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
    }
    public class UberState {
        public UberStateType Type;
        public int ID;
        public string Name;
        public int GroupID;
        public string GroupName;
        public UberValue Value;

        public UberState Clone() {
            return new UberState() { Type = Type, ID = ID, Name = Name, GroupID = GroupID, GroupName = GroupName, Value = Value };
        }
        public bool IsObjectType {
            get {
                return Type == UberStateType.SavePedestalUberState || Type == UberStateType.PlayerUberStateDescriptor;
            }
        }
        public bool IsBoolType {
            get {
                return Type == UberStateType.SerializedBooleanUberState;
            }
        }
        public bool IsIntType {
            get {
                return Type == UberStateType.SerializedIntUberState;
            }
        }
        public bool IsFloatType {
            get {
                return Type == UberStateType.SerializedFloatUberState;
            }
        }
        public bool IsByteType {
            get {
                return Type == UberStateType.SerializedByteUberState;
            }
        }
        public override string ToString() {
            switch (Type) {
                case UberStateType.SavePedestalUberState:
                case UberStateType.SerializedBooleanUberState:
                    return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value.Bool}";
                case UberStateType.SerializedByteUberState:
                    return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value.Byte}";
                case UberStateType.SerializedIntUberState:
                    return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value.Int}";
                case UberStateType.SerializedFloatUberState:
                    return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value.Float}";
            }
            return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value}";
        }
    }
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
    public struct UberValue {
        [FieldOffset(0)]
        public float Float;
        [FieldOffset(0)]
        public int Int;
        [FieldOffset(0)]
        public byte Byte;
        [FieldOffset(0)]
        public bool Bool;

        public override string ToString() {
            return $"{Int}|{Float}";
        }
    }
}