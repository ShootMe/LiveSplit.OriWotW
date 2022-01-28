using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace LiveSplit.OriWotW {
    public partial class MemoryManager {
        private static ProgramPointer Characters = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.P4, AutoDeref.Single, "__mainWisp.Characters.SetCurrentCharacter", 0x9b),
            new FindPointerSignature(PointerVersion.P4, AutoDeref.Single, "9033C9FF15????????90C605????????01488B0D????????F6812701000002740E83B9D8000000007505E8????????33D2488BCBE8????????488B05????????488B88B8000000C641040133C9", 0x3b, 0x0),
            new FindIl2Cpp(PointerVersion.P3, AutoDeref.Single, "__mainWisp.Characters.SetCurrentCharacter", 0x9b),
            new FindPointerSignature(PointerVersion.P3, AutoDeref.Single, "9033C9FF15????????90C605????????01488B0D????????F6812701000002740E83B9D8000000007505E8????????33D2488BCBE8????????488B05????????488B88B8000000C641040133C9", 0x3b, 0x0),
            new FindIl2Cpp(PointerVersion.P2, AutoDeref.Single, "__mainWisp.Characters.SetCurrentCharacter", 0x15c),
            new FindPointerSignature(PointerVersion.P2, AutoDeref.Single, "488B80B80000004C8B40084D85C0743D488B15????????B90C000000E8????????488BF84885DB743C488B4B304885C9742D33D2E8????????4885C0741B48897818488B5C24504883C4405FC3", -0x4, 0x0),
            new FindIl2Cpp(PointerVersion.P1, AutoDeref.Single, "__mainWisp.Characters.SetCurrentCharacter", 0x15c),
            new FindPointerSignature(PointerVersion.P1, AutoDeref.Single, "488B80B80000004C8B40084D85C0743D488B15????????B90C000000E8????????488BF84885DB743C488B4B304885C9742D33D2E8????????4885C0741B48897818488B5C24504883C4405FC3", -0x4, 0x0),
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.Characters.SetCurrentCharacter", 0x15c),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "488B80B80000004C8B40084D85C0743D488B15????????B90C000000E8????????488BF84885DB743C488B4B304885C9742D33D2E8????????4885C0741B48897818488B5C24504883C4405FC3", -0x4, 0x0));
        private static ProgramPointer GameWorld = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.P4, AutoDeref.Single, "__mainWisp.GameWorld.Awake", 0x79),
            new FindPointerSignature(PointerVersion.P4, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B80000004C8931498B4E??498B46??4885C00F84????????4885C90F84????????4C8B05????????8B5018E8????????458BFD418BD5498B4E", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P3, AutoDeref.Single, "__mainWisp.GameWorld.Awake", 0x79),
            new FindPointerSignature(PointerVersion.P3, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B80000004C8931498B4E??498B46??4885C00F84????????4885C90F84????????4C8B05????????8B5018E8????????458BFD418BD5498B4E", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P2, AutoDeref.Single, "__mainWisp.GameWorld.Awake", 0x79),
            new FindPointerSignature(PointerVersion.P2, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B80000004C8931498B4E??498B46??4885C00F84????????4885C90F84????????4C8B05????????8B5018E8????????458BFD418BD5498B4E", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P1, AutoDeref.Single, "__mainWisp.GameWorld.Awake", 0x79),
            new FindPointerSignature(PointerVersion.P1, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B80000004C8931498B4E??498B46??4885C00F84????????4885C90F84????????4C8B05????????8B5018E8????????458BFD418BD5498B4E", 0x14, 0x0),
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
            new FindIl2Cpp(PointerVersion.P4, AutoDeref.Single, "__mainWisp.GameController.Initialize", 0xc6),
            new FindIl2Cpp(PointerVersion.P3, AutoDeref.Single, "__mainWisp.GameController.Initialize", 0xc6),
            new FindIl2Cpp(PointerVersion.P2, AutoDeref.Single, "__mainWisp.GameController.Initialize", 0xc3),
            new FindPointerSignature(PointerVersion.P2, AutoDeref.Single, "014C8975288B04244883EC20488D4C24308B0148894D20C785C0000000FFFFFFFF488B05????????F6802701000002741883B8D800000000750F488BC8", 0x45, 0x0),
            new FindIl2Cpp(PointerVersion.P1, AutoDeref.Single, "__mainWisp.GameController.Initialize", 0xc3),
            new FindPointerSignature(PointerVersion.P1, AutoDeref.Single, "014C8975288B04244883EC20488D4C24308B0148894D20C785C0000000FFFFFFFF488B05????????F6802701000002741883B8D800000000750F488BC8", 0x45, 0x0),
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.GameController.Initialize", 0xc3),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "014C8975288B04244883EC20488D4C24308B0148894D20C785C0000000FFFFFFFF488B05????????F6802701000002741883B8D800000000750F488BC8", 0x45, 0x0));
        private static ProgramPointer ScenesManager = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.P4, AutoDeref.Single, "__mainWisp.ScenesManager.Awake", 0x75),
            new FindPointerSignature(PointerVersion.P4, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488931488B1D????????488BCBE8????????488B43604885C074278B08E8????????483B05????????7517", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P3, AutoDeref.Single, "__mainWisp.ScenesManager.Awake", 0x75),
            new FindPointerSignature(PointerVersion.P3, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488931488B1D????????488BCBE8????????488B43604885C074278B08E8????????483B05????????7517", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P2, AutoDeref.Single, "__mainWisp.ScenesManager.Awake", 0x76),
            new FindPointerSignature(PointerVersion.P2, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488931488B1D????????488BCBE8????????488B43604885C074278B08E8????????483B05????????7517", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P1, AutoDeref.Single, "__mainWisp.ScenesManager.Awake", 0x76),
            new FindPointerSignature(PointerVersion.P1, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488931488B1D????????488BCBE8????????488B43604885C074278B08E8????????483B05????????7517", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.ScenesManager.Awake", 0x76),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488931488B1D????????488BCBE8????????488B43604885C074278B08E8????????483B05????????7517", 0x14, 0x0));
        private static ProgramPointer UberStateController = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__uberSerialization.UberStateController.get_Instance", 0x90),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B1D????????F6832701000002741883BBD800000000750F488BCBE8????????488B1D????????488B83B800000048837828000F85", 0x35, 0x0));
        private static ProgramPointer UberStateCollection = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.P4, AutoDeref.Single, "__uberSerialization.UberStateCollection.get_Instance", 0x8b),
            new FindIl2Cpp(PointerVersion.P3, AutoDeref.Single, "__uberSerialization.UberStateCollection.get_Instance", 0x8b),
            new FindIl2Cpp(PointerVersion.P2, AutoDeref.Single, "__uberSerialization.UberStateCollection.GetGroup", 0x73),
            new FindPointerSignature(PointerVersion.P2, AutoDeref.Single, "9033C9FF15????????90C605????????01488B0D????????F6812701000002740E83B9D8000000007505E8????????33C9E8????????4885C07469488B58384885DB745A", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P1, AutoDeref.Single, "__uberSerialization.UberStateCollection.GetGroup", 0x73),
            new FindPointerSignature(PointerVersion.P1, AutoDeref.Single, "9033C9FF15????????90C605????????01488B0D????????F6812701000002740E83B9D8000000007505E8????????33C9E8????????4885C07469488B58384885DB745A", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__uberSerialization.UberStateCollection.GetGroup", 0x73),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B0D????????F6812701000002740E83B9D8000000007505E8????????33C9E8????????4885C07469488B58384885DB745A", 0x14, 0x0));
        private static ProgramPointer DifficultyController = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.P4, AutoDeref.Single, "__mainWisp.ConfirmChangingDifficulty.Perform", 0xdf),
            new FindPointerSignature(PointerVersion.P4, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488B094885C9747B4533C08B5338E8????????807B29007531C6432901488B05", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P3, AutoDeref.Single, "__mainWisp.ConfirmChangingDifficulty.Perform", 0xdf),
            new FindPointerSignature(PointerVersion.P3, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488B094885C9747B4533C08B5338E8????????807B29007531C6432901488B05", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P2, AutoDeref.Single, "__mainWisp.ConfirmChangingDifficulty.Perform", 0xdf),
            new FindPointerSignature(PointerVersion.P2, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488B094885C974694533C08B5320E8????????488B05????????4885C07518", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P1, AutoDeref.Single, "__mainWisp.ConfirmChangingDifficulty.Perform", 0xdf),
            new FindPointerSignature(PointerVersion.P1, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488B094885C974694533C08B5320E8????????488B05????????4885C07518", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.ConfirmChangingDifficulty.Perform", 0xdf),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????488B88B8000000488B094885C974694533C08B5320E8????????488B05????????4885C07518", 0x14, 0x0));
        private static ProgramPointer NoPausePatch = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.None, "__mainWisp.GameController.OnApplicationFocus", 0x1b),
            new FindPointerSignature(PointerVersion.All, AutoDeref.None, "4C8BDC565741564883EC5049C743C8FEFFFFFF49895B1049896B18??????488BF14533F6443835????????754B488B05????????4C6380C0000000488B05????????418B8C00????????418B9400????????4D8973D04D8973D84D8973E04D8D43D0E8????????9033C9FF15????????90C605????????0180BE????????000F85????????4084ED0F85????????33C9E8????????4885C00F84????????33D2488BC8E8????????84C07561", 0x1b, 0x0));
        private static ProgramPointer FrameCounter = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.P4, AutoDeref.Single, "__mainWisp.GameController.FixedUpdate", 0x1c5),
            new FindIl2Cpp(PointerVersion.P3, AutoDeref.Single, "__mainWisp.GameController.FixedUpdate", 0x1c5),
            new FindIl2Cpp(PointerVersion.P2, AutoDeref.Single, "__mainWisp.GameController.FixedUpdate", 0x1c8),
            new FindPointerSignature(PointerVersion.P2, AutoDeref.Single, "80780A007538488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B8000000FF0033C9", 0x2a, 0x0),
            new FindIl2Cpp(PointerVersion.P1, AutoDeref.Single, "__mainWisp.GameController.FixedUpdate", 0x1c8),
            new FindPointerSignature(PointerVersion.P1, AutoDeref.Single, "80780A007538488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B8000000FF0033C9", 0x2a, 0x0),
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.GameController.FixedUpdate", 0x1c8),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "80780A007538488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B8000000FF0033C9", 0x2a, 0x0));
        private static ProgramPointer CheatsHandler = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.P4, AutoDeref.Single, "__mainWisp.CheatsHandler.Awake", 0x7e),
            new FindPointerSignature(PointerVersion.P4, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B80000004C89??488B0D", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P3, AutoDeref.Single, "__mainWisp.CheatsHandler.Awake", 0x7e),
            new FindPointerSignature(PointerVersion.P3, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B80000004C89??488B0D", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.P2, AutoDeref.Single, "__mainWisp.CheatsHandler.Awake", 0x7e),
            new FindPointerSignature(PointerVersion.P2, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B80000004C89??488B0D", 0x14, 0x0),
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.CheatsHandler.Awake", 0x7a),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B80000004C89??488B0D", 0x14, 0x0));
        private static ProgramPointer DebugControls = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.AdvancedDebugMenuPage.DebugControlsSetter", 0x8e));
        private static ProgramPointer RaceSystem = new ProgramPointer("GameAssembly.dll",
            new FindIl2Cpp(PointerVersion.All, AutoDeref.Single, "__mainWisp.RaceSystem.get_CurrentStateTime", 0x8f),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "4885C00F8499000000488B80280100004885C00F849B00000048837820007675488B0D????????F6812701000002740E83B9D8000000007505E8", 0x23, 0x0));
        private static ProgramPointer GameSettings = new ProgramPointer("GameAssembly.dll",
            new FindPointerSignature(PointerVersion.P4, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B800000048893833D2488BCF", 0x14, 0x0),
            new FindPointerSignature(PointerVersion.P3, AutoDeref.Single, "9033C9FF15????????90C605????????01488B05????????F6802701000002741883B8D800000000750F488BC8E8????????488B05????????488B80B800000048893833D2488BCF", 0x14, 0x0),
            new FindPointerSignature(PointerVersion.P2, AutoDeref.Single, "448975E848C745EC??000000C645F400488D4DC8E8????????488905????????4885C00F84????????FFD0C64718014889471033C9E8????????4533C08BD0488BCFE8", 0x4a, 0x0),
            new FindPointerSignature(PointerVersion.P1, AutoDeref.Single, "448975E848C745EC??000000C645F400488D4DC8E8????????488905????????4885C00F84????????FFD0C64718014889471033C9E8????????4533C08BD0488BCFE8", 0x4a, 0x0),
            new FindPointerSignature(PointerVersion.All, AutoDeref.Single, "448975E848C745EC??000000C645F400488D4DC8E8????????488905????????4885C00F84????????FFD0C64718014889471033C9E8????????4533C08BD0488BCFE8", 0x4a, 0x0));
        public static PointerVersion Version { get; set; } = PointerVersion.All;
        public Process Program { get; set; }
        public Module64 GameAssembly { get; set; }
        public bool IsHooked { get; set; }
        public DateTime LastHooked { get; set; }
        public ControlScheme LastControlScheme { get; set; }
        public int ControllerCounter { get; set; } = 0;
        private bool? noPausePatched = null;
        private bool? debugEnabled = null;
        private FPSTimer fpsTimer = new FPSTimer(200, 15);
        public Fader CurrentFader = new Fader();
        private PointerCache playerUberStateGroup = new PointerCache();
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
                $"RS: {(ulong)RaceSystem.GetPointer(Program):X} ",
                $"GS: {(ulong)GameSettings.GetPointer(Program):X} "
            );
        }
        public bool IsRacing() {
            //RaceSystem.Instance.m_timer.m_startedRace
            int m_timer = Version <= PointerVersion.P2 ? 0x28 : 0x40;
            int m_startedRace = Version <= PointerVersion.P2 ? 0x4c : 0x64;
            return RaceSystem.Read<bool>(Program, 0xb8, 0x0, m_timer, m_startedRace);
        }
        public float RaceTime() {
            //RaceSystem.Instance.m_timer.ElapsedTime
            int m_timer = Version <= PointerVersion.P2 ? 0x28 : 0x40;
            int m_elapsedTime = Version <= PointerVersion.P2 ? 0x18 : 0x30;
            return RaceSystem.Read<float>(Program, 0xb8, 0x0, m_timer, m_elapsedTime);
        }
        public float LastRaceTime() {
            //RaceSystem.Instance.Context.LastRaceTime
            int m_Context = Version <= PointerVersion.P2 ? 0x140 : 0x168;
            int lastRaceTime = Version <= PointerVersion.P2 ? 0x104 : 0x114;
            return RaceSystem.Read<float>(Program, 0xb8, 0x0, m_Context, lastRaceTime);
        }
        public float PersonalBestTime() {
            //RaceSystem.Instance.m_timer.PersonalBestTime
            int m_timer = Version <= PointerVersion.P2 ? 0x28 : 0x40;
            int m_personalBestTime = Version <= PointerVersion.P2 ? 0x1c : 0x34;
            return RaceSystem.Read<float>(Program, 0xb8, 0x0, m_timer, m_personalBestTime);
        }
        public bool RaceCountdownFinished() {
            //RaceSystem.Instance.m_states
            int m_states = Version <= PointerVersion.P2 ? 0x150 : 0x178;
            IntPtr states = RaceSystem.Read<IntPtr>(Program, 0xb8, 0x0, m_states, 0x10);
            int count = Program.Read<int>(states, 0x18);
            if (count > 5) {
                //RaceSystem.Instance.m_states[6].m_countdownFinished
                return Program.Read<bool>(states, 0x20 + (6 * 0x8), 0x18);
            }
            return false;
        }
        public RaceHandler GetRaceHandler() {
            int m_Context = Version <= PointerVersion.P2 ? 0x140 : 0x168;
            //RaceSystem.Instance.Context.Configuration.Handler
            IntPtr handler = RaceSystem.Read<IntPtr>(Program, 0xb8, 0x0, m_Context, 0x18, 0x20);
            int data = Version <= PointerVersion.P2 ? 0x18 : 0x30;
            int m_inProgress = Version <= PointerVersion.P2 ? 0x21 : 0x39;
            int raceInProgressState = Version <= PointerVersion.P2 ? 0x60 : 0x80;
            int m_value = Version <= PointerVersion.P2 ? 0x40 : 0x49;
            return new RaceHandler() {
                InProgress = Program.Read<bool>(handler, m_inProgress),
                RaceInProgressState = Program.Read<bool>(handler, data, raceInProgressState, m_value)
            };
        }
        public RaceStateMachineContext GetRaceStateContext() {
            int m_Context = Version <= PointerVersion.P2 ? 0x140 : 0x168;
            int userRequestedRetry = Version <= PointerVersion.P2 ? 0xd0 : 0xe0;
            //RaceSystem.Instance.Context
            IntPtr context = RaceSystem.Read<IntPtr>(Program, 0xb8, 0x0, m_Context);
            return new RaceStateMachineContext() {
                StopReason = Program.Read<RaceStopReason>(context, 0x3c),
                UserRequestedRetry = Program.Read<bool>(context, userRequestedRetry)
            };
        }
        public bool DebugEnabled() {
            //CheatsHandler.Instance.DebugEnabled
            int m_debugEnabled = Version <= PointerVersion.P2 ? 0x20 : 0x38;
            return CheatsHandler.Read<bool>(Program, 0xb8, 0x0, m_debugEnabled);
        }
        public void EnableDebug(bool enable) {
            if (!debugEnabled.HasValue || enable != debugEnabled.Value) {
                if (CheatsHandler.GetPointer(Program) == IntPtr.Zero) { return; }

                // Disable infinite energy - Characters.sein.Energy.InfiniteEnergy
                int energy = Version <= PointerVersion.P2 ? 0x80 : 0x98;
                Characters.Write<bool>(Program, false, 0xb8, 0x10, energy, 0x0, 0xb8, 0x0);

                DebugControls.Write<bool>(Program, enable, 0xb8, 0x8);
                //CheatsHandler.Instance.DebugEnabled
                int m_debugEnabled = Version <= PointerVersion.P2 ? 0x20 : 0x38;
                CheatsHandler.Write<bool>(Program, enable, 0xb8, 0x0, m_debugEnabled);
                //CheatsHandler.DebugWasEnabled/DebugAlwaysEnabled
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
            int m_difficulty = Version <= PointerVersion.P2 ? 0x20 : 0x38;
            return DifficultyController.Read<int>(Program, 0xb8, 0x0, m_difficulty);
        }
        public void SetDifficulty(int difficulty) {
            //DifficultyController.Instance.Difficulty
            int m_difficulty = Version <= PointerVersion.P2 ? 0x20 : 0x38;
            DifficultyController.Write<int>(Program, difficulty, 0xb8, 0x0, m_difficulty);
        }
        public Stats PlayerStats() {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Stats
            int playerUberState = Version <= PointerVersion.P2 ? 0x18 : 0x30;
            playerUberStateGroup.Update(PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, playerUberState, 0x30));
            return Program.Read<Stats>(playerUberStateGroup, 0x28, 0x10);
        }
        public int Keystones() {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Inventory.m_keystones
            int playerUberState = Version <= PointerVersion.P2 ? 0x18 : 0x30;
            playerUberStateGroup.Update(PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, playerUberState, 0x30));
            return Program.Read<int>(playerUberStateGroup, 0x18, 0x28);
        }
        public int AllocatedKeystones() {
            //Characters.Sein.Inventory.m_allocatedKeystones.Count
            int inventory = Version <= PointerVersion.P2 ? 0x60 : 0x78;
            int m_allocatedKeystones = Version <= PointerVersion.P2 ? 0x28 : 0x40;
            return Characters.Read<int>(Program, 0xb8, 0x10, inventory, m_allocatedKeystones, 0x18, 0x30);
        }
        public int Ore() {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Inventory.m_ore
            int playerUberState = Version <= PointerVersion.P2 ? 0x18 : 0x30;
            playerUberStateGroup.Update(PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, playerUberState, 0x30));
            return Program.Read<int>(playerUberStateGroup, 0x18, 0x34);
        }
        public Vector2 Position() {
            //Characters.Sein.PlatformBehaviour.PlatformMovement.m_prevPosition
            int platformBehaviour = Version <= PointerVersion.P2 ? 0x98 : 0xb0;
            int platformMovement = Version <= PointerVersion.P2 ? 0x18 : 0x30;
            int m_prevPosition = Version <= PointerVersion.P2 ? 0xd0 : 0xe8;
            return Characters.Read<Vector2>(Program, 0xb8, 0x10, platformBehaviour, platformMovement, m_prevPosition);
        }
        public string CurrentScene() {
            //Scenes.Manager.m_currentScene.Scene
            //int m_currentScene = FindIl2CppOffset.GetOffset(Program, "__mainWisp.ScenesManager.m_currentScene");
            int m_currentScene = Version < PointerVersion.P1 ? 0x180 : Version <= PointerVersion.P2 ? 0x190 : 0x1b8;
            return ScenesManager.Read(Program, 0xb8, 0x0, m_currentScene, 0x10, 0x0);
        }
        public AreaType PlayerArea() {
            //GameWorld.CurrentArea.Area.WorldMapAreaUniqueID
            int currentArea = Version <= PointerVersion.P2 ? 0x30 : 0x48;
            int worldMapAreaUniqueID = Version <= PointerVersion.P2 ? 0x20 : 0x38;
            return GameWorld.Read<AreaType>(Program, 0xb8, 0x0, currentArea, 0x10, worldMapAreaUniqueID);
        }
        public double ElapsedTime() {
            //GameController.Instance.Timer.CurrentTime
            int m_timer = Version <= PointerVersion.P2 ? 0x28 : 0x40;
            int m_currentTime = Version <= PointerVersion.P2 ? 0x20 : 0x38;
            return GameController.Read<double>(Program, 0xb8, 0x0, m_timer, m_currentTime);
        }
        public bool Dead() {
            //Characters.Sein.Mortality.DamageReciever.m_died
            int mortality = Version <= PointerVersion.P2 ? 0x88 : 0xa0;
            int m_died = Version <= PointerVersion.P2 ? 0xe4 : 0x110;
            return Characters.Read<bool>(Program, 0xb8, 0x10, mortality, 0x10, m_died);
        }
        public GameState GameState() {
            //GameStateMachine.m_instance.CurrentState
            return GameStateMachine.Read<GameState>(Program, 0xb8, 0x0, 0x10);
        }
        public Screen TitleScreen() {
            //TitleScreenManager.Instance.m_currentScreen
            int m_currentScreen = Version <= PointerVersion.P2 ? 0xb8 : 0xf0;
            return (Screen)TitleScreenManager.Read<int>(Program, 0xb8, 0x0, m_currentScreen);
        }
        public ControlScheme GetControlScheme() {
            //GameSettings.Instance.m_currentControlSchemes
            int m_currentControlSchemes = Version <= PointerVersion.P2 ? 0x94 : 0xd0;
            return GameSettings.Read<ControlScheme>(Program, 0xb8, 0x0, m_currentControlSchemes);
        }
        public bool FaderPause() {
            int currentFrameCount = FrameCount();
            float fadeTimer = -1.0f;
            FadeState fadeState = FadeState.Null;

            //UI -> UI.static -> UI.Fader -> FaderB.m_stateCurrentTime
            //UI -> UI.static -> UI.Fader -> FaderB.m_currentTimelineFaderType
            switch (Version) {
                case PointerVersion.P1:
                    fadeTimer = MemoryReader.Read<float>(Program, GameAssembly.BaseAddress, 0x043E6650, 0xb8, 0x10, 0x50);
                    fadeState = (FadeState)MemoryReader.Read<int>(Program, GameAssembly.BaseAddress, 0x043E6650, 0xb8, 0x10, 0x68);
                    break;
                case PointerVersion.P2:
                    fadeTimer = MemoryReader.Read<float>(Program, GameAssembly.BaseAddress, 0x04464828, 0xb8, 0x10, 0x50);
                    fadeState = (FadeState)MemoryReader.Read<int>(Program, GameAssembly.BaseAddress, 0x04464828, 0xb8, 0x10, 0x68);
                    break;

                case PointerVersion.P3:
                case PointerVersion.P4:
                    fadeTimer = MemoryReader.Read<float>(Program, GameAssembly.BaseAddress, 0x04783338, 0xb8, 0x10, 0x78);
                    fadeState = (FadeState)MemoryReader.Read<int>(Program, GameAssembly.BaseAddress, 0x04783338, 0xb8, 0x10, 0x94);
                    break;
            }

            CurrentFader.ShouldPause(fadeTimer, currentFrameCount);
            CurrentFader.UpdateFadeState(fadeState);

            return CurrentFader.IsPaused();
        }
        public bool IsLoadingGame(GameState state, bool running) {
            UberState finishedIntroTopSwamp = GetUberState(21786, 48748);
            if (finishedIntroTopSwamp != null) {
                UpdateUberState(finishedIntroTopSwamp);
                CurrentFader.CheckStartQTM(finishedIntroTopSwamp.Value.Bool, state, running);
            }

            ControlScheme currentControlScheme = GetControlScheme();
            if (LastControlScheme != currentControlScheme) {
                ControllerCounter = 0;
            }
            LastControlScheme = currentControlScheme;
            ControllerCounter++;

            if (FrameCounter.GetPointer(Program) != IntPtr.Zero && fpsTimer.FPSShort == 0 && ControllerCounter > 30) {
                return true;
            }

            //int m_isLoadingGame = FindIl2CppOffset.GetOffset(Program, "__mainWisp.GameController.m_isLoadingGame");
            int m_isLoadingGame = Version < PointerVersion.P1 ? 0x103 : Version <= PointerVersion.P2 ? 0x10b : 0x123;
            //GameController.FreezeFixedUpdate || GameController.Instance.m_isLoadingGame
            if (GameController.Read<bool>(Program, 0xb8, 0xa) || GameController.Read<bool>(Program, 0xb8, 0x0, m_isLoadingGame)) {
                return true;
            }

            if (FaderPause()) {
                return true;
            }
            return (state == OriWotW.GameState.TitleScreen || state == OriWotW.GameState.StartScreen) && CurrentScene() == "wotwTitleScreen";
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

                UberStateType type;
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
        public UberState GetUberState(int groupId, int id) {
            if (uberIDLookup == null || uberIDLookup.Count == 0) {
                PopulateUberStates();
            }

            if (uberIDLookup.TryGetValue(((long)groupId << 32) | (long)id, out UberState value)) {
                return value;
            }

            return null;
        }
        public void UpdateUberState(UberState uberState = null) {
            if (uberState != null && uberState.GroupPointer != IntPtr.Zero) {
                int groupID = Program.Read<int>(uberState.GroupPointer);
                int id = Program.Read<int>(uberState.IDPointer);
                uberState.PointerTimer++;
                if (groupID == uberState.GroupID && id == uberState.ID && uberState.PointerTimer < 40) {
                    switch (uberState.Type) {
                        case UberStateType.SavePedestalUberState: uberState.Value.Byte = Program.Read<byte>(uberState.ValuePointer); break;
                        case UberStateType.SerializedBooleanUberState: uberState.Value.Bool = Program.Read<byte>(uberState.ValuePointer) != 0; break;
                        case UberStateType.SerializedFloatUberState: uberState.Value.Float = Program.Read<float>(uberState.ValuePointer); break;
                        case UberStateType.SerializedIntUberState: uberState.Value.Int = Program.Read<int>(uberState.ValuePointer); break;
                        case UberStateType.SerializedByteUberState: uberState.Value.Byte = Program.Read<byte>(uberState.ValuePointer); break;
                    }
                    return;
                }
                uberState.PointerTimer = 0;
            }

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
                IntPtr groupPtr = (IntPtr)BitConverter.ToUInt64(groupData, 0) + 0x10;
                long groupID = Program.Read<int>(groupPtr);

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
                            uberState.GroupPointer = groupPtr;
                            uberState.IDPointer = map + 0x20 + (j * 0x18);
                            if (uberState.Type == UberStateType.SavePedestalUberState) {
                                uberState.ValuePointer = (IntPtr)(BitConverter.ToUInt64(data, 0x10 + (j * 0x18)) + 0x11);
                                uberState.Value.Byte = Program.Read<byte>(uberState.ValuePointer);
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
                            uberState.GroupPointer = groupPtr;
                            uberState.IDPointer = map + 0x20 + (j * 0x18);
                            uberState.ValuePointer = map + 0x30 + (j * 0x18);
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
                            uberState.GroupPointer = groupPtr;
                            uberState.IDPointer = map + 0x20 + (j * 0x18);
                            uberState.ValuePointer = map + 0x30 + (j * 0x18);
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
                            uberState.GroupPointer = groupPtr;
                            uberState.IDPointer = map + 0x20 + (j * 0x18);
                            uberState.ValuePointer = map + 0x30 + (j * 0x18);
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
                            uberState.GroupPointer = groupPtr;
                            uberState.IDPointer = map + 0x20 + (j * 0x18);
                            uberState.ValuePointer = map + 0x30 + (j * 0x18);
                            uberState.Value.Byte = data[0x10 + (j * 0x18)];
                        }
                    }
                }
            }
        }
        public bool HasAbility(AbilityType type) {
            //PlayerUberStateGroup.Instance.PlayerUberState.m_state.Abilities.m_abilitiesList
            int playerUberState = Version <= PointerVersion.P2 ? 0x18 : 0x30;
            playerUberStateGroup.Update(PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, playerUberState, 0x30));
            IntPtr abilities = Program.Read<IntPtr>(playerUberStateGroup, 0x10, 0x18);
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
            int playerUberState = Version <= PointerVersion.P2 ? 0x18 : 0x30;
            playerUberStateGroup.Update(PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, playerUberState, 0x30));
            IntPtr abilities = Program.Read<IntPtr>(playerUberStateGroup, 0x10, 0x18);

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
            int playerUberState = Version <= PointerVersion.P2 ? 0x18 : 0x30;
            playerUberStateGroup.Update(PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, playerUberState, 0x30));
            IntPtr shards = Program.Read<IntPtr>(playerUberStateGroup, 0x20, 0x18);
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
            int playerUberState = Version <= PointerVersion.P2 ? 0x18 : 0x30;
            playerUberStateGroup.Update(PlayerUberStateGroup.Read<IntPtr>(Program, 0xb8, 0x0, playerUberState, 0x30));
            IntPtr shards = Program.Read<IntPtr>(playerUberStateGroup, 0x20, 0x18);
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

            int worldMapAreaUniqueID = Version <= PointerVersion.P2 ? 0x20 : 0x38;
            int runtimeAreas = Version <= PointerVersion.P2 ? 0x28 : 0x40;
            int m_completionAmount = Version <= PointerVersion.P1 ? 0x34 : 0x5c;

            //GameWorld.RuntimeAreas
            IntPtr areas = GameWorld.Read<IntPtr>(Program, 0xb8, 0x0, runtimeAreas);
            //.Count
            int count = Program.Read<int>(areas, 0x18);
            //.Items
            areas = Program.Read<IntPtr>(areas, 0x10);
            byte[] data = Program.Read(areas + 0x20, count * 0x8);
            for (int i = 0; i < count; i++) {
                IntPtr area = (IntPtr)BitConverter.ToUInt64(data, i * 0x8);
                if (areaType != AreaType.None) {
                    //.Items[i].Area.WorldMapAreaUniqueID
                    AreaType type = Program.Read<AreaType>(area, 0x10, worldMapAreaUniqueID);
                    if (type == areaType) {
                        //.Items[i].m_completionAmount
                        return Program.Read<float>(area, m_completionAmount) * 100f;
                    }
                } else {
                    //.Items[i].m_completionAmount
                    totalCompletion += Program.Read<float>(area, m_completionAmount);
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
                    Module64 gameAssembly = Program.Module64("GameAssembly.dll");
                    GameAssembly = Program.Module64("GameAssembly.dll");
                    MemoryManager.Version = PointerVersion.All;
                    if (gameAssembly != null) {
                        switch (gameAssembly.MemorySize) {
                            case 77447168: MemoryManager.Version = PointerVersion.P1; break;
                            case 77844480: MemoryManager.Version = PointerVersion.P2; break;
                            case 81121280: MemoryManager.Version = PointerVersion.P3; break;
                            case 81129472: MemoryManager.Version = PointerVersion.P4; break;
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