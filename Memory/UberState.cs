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
        public override string ToString() {
            switch (Type) {
                case UberStateType.BooleanUberState:
                case UberStateType.SavePedestalUberState:
                case UberStateType.SerializedBooleanUberState:
                    return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value.Bool}";
                case UberStateType.ByteUberState:
                case UberStateType.SerializedByteUberState:
                    return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value.Byte}";
                case UberStateType.ConditionUberState:
                case UberStateType.CountUberState:
                case UberStateType.IntUberState:
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