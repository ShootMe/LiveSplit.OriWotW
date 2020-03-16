using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace LiveSplit.OriWotW {
    public partial class MemoryManager {
        //__mainWisp.Game.Characters.SetCurrentCharacter
        private static ProgramPointer Characters = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "488B80B80000004C8B40084D85C0743D488B15????????B90C000000E8????????488BF84885DB743C488B4B304885C9742D33D2E8????????4885C0741B48897818488B5C24504883C4405FC3", -0x4));
        //__mainWisp.GameWorld.Awake
        private static ProgramPointer GameWorld = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "4C8BDC55565741544155415641574883EC5049C743A8FEFFFFFF49895B104C8BE933ED", 0xa7));
        //__mainWisp.Seinlevel.get_PartialHealthContainers
        private static ProgramPointer PlayerUberStateGroup = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "488B05????????488B88B8000000488B014885C0742C488B48184885C9741D33D2E8????????4885C07423488B40184885C074148B40384883C448C3", 0x3));
        //__mainWisp.TitleScreenManager.Awake
        private static ProgramPointer TitleScreenManager = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "9033C9FF15????????90C605????????01488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B8000000488928488B05", 0x35));
        //__mainWisp.GameStateMachine.get_Instance
        private static ProgramPointer GameStateMachine = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "9033C9FF15????????90C605????????01488B1D????????488B83B8000000488B004885C00F85C6000000488BCBE8????????488B43604885C074278B08E8", 0x14));
        //__mainWisp.GameController.OnGameAwake
        private static ProgramPointer GameController = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "014C8975288B04244883EC20488D4C24308B0148894D20C785C0000000FFFFFFFF488B05????????F6802701000002741883B8D800000000750F488BC8", 0x45));
        //__mainWisp.SeinWorldState.Awake
        private static ProgramPointer SeinWorldState = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488939BA0E000000488B0D????????E8????????488BD8488B77184885C0", 0x14));
        //__mainWisp.ScenesManager.Awake
        private static ProgramPointer ScenesManager = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488931488B1D????????488BCBE8????????488B43604885C074278B08E8????????483B05????????7517", 0x14));
        //__uberSerialization.Moon.UberStateController.get_Instance
        private static ProgramPointer UberStateController = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "9033C9FF15????????90C605????????01488B1D????????F6832701000002741883BBD800000000750F488BCBE8????????488B1D????????488B83B800000048837828000F85", 0x35));
        //__uberSerialization.Moon.UberStateCollection.GetState
        private static ProgramPointer UberStateCollection = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "9033C9FF15????????90C605????????01488B0D????????F6812701000002740E83B9D8000000007505E8????????33C9E8????????4885C07469488B58384885DB745A", 0x14));
        //__mainWisp.ConfirmChangingDifficulty.Perform
        private static ProgramPointer DifficultyController = new ProgramPointer(AutoDeref.Single, new ProgramSignature(PointerVersion.V1, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488B094885C974694533C08B5320E8????????488B05????????4885C07518", 0x14));
        public Process Program { get; set; }
        public bool IsHooked { get; set; }
        public DateTime LastHooked { get; set; }

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
                $"SWS: {SeinWorldState.GetPointer(Program)} ",
                $"SM: {ScenesManager.GetPointer(Program)} ",
                $"USC: {UberStateController.GetPointer(Program)} ",
                $"USL: {UberStateCollection.GetPointer(Program)} ",
                $"DC: {DifficultyController.GetPointer(Program)} "
            );
        }
        public int Difficulty() {
            //DifficultyController.Instance.Difficulty
            return DifficultyController.Read<int>(Program, 0xb8, 0x0, 0x20);
        }
        public void SetDifficulty(int difficulty) {
            //DifficultyController.Instance.Difficulty
            DifficultyController.Write<int>(Program, difficulty, 0xb8, 0x0, 0x20);
        }
        public int MaxEnergy() {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Stats.m_maxEnergy
            return (int)PlayerUberStateGroup.Read<float>(Program, 0xb8, 0x0, 0x18, 0x30, 0x28, 0x1c) * 2;
        }
        public int MaxHealth() {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Stats.m_maxHealth
            return PlayerUberStateGroup.Read<int>(Program, 0xb8, 0x0, 0x18, 0x30, 0x28, 0x14) / 5;
        }
        public int HealthFragments() {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Inventory.m_partialHealthContainers
            return PlayerUberStateGroup.Read<int>(Program, 0xb8, 0x0, 0x18, 0x30, 0x18, 0x38);
        }
        public int EnergyFragments() {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Inventory.m_partialEnergyContainers
            return PlayerUberStateGroup.Read<int>(Program, 0xb8, 0x0, 0x18, 0x30, 0x18, 0x3c);
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
            return ScenesManager.Read(Program, 0xb8, 0x0, 0x180, 0x10, 0x0);
        }
        public List<WorldStateValue> WorldStates() {
            List<WorldStateValue> currentStates = new List<WorldStateValue>();
            //SeinWorldState.Instance
            foreach (WorldState key in Enum.GetValues(typeof(WorldState))) {
                WorldStateValue value = GetWorldState(key, true);
                if (value != null) {
                    currentStates.Add(value);
                }
            }

            currentStates.Sort(delegate (WorldStateValue one, WorldStateValue two) {
                return one.State.CompareTo(two.State);
            });
            return currentStates;
        }
        public WorldStateValue GetWorldState(WorldState worldState, bool readName = false) {
            int value = 0;
            string description = string.Empty;
            if (worldState == WorldState.DarknessLifted || worldState == WorldState.MistLifted || worldState == WorldState.WaterPurified || worldState == WorldState.KwolokDead) {
                value = SeinWorldState.Read<byte>(Program, 0xb8, 0x0, 0x8 * (int)worldState, 0x40);
                if (readName) {
                    description = value > 0 ? "Completed" : string.Empty;
                }
            } else {
                value = SeinWorldState.Read<int>(Program, 0xb8, 0x0, 0x8 * (int)worldState, 0x38);
                if (value > 0 && readName) {
                    description = SeinWorldState.Read(Program, 0xb8, 0x0, 0x8 * (int)worldState, 0x40, 0x10, 0x20 + (value * 0x8), 0x10, 0x0);
                }
            }

            if (value > 0) {
                return new WorldStateValue() { State = worldState, Value = value, Description = description };
            }
            return null;
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
                    case UberStateType.CountUberState:
                    case UberStateType.SerializedIntUberState:
                    case UberStateType.SavePedestalUberState: groupOffset = 0x30; break;
                    case UberStateType.ConditionUberState: groupOffset = 0x28; break;
                    case UberStateType.PlayerUberStateDescriptor: groupOffset = 0x40; break;
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

                uberIDLookup.Add(((long)groupID << 32) | (long)id, new UberState() { Type = type, ID = id, Name = name, GroupID = groupID, GroupName = groupName });
            }
        }
        public Dictionary<long, UberState> GetUberStates() {
            if (uberIDLookup == null) {
                PopulateUberStates();
            }

            //UbserStateController.m_currentStateValueStore.m_groupMap
            IntPtr groups = (IntPtr)UberStateController.Read<ulong>(Program, 0xb8, 0x40, 0x18);
            //.Count
            int groupCount = Program.Read<int>(groups, 0x20);
            //.Values
            groups = (IntPtr)Program.Read<ulong>(groups, 0x18);
            byte[] groupsData = Program.Read(groups + 0x20, groupCount * 0x18);
            for (int i = 0; i < groupCount; i++) {
                //.Values[i].m_id.m_id
                IntPtr group = (IntPtr)BitConverter.ToUInt64(groupsData, 0x10 + (i * 0x18));
                byte[] groupData = Program.Read(group + 0x18, 48);
                long groupID = Program.Read<int>((IntPtr)BitConverter.ToUInt64(groupData, 0), 0x10);

                //.Values[i].m_objectStateMap
                IntPtr map = (IntPtr)BitConverter.ToUInt64(groupData, 8);
                //.Values[i].m_objectStateMap.Count
                int mapCount = Program.Read<int>(map, 0x20);
                if (mapCount > 0) {
                    map = (IntPtr)Program.Read<ulong>(map, 0x18);
                    byte[] data = Program.Read(map + 0x20, mapCount * 0x18);
                    for (int j = 0; j < mapCount; j++) {
                        //.Values[i].m_objectStateMap.Keys[j]
                        long id = BitConverter.ToInt32(data, j * 0x18);

                        UberState uberState = null;
                        if (uberIDLookup.TryGetValue((groupID << 32) | id, out uberState)) {
                            if (uberState.Name.IndexOf("savePedestal", StringComparison.OrdinalIgnoreCase) >= 0) {
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
                if (mapCount > 0) {
                    map = (IntPtr)Program.Read<ulong>(map, 0x18);
                    byte[] data = Program.Read(map + 0x20, mapCount * 0x18);
                    for (int j = 0; j < mapCount; j++) {
                        //.Values[i].m_boolStateMap.Keys[j]
                        long id = BitConverter.ToInt32(data, j * 0x18);

                        UberState uberState = null;
                        if (uberIDLookup.TryGetValue((groupID << 32) | id, out uberState)) {
                            uberState.Value.Bool = data[0x10 + (j * 0x18)] != 0;
                        }
                    }
                }

                //.Values[i].m_floatStateMap
                map = (IntPtr)BitConverter.ToUInt64(groupData, 24);
                //.Values[i].m_floatStateMap.Count
                mapCount = Program.Read<int>(map, 0x20);
                if (mapCount > 0) {
                    map = (IntPtr)Program.Read<ulong>(map, 0x18);
                    byte[] data = Program.Read(map + 0x20, mapCount * 0x18);
                    for (int j = 0; j < mapCount; j++) {
                        //.Values[i].m_floatStateMap.Keys[j]
                        long id = BitConverter.ToInt32(data, j * 0x18);

                        UberState uberState = null;
                        if (uberIDLookup.TryGetValue((groupID << 32) | id, out uberState)) {
                            uberState.Value.Float = BitConverter.ToSingle(data, 0x10 + (j * 0x18));
                        }
                    }
                }

                //.Values[i].m_intStateMap
                map = (IntPtr)BitConverter.ToUInt64(groupData, 32);
                //.Values[i].m_intStateMap.Count
                mapCount = Program.Read<int>(map, 0x20);
                if (mapCount > 0) {
                    map = (IntPtr)Program.Read<ulong>(map, 0x18);
                    byte[] data = Program.Read(map + 0x20, mapCount * 0x18);
                    for (int j = 0; j < mapCount; j++) {
                        //.Values[i].m_intStateMap.Keys[j]
                        long id = BitConverter.ToInt32(data, j * 0x18);

                        UberState uberState = null;
                        if (uberIDLookup.TryGetValue((groupID << 32) | id, out uberState)) {
                            uberState.Value.Int = BitConverter.ToInt32(data, 0x10 + (j * 0x18));
                        }
                    }
                }

                //.Values[i].m_byteStateMap
                map = (IntPtr)BitConverter.ToUInt64(groupData, 40);
                //.Values[i].m_byteStateMap.Count
                mapCount = Program.Read<int>(map, 0x20);
                if (mapCount > 0) {
                    map = (IntPtr)Program.Read<ulong>(map, 0x18);
                    byte[] data = Program.Read(map + 0x20, mapCount * 0x18);
                    for (int j = 0; j < mapCount; j++) {
                        //.Values[i].m_byteStateMap.Keys[j]
                        long id = BitConverter.ToInt32(data, j * 0x18);

                        UberState uberState = null;
                        if (uberIDLookup.TryGetValue((groupID << 32) | id, out uberState)) {
                            uberState.Value.Byte = data[0x10 + (j * 0x18)];
                        }
                    }
                }
            }

            return uberIDLookup;
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
        public bool HasShard(ShardType type) {
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
                    return shard.Gained == 1;
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
            return (Screen)TitleScreenManager.Read<int>(Program, 0xb8, 0x0, 0xb8);
        }
        public bool IsLoadingGame() {
            //GameController.Instance.m_isLoadingGame
            return GameController.Read<bool>(Program, 0xb8, 0x0, 0x103);
        }
        public bool HookProcess() {
            IsHooked = Program != null && !Program.HasExited;
            if (!IsHooked && DateTime.Now > LastHooked.AddSeconds(1)) {
                LastHooked = DateTime.Now;
                Process[] processes = Process.GetProcessesByName("OriWotW");
                Program = processes != null && processes.Length > 0 ? processes[0] : null;

                if (Program != null && !Program.HasExited) {
                    MemoryReader.Update64Bit(Program);
                    uberIDLookup = null;
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