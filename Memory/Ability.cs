using System.Runtime.InteropServices;
namespace LiveSplit.OriWotW {
    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct Ability {
        [FieldOffset(0)]
        public AbilityType Type;
        [FieldOffset(1)]
        public byte HasAbility;
        [FieldOffset(4)]
        public int AbilityLevel;

        public override string ToString() {
            return $"{Type} Has: {HasAbility} Level: {AbilityLevel}";
        }
    }
}