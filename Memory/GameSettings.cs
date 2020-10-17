using System;
using System.Runtime.InteropServices;
namespace LiveSplit.OriWotW {
    public enum ControlScheme: byte {
        Controller = 0,
        KeyboardAndMouse = 1,
        Keyboard = 2,
        Switch = 3
}

    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 1)]
    public struct GameSettings {
        [FieldOffset(0x0)]
        public IntPtr Instance;
        [FieldOffset(0x94)]
        public ControlScheme m_currentControlSchemes;
        [FieldOffset(0x88)]
        public IntPtr m_unlockedCutscenes;
        [FieldOffset(0xA8)]
        public IntPtr m_hudEnabled;
        [FieldOffset(0x58)]
        public IntPtr m_language;
    }
}
