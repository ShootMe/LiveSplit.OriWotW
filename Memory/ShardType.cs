using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum ShardType : byte {
        None,
        [Description("Overcharge")]
        Overcharge,
        [Description("Triple Jump")]
        TripleJump,
        [Description("Wingclip")]
        Wingclip,
        [Description("Bounty")]
        Bounty,
        [Description("Swap")]
        Swap,
        [Description("Magnet")]
        Magnet = 8,
        [Description("Splinter")]
        Splinter,
        [Description("Reckless")]
        Reckless = 13,
        [Description("Quickshot")]
        Quickshot,
        [Description("Resilience")]
        Resilience = 18,
        [Description("Light Harvest")]
        LightHarvest,
        [Description("Vitality")]
        Vitality = 22,
        [Description("Life Harvest")]
        LifeHarvest,
        [Description("Energy Harvest")]
        EnergyHarvest = 25,
        [Description("Energy")]
        Energy,
        [Description("Life Pact")]
        LifePact,
        [Description("Last Stand")]
        LastStand,
        [Description("Secret")]
        Secret = 30,
        [Description("Ultra Bash")]
        UltraBash = 32,
        [Description("Ultra Grapple")]
        UltraGrapple,
        [Description("Overflow")]
        Overflow,
        [Description("Thorn")]
        Thorn,
        [Description("Catalyst")]
        Catalyst,
        [Description("Turmoil")]
        Turmoil = 38,
        [Description("Sticky")]
        Sticky,
        [Description("Finesse")]
        Finesse,
        [Description("Spirit Surge")]
        SpiritSurge,
        [Description("Lifeforce")]
        Lifeforce = 43,
        [Description("Deflector")]
        Deflector,
        [Description("Fracture")]
        Fracture = 46,
        [Description("Arcing")]
        Arcing
    }
}