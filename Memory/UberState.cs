﻿using System;
using System.Runtime.InteropServices;
namespace LiveSplit.OriWotW {
    public enum UberStateType {
        BooleanUberState,
        ByteUberState,
        IntUberState,
        SerializedBooleanUberState,
        SerializedFloatUberState,
        SerializedIntUberState,
        SerializedByteUberState,
        CountUberState,
        SavePedestalUberState,
        ConditionUberState,
        PlayerUberStateDescriptor
    }
    public static class UberStateDefaults {
        public static UberState playerOnTandem = new UberState() { Name = "playerOnTandemUberState", ID = 54402, GroupName = "playerUberStateGroupDescriptor", GroupID = 3440, Type = UberStateType.SerializedBooleanUberState };
        public static UberState lumaPoolsSeed = new UberState() { Name = "gardenerSeedGrassCollected", ID = 28662, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedBooleanUberState };
        public static UberState windsweptWastesSeed = new UberState() { Name = "gardenerSeedFlowersCollected", ID = 20601, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedBooleanUberState };
        public static UberState mouldwoodDepthsSeed = new UberState() { Name = "gardenerSeedBashCollected", ID = 8192, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedBooleanUberState };
        public static UberState baursReachSeed = new UberState() { Name = "gardenerSeedSpringCollected", ID = 32376, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedBooleanUberState };
        public static UberState wellspringSeed = new UberState() { Name = "gardenerSeedGrappleCollected", ID = 24142, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedBooleanUberState };
        public static UberState silentWoodsSeed = new UberState() { Name = "gardenerSeedTreeCollected", ID = 7470, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedBooleanUberState };
        public static UberState winterForestWispQuest = new UberState() { Name = "winterForestWispQuestUberState", ID = 8973, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedIntUberState };
        public static UberState watermillEscapeState = new UberState() { Name = "watermillEscapeState", ID = 10720, GroupName = "waterMillStateGroupDescriptor", GroupID = 37858, Type = UberStateType.SerializedIntUberState };
        public static UberState desertRuinsEscape = new UberState() { Name = "desertRuinsEscape", ID = 19890, GroupName = "windtornRuinsGroup", GroupID = 10289, Type = UberStateType.SerializedIntUberState };
        public static UberState cleanseWellspringQuest = new UberState() { Name = "cleanseWellspringQuestUberState", ID = 34641, GroupName = "kwolokGroupDescriptor", GroupID = 937, Type = UberStateType.SerializedIntUberState };
        public static UberState finishedWatermillEscape = new UberState() { Name = "finishedWatermillEscape", ID = 12379, GroupName = "waterMillStateGroupDescriptor", GroupID = 37858, Type = UberStateType.SerializedBooleanUberState };
        public static UberState nightCrawlerChaseStarted = new UberState() { Name = "nightCrawlerChaseStarted", ID = 30656, GroupName = "swampStateGroup", GroupID = 21786, Type = UberStateType.SerializedBooleanUberState };
        public static UberState nightCrawlerDefeated = new UberState() { Name = "nightCrawlerDefeated", ID = 40322, GroupName = "swampStateGroup", GroupID = 21786, Type = UberStateType.SerializedBooleanUberState };
        public static UberState findToadQuest = new UberState() { Name = "findToadQuestUberState", ID = 48794, GroupName = "questUberStateGroup", GroupID = 14019, Type = UberStateType.SerializedIntUberState };
        public static UberState wispRewardPickupKwolok = new UberState() { Name = "wispRewardPickup", ID = 59806, GroupName = "kwoloksCavernThroneRoomGroup", GroupID = 46462, Type = UberStateType.SerializedBooleanUberState };
        public static UberState petrifiedOwlBossState = new UberState() { Name = "petrifiedOwlBossState", ID = 47278, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedByteUberState };
        public static UberState kwolokDropRace = new UberState() { Name = "kwolokDropRace", ID = 25545, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState wellspringRace = new UberState() { Name = "wellspringRace", ID = 11512, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState silentWoodlandRace = new UberState() { Name = "silentWoodlandRace", ID = 22703, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState baursReachWindTunnelRace = new UberState() { Name = "baursReachWindTunnelRace", ID = 23661, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState mouldwoodDepthsRace = new UberState() { Name = "mouldwoodDepthsRace", ID = 28552, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState desertRace = new UberState() { Name = "desertRace", ID = 30767, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState inkwaterMarshRace = new UberState() { Name = "inkwaterMarshRace", ID = 45951, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState lumaPoolsRace = new UberState() { Name = "lumaPoolsRace", ID = 54686, GroupName = "raceGroup", GroupID = 44964, Type = UberStateType.SerializedIntUberState };
        public static UberState wispRewardPickupBaur = new UberState() { Name = "wispRewardPickup", ID = 25522, GroupName = "baursReachGroup", GroupID = 28895, Type = UberStateType.SerializedBooleanUberState };
        public static UberState wispRewardPickupMouldwood = new UberState() { Name = "bossReward", ID = 63291, GroupName = "mouldwoodDepthsGroup", GroupID = 18793, Type = UberStateType.SerializedBooleanUberState };
        public static UberState wispRewardPickupLagoon = new UberState() { Name = "bossReward", ID = 49747, GroupName = "lagoonStateGroup", GroupID = 945, Type = UberStateType.SerializedBooleanUberState };
        public static UberState wispRewardPickupWindtorn = new UberState() { Name = "wispRewardPickup", ID = 22102, GroupName = "windtornRuinsGroup", GroupID = 10289, Type = UberStateType.SerializedBooleanUberState };
        public static UberState elevatorCompleteState = new UberState() { Name = "elevatorCompleteState", ID = 23584, GroupName = "corruptedPeakGroup", GroupID = 36153, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineAClear = new UberState() { Name = "vineAClear", ID = 42976, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineBClear = new UberState() { Name = "vineBClear", ID = 54940, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineCClear = new UberState() { Name = "vineCClear", ID = 24290, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineDClear = new UberState() { Name = "vineDClear", ID = 28478, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineEClear = new UberState() { Name = "vineEClear", ID = 3588, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineFClear = new UberState() { Name = "vineFClear", ID = 65277, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineGClear = new UberState() { Name = "vineGClear", ID = 41488, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState vineHClear = new UberState() { Name = "vineHClear", ID = 60752, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedBooleanUberState };
        public static UberState petrifiedForestTransitionPlayed = new UberState() { Name = "petrifiedForestNewTransitionOriVignettePlayed", ID = 46980, GroupName = "_petrifiedForestGroup", GroupID = 58674, Type = UberStateType.SerializedBooleanUberState };
        public static UberState hornBugBossState = new UberState() { Name = "hornBugBossState", ID = 48534, GroupName = "kwolokGroupDescriptor", GroupID = 937, Type = UberStateType.SerializedIntUberState };
        public static UberState energyContainersCounter = new UberState() { Name = "energyContainersCounter", ID = 41928, GroupName = "achievementsGroup", GroupID = 33399, Type = UberStateType.SerializedIntUberState };
        public static UberState healthContainersCounter = new UberState() { Name = "healthContainersCounter", ID = 52378, GroupName = "achievementsGroup", GroupID = 33399, Type = UberStateType.SerializedIntUberState };
        public static UberState kwolokBossState = new UberState() { Name = "kwolokBossState", ID = 58403, GroupName = "lagoonStateGroup", GroupID = 945, Type = UberStateType.SerializedByteUberState };
        public static UberState spiderBossState = new UberState() { Name = "spiderBossState", ID = 26713, GroupName = "mouldwoodDepthsGroup", GroupID = 18793, Type = UberStateType.SerializedIntUberState };
        public static UberState laserShooterBossState = new UberState() { Name = "laserShooterBossState", ID = 12971, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SerializedIntUberState };
        public static UberState savePedestalMidnightBurrows = new UberState() { Name = "howlOriginEntranceSavePedestal", ID = 42531, GroupName = "howlsOriginGroup", GroupID = 24922, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalInkwaterMarsh = new UberState() { Name = "savePedestalSwampIntroTop", ID = 10185, GroupName = "swampStateGroup", GroupID = 21786, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalHowlsDen = new UberState() { Name = "savePedestal", ID = 61594, GroupName = "howlsDenGRoup", GroupID = 11666, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalLumaPoolsA = new UberState() { Name = "savePedestalUberState", ID = 58183, GroupName = "lagoonStateGroup", GroupID = 945, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalLumaPoolsB = new UberState() { Name = "savePedestalUberState", ID = 1370, GroupName = "lagoonStateGroup", GroupID = 945, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalWellspring = new UberState() { Name = "savePedestalUberState", ID = 18181, GroupName = "wellspringGroupDescriptor", GroupID = 53632, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalBaursReach = new UberState() { Name = "savePedestalUberState", ID = 54235, GroupName = "baursReachGroup", GroupID = 28895, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalKwoloksHollow = new UberState() { Name = "savePedestal", ID = 26601, GroupName = "kwolokGroupDescriptor", GroupID = 937, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalMouldwood = new UberState() { Name = "savePedestalUberState", ID = 38871, GroupName = "mouldwoodDepthsGroup", GroupID = 18793, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalWillowsEnd = new UberState() { Name = "savePedestalUberState", ID = 41465, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalWillowsEndShriek = new UberState() { Name = "savePedestalUberState", ID = 50867, GroupName = "willowsEndGroup", GroupID = 16155, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalSilentWoodsA = new UberState() { Name = "savePedestalUberState", ID = 7071, GroupName = "_petrifiedForestGroup", GroupID = 58674, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalSilentWoodsB = new UberState() { Name = "savePedestalA", ID = 1965, GroupName = "_petrifiedForestGroup", GroupID = 58674, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalWindsweptWastesA = new UberState() { Name = "savePedestalUberState", ID = 10029, GroupName = "_petrifiedForestGroup", GroupID = 58674, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalWindsweptWastesB = new UberState() { Name = "savePedestal", ID = 26601, GroupName = "kwolokGroupDescriptor", GroupID = 937, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalWindtornRuinsA = new UberState() { Name = "savePedestalUberState", ID = 41398, GroupName = "windsweptWastesGroupDescriptor", GroupID = 20120, Type = UberStateType.SavePedestalUberState };
        public static UberState savePedestalWindtornRuinsB = new UberState() { Name = "savePedestalUberState", ID = 4928, GroupName = "windtornRuinsGroup", GroupID = 10289, Type = UberStateType.SavePedestalUberState };
        public static UberState builderProjectSpiritWell = new UberState() { Name = "builderProjectSpiritWell", ID = 16825, GroupName = "hubUberStateGroup", GroupID = 42178, Type = UberStateType.SerializedByteUberState };
        public static UberState fastTravel = new UberState() { Name = "fastTravelEnabledUberState", ID = 16489, GroupName = "npcsStateGroup", GroupID = 48248, Type = UberStateType.SerializedByteUberState };
    }
    public class UberState {
        public UberStateType Type;
        public int ID;
        public string Name;
        public int GroupID;
        public string GroupName;
        public UberValue Value;
        public IntPtr GroupPointer, IDPointer, ValuePointer;

        public UberState Clone() {
            return new UberState() { Type = Type, ID = ID, Name = Name, GroupID = GroupID, GroupName = GroupName, Value = Value };
        }
        public bool IsObjectType {
            get {
                return Type == UberStateType.SavePedestalUberState || Type == UberStateType.PlayerUberStateDescriptor;
            }
        }
        public bool IsBoolType {
            get {
                return Type == UberStateType.SerializedBooleanUberState;
            }
        }
        public bool IsIntType {
            get {
                return Type == UberStateType.SerializedIntUberState;
            }
        }
        public bool IsFloatType {
            get {
                return Type == UberStateType.SerializedFloatUberState;
            }
        }
        public bool IsByteType {
            get {
                return Type == UberStateType.SerializedByteUberState;
            }
        }
        public override string ToString() {
            switch (Type) {
                case UberStateType.SavePedestalUberState:
                case UberStateType.SerializedBooleanUberState:
                    return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value.Bool}";
                case UberStateType.SerializedByteUberState:
                    return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value.Byte}";
                case UberStateType.SerializedIntUberState:
                    return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value.Int}";
                case UberStateType.SerializedFloatUberState:
                    return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value.Float}";
            }
            return $"{Name}[{ID}]({GroupName}[{GroupID}]) = {Value}";
        }
    }
    [StructLayout(LayoutKind.Explicit, Size = 4, Pack = 1)]
    public struct UberValue {
        [FieldOffset(0)]
        public float Float;
        [FieldOffset(0)]
        public int Int;
        [FieldOffset(0)]
        public byte Byte;
        [FieldOffset(0)]
        public bool Bool;

        public override string ToString() {
            return $"{Int}|{Float}";
        }
    }
}