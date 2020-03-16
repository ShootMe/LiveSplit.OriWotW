using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum SplitWorldEvent {
        [Description("Find Ku (Silent Woods)")]
        FindKu,
        [Description("Lose Ku (Silent Woods)")]
        LoseKu,
        [Description("Water Purified (Watermill Escape)")]
        WaterPurified,
        [Description("Winter Forest Escape")]
        WinterForestEscape
    }
}