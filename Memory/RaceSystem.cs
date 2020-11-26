using System;
using System.Runtime.InteropServices;
namespace LiveSplit.OriWotW {
    public enum RaceStopReason {
        None = 0, // 0x0
        Finished = 1, // 0x0
        Timeout = 2, // 0x0
        Death = 3, // 0x0
        SpectatingFinished = 4, // 0x0
        TechnicalFailure = 5, // 0x0
        UserAction = 6, // 0x0
    }
    public struct RaceSystemPtr {
        public IntPtr m_timer;
        public IntPtr Context;
        public IntPtr m_states;
    }

    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceSystemPtrV2 {
        [FieldOffset(40)]
        public IntPtr m_timer;
        [FieldOffset(320)]
        public IntPtr Context;
        [FieldOffset(336)]
        public IntPtr m_states;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceSystemPtrV3V4 {
        [FieldOffset(0x40)]
        public IntPtr m_timer;
        [FieldOffset(0x168)]
        public IntPtr Context;
        [FieldOffset(0x178)]
        public IntPtr m_states;
    }

    public struct m_timer {
        public float ElapsedTime;
        public float PersonalBestTime;
        public float BestTime;
        public float TimeLimit;
        public float TimeToBeat;
        public float PreviousElapsedTime;
        public float m_adjustedElapsedTime;
        public bool m_startedRace;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct m_timerV2 {
        [FieldOffset(24)]
        public float ElapsedTime;
        [FieldOffset(28)]
        public float PersonalBestTime;
        [FieldOffset(32)]
        public float BestTime;
        [FieldOffset(36)]
        public float TimeLimit;
        [FieldOffset(40)]
        public float TimeToBeat;
        [FieldOffset(44)]
        public float PreviousElapsedTime;
        [FieldOffset(76)]
        public bool m_startedRace;
        [FieldOffset(0x48)]
        public float m_adjustedElapsedTime;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct m_timerV3V4 {
        [FieldOffset(0x30)]
        public float ElapsedTime;
        [FieldOffset(0x34)]
        public float PersonalBestTime;
        [FieldOffset(0x3C)]
        public float BestTime;
        [FieldOffset(0x38)]
        public float TimeLimit;
        [FieldOffset(0x40)]
        public float TimeToBeat;
        [FieldOffset(0x44)]
        public float PreviousElapsedTime;
        [FieldOffset(0x64)]
        public bool m_startedRace;
        [FieldOffset(0x60)]
        public float m_adjustedElapsedTime;
    }

    public struct RaceCountdownStatePtr {
        public bool m_countdownFinished;
        public IntPtr m_countdownTimeline;
        public float CurrentTime;
        public bool m_markerInitialized;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceCountdownStatePtrV2 {
        [FieldOffset(24)]
        public bool m_countdownFinished;
        [FieldOffset(32)]
        public IntPtr m_countdownTimeline;
        [FieldOffset(184)]
        public float CurrentTime;
        [FieldOffset(256)]
        public bool m_markerInitialized;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceCountdownStatePtrV3V4 {
        [FieldOffset(0x18)]
        public bool m_countdownFinished;
        [FieldOffset(0x20)]
        public IntPtr m_countdownTimeline;
    }

    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct MoonTimelineV2 {
        [FieldOffset(48)]
        public bool PlayState;
        [FieldOffset(180)]
        public bool StartMode;
        [FieldOffset(184)]
        public float CurrentTime;
        [FieldOffset(232)]
        public bool m_isFinished;
        [FieldOffset(256)]
        public bool m_markerInitialized;
        [FieldOffset(257)]
        public bool m_contentEnd;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct MoonTimelineV3V4 {
        [FieldOffset(0x08)]
        public bool PlayState;
        [FieldOffset(0xCC)]
        public bool StartMode;
        [FieldOffset(0xD0)]
        public float CurrentTime;
        [FieldOffset(0x100)]
        public bool m_isFinished;
        [FieldOffset(0x118)]
        public bool m_markerInitialized;
        [FieldOffset(0x119)]
        public bool m_contentEnd;
    }

    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceConfiguration {
        [FieldOffset(24)]
        public IntPtr Race;
        [FieldOffset(32)]
        public IntPtr Handler;
    }

    public struct RaceStateMachineContext {
        public IntPtr Configuration;
        public RaceStopReason StopReason;
        public float DeltaTime;
        public bool UserRequestedRetry;
        public bool UserRequestedWatchReplay;
        public bool UserRequestedLeaderboard;
        public float LastRaceTime;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceStateMachineContextV2 {
        [FieldOffset(0x18)]
        public IntPtr Configuration;
        [FieldOffset(60)]
        public RaceStopReason StopReason;
        [FieldOffset(64)]
        public float DeltaTime;
        [FieldOffset(208)]
        public bool UserRequestedRetry;
        [FieldOffset(209)]
        public bool UserRequestedWatchReplay;
        [FieldOffset(210)]
        public bool UserRequestedLeaderboard;
        [FieldOffset(0x104)]
        public float LastRaceTime;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceStateMachineContextV3V4 {
        [FieldOffset(0x18)]
        public IntPtr Configuration;
        [FieldOffset(0x3C)]
        public RaceStopReason StopReason;
        [FieldOffset(0x40)]
        public float DeltaTime;
        [FieldOffset(0xE0)]
        public bool UserRequestedRetry;
        [FieldOffset(0xE1)]
        public bool UserRequestedWatchReplay;
        [FieldOffset(0xE2)]
        public bool UserRequestedLeaderboard;
        [FieldOffset(0x114)]
        public float LastRaceTime;
    }

    public struct RaceHandlerPtr {
        public RaceData Data;
        public bool m_initialized;
        public bool m_inProgress;
        public bool m_inStartProximity;
        public bool m_inEndProximity;
        public bool m_isSyncing;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceHandlerV2 {
        [FieldOffset(0x18)]
        public IntPtr Data;
        [FieldOffset(0x20)]
        public bool m_initialized;
        [FieldOffset(0x21)]
        public bool m_inProgress;
        [FieldOffset(0x22)]
        public bool m_inStartProximity;
        [FieldOffset(0x23)]
        public bool m_inEndProximity;
        [FieldOffset(0x24)]
        public bool m_isSyncing;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceHandlerV3V4 {
        [FieldOffset(0x30)]
        public IntPtr Data;
        [FieldOffset(0x38)]
        public bool m_initialized;
        [FieldOffset(0x39)]
        public bool m_inProgress;
        [FieldOffset(0x3A)]
        public bool m_inStartProximity;
        [FieldOffset(0x3B)]
        public bool m_inEndProximity;
        [FieldOffset(0x3C)]
        public bool m_isSyncing;
    }

    public struct RaceData {
        public int m_raceState;
        public bool RaceInProgressState;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceDataV2 {
        [FieldOffset(0x18)]
        public IntPtr m_raceState;
        [FieldOffset(0x60)]
        public IntPtr RaceInProgressState;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceDataV3V4 {
        [FieldOffset(0x30)]
        public IntPtr m_raceState;
        [FieldOffset(0x80)]
        public IntPtr RaceInProgressState;
    }

    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct MoonRaceV2 {
        [FieldOffset(104)]
        public IntPtr m_startTransform;
        [FieldOffset(120)]
        public IntPtr m_endTransform;
        [FieldOffset(176)]
        public IntPtr CountdownTimeline;
        [FieldOffset(232)]
        public IntPtr StartZoneChecker;
        [FieldOffset(240)]
        public IntPtr EndZoneChecker;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct MoonRaceV3V4 {
        [FieldOffset(0x88)]
        public IntPtr m_startTransform;
        [FieldOffset(0x98)]
        public IntPtr m_endTransform;
        [FieldOffset(0xD0)]
        public IntPtr CountdownTimeline;
        [FieldOffset(0x108)]
        public IntPtr StartZoneChecker;
        [FieldOffset(0x110)]
        public IntPtr EndZoneChecker;
    }

    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct RaceHandlerV2V3V4 {
        [FieldOffset(48)]
        public Vector2 m_oriStartRacePosition;
    }

    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct ObjectInsideZoneCheckerV2 {
        [FieldOffset(24)]
        public Rect m_bounds;
        [FieldOffset(40)]
        public IntPtr ExternalTransform;
        [FieldOffset(48)]
        public Vector2 Size;
        [FieldOffset(56)]
        public Vector2 Anchor;
        [FieldOffset(64)]
        public float checkResultDelay;
        [FieldOffset(68)]
        public IntPtr EditorColor;
        [FieldOffset(88)]
        public bool OnlyTriggerIfGrounded;
    }
    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct ObjectInsideZoneCheckerV3V4 {
        [FieldOffset(0x30)]
        public Rect m_bounds;
        [FieldOffset(0x40)]
        public IntPtr ExternalTransform;
        [FieldOffset(0x48)]
        public Vector2 Size;
        [FieldOffset(0x50)]
        public Vector2 Anchor;
        [FieldOffset(0x58)]
        public float checkResultDelay;
        [FieldOffset(0x5C)]
        public IntPtr EditorColor;
    }

    [StructLayout(LayoutKind.Explicit, Size = 200, Pack = 1)]
    public struct Rect {
        [FieldOffset(0)]
        public float m_XMin;
        [FieldOffset(4)]
        public float m_YMin;
        [FieldOffset(8)]
        public float m_Width;
        [FieldOffset(12)]
        public float m_Height;
    }

    public class RaceSystemV2 {
        public m_timerV2 Timer;
        public MoonTimelineV2 CountdownTimeline;
    }
    public class RaceSystemV3V4 {
        public m_timerV3V4 Timer;
        public MoonTimelineV3V4 CountdownTimeline;
    }

    public class RaceState {
        public bool RaceHasStarted { get; set; } = false;
        public RaceStopReason LastReason { get; set; } = RaceStopReason.None;
    }
}