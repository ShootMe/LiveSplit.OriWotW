namespace LiveSplit.OriWotW {
    public class LogicManager {
        public bool ShouldSplit { get; private set; }
        public bool ShouldReset { get; private set; }
        public int CurrentSplit { get; private set; }
        public bool Running { get; private set; }
        public bool Paused { get; private set; }
        public float GameTime { get; private set; }
        public MemoryManager Memory { get; private set; }

        public LogicManager() {
            Reset();
            Memory = new MemoryManager();
        }

        public void Reset() {
            Paused = false;
            Running = false;
            CurrentSplit = 0;
            ShouldSplit = false;
            ShouldReset = false;
        }
        public void Undo() {
            CurrentSplit--;
        }
        public void Skip() {
            CurrentSplit++;
        }
        public bool IsHooked() {
            bool hooked = Memory.HookProcess();
            Paused = !hooked;
            ShouldSplit = false;
            ShouldReset = false;
            GameTime = -1;
            return hooked;
        }
        public void Update(SplitterSettings settings) {
            if (!Running) {
                ShouldSplit = Memory.MaxEnergy() > 0;

                Paused = true;
                if (ShouldSplit) {
                    Running = true;
                }
            } else {
                float extraCount = Memory.MaxEnergy();
                ShouldSplit = false;

                Paused = Memory.IsPaused();

                if (ShouldSplit) {
                    CurrentSplit++;
                }
            }
        }
    }
}