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

        //get a public instance of the config
        public static BossConfig Instance;

        //set the general headers
        [Header("$Mods.BossAdreanlineMode.Config.Header.GeneralOptions")]

        //how long the cooldown should be 
        [Label("$Mods.BossAdreanlineMode.Config.AdrenalineCooldown.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdrenalineCooldown.Tooltip")]
        [DefaultValue(20)]
        public int AdrenalineCooldown;

        //how long the duration should be
        [Label("$Mods.BossAdreanlineMode.Config.AdrenalineDuration.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdrenalineDuration.Tooltip")]
        [DefaultValue(20)]
        public int AdrenalineDuration;

        //how much faster should it be moving at
        [Label("$Mods.BossAdreanlineMode.Config.AdrenalineMulti.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.AdrenalineMulti.Tooltip")]
        [DefaultValue(1.5f)]
        [Range(1f, 3f)]
        public float AdrenalineMulti;

        //should bosses not be allowed to despawn
        [Label("$Mods.BossAdreanlineMode.Config.DisableBossDespawn.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.DisableBossDespawn.Tooltip")]
        [DefaultValue(true)]
        public bool DisableBossDespawn;

        //should it fix the horzontal moving issue with EoL and LC
        [Label("$Mods.BossAdreanlineMode.Config.HorzontalBossMoveFix.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.Config.HorzontalBossMoveFix.Tooltip")]
        [DefaultValue(true)]
        public bool HorzontalBossMoveFix;
    }

    //set the config label
    [Label("$Mods.BossAdreanlineMode.BossGUI.Label")]
    public class BossGUIConfig : ModConfig
    {
        //make the config only run on the client side
        public override ConfigScope Mode => ConfigScope.ClientSide;
        
        //get a public instance of the config
        public static BossGUIConfig Instance;

        [Header("$Mods.BossAdreanlineMode.BossGUI.Header.GeneralOptions")]

        //should the bar be displayed when a boss is alive
        [Label("$Mods.BossAdreanlineMode.BossGUI.DisplayBar.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.BossGUI.DisplayBar.Tooltip")]
        [DefaultValue(true)]
        public bool DisplayBar;

        //set the x pos of the bar
        [Label("$Mods.BossAdreanlineMode.BossGUI.AdreanlineBarX.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.BossGUI.AdreanlineBarX.Tooltip")]
        [DefaultValue(50)]
        [Slider()]
        [Range(0, 100)]
        public int AdreanlineBarX;

        //set the y pos of the bar
        [Label("$Mods.BossAdreanlineMode.BossGUI.AdreanlineBarY.Label")]
        [Tooltip("$Mods.BossAdreanlineMode.BossGUI.AdreanlineBarY.Tooltip")]
        [DefaultValue(3)]
        [Slider()]
        [Range(0, 100)]
        public int AdreanlineBarY;
    }
}