using System;
namespace LiveSplit.OriWotW {
    public class LogicManager {
        public bool ShouldSplit { get; private set; }
        public bool ShouldReset { get; private set; }
        public int CurrentSplit { get; private set; }
        public bool Running { get; private set; }
        public bool Paused { get; private set; }
        public float GameTime { get; private set; }
        public MemoryManager Memory { get; private set; }
        public SplitterSettings Settings { get; private set; }
        private bool lastBoolValue;
        private int lastIntValue;
        private string lastStrValue;

        public LogicManager(SplitterSettings settings) {
            Memory = new MemoryManager();
            Settings = settings;
        }

        public void Reset() {
            Paused = false;
            Running = false;
            CurrentSplit = 0;
            InitializeSplit();
            ShouldSplit = false;
            ShouldReset = false;
        }
        public void Decrement() {
            CurrentSplit--;
            InitializeSplit();
        }
        public void Increment() {
            CurrentSplit++;
            InitializeSplit();
        }
        private void InitializeSplit() {
            if (CurrentSplit < Settings.Autosplits.Count) {
                bool temp = ShouldSplit;
                CheckSplit(Settings.Autosplits[CurrentSplit]);
                ShouldSplit = temp;
            }
        }
        public bool IsHooked() {
            bool hooked = Memory.HookProcess();
            Paused = !hooked;
            ShouldSplit = false;
            ShouldReset = false;
            GameTime = -1;
            return hooked;
        }
        public void Update() {
            if (CurrentSplit < Settings.Autosplits.Count) {
                CheckSplit(Settings.Autosplits[CurrentSplit]);
                if (!Running) {
                    Paused = true;
                    if (ShouldSplit) {
                        Running = true;
                    }
                }

                if (ShouldSplit) {
                    Increment();
                }
            }
        }
        private void CheckSplit(Split split) {
            GameState state = Memory.GameState();
            if (split.Type == SplitType.GameStart) {
                bool isStarted = state == GameState.Game && Memory.TitleScreen() == Screen.ProfileSelected;
                ShouldSplit = !lastBoolValue && isStarted;
                lastBoolValue = isStarted;
            } else {
                ShouldSplit = false;
                Paused = Memory.IsLoadingGame();
                if (state != GameState.Game || Memory.Dead() || Paused) {
                    return;
                }

                switch (split.Type) {
                    case SplitType.ManualSplit:
                        break;
                    case SplitType.Ability:
                        bool hasAbility = Memory.HasAbility(Utility.GetEnumValue<AbilityType>(split.Value));
                        ShouldSplit = lastBoolValue != hasAbility;
                        lastBoolValue = hasAbility;
                        break;
                    case SplitType.Shard:
                        bool hasShard = Memory.HasShard(Utility.GetEnumValue<ShardType>(split.Value));
                        ShouldSplit = !lastBoolValue && hasShard;
                        lastBoolValue = hasShard;
                        break;
                    case SplitType.AreaEnter:
                        CheckArea(split, true);
                        break;
                    case SplitType.AreaLeave:
                        CheckArea(split, false);
                        break;
                    case SplitType.WorldEvent:
                        CheckWorldEvent(split);
                        break;
                    case SplitType.Wisp:
                        CheckWisp(split);
                        break;
                    case SplitType.Keystone:
                        int keystones = Memory.Keystones();
                        int splitKeystones = -1;
                        int.TryParse(split.Value, out splitKeystones);
                        ShouldSplit = lastIntValue != keystones && keystones == splitKeystones;
                        lastIntValue = keystones;
                        break;
                    case SplitType.Ore:
                        int ore = Memory.Ore();
                        int splitOre = -1;
                        int.TryParse(split.Value, out splitOre);
                        ShouldSplit = lastIntValue != ore && ore == splitOre;
                        lastIntValue = ore;
                        break;
                    case SplitType.HealthCell:
                        int health = Memory.MaxHealth() + Memory.HealthFragments();
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
        private void CheckHitbox(Split split) {
            //Desert Escape End Metal Gear Solid
            //Vector4 hitbox = new Vector4("1440,-3990,35,10");
        }
        private void CheckWorldEvent(Split split) {
            SplitWorldEvent worldEvent = Utility.GetEnumValue<SplitWorldEvent>(split.Value);
            WorldStateValue value;
            switch (worldEvent) {
                case SplitWorldEvent.FindKu:
                    bool hasFlap = Memory.HasAbility(AbilityType.Flap);
                    ShouldSplit = hasFlap && !lastBoolValue;
                    lastBoolValue = hasFlap;
                    break;
                case SplitWorldEvent.LoseKu:
                    bool lostFlap = !Memory.HasAbility(AbilityType.Flap);
                    ShouldSplit = lostFlap && !lastBoolValue;
                    lastBoolValue = lostFlap;
                    break;
                case SplitWorldEvent.WaterPurified:
                    value = Memory.GetWorldState(WorldState.WaterPurified);
                    ShouldSplit = value.Value == 1 && !lastBoolValue;
                    lastBoolValue = value.Value == 1;
                    break;
                case SplitWorldEvent.WinterForestEscape:
                    value = Memory.GetWorldState(WorldState.WinterForestWispQuest);
                    ShouldSplit = value.Value == 3 && !lastBoolValue;
                    lastBoolValue = value.Value == 3;
                    break;
            }
        }
        private void CheckWisp(Split split) {
            SplitWisp wisp = Utility.GetEnumValue<SplitWisp>(split.Value);
            WorldStateValue value;
            switch (wisp) {
                case SplitWisp.VoiceOfTheForest:
                    value = Memory.GetWorldState(WorldState.KwolokNpc);
                    ShouldSplit = value.Value == 1 && !lastBoolValue;
                    lastBoolValue = value.Value == 1;
                    break;
            }
        }
        private void CheckArea(Split split, bool onEnter) {
            SplitArea splitArea = Utility.GetEnumValue<SplitArea>(split.Value);
            switch (splitArea) {
                case SplitArea.WaterMillSub1:
                case SplitArea.WaterMillSub2:
                case SplitArea.WaterMillSub3:
                case SplitArea.WeepingRidge:
                    string[] scenesToCheck = Utility.GetEnumScenes<SplitArea>(splitArea);
                    string scene = Memory.CurrentScene();
                    if (!string.IsNullOrEmpty(scene)) {
                        if (!scene.Equals(lastStrValue, StringComparison.OrdinalIgnoreCase)) {
                            for (int i = 0; i < scenesToCheck.Length; i++) {
                                if (onEnter) {
                                    if (scene.Equals(scenesToCheck[i], StringComparison.OrdinalIgnoreCase)) {
                                        ShouldSplit = true;
                                        break;
                                    }
                                } else if (lastStrValue.Equals(scenesToCheck[i], StringComparison.OrdinalIgnoreCase)) {
                                    ShouldSplit = true;
                                    break;
                                }
                            }
                        }
                        lastStrValue = scene;
                    }
                    break;
                default:
                    AreaType area = Memory.PlayerArea();
                    AreaType splitValue = Utility.GetEnumValue<AreaType>(split.Value);
                    if (area != AreaType.None) {
                        ShouldSplit = lastIntValue != (int)area && (onEnter ? (int)area : lastIntValue) == (int)splitValue;
                        lastIntValue = (int)area;
                    }
                    break;
            }
        }
    }
}