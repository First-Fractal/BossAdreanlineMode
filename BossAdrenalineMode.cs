using Terraria;
using Terraria.ModLoader;
using System.IO;

namespace BossAdreanlineMode
{
	public class BossAdreanlineMode : Mod
	{
        //for synching custom values between server and client
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            //get the custom class of the current player
            BossAdrenalinePlayer BAP = Main.CurrentPlayer.GetModPlayer<BossAdrenalinePlayer>();

            //set the values from the server
            BAP.boss = reader.ReadBoolean();
            BAP.adrenaline = reader.ReadBoolean();
            BAP.adrenalineCounter = reader.ReadInt32();
            BAP.adrenalineCounterMax = reader.ReadInt32();
        }
    }

    //the custom player class that's for storing values from the server
    public class BossAdrenalinePlayer : ModPlayer
    {
        public bool boss = false;
        public bool adrenaline = false;
        public int adrenalineCounter = 0;
        public int adrenalineCounterMax = 0;
    }
}