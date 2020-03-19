using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum SplitWorldEvent {
        [Description("Ku Obtained")]
        FindKu,
        [Description("Ku Lost")]
        LoseKu,
        [Description("Silent Woods Shriek Cutscene")]
        SilentWoodsShriekCutscene,
        [Description("Water Purified (Watermill Escape)")]
        WaterPurified,
        [Description("Water Purified (So Soggy)")]
        SoSoggy
    }
}