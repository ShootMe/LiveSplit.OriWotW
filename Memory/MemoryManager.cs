using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace LiveSplit.OriWotW {
    public partial class MemoryManager {
        private static ProgramPointer Characters = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.Characters.SetCurrentCharacter", 0x15c),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "488B80B80000004C8B40084D85C0743D488B15????????B90C000000E8????????488BF84885DB743C488B4B304885C9742D33D2E8????????4885C0741B48897818488B5C24504883C4405FC3", -0x4, 0x0));
        private static ProgramPointer GameWorld = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.V3, AutoDeref.Single, "__mainWisp.GameWorld.Awake", 0x79),
            new FindPointerSignature(PointerVersion.V3, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B80000004C8931498B4E28498B46204885C00F84????????4885C90F84????????4C8B05????????8B5018E8????????458BFD418BD5498B4E204885C9", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.V2, AutoDeref.Single, "__mainWisp.GameWorld.Awake", 0x79),
            new FindPointerSignature(PointerVersion.V2, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B80000004C8931498B4E28498B46204885C00F84????????4885C90F84????????4C8B05????????8B5018E8????????458BFD418BD5498B4E204885C9", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.GameWorld.Awake", 0xa7),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "4C8BDC55565741544155415641574883EC5049C743A8FEFFFFFF49895B104C8BE933ED", 0xa7, 0x0));
        private static ProgramPointer PlayerUberStateGroup = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.Seinlevel.get_PartialHealthContainers", 0x68),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "488B05????????488B88B8000000488B014885C0742C488B48184885C9741D33D2E8????????4885C07423488B40184885C074148B40384883C448C3", 0x3, 0x0));
        private static ProgramPointer TitleScreenManager = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.TitleScreenManager.Awake", 0x97),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B8000000488928488B05", 0x35, 0x0));
        private static ProgramPointer GameStateMachine = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.GameStateMachine.get_Instance", 0x6f),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B1D????????488B83B8000000488B004885C00F85C6000000488BCBE8????????488B43604885C074278B08E8", 0x14, 0x0));
        private static ProgramPointer GameController = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.GameController.Initialize", 0xc3),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "014C8975288B04244883EC20488D4C24308B0148894D20C785C0000000FFFFFFFF488B05????????F6802701000002741883B8D800000000750F488BC8", 0x45, 0x0));
        private static ProgramPointer ScenesManager = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.ScenesManager.Awake", 0x76),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488931488B1D????????488BCBE8????????488B43604885C074278B08E8????????483B05????????7517", 0x14, 0x0));
        private static ProgramPointer UberStateController = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__uberSerialization.UberStateController.get_Instance", 0x90),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B1D????????F6832701000002741883BBD800000000750F488BCBE8????????488B1D????????488B83B800000048837828000F85", 0x35, 0x0));
        private static ProgramPointer UberStateCollection = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__uberSerialization.UberStateCollection.GetGroup", 0x73),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B0D????????F6812701000002740E83B9D8000000007505E8????????33C9E8????????4885C07469488B58384885DB745A", 0x14, 0x0));
        private static ProgramPointer DifficultyController = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.ConfirmChangingDifficulty.Perform", 0xdf),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488B094885C974694533C08B5320E8????????488B05????????4885C07518", 0x14, 0x0));
        private static ProgramPointer NoPausePatch = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.None, "__mainWisp.GameController.OnApplicationFocus", 0x1b),
            new FindPointerSignature(PointerVersion.All, AutoDeref.None, "4C8BDC565741564883EC5049C743C8FEFFFFFF49895B1049896B18??????488BF14533F6443835????????754B488B05????????4C6380C0000000488B05????????418B8C00????????418B9400????????4D8973D04D8973D84D8973E04D8D43D0E8????????9033C9FF15????????90C605????????0180BE????????000F85????????4084ED0F85????????33C9E8????????4885C00F84????????33D2488BC8E8????????84C07561", 0x1b, 0x0));
        private static ProgramPointer FrameCounter = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.GameController.FixedUpdate", 0x1c8),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "80780A007538488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B8000000FF0033C9", 0x2a, 0x0));
        private static ProgramPointer CheatsHandler = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.V3, AutoDeref.Single, "__mainWisp.CheatsHandler.Awake", 0x7e),
            new FindPointerSignature(PointerVersion.V3, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B80000004C89??488B0D", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.CheatsHandler.Awake", 0x7a),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B80000004C89??488B0D", 0x14, 0x0));
        private static ProgramPointer DebugControls = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.AdvancedDebugMenuPage.DebugControlsSetter", 0x8e));
        private static ProgramPointer RaceSystem = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.RaceSystem.get_CurrentStateTime", 0x8f),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "4885C00F8499000000488B80280100004885C00F849B00000048837820007675488B0D????????F6812701000002740E83B9D8000000007505E8", 0x23, 0x0));
        public static PointerVersion Version { get; set; } = PointerVersion.All;
        public Process Program { get; set; }
        public bool IsHooked { get; set; }
        public DateTime LastHooked { get; set; }
        private bool? noPausePatched = null;
        private bool? debugEnabled = null;
        private FPSTimer fpsTimer = new FPSTimer(200, 15);
        private static Dictionary<long, UberState> uberIDLookup = null;

        public MemoryManager() {
            LastHooked = DateTime.MinValue;
        }
        public string GamePointers() {
            return string.Concat(
                $"CHR: {(ulong)Characters.GetPointer(Program):X} ",
                $"GW: {(ulong)GameWorld.GetPointer(Program):X} ",
                $"PUS: {(ulong)PlayerUberStateGroup.GetPointer(Program):X} ",
                $"TSM: {(ulong)TitleScreenManager.GetPointer(Program):X} ",
                $"GSM: {(ulong)GameStateMachine.GetPointer(Program):X} ",
                $"GC: {(ulong)GameController.GetPointer(Program):X} ",
                $"SM: {(ulong)ScenesManager.GetPointer(Program):X} ",
                $"USC: {(ulong)UberStateController.GetPointer(Program):X} ",
                $"USL: {(ulong)UberStateCollection.GetPointer(Program):X} ",
                $"DC: {(ulong)DifficultyController.GetPointer(Program):X} ",
                $"NP: {(ulong)NoPausePatch.GetPointer(Program):X} ",
                $"FC: {(ulong)FrameCounter.GetPointer(Program):X} ",
                $"CH: {(ulong)CheatsHandler.GetPointer(Program):X} ",
                $"DC: {(ulong)DebugControls.GetPointer(Program):X} ",
                $"RS: {(ulong)RaceSystem.GetPointer(Program):X} "
            );
        }
        public float RaceTime() {
            return RaceSystem.Read<float>(Program, 0xb8, 0x0, 0x28, 0x18);
        }
        public float LastRaceTime() {
            return RaceSystem.Read<float>(Program, 0xb8, 0x0, 0x140, 0x104);
        }
        public float PersonalBestTime() {
            return RaceSystem.Read<float>(Program, 0xb8, 0x0, 0x28, 0x1c);
        }
        public bool DebugEnabled() {
            return CheatsHandler.Read<bool>(Program, 0xb8, 0x0, 0x20);
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
            return "NoPause: " + (!noPausePatched.HasValue ? "No Value" : noPausePatched.ToString()) + " Debug: " + DebugEnabled().ToString();
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
            //int m_currentScene = FindIl2CppOffset.GetOffset(Program, "__mainWisp.ScenesManager.m_currentScene");
            int m_currentScene = Version == PointerVersion.All ? 0x180 : 0x190;
            return ScenesManager.Read(Program, 0xb8, 0x0, m_currentScene, 0x10, 0x0);
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
        public bool IsLoadingGame(GameState state) {
            if (FrameCounter.GetPointer(Program) != IntPtr.Zero && fpsTimer.FPSShort == 0) {
                return true;
            }
            //int m_isLoadingGame = FindIl2CppOffset.GetOffset(Program, "__mainWisp.GameController.m_isLoadingGame");
            int m_isLoadingGame = Version == PointerVersion.All ? 0x103 : 0x10b;
            //GameController.FreezeFixedUpdate || GameController.Instance.m_isLoadingGame
            if (GameController.Read<bool>(Program, 0xb8, 0xa) || GameController.Read<bool>(Program, 0xb8, 0x0, m_isLoadingGame)) {
                return true;
            }
            string scene = CurrentScene();
            return (state == OriWotW.GameState.Game && (scene == "wotwTitleScreen" || scene == "kuFlyAway"))
                || ((state == OriWotW.GameState.TitleScreen || state == OriWotW.GameState.StartScreen) && scene == "wotwTitleScreen");
        }
        private void PopulateUberStates() {
            uberIDLookup = new Dictionary<long, UberState>();
            //UberStateCollection.Instance.m_descriptorsArray
            IntPtr descriptors = UberStateCollection.Read<IntPtr>(Program, 0xb8, 0x10, 0x20);
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
                IntPtr namePtr = Program.Read<IntPtr>(descriptor, 0x10, 0x48);
                string name = string.Empty;
                if (namePtr != IntPtr.Zero) {
                    name = Program.ReadAscii(namePtr);
                } else {
                    name = Program.ReadAscii(descriptor, 0x10, 0x50);
                }

                //.m_descriptorsArray[i].Group.ID.m_id
                int groupID = Program.Read<int>(descriptor, groupOffset, 0x18, 0x10);
                //.m_descriptorsArray[i].Group.Name
                namePtr = Program.Read<IntPtr>(descriptor, groupOffset, 0x10, 0x48);
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
            IntPtr groups = UberStateController.Read<IntPtr>(Program, 0xb8, 0x40, 0x18);
            //.Count
            int groupCount = Program.Read<int>(groups, 0x20);
            //.Values
            groups = Program.Read<IntPtr>(groups, 0x18);
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
                    map = Program.Read<IntPtr>(map, 0x18);
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
                    map = Program.Read<IntPtr>(map, 0x18);
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
                    map = Program.Read<IntPtr>(map, 0x18);
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
                    map = Program.Read<IntPtr>(map, 0x18);
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
                    map = Program.Read<IntPtr>(map, 0x18);
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
            IntPtr abilities = PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, 0x18, 0x30, 0x10, 0x18);
            //.Count
            int count = Program.Read<int>(abilities, 0x18);
            //.Items
            abilities = Program.Read<IntPtr>(abilities, 0x10);
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
            IntPtr abilities = PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, 0x18, 0x30, 0x10, 0x18);
            //.Count
            int count = Program.Read<int>(abilities, 0x18);
            //.Items
            abilities = Program.Read<IntPtr>(abilities, 0x10);
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
            IntPtr shards = PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, 0x18, 0x30, 0x20, 0x18);
            //.Count
            int count = Program.Read<int>(shards, 0x18);
            //.Items
            shards = Program.Read<IntPtr>(shards, 0x10);
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
            IntPtr shards = PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, 0x18, 0x30, 0x20, 0x18);
            //.Count
            int count = Program.Read<int>(shards, 0x18);
            //.Items
            shards = Program.Read<IntPtr>(shards, 0x10);
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
            IntPtr areas = GameWorld.Read<IntPtr>(Program, 0xb8, 0x0, 0x28);
            //.Count
            int count = Program.Read<int>(areas, 0x18);
            //.Items
            areas = Program.Read<IntPtr>(areas, 0x10);
            byte[] data = Program.Read(areas + 0x20, count * 0x8);
            int completionOffset = Version == PointerVersion.V3 ? 0x5c : 0x34;
            for (int i = 0; i < count; i++) {
                IntPtr area = (IntPtr)BitConverter.ToUInt64(data, i * 0x8);
                if (areaType != AreaType.None) {
                    //.Items[i].Area.WorldMapAreaUniqueID
                    AreaType type = Program.Read<AreaType>(area, 0x10, 0x20);
                    if (type == areaType) {
                        //.Items[i].m_completionAmount
                        return Program.Read<float>(area, completionOffset) * 100f;
                    }
                } else {
                    //.Items[i].m_completionAmount
                    totalCompletion += Program.Read<float>(area, completionOffset);
                }
            }
            return totalCompletion * 100f / (count == 0 ? 1 : count);
        }
        public bool HookProcess() {
            IsHooked = Program != null && !Program.HasExited;
            if (!IsHooked && DateTime.Now > LastHooked.AddSeconds(1)) {
                LastHooked = DateTime.Now;

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
                    Module64 module = Program.Module64("GameAssembly.dll");
                    MemoryManager.Version = PointerVersion.All;
                    if (module != null) {
                        switch (module.MemorySize) {
                            case 77447168: MemoryManager.Version = PointerVersion.V2; break;
                            case 77844480: MemoryManager.Version = PointerVersion.V3; break;
                        }
                    }
                    uberIDLookup = null;
                    noPausePatched = null;
                    debugEnabled = null;
                    IsHooked = true;
                    fpsTimer.Reset();
                }
            }

            fpsTimer.Update(IsHooked ? FrameCount() : 0);
            return IsHooked;
        }
        public void Dispose() {
            if (Program != null) {
                Program.Dispose();
            }
        }
    }
}