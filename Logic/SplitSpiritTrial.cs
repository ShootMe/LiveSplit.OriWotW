using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum SplitSpiritTrial {
        [Description("Baurs Reach (Activate)")]
        BaursReachActivate,
        [Description("Baurs Reach (Complete)")]
        BaursReachComplete,
        [Description("Inkwater Marsh (Activate)")]
        InkwaterMarshActivate,
        [Description("Inkwater Marsh (Complete)")]
        InkwaterMarshComplete,
        [Description("Kwoloks Hollow (Activate)")]
        KwoloksHollowActivate,
        [Description("Kwoloks Hollow (Complete)")]
        KwoloksHollowComplete,
        [Description("Luma Pools (Activate)")]
        LumaPoolsActivate,
        [Description("Luma Pools (Complete)")]
        LumaPoolsComplete,
        [Description("Mouldwood Depths (Activate)")]
        MouldwoodDepthsActivate,
        [Description("Mouldwood Depths (Complete)")]
        MouldwoodDepthsComplete,
        [Description("Silent Woods (Activate)")]
        SilentWoodsActivate,
        [Description("Silent Woods (Complete)")]
        SilentWoodsComplete,
        [Description("The Wellspring (Activate)")]
        WellspringActivate,
        [Description("The Wellspring (Complete)")]
        WellspringComplete,
        [Description("Windswept Wastes (Activate)")]
        WindsweptWastesActivate,
        [Description("Windswept Wastes (Complete)")]
        WindsweptWastesComplete
    }
    public enum SplitRace {
        [Description("Race Has Started")]
        RaceHasStarted,
        [Description("Race Has Finished")]
        RaceHasFinished,
        [Description("Race Has Started Auto Reset")]
        RaceHasStartedAutoReset,
        [Description("Race Has Finished Auto Stop")]
        RaceHasFinishedAutoStop
    }
    public struct RaceState {
        public bool RaceHasStarted;
        public RaceStopReason LastReason;
    }
    public enum RaceStopReason {
        None = 0,
        Finished = 1,
        Timeout = 2,
        Death = 3,
        SpectatingFinished = 4,
        TechnicalFailure = 5,
        UserAction = 6
    }
    public struct RaceHandler {
        public bool RaceInProgressState;
        public bool InProgress;
    }
    public struct RaceStateMachineContext {
        public bool UserRequestedRetry;
        public RaceStopReason StopReason;
    }
}