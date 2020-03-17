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
        private Screen lastScreen;

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
            Running = true;
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
                Screen screen = Memory.TitleScreen();
                int difficulty = Memory.Difficulty();
                bool isStarted = difficulty != 4 && lastIntValue == 4 && screen == Screen.ProfileSelected;
                if (screen == Screen.ProfileSelected && lastScreen == Screen.SaveSlots) {
                    Memory.SetDifficulty(4);
                }
                ShouldSplit = !lastBoolValue && isStarted;
                lastBoolValue = isStarted;
                lastIntValue = difficulty;
                lastScreen = screen;
            } else {
                ShouldSplit = false;
                Paused = Memory.IsLoadingGame();

                switch (split.Type) {
                    case SplitType.ManualSplit:
                        break;
                    case SplitType.Ability:
                        CheckAbility(Utility.GetEnumValue<AbilityType>(split.Value));
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
                    case SplitType.SpiritTrial:
                        CheckSpiritTrial(split);
                        break;
                    case SplitType.GameEnd:
                        CheckHitbox(new Vector4("-4628.05,-6756,10,10"));
                        break;
                    case SplitType.CreepHeart:
                        Memory.UpdateUberState(UberStateDefaults.vineAClear);
                        Memory.UpdateUberState(UberStateDefaults.vineBClear);
                        Memory.UpdateUberState(UberStateDefaults.vineCClear);
                        Memory.UpdateUberState(UberStateDefaults.vineDClear);
                        Memory.UpdateUberState(UberStateDefaults.vineEClear);
                        Memory.UpdateUberState(UberStateDefaults.vineFClear);
                        Memory.UpdateUberState(UberStateDefaults.vineGClear);
                        Memory.UpdateUberState(UberStateDefaults.vineHClear);
                        int creepCount = UberStateDefaults.vineAClear.Value.Int + UberStateDefaults.vineBClear.Value.Int;
                        creepCount += UberStateDefaults.vineCClear.Value.Int + UberStateDefaults.vineDClear.Value.Int;
                        creepCount += UberStateDefaults.vineEClear.Value.Int + UberStateDefaults.vineFClear.Value.Int;
                        creepCount += UberStateDefaults.vineGClear.Value.Int + UberStateDefaults.vineHClear.Value.Int;

                        int splitCreeps = -1;
                        int.TryParse(split.Value, out splitCreeps);
                        ShouldSplit = lastIntValue != creepCount && creepCount == splitCreeps;
                        lastIntValue = creepCount;
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

                if (state != GameState.Game || Memory.Dead() || Paused) {
                    ShouldSplit = false;
                }
            }
        }
        private void CheckHitbox(Vector4 hitbox) {
            Vector4 ori = new Vector4(Memory.Position(), 0f, 0f, true);
            bool containsOri = hitbox.Intersects(ori);
            ShouldSplit = containsOri && !lastBoolValue;
            lastBoolValue = containsOri;
        }
        private void CheckSpiritTrial(Split split) {
            SplitSpiritTrial spiritTrial = Utility.GetEnumValue<SplitSpiritTrial>(split.Value);
            switch (spiritTrial) {
                case SplitSpiritTrial.BaursReachActivate: CheckUberIntValue(UberStateDefaults.baursReachWindTunnelRace, 1, 0); break;
                case SplitSpiritTrial.BaursReachComplete: CheckUberIntValue(UberStateDefaults.baursReachWindTunnelRace, 2, 1); break;
                case SplitSpiritTrial.InkwaterMarshActivate: CheckUberIntValue(UberStateDefaults.inkwaterMarshRace, 1, 0); break;
                case SplitSpiritTrial.InkwaterMarshComplete: CheckUberIntValue(UberStateDefaults.inkwaterMarshRace, 2, 1); break;
                case SplitSpiritTrial.KwoloksHollowActivate: CheckUberIntValue(UberStateDefaults.kwolokDropRace, 1, 0); break;
                case SplitSpiritTrial.KwoloksHollowComplete: CheckUberIntValue(UberStateDefaults.kwolokDropRace, 2, 1); break;
                case SplitSpiritTrial.LumaPoolsActivate: CheckUberIntValue(UberStateDefaults.lumaPoolsRace, 1, 0); break;
                case SplitSpiritTrial.LumaPoolsComplete: CheckUberIntValue(UberStateDefaults.lumaPoolsRace, 2, 1); break;
                case SplitSpiritTrial.MouldwoodDepthsActivate: CheckUberIntValue(UberStateDefaults.mouldwoodDepthsRace, 1, 0); break;
                case SplitSpiritTrial.MouldwoodDepthsComplete: CheckUberIntValue(UberStateDefaults.mouldwoodDepthsRace, 2, 1); break;
                case SplitSpiritTrial.SilentWoodsActivate: CheckUberIntValue(UberStateDefaults.silentWoodlandRace, 1, 0); break;
                case SplitSpiritTrial.SilentWoodsComplete: CheckUberIntValue(UberStateDefaults.silentWoodlandRace, 2, 1); break;
                case SplitSpiritTrial.WellspringActivate: CheckUberIntValue(UberStateDefaults.wellspringRace, 1, 0); break;
                case SplitSpiritTrial.WellspringComplete: CheckUberIntValue(UberStateDefaults.wellspringRace, 2, 1); break;
                case SplitSpiritTrial.WindsweptWastesActivate: CheckUberIntValue(UberStateDefaults.desertRace, 1, 0); break;
                case SplitSpiritTrial.WindsweptWastesComplete: CheckUberIntValue(UberStateDefaults.desertRace, 2, 1); break;
            }
        }
        private void CheckWorldEvent(Split split) {
            SplitWorldEvent worldEvent = Utility.GetEnumValue<SplitWorldEvent>(split.Value);
            switch (worldEvent) {
                case SplitWorldEvent.HowlFight: CheckUberBoolValue(UberStateDefaults.nightCrawlerDefeated); break;
                case SplitWorldEvent.ShriekDefeated: CheckUberIntValue(UberStateDefaults.petrifiedOwlBossState, 5); break;
                case SplitWorldEvent.FindKu: CheckAbility(AbilityType.Flap); break;
                case SplitWorldEvent.LoseKu: CheckAbility(AbilityType.Flap, false); break;
                case SplitWorldEvent.WaterPurified: CheckUberBoolValue(UberStateDefaults.finishedWatermillEscape); break;
                case SplitWorldEvent.SoSoggy: CheckUberIntValue(UberStateDefaults.cleanseWellspringQuestUberState, 3); break;
                case SplitWorldEvent.WeepingRidgeElevatorFight: CheckUberBoolValue(UberStateDefaults.elevatorCompleteState); break;
            }
        }
        private void CheckWisp(Split split) {
            SplitWisp wisp = Utility.GetEnumValue<SplitWisp>(split.Value);
            switch (wisp) {
                case SplitWisp.VoiceOfTheForest: CheckUberIntValue(UberStateDefaults.kwolokNpcState, 1, 0); break;
                case SplitWisp.EyesOfTheForest: CheckUberIntValue(UberStateDefaults.mouldwoodDepthsWispQuestUberState, 3); break;
                case SplitWisp.HeartOfTheForest: CheckUberIntValue(UberStateDefaults.desertWispQuestUberState, 3); break;
                case SplitWisp.MemoryOfTheForest: CheckUberIntValue(UberStateDefaults.winterForestWispQuestUberState, 3); break;
                case SplitWisp.StrengthOfTheForest: CheckUberIntValue(UberStateDefaults.lagoonWispQuestUberState, 3); break;
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
        private void CheckUberIntValue(UberState value, int currentValue, int lastValue = int.MinValue) {
            Memory.UpdateUberState(value);
            if (lastValue == int.MinValue) {
                ShouldSplit = value.Value.Int == currentValue && lastIntValue != currentValue;
            } else {
                ShouldSplit = value.Value.Int == currentValue && lastIntValue == lastValue;
            }
            lastIntValue = value.Value.Int;
        }
        private void CheckUberBoolValue(UberState value, bool currentValue = true) {
            Memory.UpdateUberState(value);
            ShouldSplit = value.Value.Bool == currentValue && lastBoolValue != currentValue;
            lastBoolValue = value.Value.Bool;
        }
        private void CheckAbility(AbilityType value, bool currentValue = true) {
            bool hasAbility = Memory.HasAbility(value);
            ShouldSplit = hasAbility == currentValue && lastBoolValue != currentValue;
            lastBoolValue = hasAbility;
        }
    }
}