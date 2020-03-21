using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum SplitWorldEvent {
        [Description("Desert Escape (Start)")]
        DesertEscapeStart,
        [Description("Desert Escape (End)")]
        DesertEscapeEnd,
        [Description("Ku Obtained")]
        FindKu,
        [Description("Ku Lost")]
        LoseKu,
        [Description("Silent Woods Shriek Cutscene")]
        SilentWoodsShriekCutscene,
        [Description("Watermill Escape (Start)")]
        WaterEscapeStart,
        [Description("Watermill Escape (End)")]
        WaterPurified,
        [Description("Water Purified (So Soggy)")]
        SoSoggy,
        [Description("Winter Forest Escape (Start)")]
        WinterForestEscapeStart,
        [Description("Winter Forest Escape (End)")]
        WinterForestEscapeEnd,
    }
}