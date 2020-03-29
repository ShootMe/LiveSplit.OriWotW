using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum SplitBoss {
        [Description("Howl Start")]
        HowlStart,
        [Description("Howl Defeated")]
        HowlEnd,
        [Description("Hornbug Start")]
        HornbugStart,
        [Description("Hornbug Defeated")]
        HornbugEnd,
        [Description("Kwolok Start")]
        KwolokStart,
        [Description("Kwolok Defeated")]
        KwolokEnd,
        [Description("Mora Start")]
        MoraStart,
        [Description("Mora Defeated")]
        MoraEnd,
        [Description("Weeping Ridge Elevator Finished")]
        WeepingRidgeElevatorFight,
        [Description("Willow Stone Start")]
        WillowStoneStart,
        [Description("Willow Stone Defeated")]
        WillowStoneEnd,
        [Description("Shriek Start")]
        ShriekStart,
        [Description("Shriek Defeated")]
        ShriekDefeated
    }
}