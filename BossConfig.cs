using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace BossAdreanlineMode
{
    [Label("$Mods.BossAdreanlineMode.Config.Label")]
    public class BossConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        // Automatically set by tModLoader
        public static BossConfig Instance;

        [Header("$Mods.BossAdreanlineMode.Config.Header.GeneralOptions")]

        [Label("$Mods.BossAdreanlineMode.Config.AdreanlineCooldown.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdreanlineCooldown.Tooltip")]
        [DefaultValue(20)]
        public int AdreanlineCooldown;

        [Label("$Mods.BossAdreanlineMode.Config.AdreanlineDuration.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdreanlineDuration.Tooltip")]
        [DefaultValue(20)]
        public int AdreanlineDuration;

        [Label("$Mods.BossAdreanlineMode.Config.AdreanlineMulti.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdreanlineMulti.Tooltip")]
        [DefaultValue(1.5f)]
        [Range(1f, 3f)]
        public float AdreanlineMulti;
    }
}
