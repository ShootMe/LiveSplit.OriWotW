namespace LiveSplit.OriWotW {
    public class LogicManager {
        public bool ShouldSplit { get; private set; }
        public bool ShouldReset { get; private set; }
        public int CurrentSplit { get; private set; }
        public bool Running { get; private set; }
        public bool Paused { get; private set; }
        public float GameTime { get; private set; }
        public MemoryManager Memory { get; private set; }
        private bool lastBoolValue;
        private int lastIntValue;

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
            lastBoolValue = false;
            lastIntValue = -1;
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
            if (CurrentSplit < settings.Autosplits.Count) {
                Split split = settings.Autosplits[CurrentSplit];
                CheckSplit(split);
                if (!Running) {
                    Paused = true;
                    if (ShouldSplit) {
                        Running = true;
                    }
                }

                if (ShouldSplit) {
                    lastBoolValue = false;
                    lastIntValue = -1;
                    CurrentSplit++;
                }
            }
        }
        private void CheckSplit(Split split) {
            GameState state = Memory.GameState();
            if (split.Type == SplitType.GameStart) {
                bool isStarted = state == GameState.Game && Memory.MainMenuScreen() == Screen.ProfileSelected;
                ShouldSplit = !lastBoolValue && isStarted;
                lastBoolValue = isStarted;
            } else {
                float mapCompletion = Memory.MapCompletion();
                int maxHealth = Memory.MaxHealth();
                ShouldSplit = false;
                Paused = state == GameState.Game && (mapCompletion <= 0.001 || maxHealth <= 0);
                if (state != GameState.Game || Memory.Dead()) {
                    return;
                }

                switch (split.Type) {
                    case SplitType.ManualSplit:
                        break;
                    case SplitType.Ability:
                        bool hasAbility = Memory.HasAbility(Utility.GetEnumValue<AbilityType>(split.Value));
                        ShouldSplit = !lastBoolValue && hasAbility;
                        lastBoolValue = hasAbility;
                        break;
                    case SplitType.Shard:
                        bool hasShard = Memory.HasShard(Utility.GetEnumValue<ShardType>(split.Value));
                        ShouldSplit = !lastBoolValue && hasShard;
                        lastBoolValue = hasShard;
                        break;
                    case SplitType.AreaEnter: {
                            GameWorldAreaID area = Memory.PlayerArea();
                            GameWorldAreaID splitArea = Utility.GetEnumValue<GameWorldAreaID>(split.Value);
                            ShouldSplit = area != GameWorldAreaID.None && lastIntValue != (int)area && area == splitArea;
                            if (area != GameWorldAreaID.None) {
                                lastIntValue = (int)area;
                            }
                            break;
                        }
                    case SplitType.AreaLeave: {
                            GameWorldAreaID area = Memory.PlayerArea();
                            GameWorldAreaID splitArea = Utility.GetEnumValue<GameWorldAreaID>(split.Value);
                            ShouldSplit = area != GameWorldAreaID.None && lastIntValue != (int)area && lastIntValue == (int)splitArea;
                            if (area != GameWorldAreaID.None) {
                                lastIntValue = (int)area;
                            }
                            break;
                        }
                    case SplitType.Keystone:
                        int keystones = Memory.Keystones();
                        int splitKeystones = -1;
                        int.TryParse(split.Value, out splitKeystones);
                        ShouldSplit = lastIntValue != keystones && keystones == splitKeystones;
                        lastIntValue = keystones;
                        break;
                    case SplitType.HealthCell:
                        int health = maxHealth + Memory.HealthFragments();
                        int splitHealth = -1;
                        int.TryParse(split.Value, out splitHealth);
                        splitHealth += 6;
                        ShouldSplit = lastIntValue != health && health == splitHealth;
                        lastIntValue = health;
                        break;
                    case SplitType.EnergyCell:
                        int energy = Memory.MaxEnergy() + Memory.EnergyFragments();
                        int splitEnergy = -1;
                        int.TryParse(split.Value, out splitEnergy);
                        splitEnergy += 6;
                        ShouldSplit = lastIntValue != energy && energy == splitEnergy;
                        lastIntValue = energy;
                        break;
                }
            }
        }
    }
}