using Terraria;
using Terraria.ModLoader;
using System.IO;

namespace BossAdreanlineMode
{
	public class BossAdreanlineMode : Mod
	{
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            BossAdrenalinePlayer BAP = Main.CurrentPlayer.GetModPlayer<BossAdrenalinePlayer>();

            BAP.boss = reader.ReadBoolean();
            BAP.adrenaline = reader.ReadBoolean();
            BAP.adrenalineCounter = reader.ReadInt32();
            BAP.adrenalineCounterMax = reader.ReadInt32();
        }
    }

    public class BossAdrenalinePlayer : ModPlayer
    {
        public bool boss = false;
        public bool adrenaline = false;
        public int adrenalineCounter = 0;
        public int adrenalineCounterMax = 0;
    }
}