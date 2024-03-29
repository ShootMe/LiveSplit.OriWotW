﻿using System.ComponentModel;
namespace LiveSplit.OriWotW {
    public enum SplitType {
        [Description("Manual Split")]
        ManualSplit,
        [Description("Ability")]
        Ability,
        [Description("Area (Enter)")]
        AreaEnter,
        [Description("Area (Leave)")]
        AreaLeave,
        [Description("Boss Fight")]
        Boss,
        [Description("Creep Heart")]
        CreepHeart,
        [Description("Energy Cell")]
        EnergyCell,
        [Description("Game Start")]
        GameStart,
        [Description("Game End (Crawl)")]
        GameEndCrawl,
        [Description("Final Cutscene Started")]
        FinalCutsceneStarted,
        [Description("Health Cell")]
        HealthCell,
        [Description("Hitbox")]
        Hitbox,
        [Description("Keystone")]
        Keystone,
        [Description("Map %")]
        Map,
        [Description("Ore")]
        Ore,
        [Description("Race State")]
        RaceState,
        [Description("Seed")]
        Seed,
        [Description("Shard")]
        Shard,
        [Description("Spirit Trial")]
        SpiritTrial,
        [Description("Teleporter")]
        Teleporter,
        [Description("Wisp")]
        Wisp,
        [Description("World Event")]
        WorldEvent,
        [Description("Uber State")]
        UberState,
        [Description("Keystone Door")]
        KeystoneDoor,
        [Description("Uber State Value")]
        UberStateValue
    }
    public class Split {
        public string Name { get; set; }
        public SplitType Type { get; set; }
        public string Value { get; set; }

        public override string ToString() {
            return $"{Type}|{Value}";
        }
    }
}