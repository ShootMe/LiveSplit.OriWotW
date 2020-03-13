using System.Runtime.InteropServices;
namespace LiveSplit.OriWotW {
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct Vector2 {
        [FieldOffset(0)]
        public float X;
        [FieldOffset(4)]
        public float Y;

        public override string ToString() {
            return $"{X:0.00}, {Y:0.00}";
        }
    }
}
