using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum SplitWorldEvent {
        [Description("Howl Fight")]
        HowlFight,
        [Description("Ku Obtained")]
        FindKu,
        [Description("Ku Lost")]
        LoseKu,
        [Description("Shriek Defeated")]
        ShriekDefeated,
        [Description("Water Purified (Watermill Escape)")]
        WaterPurified,
        [Description("Water Purified (So Soggy)")]
        SoSoggy,
        [Description("Weeping Ridge Elevator Fight")]
        WeepingRidgeElevatorFight
    }
}