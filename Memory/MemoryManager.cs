using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace LiveSplit.OriWotW {
    public partial class MemoryManager {
        private static ProgramPointer Characters = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.Characters.SetCurrentCharacter", 0x15c));
        private static ProgramPointer GameWorld = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.V2, AutoDeref.Single, "__mainWisp.GameWorld.Awake", 0x79),
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.GameWorld.Awake", 0xa7));
        private static ProgramPointer PlayerUberStateGroup = new ProgramPointer("GameAssembly.dll", 
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.Seinlevel.get_PartialHealthContainers", 0x68));
        private static ProgramPointer TitleScreenManager = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.TitleScreenManager.Awake", 0x97));
        private static ProgramPointer GameStateMachine = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.GameStateMachine.get_Instance", 0x6f));
        private static ProgramPointer GameController = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.GameController.Initialize", 0xc3));
        private static ProgramPointer ScenesManager = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.ScenesManager.Awake", 0x76));
        private static ProgramPointer UberStateController = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__uberSerialization.UberStateController.get_Instance", 0x90));
        private static ProgramPointer UberStateCollection = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__uberSerialization.UberStateCollection.GetGroup", 0x73));
        private static ProgramPointer DifficultyController = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.ConfirmChangingDifficulty.Perform", 0xdf));
        private static ProgramPointer NoPausePatch = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.None, "__mainWisp.GameController.OnApplicationFocus", 0x1b));
        private static ProgramPointer TargetFrameRatePatch = new ProgramPointer("UnityPlayer.dll",
            new FindPointerSignature(PointerVersion.All, AutoDeref.None, "660F6EC30F5BC0F30F5EC8????8BDE??????????660F6EC80F5BC90F57C00F297424300F2FC1720A", 0xb));
        private static ProgramPointer VSyncPatch = new ProgramPointer("UnityPlayer.dll", 
            new FindPointerSignature(PointerVersion.All, AutoDeref.None, "E8????????4863484C488B4030488D148948C1E2058B4402684883C428C3", -0x4));
        private static ProgramPointer FrameCounter = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.GameController.FixedUpdate", 0x1c8));
        private static ProgramPointer CheatsHandler = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.CheatsHandler.Awake", 0x7a));
        private static ProgramPointer DebugControls = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.AdvancedDebugMenuPage.DebugControlsSetter", 0x8e));
        public static PointerVersion Version { get; set; }
        public Process Program { get; set; }
        public bool IsHooked { get; set; }
        public DateTime LastHooked { get; set; }
        private bool? noPausePatched = null;
        private bool? targetFrameRatePatched = null;
        private bool? debugEnabled = null;
        private FPSTimer fpsTimer = new FPSTimer(200, 15);

        public MemoryManager() {
            LastHooked = DateTime.MinValue;
        }
        public string GamePointers() {
            return string.Concat(
                $"CHR: {Characters.GetPointer(Program)} ",
                $"GW: {GameWorld.GetPointer(Program)} ",
                $"PUS: {PlayerUberStateGroup.GetPointer(Program)} ",
                $"TSM: {TitleScreenManager.GetPointer(Program)} ",
                $"GSM: {GameStateMachine.GetPointer(Program)} ",
                $"GC: {GameController.GetPointer(Program)} ",
                $"SM: {ScenesManager.GetPointer(Program)} ",
                $"USC: {UberStateController.GetPointer(Program)} ",
                $"USL: {UberStateCollection.GetPointer(Program)} ",
                $"DC: {DifficultyController.GetPointer(Program)} ",
                $"NP: {NoPausePatch.GetPointer(Program)} ",
                $"TFR: {TargetFrameRatePatch.GetPointer(Program)} ",
                $"VS: {VSyncPatch.GetPointer(Program)} ",
                $"FC: {FrameCounter.GetPointer(Program)} ",
                $"CH: {CheatsHandler.GetPointer(Program)} ",
                $"DC: {DebugControls.GetPointer(Program)} "
            );
        }
        public bool AllPointersFound() {
            return Characters.GetPointer(Program) != IntPtr.Zero
                && GameWorld.GetPointer(Program) != IntPtr.Zero
                && PlayerUberStateGroup.GetPointer(Program) != IntPtr.Zero
                && TitleScreenManager.GetPointer(Program) != IntPtr.Zero
                && GameStateMachine.GetPointer(Program) != IntPtr.Zero
                && GameController.GetPointer(Program) != IntPtr.Zero
                && ScenesManager.GetPointer(Program) != IntPtr.Zero
                && UberStateController.GetPointer(Program) != IntPtr.Zero
                && UberStateCollection.GetPointer(Program) != IntPtr.Zero
                && DifficultyController.GetPointer(Program) != IntPtr.Zero
                && NoPausePatch.GetPointer(Program) != IntPtr.Zero
                && TargetFrameRatePatch.GetPointer(Program) != IntPtr.Zero
                && VSyncPatch.GetPointer(Program) != IntPtr.Zero
                && FrameCounter.GetPointer(Program) != IntPtr.Zero
                && CheatsHandler.GetPointer(Program) != IntPtr.Zero
                && DebugControls.GetPointer(Program) != IntPtr.Zero;
        }
        public bool DebugEnabled() {
            return CheatsHandler.Read<bool>(Program, 0xb8, 0x8);
        }
        public void EnableDebug(bool enable) {
            if (!debugEnabled.HasValue || enable != debugEnabled.Value) {
                if (CheatsHandler.GetPointer(Program) == IntPtr.Zero) { return; }

                DebugControls.Write<bool>(Program, enable, 0xb8, 0x8);
                CheatsHandler.Write<bool>(Program, enable, 0xb8, 0x0, 0x20);
                CheatsHandler.Write<short>(Program, enable ? (short)0x0101 : (short)0x0, 0xb8, 0x8);

                debugEnabled = enable;
            }
        }
        public string Patches() {
            return "NoPause: " + (!noPausePatched.HasValue ? "No Value" : noPausePatched.ToString()) + " FPS: " + (!targetFrameRatePatched.HasValue ? "No Value" : targetFrameRatePatched.ToString());
        }
        public bool NoPauseEnabled() {
            return NoPausePatch.Read<int>(Program) == 0x4890C5FF;
        }
        public void PatchNoPause(bool patch) {
            if (!noPausePatched.HasValue || patch != noPausePatched.Value) {
                if (NoPausePatch.GetPointer(Program) == IntPtr.Zero) { return; }

                if (patch) {
                    NoPausePatch.Write(Program, new byte[] { 0xFF, 0xC5, 0x90 });
                } else {
                    NoPausePatch.Write(Program, new byte[] { 0x0F, 0xB6, 0xEA });
                }
                noPausePatched = patch;
            }
        }
        public bool FPSLockEnabled() {
            return VSyncPatch.Read<uint>(Program) == 0x90C3C033;
        }
        public void PatchFPSLock(bool patch) {
            if (!targetFrameRatePatched.HasValue || patch != targetFrameRatePatched.Value) {
                if (TargetFrameRatePatch.GetPointer(Program) == IntPtr.Zero) { return; }

                if (patch) {
                    TargetFrameRatePatch.Write(Program, new byte[] { 0x90, 0x90, 0x8B, 0xDE, 0xB8, 0x3C, 0x00, 0x00, 0x00 });
                    VSyncPatch.Write(Program, new byte[] { 0x33, 0xC0, 0xC3, 0x90 });
                } else {
                    TargetFrameRatePatch.Write(Program, new byte[] { 0xEB, 0x0E, 0x8B, 0xDE, 0xE8, 0xE7, 0x1E, 0xFF, 0xFF });
                    VSyncPatch.Write(Program, new byte[] { 0x48, 0x83, 0xEC, 0x28 });
                }
                targetFrameRatePatched = patch;
            }
        }
        public int FrameCount() {
            return FrameCounter.Read<int>(Program, 0xb8, 0x0);
        }
        public float FPS() {
            return fpsTimer.FPS;
        }
        public int Difficulty() {
            //DifficultyController.Instance.Difficulty
            return DifficultyController.Read<int>(Program, 0xb8, 0x0, 0x20);
        }
        public void SetDifficulty(int difficulty) {
            //DifficultyController.Instance.Difficulty
            DifficultyController.Write<int>(Program, difficulty, 0xb8, 0x0, 0x20);
        }
        public Stats PlayerStats() {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Stats
            return PlayerUberStateGroup.Read<Stats>(Program, 0xb8, 0x0, 0x18, 0x30, 0x28, 0x10);
        }
        public int Keystones() {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Inventory.m_keystones
            return PlayerUberStateGroup.Read<int>(Program, 0xb8, 0x0, 0x18, 0x30, 0x18, 0x28);
        }
        public int Ore() {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Inventory.m_ore
            return PlayerUberStateGroup.Read<int>(Program, 0xb8, 0x0, 0x18, 0x30, 0x18, 0x34);
        }
        public Vector2 Position() {
            //Characters.Sein.PlatformBehaviour.PlatformMovement.m_prevPosition
            return Characters.Read<Vector2>(Program, 0xb8, 0x10, 0x98, 0x18, 0xd0);
        }
        public string CurrentScene() {
            //Scenes.Manager.m_currentScene.Scene
            int m_currentScene = FindIl2CppOffset.GetOffset(Program, "__mainWisp.ScenesManager.m_currentScene");
            return ScenesManager.Read(Program, 0xb8, 0x0, m_currentScene, 0x10, 0x0);
        }
        private static Dictionary<long, UberState> uberIDLookup = null;
        private void PopulateUberStates() {
            uberIDLookup = new Dictionary<long, UberState>();
            //UberStateCollection.Instance.m_descriptorsArray
            IntPtr descriptors = (IntPtr)UberStateCollection.Read<ulong>(Program, 0xb8, 0x10, 0x20);
            //.Count
            int descriptorsCount = Program.Read<int>(descriptors, 0x18);
            byte[] data = Program.Read(descriptors + 0x20, descriptorsCount * 0x8);
            for (int i = 0; i < descriptorsCount; i++) {
                //.m_descriptorsArray[i]
                IntPtr descriptor = (IntPtr)BitConverter.ToUInt64(data, i * 0x8);

                UberStateType type = UberStateType.SerializedBooleanUberState;
                Enum.TryParse<UberStateType>(Program.ReadAscii(descriptor, 0x0, 0x10, 0x0), out type);

                int groupOffset = 0x38;
                switch (type) {
                    case UberStateType.SerializedByteUberState:
                    case UberStateType.SerializedIntUberState:
                    case UberStateType.SavePedestalUberState: groupOffset = 0x30; break;
                    case UberStateType.PlayerUberStateDescriptor: groupOffset = 0x40; break;
                    case UberStateType.CountUberState:
                    case UberStateType.BooleanUberState:
                    case UberStateType.ByteUberState:
                    case UberStateType.IntUberState:
                    case UberStateType.ConditionUberState: continue;
                }

                //.m_descriptorsArray[i].ID.m_id
                int id = Program.Read<int>(descriptor, 0x18, 0x10);
                //.m_descriptorsArray[i].Name
                IntPtr namePtr = (IntPtr)Program.Read<ulong>(descriptor, 0x10, 0x48);
                string name = string.Empty;
                if (namePtr != IntPtr.Zero) {
                    name = Program.ReadAscii(namePtr);
                } else {
                    name = Program.ReadAscii(descriptor, 0x10, 0x50);
                }

                //.m_descriptorsArray[i].Group.ID.m_id
                int groupID = Program.Read<int>(descriptor, groupOffset, 0x18, 0x10);
                //.m_descriptorsArray[i].Group.Name
                namePtr = (IntPtr)Program.Read<ulong>(descriptor, groupOffset, 0x10, 0x48);
                string groupName = string.Empty;
                if (namePtr != IntPtr.Zero) {
                    groupName = Program.ReadAscii(namePtr);
                } else {
                    groupName = Program.ReadAscii(descriptor, groupOffset, 0x10, 0x50);
                }
                UberState uberState = new UberState() { Type = type, ID = id, Name = name, GroupID = groupID, GroupName = groupName };
                uberIDLookup.Add(((long)groupID << 32) | (long)id, uberState);
            }
        }
        public Dictionary<long, UberState> GetUberStates() {
            if (uberIDLookup == null) {
                PopulateUberStates();
            }

            UpdateUberState();

            return uberIDLookup;
        }
        public void UpdateUberState(UberState uberState = null) {
            //UbserStateController.m_currentStateValueStore.m_groupMap
            IntPtr groups = (IntPtr)UberStateController.Read<ulong>(Program, 0xb8, 0x40, 0x18);
            //.Count
            int groupCount = Program.Read<int>(groups, 0x20);
            //.Values
            groups = (IntPtr)Program.Read<ulong>(groups, 0x18);
            byte[] groupsData = Program.Read(groups + 0x20, groupCount * 0x18);

            bool updateAll = uberState == null;
            for (int i = 0; i < groupCount; i++) {
                //.Values[i].m_id.m_id
                IntPtr group = (IntPtr)BitConverter.ToUInt64(groupsData, 0x10 + (i * 0x18));
                byte[] groupData = Program.Read(group + 0x18, 48);
                long groupID = Program.Read<int>((IntPtr)BitConverter.ToUInt64(groupData, 0), 0x10);

                if (!updateAll && groupID != uberState.GroupID) { continue; }

                //.Values[i].m_objectStateMap
                IntPtr map = (IntPtr)BitConverter.ToUInt64(groupData, 8);
                //.Values[i].m_objectStateMap.Count
                int mapCount = Program.Read<int>(map, 0x20);
                if (mapCount > 0 && (updateAll || uberState.IsObjectType)) {
                    map = (IntPtr)Program.Read<ulong>(map, 0x18);
                    byte[] data = Program.Read(map + 0x20, mapCount * 0x18);
                    for (int j = 0; j < mapCount; j++) {
                        //.Values[i].m_objectStateMap.Keys[j]
                        long id = BitConverter.ToInt32(data, j * 0x18);

                        if (!updateAll && id != uberState.ID) { continue; }

                        if (!updateAll || uberIDLookup.TryGetValue((groupID << 32) | id, out uberState)) {
                            if (uberState.Type == UberStateType.SavePedestalUberState) {
                                uberState.Value.Byte = Program.Read<byte>((IntPtr)BitConverter.ToUInt64(data, 0x10 + (j * 0x18)), 0x11);
                            } else {
                                //playerUberStateDescriptor
                            }
                        }
                    }
                }

                //.Values[i].m_boolStateMap
                map = (IntPtr)BitConverter.ToUInt64(groupData, 16);
                //.Values[i].m_boolStateMap.Count
                mapCount = Program.Read<int>(map, 0x20);
                if (mapCount > 0 && (updateAll || uberState.IsBoolType)) {
                    map = (IntPtr)Program.Read<ulong>(map, 0x18);
                    byte[] data = Program.Read(map + 0x20, mapCount * 0x18);
                    for (int j = 0; j < mapCount; j++) {
                        //.Values[i].m_boolStateMap.Keys[j]
                        long id = BitConverter.ToInt32(data, j * 0x18);

                        if (!updateAll && id != uberState.ID) { continue; }

                        if (!updateAll || uberIDLookup.TryGetValue((groupID << 32) | id, out uberState)) {
                            uberState.Value.Bool = data[0x10 + (j * 0x18)] != 0;
                        }
                    }
                }

                //.Values[i].m_floatStateMap
                map = (IntPtr)BitConverter.ToUInt64(groupData, 24);
                //.Values[i].m_floatStateMap.Count
                mapCount = Program.Read<int>(map, 0x20);
                if (mapCount > 0 && (updateAll || uberState.IsFloatType)) {
                    map = (IntPtr)Program.Read<ulong>(map, 0x18);
                    byte[] data = Program.Read(map + 0x20, mapCount * 0x18);
                    for (int j = 0; j < mapCount; j++) {
                        //.Values[i].m_floatStateMap.Keys[j]
                        long id = BitConverter.ToInt32(data, j * 0x18);

                        if (!updateAll && id != uberState.ID) { continue; }

                        if (!updateAll || uberIDLookup.TryGetValue((groupID << 32) | id, out uberState)) {
                            uberState.Value.Float = BitConverter.ToSingle(data, 0x10 + (j * 0x18));
                        }
                    }
                }

                //.Values[i].m_intStateMap
                map = (IntPtr)BitConverter.ToUInt64(groupData, 32);
                //.Values[i].m_intStateMap.Count
                mapCount = Program.Read<int>(map, 0x20);
                if (mapCount > 0 && (updateAll || uberState.IsIntType)) {
                    map = (IntPtr)Program.Read<ulong>(map, 0x18);
                    byte[] data = Program.Read(map + 0x20, mapCount * 0x18);
                    for (int j = 0; j < mapCount; j++) {
                        //.Values[i].m_intStateMap.Keys[j]
                        long id = BitConverter.ToInt32(data, j * 0x18);

                        if (!updateAll && id != uberState.ID) { continue; }

                        if (!updateAll || uberIDLookup.TryGetValue((groupID << 32) | id, out uberState)) {
                            uberState.Value.Int = BitConverter.ToInt32(data, 0x10 + (j * 0x18));
                        }
                    }
                }

                //.Values[i].m_byteStateMap
                map = (IntPtr)BitConverter.ToUInt64(groupData, 40);
                //.Values[i].m_byteStateMap.Count
                mapCount = Program.Read<int>(map, 0x20);
                if (mapCount > 0 && (updateAll || uberState.IsByteType)) {
                    map = (IntPtr)Program.Read<ulong>(map, 0x18);
                    byte[] data = Program.Read(map + 0x20, mapCount * 0x18);
                    for (int j = 0; j < mapCount; j++) {
                        //.Values[i].m_byteStateMap.Keys[j]
                        long id = BitConverter.ToInt32(data, j * 0x18);

                        if (!updateAll && id != uberState.ID) { continue; }

                        if (!updateAll || uberIDLookup.TryGetValue((groupID << 32) | id, out uberState)) {
                            uberState.Value.Byte = data[0x10 + (j * 0x18)];
                        }
                    }
                }
            }
        }
        public bool HasAbility(AbilityType type) {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Abilities.m_abilitiesList
            IntPtr abilities = (IntPtr)PlayerUberStateGroup.Read<ulong>(Program, 0xb8, 0x0, 0x18, 0x30, 0x10, 0x18);
            //.Count
            int count = Program.Read<int>(abilities, 0x18);
            //.Items
            abilities = (IntPtr)Program.Read<ulong>(abilities, 0x10);
            byte[] data = Program.Read(abilities + 0x20, count * 0x8);
            for (int i = 0; i < count; i++) {
                //.Items[i]
                Ability ability = Program.Read<Ability>((IntPtr)BitConverter.ToUInt64(data, i * 0x8), 0x10);
                if (ability.Type == type) {
                    return ability.HasAbility == 1;
                }
            }
            return false;
        }
        public Dictionary<AbilityType, Ability> PlayerAbilities() {
            Dictionary<AbilityType, Ability> currentAbilities = new Dictionary<AbilityType, Ability>();
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Abilities.m_abilitiesList
            IntPtr abilities = (IntPtr)PlayerUberStateGroup.Read<ulong>(Program, 0xb8, 0x0, 0x18, 0x30, 0x10, 0x18);
            //.Count
            int count = Program.Read<int>(abilities, 0x18);
            //.Items
            abilities = (IntPtr)Program.Read<ulong>(abilities, 0x10);
            byte[] data = Program.Read(abilities + 0x20, count * 0x8);
            for (int i = 0; i < count; i++) {
                //.Items[i]
                Ability ability = Program.Read<Ability>((IntPtr)BitConverter.ToUInt64(data, i * 0x8), 0x10);
                if (Enum.IsDefined(typeof(AbilityType), ability.Type)) {
                    currentAbilities[ability.Type] = ability;
                }
            }
            return currentAbilities;
        }
        public bool HasShard(ShardType type, int level) {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Shards.m_shardsList
            IntPtr shards = (IntPtr)PlayerUberStateGroup.Read<ulong>(Program, 0xb8, 0x0, 0x18, 0x30, 0x20, 0x18);
            //.Count
            int count = Program.Read<int>(shards, 0x18);
            //.Items
            shards = (IntPtr)Program.Read<ulong>(shards, 0x10);
            byte[] data = Program.Read(shards + 0x20, count * 0x8);
            for (int i = 0; i < count; i++) {
                //.Items[i]
                Shard shard = Program.Read<Shard>((IntPtr)BitConverter.ToUInt64(data, i * 0x8), 0x10);
                if (shard.Type == type) {
                    return shard.Gained == 1 && (level == 0 || shard.Level == level);
                }
            }
            return false;
        }
        public Dictionary<ShardType, Shard> PlayerShards() {
            Dictionary<ShardType, Shard> currentShards = new Dictionary<ShardType, Shard>();
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Shards.m_shardsList
            IntPtr shards = (IntPtr)PlayerUberStateGroup.Read<ulong>(Program, 0xb8, 0x0, 0x18, 0x30, 0x20, 0x18);
            //.Count
            int count = Program.Read<int>(shards, 0x18);
            //.Items
            shards = (IntPtr)Program.Read<ulong>(shards, 0x10);
            byte[] data = Program.Read(shards + 0x20, count * 0x8);
            for (int i = 0; i < count; i++) {
                //.Items[i]
                Shard shard = Program.Read<Shard>((IntPtr)BitConverter.ToUInt64(data, i * 0x8), 0x10);
                if (Enum.IsDefined(typeof(ShardType), shard.Type)) {
                    currentShards[shard.Type] = shard;
                }
            }
            return currentShards;
        }
        public float MapCompletion(AreaType areaType = AreaType.None) {
            float totalCompletion = 0;
            //GameWorld.RuntimeAreas
            IntPtr areas = (IntPtr)GameWorld.Read<ulong>(Program, 0xb8, 0x0, 0x28);
            //.Count
            int count = Program.Read<int>(areas, 0x18);
            //.Items
            areas = (IntPtr)Program.Read<ulong>(areas, 0x10);
            byte[] data = Program.Read(areas + 0x20, count * 0x8);
            for (int i = 0; i < count; i++) {
                IntPtr area = (IntPtr)BitConverter.ToUInt64(data, i * 0x8);
                if (areaType != AreaType.None) {
                    //.Items[i].Area.WorldMapAreaUniqueID
                    AreaType type = Program.Read<AreaType>(area, 0x10, 0x20);
                    if (type == areaType) {
                        //.Items[i].m_completionAmount
                        return Program.Read<float>(area, 0x34) * 100f;
                    }
                } else {
                    //.Items[i].m_completionAmount
                    totalCompletion += Program.Read<float>(area, 0x34);
                }
            }
            return totalCompletion * 100f / count;
        }
        public AreaType PlayerArea() {
            //GameWorld.CurrentArea.Area.WorldMapAreaUniqueID
            return GameWorld.Read<AreaType>(Program, 0xb8, 0x0, 0x30, 0x10, 0x20);
        }
        public double ElapsedTime() {
            //GameController.Instance.Timer.CurrentTime
            return GameController.Read<double>(Program, 0xb8, 0x0, 0x28, 0x20);
        }
        public bool Dead() {
            //Characters.Sein.PlatformBehaviour.Mortality.DamageReciever.m_died
            return Characters.Read<bool>(Program, 0xb8, 0x10, 0x88, 0x10, 0xe4);
        }
        public GameState GameState() {
            //GameStateMachine.m_instance.CurrentState
            return (GameState)GameStateMachine.Read<int>(Program, 0xb8, 0x0, 0x10);
        }
        public Screen TitleScreen() {
            //TitleScreenManager.Instance.m_currentScreen
            int m_currentScreen = FindIl2CppOffset.GetOffset(Program, "__mainWisp.TitleScreenManager.m_currentScreen");
            return (Screen)TitleScreenManager.Read<int>(Program, 0xb8, 0x0, m_currentScreen);
        }
        public bool IsLoadingGame() {
            //GameController.FreezeFixedUpdate || GameController.Instance.m_isLoadingGame
            int m_isLoadingGame = FindIl2CppOffset.GetOffset(Program, "__mainWisp.GameController.m_isLoadingGame");
            return (FrameCounter.GetPointer(Program) != IntPtr.Zero && fpsTimer.FPSShort == 0) || GameController.Read<bool>(Program, 0xb8, 0xa) || GameController.Read<bool>(Program, 0xb8, 0x0, m_isLoadingGame);
        }
        public bool HookProcess() {
            IsHooked = Program != null && !Program.HasExited;
            if (!IsHooked && DateTime.Now > LastHooked.AddSeconds(1)) {
                LastHooked = DateTime.Now;
                ClearPointers();
                Process[] processes = Process.GetProcessesByName("OriWotW");
                Program = processes != null && processes.Length > 0 ? processes[0] : null;

                if (Program == null) {
                    processes = Process.GetProcessesByName("OriAndTheWillOfTheWisps");
                    Program = processes != null && processes.Length > 0 ? processes[0] : null;
                }

                if (Program == null) {
                    processes = Process.GetProcessesByName("OriAndTheWillOfTheWisps-PC");
                    Program = processes != null && processes.Length > 0 ? processes[0] : null;
                }

                if (Program != null && !Program.HasExited) {
                    MemoryReader.Update64Bit(Program);
                    FindIl2Cpp.InitializeIl2Cpp(Program);
                    uberIDLookup = null;
                    noPausePatched = null;
                    targetFrameRatePatched = null;
                    debugEnabled = null;
                    IsHooked = true;
                    fpsTimer.Reset();
                }
            }

            if (IsHooked) {
                fpsTimer.Update(FrameCount());
            } else {
                fpsTimer.Update(1);
            }
            return IsHooked;
        }
        private void ClearPointers() {
            Characters.ClearPointer();
            GameWorld.ClearPointer();
            PlayerUberStateGroup.ClearPointer();
            TitleScreenManager.ClearPointer();
            GameStateMachine.ClearPointer();
            GameController.ClearPointer();
            ScenesManager.ClearPointer();
            UberStateController.ClearPointer();
            UberStateCollection.ClearPointer();
            DifficultyController.ClearPointer();
            NoPausePatch.ClearPointer();
            TargetFrameRatePatch.ClearPointer();
            VSyncPatch.ClearPointer();
            FrameCounter.ClearPointer();
            CheatsHandler.ClearPointer();
            DebugControls.ClearPointer();
        }
        public void Dispose() {
            if (Program != null) {
                Program.Dispose();
            }
        }
    }
}