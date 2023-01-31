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

        [Label("$Mods.BossAdreanlineMode.Config.AdreanlineMulti.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdreanlineMulti.Tooltip")]
        [DefaultValue(1.5f)]
        [Range(1f, 3f)]
        public float AdreanlineMulti;

        [Label("$Mods.BossAdreanlineMode.Config.AdreanlineBarX.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdreanlineBarX.Tooltip")]
        [DefaultValue(800)]
        [Range(0f, 1660f)]
        public float AdreanlineBarX;

        [Label("$Mods.BossAdreanlineMode.Config.AdreanlineBarY.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdreanlineBarY.Tooltip")]
        [DefaultValue(22f)]
        [Range(0f, 900f)]
        public float AdreanlineBarY;
    }
}
