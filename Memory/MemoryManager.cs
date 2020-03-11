using System;
using System.Diagnostics;
namespace LiveSplit.Memory {
    public partial class MemoryManager {
        //MagicalTimeBean.Bastille.BastilleGame::InitializeScenes
        private static ProgramPointer SceneManager = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "8B40048B4804FF15????????FF15????????B9????????E8????????8BF88B1D", 32));
        public Process Program { get; set; }
        public bool IsHooked { get; set; }
        public DateTime LastHooked { get; set; }

        public MemoryManager() {
            LastHooked = DateTime.MinValue;
        }
        public string SceneManagerPointer() {
            return SceneManager.GetPointer(Program).ToString("X");
        }
        public bool IsPaused() {
            //SceneManager.ActionSceneInstance._suspendPlayerInput
            return SceneManager.Read<bool>(Program, 0x4, 0x94);
        }
        public int OrbCount() {
            //SceneManager.ActionSceneInstance.GameState._orbObtainedPositions._size
            return SceneManager.Read<int>(Program, 0x4, 0x84, 0x10, 0xc);
        }
        public double ElapsedTime() {
            //SceneManager.ActionSceneInstance.GameState._totalTime
            return (double)SceneManager.Read<long>(Program, -0xc, 0x80, 0x30) / (double)10000000;
        }
        public bool HookProcess() {
            IsHooked = Program != null && !Program.HasExited;
            if (!IsHooked && DateTime.Now > LastHooked.AddSeconds(1)) {
                LastHooked = DateTime.Now;
                Process[] processes = Process.GetProcessesByName("OriWotW");
                Program = processes != null && processes.Length > 0 ? processes[0] : null;

                if (Program != null && !Program.HasExited) {
                    MemoryReader.Update64Bit(Program);
                    IsHooked = true;
                }
            }

            return IsHooked;
        }
        public void Dispose() {
            if (Program != null) {
                Program.Dispose();
            }
        }
    }
}