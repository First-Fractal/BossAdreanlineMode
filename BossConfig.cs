using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;

namespace BossAdreanlineMode
{
    [Label("$Mods.BossAdreanlineMode.Config.Label")]
    public class BossConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        // Automatically set by tModLoader
        public static BossConfig Instance;

        [Header("$Mods.BossAdreanlineMode.Config.Header.GeneralOptions")]

        [Label("$Mods.BossAdreanlineMode.Config.AdrenalineCooldown.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdrenalineCooldown.Tooltip")]
        [DefaultValue(20)]
        public int AdrenalineCooldown;

        [Label("$Mods.BossAdreanlineMode.Config.AdrenalineDuration.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdrenalineDuration.Tooltip")]
        [DefaultValue(20)]
        public int AdrenalineDuration;

        [Label("$Mods.BossAdreanlineMode.Config.AdrenalineMulti.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdrenalineMulti.Tooltip")]
        [DefaultValue(1.5f)]
        [Range(1f, 3f)]
        public float AdrenalineMulti;

        [Label("$Mods.BossAdreanlineMode.Config.HorzontalBossMoveFix.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.HorzontalBossMoveFix.Tooltip")]
        [DefaultValue(true)]
        public bool HorzontalBossMoveFix;
    }
}
