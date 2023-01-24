using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [Range(0f, 3f)]
        public float AdreanlineMulti;
    }
}
