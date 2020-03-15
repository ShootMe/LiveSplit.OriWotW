using System.Runtime.InteropServices;
namespace LiveSplit.OriWotW {
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 1)]
    public struct Shard {
        [FieldOffset(0)]
        public ShardType Type;
        [FieldOffset(4)]
        public int Level;
        [FieldOffset(8)]
        public byte IsNew;
        [FieldOffset(9)]
        public byte Gained;
        [FieldOffset(10)]
        public byte EquipOnStart;
        [FieldOffset(12)]
        public int Index;

        public override string ToString() {
            return $"{Type} Level: {Level} New: {IsNew} Gained: {Gained}";
        }
    }
}