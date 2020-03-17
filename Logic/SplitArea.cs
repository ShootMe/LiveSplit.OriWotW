using System;
using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum SplitArea {
        [Description("Baurs Reach")]
        BaursReach,
        [Description("Gorlek Mines")]
        GorlekMines,
        [Description("Inkwater Marsh")]
        InkwaterMarsh,
        [Description("Kwoloks Hollow")]
        KwoloksHollow,
        [Description("Luma Pools")]
        LumaPools,
        [Description("Midnight Burrows")]
        MidnightBurrow,
        [Description("Mouldwood Depths")]
        MouldwoodDepths,
        [Description("Riverlands")]
        Riverlands,
        [Description("Silent Woods")]
        SilentWoodland,
        [Description("The Wellspring")]
        WaterMill,
        [Description("The Wellspring (Watermill 1)")]
        [Scene("wotwSaveRoomC__clone0__clone1", "waterMillAExit")]
        WaterMillSub1,
        [Description("The Wellspring (Watermill 2)")]
        [Scene("waterMillBEntrance")]
        WaterMillSub2,
        [Description("The Wellspring (Watermill 3)")]
        [Scene("waterMillCEntrance")]
        WaterMillSub3,
        [Description("Weeping Ridge")]
        [Scene("weepingRidgeWillowsEndEntrance", "weepingRidgeElevatorFight")]
        WeepingRidge,
        [Description("Wellspring Glades")]
        WellspringGlades,
        [Description("Willows End")]
        WillowsEnd,
        [Description("Willows End (Boss Room)")]
        [Scene("willowCeremonyIntro")]
        WillowsEndBoss,
        [Description("Windswept Wastes")]
        WindsweptWastes,
        [Description("Windtorn Ruins")]
        WindtornRuins
    }
    [AttributeUsage(AttributeTargets.Field)]
    public class SceneAttribute : Attribute {
        public string[] Names { get; private set; }
        public SceneAttribute(params string[] names) {
            Names = names;
        }

        public override string ToString() {
            return Names.ToString();
        }
    }
}