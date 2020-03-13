using System.Runtime.InteropServices;
namespace LiveSplit.OriWotW {
    [StructLayout(LayoutKind.Explicit, Size = 6, Pack = 1)]
    public struct InventoryItem {
        [FieldOffset(0)]
        public EquipmentType Type;
        [FieldOffset(4)]
        public byte IsNew;
        [FieldOffset(5)]
        public byte Gained;

        public override string ToString() {
            return $"{Type} New: {IsNew} Gained: {Gained}";
        }
    }
}