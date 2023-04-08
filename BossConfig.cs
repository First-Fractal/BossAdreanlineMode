using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;

namespace BossAdrenalineMode
{
    [Label("$Mods.BossAdrenalineMode.Config.Label")]
    public class BossConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        // Automatically set by tModLoader
        public static BossConfig Instance;

        [Header("$Mods.BossAdrenalineMode.Config.Header.GeneralOptions")]

        [Label("$Mods.BossAdrenalineMode.Config.AdrenalineCooldown.Label")]
        [Tooltip("$Mods.BossAdrenalineMode.Config.AdrenalineCooldown.Tooltip")]
        [DefaultValue(20)]
        public int AdrenalineCooldown;

        [Label("$Mods.BossAdrenalineMode.Config.AdrenalineDuration.Label")]
        [Tooltip("$Mods.BossAdrenalineMode.Config.AdrenalineDuration.Tooltip")]
        [DefaultValue(20)]
        public int AdrenalineDuration;

        [Label("$Mods.BossAdrenalineMode.Config.AdrenalineMulti.Label")]
        [Tooltip("$Mods.BossAdrenalineMode.Config.AdrenalineMulti.Tooltip")]
        [DefaultValue(1.5f)]
        [Range(1f, 3f)]
        public float AdrenalineMulti;

        [Label("$Mods.BossAdrenalineMode.Config.ForceEOLToStayNearby.Label")]
        [Tooltip("$Mods.BossAdrenalineMode.Config.ForceEOLToStayNearby.Tooltip")]
        [DefaultValue(true)]
        public bool ForceEOLToStayNearby;
    }
}
