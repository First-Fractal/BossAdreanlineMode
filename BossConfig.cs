using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;

namespace BossAdreanlineMode
{
    [Label("$Mods.BossAdreanlineMode.Config.Label")]
    public class BossConfig : ModConfig
    {
        //make the config only work on non server side
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

        [Label("$Mods.BossAdreanlineMode.Config.DisableBossDespawn.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.DisableBossDespawn.Tooltip")]
        [DefaultValue(true)]
        public bool DisableBossDespawn;

        [Label("$Mods.BossAdreanlineMode.Config.HorzontalBossMoveFix.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.HorzontalBossMoveFix.Tooltip")]
        [DefaultValue(true)]
        public bool HorzontalBossMoveFix;
    }

    [Label("$Mods.BossAdreanlineMode.BossGUI.Label")]
    public class BossGUIConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        // Automatically set by tModLoader
        public static BossGUIConfig Instance;

        [Header("$Mods.BossAdreanlineMode.BossGUI.Header.GeneralOptions")]

        [Label("$Mods.BossAdreanlineMode.BossGUI.DisplayBar.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.BossGUI.DisplayBar.Tooltip")]
        [DefaultValue(true)]
        public bool DisplayBar;

        [Label("$Mods.BossAdreanlineMode.BossGUI.AdreanlineBarX.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.BossGUI.AdreanlineBarX.Tooltip")]
        [DefaultValue(50)]
        [Slider()]
        [Range(0, 100)]
        public int AdreanlineBarX;

        [Label("$Mods.BossAdreanlineMode.BossGUI.AdreanlineBarY.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.BossGUI.AdreanlineBarY.Tooltip")]
        [DefaultValue(3)]
        [Slider()]
        [Range(0, 100)]
        public int AdreanlineBarY;
    }
}