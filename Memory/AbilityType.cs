using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum AbilityType : byte {
        None = 255,
        [Description("Bash")]
        Bash = 0,
        [Description("Wall Jump")]
        WallJump = 3,
        [Description("Double Jump")]
        DoubleJump = 5,
        [Description("Launch")]
        Launch = 8,
        [Description("Feather")]
        Feather = 14,
        [Description("Water Breath")]
        WaterBreath = 23,
        [Description("Light Burst")]
        LightBurst = 51,
        [Description("Grapple")]
        Grapple = 57,
        [Description("Flash")]
        Flash = 62,
        [Description("Spike")]
        Spike = 74,
        [Description("Regenerate")]
        Regenerate = 77,
        [Description("Spirit Arc")]
        SpiritArc = 97,
        [Description("Spirit Smash")]
        SpiritSmash,
        [Description("Torch")]
        Torch,
        [Description("Spirit Edge")]
        SpiritEdge,
        [Description("Burrow")]
        Burrow,
        [Description("Dash")]
        Dash,
        [Description("Water Dash")]
        WaterDash = 104,
        [Description("Spirit Star")]
        SpiritStar = 106,
        [Description("Seir")]
        Seir = 108,
        [Description("Blaze")]
        Blaze = 115,
        [Description("Sentry")]
        Sentry,
        [Description("Flap")]
        Flap = 118,
        [Description("Damage Upgrade 1")]
        DamageUpgrade1 = 120,
        [Description("Damage Upgrade 2")]
        DamageUpgrade2
    }
}