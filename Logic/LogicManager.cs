using System;
using LiveSplit.Options;
namespace LiveSplit.OriWotW {
    public class LogicManager {
        public bool ShouldSplit { get; private set; }
        public bool ShouldReset { get; private set; }
        public int CurrentSplit { get; private set; }

        private bool _running;
        public bool Running {
            get => _running;
            private set {
                if (!_running && value) {
                    hadDebug = Memory.DebugEnabled();
                }
                _running = value;

                if (MemoryManager.UseCommunityPatchTimer) {
                    Memory.SetCommunityPatchGameTimeRunning(value);
                }
            }
        }

        public bool Paused { get; private set; }
        public float GameTime { get; private set; } = -1f;
        public MemoryManager Memory { get; private set; }
        public SplitterSettings Settings { get; private set; }
        private bool lastBoolValue;
        private bool hadDebug;
        private int lastIntValue, lastIntValue2;
        private string lastStrValue;
        private Screen lastScreen;
        private DateTime splitLate;
        private RaceState raceState;

        public LogicManager(SplitterSettings settings) {
            Memory = new MemoryManager();
            Settings = settings;
            splitLate = DateTime.MaxValue;
        }

        public void Reset() {
            if (MemoryManager.UseCommunityPatchTimer) {
                Memory.SetCommunityPatchGameTimePaused(false);
            }

            splitLate = DateTime.MaxValue;
            Paused = false;
            Running = false;
            CurrentSplit = 0;
            InitializeSplit();
            ShouldSplit = false;
            ShouldReset = false;
            Memory.CurrentFader.ResetStartQTM();
            if (Settings.DisableDebug) {
                Memory.EnableDebug(hadDebug);
            }
        }
        public void Decrement() {
            CurrentSplit--;
            splitLate = DateTime.MaxValue;
            InitializeSplit();
        }
        public void Increment() {
            Running = true;
            splitLate = DateTime.MaxValue;
            CurrentSplit++;
            InitializeSplit();
        }
        private void InitializeSplit() {
            if (CurrentSplit < Settings.Autosplits.Count) {
                bool temp = ShouldSplit;
                CheckSplit(Settings.Autosplits[CurrentSplit], true);
                ShouldSplit = temp;
            }
        }
        public bool IsHooked() {
            bool hooked = Memory.HookProcess();
            Paused = !hooked;
            ShouldSplit = false;
            ShouldReset = false;
            return hooked;
        }
        public void Update(int currentSplit) {
            Memory.PatchNoPause(Settings.NoPause);

            if (Memory.DetectCommunityPatch() && MemoryManager.UseCommunityPatchTimer) {
                Memory.SetCommunityPatchGameTimeRunning(Running);
                Memory.SetCommunityPatchInitialGameTime(GameTime);
            }
            
            if (Settings.DisableDebug && Running) {
                Memory.EnableDebug(false);
            }

            if (currentSplit != CurrentSplit) {
                CurrentSplit = currentSplit;
                Running = CurrentSplit > 0;
                InitializeSplit();
            }

            if (CurrentSplit < Settings.Autosplits.Count) {
                CheckSplit(Settings.Autosplits[CurrentSplit], false);

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

            if (ShouldResetRace() || ShouldResetRaceOver()) {
                ShouldReset = true;
                raceState.RaceHasStarted = false;
            }

            if (Settings.UseRaceTime) {
                if (CurrentSplit < Settings.Autosplits.Count) {
                    float raceTime = Memory.RaceTime();
                    if (raceTime > 0.0f) {
                        GameTime = raceTime;
                    }
                } else if (CurrentSplit >= Settings.Autosplits.Count) {
                    float raceTime = Memory.RaceTime();
                    if (raceTime > 0.0f) {
                        GameTime = raceTime;
                    } else if (raceTime == 0.0f) {
                        GameTime = Memory.LastRaceTime();
                    }
                }
            }
        }
        public bool ShouldResetRace() {
            if (Running && Settings.Autosplits.Count > 0) {
                Split firstSplit = Settings.Autosplits[0];
                if (firstSplit.Type == SplitType.RaceState && Utility.GetEnumValue<SplitRace>(firstSplit.Value) == SplitRace.RaceHasStartedAutoReset) {
                    return !Memory.RaceCountdownFinished() && Memory.RaceTime() == 0.0f;
                }
            }
            return false;
        }
        public bool ShouldResetRaceOver() {
            if (Running && Settings.Autosplits.Count > 0) {
                Split lastSplit = Settings.Autosplits[Settings.Autosplits.Count - 1];
                if (lastSplit.Type == SplitType.RaceState && Utility.GetEnumValue<SplitRace>(lastSplit.Value) == SplitRace.RaceHasFinishedAutoStop && !Memory.IsRacing()) {
                    RaceHandler handler = Memory.GetRaceHandler();
                    return !handler.InProgress && !handler.RaceInProgressState;
                }
            }
            return false;
        }
        private void CheckSplit(Split split, bool updateValues) {
            GameState state = Memory.GameState();
            ShouldSplit = false;

            if (MemoryManager.UseCommunityPatchTimer && !Settings.UseRaceTime) {
                Paused = false;
                GameTime = (float)Memory.CommunityPatchGameTime();
            } else {
                Paused = Memory.IsLoadingGame(state, Running);
                GameTime = -1;
            }

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
                if (!updateValues && ((!state.IsGameOrRace() && split.Type != SplitType.Hitbox) || Memory.Dead() || (Paused && !state.IsGameOrRace()))) {
                    return;
                }

                switch (split.Type) {
                    case SplitType.ManualSplit:
                        break;
                    case SplitType.Ability:
                        SplitAbility ability = Utility.GetEnumValue<SplitAbility>(split.Value);
                        if (ability == SplitAbility.FastTravel) {
                            CheckUberIntValue(UberStateDefaults.fastTravel, 1);
                        } else {
                            CheckAbility(Utility.GetEnumValue<AbilityType>(split.Value));
                        }
                        break;
                    case SplitType.Shard:
                        ShardType shardType = ShardType.Overcharge;
                        bool hasShard = false;
                        if (Enum.TryParse<ShardType>(split.Value, out shardType)) {
                            hasShard = Memory.HasShard(shardType, 0);
                        } else if (Enum.TryParse<ShardType>(split.Value.Substring(0, split.Value.Length - 1), out shardType)) {
                            hasShard = Memory.HasShard(shardType, int.Parse(split.Value.Substring(split.Value.Length - 1)));
                        }
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
                    case SplitType.Boss:
                        CheckBoss(split);
                        break;
                    case SplitType.Map:
                        CheckMap(split);
                        break;
                    case SplitType.SpiritTrial:
                        CheckSpiritTrial(split);
                        break;
                    case SplitType.Teleporter:
                        CheckTeleporter(split);
                        break;
                    case SplitType.Hitbox:
                        Vector4 hitbox = new Vector4(split.Value);
                        CheckHitbox(hitbox);
                        break;
                    case SplitType.UberState:
                        CheckBoolUberState(split);
                        break;
                    case SplitType.GameEndCrawl:
                        CheckHitbox(new Vector4("-4628.05,-6756,10,10"));
                        break;
                    case SplitType.FinalCutsceneStarted:
                        CheckUberBoolValue(UberStateDefaults.gameFinished);
                        break;
                    case SplitType.Seed:
                        CheckSeed(split);
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
                        Memory.UpdateUberState(UberStateDefaults.healthContainersCounter);
                        Memory.UpdateUberState(UberStateDefaults.wispRewardPickupKwolok);
                        Memory.UpdateUberState(UberStateDefaults.wispRewardPickupMouldwood);
                        Memory.UpdateUberState(UberStateDefaults.wispRewardPickupWindtorn);
                        Memory.UpdateUberState(UberStateDefaults.wispRewardPickupBaur);
                        Memory.UpdateUberState(UberStateDefaults.wispRewardPickupLagoon);
                        int health = UberStateDefaults.healthContainersCounter.Value.Int - (UberStateDefaults.wispRewardPickupKwolok.Value.Bool ? 2 : 0);
                        health -= (UberStateDefaults.wispRewardPickupMouldwood.Value.Bool ? 2 : 0) + (UberStateDefaults.wispRewardPickupWindtorn.Value.Bool ? 2 : 0);
                        health -= (UberStateDefaults.wispRewardPickupBaur.Value.Bool ? 2 : 0) + (UberStateDefaults.wispRewardPickupLagoon.Value.Bool ? 2 : 0);
                        int splitHealth = -1;
                        int.TryParse(split.Value, out splitHealth);
                        ShouldSplit = lastIntValue != health && health == splitHealth;
                        lastIntValue = health;
                        break;
                    case SplitType.EnergyCell:
                        Memory.UpdateUberState(UberStateDefaults.energyContainersCounter);
                        Memory.UpdateUberState(UberStateDefaults.wispRewardPickupKwolok);
                        Memory.UpdateUberState(UberStateDefaults.wispRewardPickupMouldwood);
                        Memory.UpdateUberState(UberStateDefaults.wispRewardPickupWindtorn);
                        Memory.UpdateUberState(UberStateDefaults.wispRewardPickupBaur);
                        Memory.UpdateUberState(UberStateDefaults.wispRewardPickupLagoon);
                        int energy = UberStateDefaults.energyContainersCounter.Value.Int - (UberStateDefaults.wispRewardPickupKwolok.Value.Bool ? 2 : 0);
                        energy -= (UberStateDefaults.wispRewardPickupMouldwood.Value.Bool ? 2 : 0) + (UberStateDefaults.wispRewardPickupWindtorn.Value.Bool ? 2 : 0);
                        energy -= (UberStateDefaults.wispRewardPickupBaur.Value.Bool ? 2 : 0) + (UberStateDefaults.wispRewardPickupLagoon.Value.Bool ? 2 : 0);
                        int splitEnergy = -1;
                        int.TryParse(split.Value, out splitEnergy);
                        ShouldSplit = lastIntValue != energy && energy == splitEnergy;
                        lastIntValue = energy;
                        break;
                    case SplitType.RaceState:
                        CheckRace(split);
                        break;
                    case SplitType.KeystoneDoor:
                        CheckKeystoneDoor(split);
                        break;
                    case SplitType.UberStateValue:
                        CheckUberStateValue(split);
                        break;
                }

                if ((!state.IsGameOrRace() && split.Type != SplitType.Hitbox) || Memory.Dead() || (Paused && !state.IsGameOrRace())) {
                    ShouldSplit = false;
                } else if (DateTime.Now > splitLate) {
                    ShouldSplit = true;
                    splitLate = DateTime.MaxValue;
                }
            }
        }
        private void CheckRace(Split split) {
            SplitRace raceSplit = Utility.GetEnumValue<SplitRace>(split.Value);
            RaceStateMachineContext raceStateContext = Memory.GetRaceStateContext();
            bool isRacing = Memory.IsRacing();

            switch (raceSplit) {
                case SplitRace.RaceHasStartedAutoReset:
                case SplitRace.RaceHasStarted:
                    if (isRacing && Memory.RaceTime() < 0.5f && Memory.RaceCountdownFinished()) {
                        raceState.RaceHasStarted = true;
                        ShouldSplit = true;
                    } else {
                        ShouldSplit = false;
                    }
                    break;

                case SplitRace.RaceHasFinishedAutoStop:
                case SplitRace.RaceHasFinished:
                    if (!isRacing && raceState.RaceHasStarted && Memory.RaceTime() == 0.0f &&
                        Memory.RaceCountdownFinished() && raceState.LastReason != RaceStopReason.UserAction && !raceStateContext.UserRequestedRetry) {
                        ShouldSplit = true;
                        raceState.RaceHasStarted = false;
                        raceState.LastReason = RaceStopReason.None;
                    } else {
                        ShouldSplit = false;
                    }
                    break;
            }
            if (raceStateContext.StopReason != RaceStopReason.None) {
                raceState.LastReason = raceStateContext.StopReason;
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
                case SplitSpiritTrial.BaursReachActivate: CheckUberIntValue(UberStateDefaults.baursReachWindTunnelRace, 1); break;
                case SplitSpiritTrial.BaursReachComplete: CheckUberIntValue(UberStateDefaults.baursReachWindTunnelRace, 2); break;
                case SplitSpiritTrial.InkwaterMarshActivate: CheckUberIntValue(UberStateDefaults.inkwaterMarshRace, 1); break;
                case SplitSpiritTrial.InkwaterMarshComplete: CheckUberIntValue(UberStateDefaults.inkwaterMarshRace, 2); break;
                case SplitSpiritTrial.KwoloksHollowActivate: CheckUberIntValue(UberStateDefaults.kwolokDropRace, 1); break;
                case SplitSpiritTrial.KwoloksHollowComplete: CheckUberIntValue(UberStateDefaults.kwolokDropRace, 2); break;
                case SplitSpiritTrial.LumaPoolsActivate: CheckUberIntValue(UberStateDefaults.lumaPoolsRace, 1); break;
                case SplitSpiritTrial.LumaPoolsComplete: CheckUberIntValue(UberStateDefaults.lumaPoolsRace, 2); break;
                case SplitSpiritTrial.MouldwoodDepthsActivate: CheckUberIntValue(UberStateDefaults.mouldwoodDepthsRace, 1); break;
                case SplitSpiritTrial.MouldwoodDepthsComplete: CheckUberIntValue(UberStateDefaults.mouldwoodDepthsRace, 2); break;
                case SplitSpiritTrial.SilentWoodsActivate: CheckUberIntValue(UberStateDefaults.silentWoodlandRace, 1); break;
                case SplitSpiritTrial.SilentWoodsComplete: CheckUberIntValue(UberStateDefaults.silentWoodlandRace, 2); break;
                case SplitSpiritTrial.WellspringActivate: CheckUberIntValue(UberStateDefaults.wellspringRace, 1); break;
                case SplitSpiritTrial.WellspringComplete: CheckUberIntValue(UberStateDefaults.wellspringRace, 2); break;
                case SplitSpiritTrial.WindsweptWastesActivate: CheckUberIntValue(UberStateDefaults.desertRace, 1); break;
                case SplitSpiritTrial.WindsweptWastesComplete: CheckUberIntValue(UberStateDefaults.desertRace, 2); break;
            }
        }
        private void CheckBoolUberState(Split split) {
            try {
                var parts = split.Value.Split('|');
                if (int.TryParse(parts[0], out int groupId)) {
                    if (int.TryParse(parts[1], out int id)) {
                        UberState value = Memory.GetUberState(groupId, id);
                        if (value != null) {
                            CheckUberBoolValue(value);
                        }
                    } else Log.Error($"failed to parse {split.Value}");
                } else Log.Error($"failed to parse {split.Value}");
            } catch (Exception e) {
                Log.Error($"Exception thrown parsing {split.Value}: {e}");
            }
        }
        private void CheckSeed(Split split) {
            SplitSeed seed = Utility.GetEnumValue<SplitSeed>(split.Value);
            switch (seed) {
                case SplitSeed.BaursReach: CheckUberBoolValue(UberStateDefaults.baursReachSeed); break;
                case SplitSeed.LumaPools: CheckUberBoolValue(UberStateDefaults.lumaPoolsSeed); break;
                case SplitSeed.MouldwoodDepths: CheckUberBoolValue(UberStateDefaults.mouldwoodDepthsSeed); break;
                case SplitSeed.SilentWoods: CheckUberBoolValue(UberStateDefaults.silentWoodsSeed); break;
                case SplitSeed.Wellspring: CheckUberBoolValue(UberStateDefaults.wellspringSeed); break;
                case SplitSeed.WindsweptWastes: CheckUberBoolValue(UberStateDefaults.windsweptWastesSeed); break;
            }
        }
        private void CheckBoss(Split split) {
            SplitBoss boss = Utility.GetEnumValue<SplitBoss>(split.Value);
            switch (boss) {
                case SplitBoss.HowlStart: CheckUberBoolValue(UberStateDefaults.nightCrawlerChaseStarted); break;
                case SplitBoss.HowlEnd: CheckUberBoolValue(UberStateDefaults.nightCrawlerDefeated); break;
                case SplitBoss.HornbugStart: CheckUberIntValue(UberStateDefaults.hornBugBossState, 1); break;
                case SplitBoss.HornbugEnd: CheckUberIntValue(UberStateDefaults.hornBugBossState, 3); break;
                case SplitBoss.KwolokStart: CheckUberIntValue(UberStateDefaults.kwolokBossState, 1); break;
                case SplitBoss.KwolokEnd: CheckUberIntValue(UberStateDefaults.kwolokBossState, 7); break;
                case SplitBoss.MoraStart: CheckUberIntValue(UberStateDefaults.spiderBossState, 1); break;
                case SplitBoss.MoraEnd: CheckUberIntValue(UberStateDefaults.spiderBossState, 7); break;
                case SplitBoss.WeepingRidgeElevatorFight: CheckUberBoolValue(UberStateDefaults.elevatorCompleteState); break;
                case SplitBoss.WillowStoneStart: CheckUberIntValue(UberStateDefaults.laserShooterBossState, 1); break;
                case SplitBoss.WillowStoneEnd: CheckUberIntValue(UberStateDefaults.laserShooterBossState, 4); break;
                case SplitBoss.ShriekStart: CheckUberIntValue(UberStateDefaults.petrifiedOwlBossState, 1); break;
                case SplitBoss.ShriekDefeated: CheckUberIntValue(UberStateDefaults.petrifiedOwlBossState, 5); break;
            }
        }
        private void CheckTeleporter(Split split) {
            SplitTeleporter teleporter = Utility.GetEnumValue<SplitTeleporter>(split.Value);
            switch (teleporter) {
                case SplitTeleporter.HowlsDenActivated: CheckUberBoolValue(UberStateDefaults.savePedestalHowlsDen); break;
                case SplitTeleporter.HowlsDenTeleported: CheckScene(true, "howlsDenSaveRoom"); break;
                case SplitTeleporter.InkwaterMarshActivated: CheckUberBoolValue(UberStateDefaults.savePedestalInkwaterMarsh); break;
                case SplitTeleporter.InkwaterMarshTeleported: CheckScene(true, "swampIntroTop"); break;
                case SplitTeleporter.KwoloksHollowActivated: CheckUberBoolValue(UberStateDefaults.savePedestalKwoloksHollow); break;
                case SplitTeleporter.KwoloksHollowTeleported: CheckScene(true, "kwoloksCavernSaveRoomA"); break;
                case SplitTeleporter.LumaPoolsAActivated: CheckUberBoolValue(UberStateDefaults.savePedestalLumaPoolsA); break;
                case SplitTeleporter.LumaPoolsATeleported: CheckScene(true, "lumaPoolsSaveRoom"); break;
                case SplitTeleporter.LumaPoolsBActivated: CheckUberBoolValue(UberStateDefaults.savePedestalLumaPoolsB); break;
                case SplitTeleporter.LumaPoolsBTeleported: CheckScene(true, "lumaPoolsSaveRoomB"); break;
                case SplitTeleporter.MidnightBurrowsActivated: CheckUberBoolValue(UberStateDefaults.savePedestalMidnightBurrows); break;
                case SplitTeleporter.MidnightBurrowsTeleported: CheckScene(true, "howlsOriginA"); break;
                case SplitTeleporter.MouldwoodDepthsActivated: CheckUberBoolValue(UberStateDefaults.savePedestalMouldwood); break;
                case SplitTeleporter.MouldwoodDepthsTeleported: CheckScene(true, "mouldwoodDepthsF"); break;
                case SplitTeleporter.SilentWoodsAActivated: CheckUberBoolValue(UberStateDefaults.savePedestalSilentWoodsA); break;
                case SplitTeleporter.SilentWoodsATeleported: CheckScene(true, "petrifiedForestTarBubbleChallenge"); break;
                case SplitTeleporter.SilentWoodsBActivated: CheckUberBoolValue(UberStateDefaults.savePedestalSilentWoodsB); break;
                case SplitTeleporter.SilentWoodsBTeleported: CheckScene(true, "petrifiedForestTandemWindChaseA"); break;
                case SplitTeleporter.WellspringActivated: CheckUberBoolValue(UberStateDefaults.savePedestalWellspring); break;
                case SplitTeleporter.WellspringTeleported: CheckScene(true, "waterMillCEntrance"); break;
                case SplitTeleporter.WellspringGladesActivated: CheckUberIntValue(UberStateDefaults.builderProjectSpiritWell, 3); break;
                case SplitTeleporter.WellspringGladesTeleported: CheckScene(true, "wellspringGladesHub"); break;
                case SplitTeleporter.WillowsEndActivated: CheckUberBoolValue(UberStateDefaults.savePedestalWillowsEnd); break;
                case SplitTeleporter.WillowsEndTeleported: CheckScene(true, "willowsEndSaveRoom"); break;
                case SplitTeleporter.WillowsEndShriekActivated: CheckUberBoolValue(UberStateDefaults.savePedestalWillowsEndShriek); break;
                case SplitTeleporter.WillowsEndShreikTeleported: CheckScene(true, "willowCeremonyIntro"); break;
                case SplitTeleporter.WindsweptWastesAActivated: CheckUberBoolValue(UberStateDefaults.savePedestalWindsweptWastesA); break;
                case SplitTeleporter.WindsweptWastesATeleported: CheckScene(true, "petrifiedOwlFeedingGroundsRevised"); break;
                case SplitTeleporter.WindsweptWastesBActivated: CheckUberBoolValue(UberStateDefaults.savePedestalWindsweptWastesB); break;
                case SplitTeleporter.WindsweptWastesBTeleported: CheckScene(true, "e3DesertI__clone0"); break;
                case SplitTeleporter.WindtornRuinsEntranceActivated: CheckUberBoolValue(UberStateDefaults.savePedestalWindtornRuinsA); break;
                case SplitTeleporter.WindtornRuinsEntranceTeleported: CheckScene(true, "desertRuinsTowerSaveRoom"); break;
                case SplitTeleporter.WindtornRuinsBossActivated: CheckUberBoolValue(UberStateDefaults.savePedestalWindtornRuinsB); break;
                case SplitTeleporter.WindtornRuinsBossTeleported: CheckScene(true, "windtornRuinsC"); break;
            }
        }
        private void CheckWorldEvent(Split split) {
            SplitWorldEvent worldEvent = Utility.GetEnumValue<SplitWorldEvent>(split.Value);
            switch (worldEvent) {
                case SplitWorldEvent.FindKu: CheckUberBoolValue(UberStateDefaults.playerOnTandem); break;
                case SplitWorldEvent.LoseKu: CheckUberBoolValue(UberStateDefaults.playerOnTandem, false); break;
                case SplitWorldEvent.DesertEscapeStart: CheckUberIntValue(UberStateDefaults.desertRuinsEscape, 1); break;
                case SplitWorldEvent.DesertEscapeEnd: CheckUberIntValue(UberStateDefaults.desertRuinsEscape, 3); break;
                case SplitWorldEvent.WinterForestEscapeStart: CheckUberIntValue(UberStateDefaults.winterForestWispQuest, 2); break;
                case SplitWorldEvent.WinterForestEscapeEnd: CheckUberIntValue(UberStateDefaults.winterForestWispQuest, 3); break;
                case SplitWorldEvent.WaterEscapeStart: CheckUberIntValue(UberStateDefaults.watermillEscapeState, 1); break;
                case SplitWorldEvent.WaterPurified: CheckUberBoolValue(UberStateDefaults.finishedWatermillEscape); break;
                case SplitWorldEvent.SoSoggy:
                    if (splitLate == DateTime.MaxValue) {
                        CheckUberBoolValue(UberStateDefaults.finishedWatermillEscape);
                        if (ShouldSplit) {
                            splitLate = DateTime.Now.AddSeconds(41.3);
                            ShouldSplit = false;
                        }
                    }
                    break;
                case SplitWorldEvent.SilentWoodsShriekCutscene: CheckUberBoolValue(UberStateDefaults.petrifiedForestTransitionPlayed); break;
            }
        }
        private void CheckWisp(Split split) {
            SplitWisp wisp = Utility.GetEnumValue<SplitWisp>(split.Value);
            switch (wisp) {
                case SplitWisp.VoiceOfTheForest: CheckUberBoolValue(UberStateDefaults.wispRewardPickupKwolok); break;
                case SplitWisp.EyesOfTheForest: CheckUberBoolValue(UberStateDefaults.wispRewardPickupMouldwood); break;
                case SplitWisp.HeartOfTheForest: CheckUberBoolValue(UberStateDefaults.wispRewardPickupWindtorn); break;
                case SplitWisp.MemoryOfTheForest: CheckUberBoolValue(UberStateDefaults.wispRewardPickupBaur); break;
                case SplitWisp.StrengthOfTheForest: CheckUberBoolValue(UberStateDefaults.wispRewardPickupLagoon); break;
            }
        }
        private void CheckArea(Split split, bool onEnter) {
            SplitArea splitArea = Utility.GetEnumValue<SplitArea>(split.Value);
            switch (splitArea) {
                case SplitArea.WaterMillSub1: CheckScene(onEnter, "wotwSaveRoomC__clone0__clone1", "waterMillAExit"); break;
                case SplitArea.WaterMillSub2: CheckScene(onEnter, "waterMillBEntrance"); break;
                case SplitArea.WaterMillSub3: CheckScene(onEnter, "waterMillCEntrance"); break;
                case SplitArea.WeepingRidge: CheckScene(onEnter, "weepingRidgeWillowsEndEntrance", "weepingRidgeElevatorFight"); break;
                case SplitArea.WillowsEndBoss: CheckScene(onEnter, "willowCeremonyIntro"); break;
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
        private void CheckMap(Split split) {
            SplitMap splitMap = Utility.GetEnumValue<SplitMap>(split.Value);
            AreaType area = AreaType.None;
            Enum.TryParse<AreaType>(splitMap.ToString(), true, out area);
            int completion = (int)Math.Floor(Memory.MapCompletion(area));
            ShouldSplit = lastIntValue != (int)completion && completion == 100;
            lastIntValue = completion;
        }
        private void CheckScene(bool onEnter, params string[] scenesToCheck) {
            string scene = Memory.CurrentScene();
            if (!string.IsNullOrEmpty(scene) && scenesToCheck != null) {
                if (!scene.Equals(lastStrValue, StringComparison.OrdinalIgnoreCase)) {
                    for (int i = 0; i < scenesToCheck.Length; i++) {
                        if (onEnter) {
                            if (scene.Equals(scenesToCheck[i], StringComparison.OrdinalIgnoreCase)) {
                                ShouldSplit = true;
                                break;
                            }
                        } else if (scenesToCheck[i].Equals(lastStrValue, StringComparison.OrdinalIgnoreCase)) {
                            ShouldSplit = true;
                            break;
                        }
                    }
                }
                lastStrValue = scene;
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
        private void CheckKeystoneDoor(Split split) {
            SplitKeystoneDoor splitKeystoneDoor = Utility.GetEnumValue<SplitKeystoneDoor>(split.Value);

            int allocatedKeystones = Memory.AllocatedKeystones();
            int keystones = Memory.Keystones();
            bool hasAllocated = allocatedKeystones > 0;

            switch (splitKeystoneDoor) {
                case SplitKeystoneDoor.OpenedKeystoneDoor:
                    ShouldSplit = (lastIntValue == keystones + 2 || lastIntValue == keystones + 4) && allocatedKeystones == 0 && lastIntValue2 > 0;
                    if (ShouldSplit) {
                        lastIntValue = -1;
                        lastIntValue2 = -1;
                    }
                    break;
                case SplitKeystoneDoor.PutInKeystone:
                    ShouldSplit = hasAllocated && !lastBoolValue;
                    break;
            }

            if (lastIntValue2 == -1 || allocatedKeystones == 0 || lastIntValue2 == keystones) {
                lastIntValue = keystones;
            }
            if (lastIntValue2 == keystones || allocatedKeystones > 0) {
                lastIntValue2 = allocatedKeystones;
            }
            lastBoolValue = hasAllocated;
        }
        private void CheckUberStateValue(Split split) {
            string[] values = split.Value.Split(',');

            if (!int.TryParse(values[0], out int uberGroup) || uberGroup == -1) {
                ShouldSplit = false;
                return;
            }

            if (!int.TryParse(values[1], out int uberId) || uberId == -1) {
                ShouldSplit = false;
                return;
            }

            UberState uberState = Memory.GetUberState(uberGroup, uberId);
            Memory.UpdateUberState(uberState);

            string splitUberValue = values[2];
            string splitUberValueComparison = "=="; //0 = ==, 1 = > 2 = < 3 = !=
            bool comparison = false;

            if (values.Length >= 4) {
                splitUberValueComparison = values[2];
                splitUberValue = values[3];
            }

            switch (uberState.Type) {
                case UberStateType.SavePedestalUberState:
                case UberStateType.SerializedBooleanUberState:
                    if (!bool.TryParse(splitUberValue, out bool splitBool)) {
                        ShouldSplit = false;
                        return;
                    }

                    switch (splitUberValueComparison) {
                        default:
                        case "==": comparison = splitBool == uberState.Value.Bool; break;
                        case "!=": comparison = splitBool != uberState.Value.Bool; break;
                    }
                    break;
                case UberStateType.SerializedFloatUberState:
                    if (!float.TryParse(splitUberValue, out float splitFloat)) {
                        ShouldSplit = false;
                        return;
                    }

                    switch (splitUberValueComparison) {
                        default:
                        case "==": comparison = splitFloat == uberState.Value.Float; break;
                        case ">": comparison = splitFloat < uberState.Value.Float; break;
                        case "<": comparison = splitFloat > uberState.Value.Float; break;
                        case "!=": comparison = splitFloat != uberState.Value.Float; break;
                    }
                    break;
                case UberStateType.SerializedIntUberState:
                    if (!int.TryParse(splitUberValue, out int splitInt)) {
                        ShouldSplit = false;
                        return;
                    }

                    switch (splitUberValueComparison) {
                        default:
                        case "==": comparison = splitInt == uberState.Value.Int; break;
                        case ">": comparison = splitInt < uberState.Value.Int; break;
                        case "<": comparison = splitInt > uberState.Value.Int; break;
                        case "!=": comparison = splitInt != uberState.Value.Int; break;
                    }
                    break;
                case UberStateType.SerializedByteUberState:
                    if (!byte.TryParse(splitUberValue, out byte splitByte)) {
                        ShouldSplit = false;
                        return;
                    }

                    switch (splitUberValueComparison) {
                        default:
                        case "==": comparison = splitByte == uberState.Value.Byte; break;
                        case ">": comparison = splitByte < uberState.Value.Byte; break;
                        case "<": comparison = splitByte > uberState.Value.Byte; break;
                        case "!=": comparison = splitByte != uberState.Value.Byte; break;
                    }
                    break;
            }

            ShouldSplit = comparison && !lastBoolValue;
            lastBoolValue = comparison;
        }
    }
}