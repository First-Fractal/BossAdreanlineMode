using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BossAdreanlineMode
{
	public class BossAdreanlineMode : Mod
	{
	}

    public class BossAdreanlineSystem : ModSystem
    {
        public bool adreanline = false;
        public int counter = 0;
        public int adreanlineCounter = 0;
        public int adreanlineCounterMax = 20;
        public void toggleAdreanline()
        {
            counter++;
            if (counter >= 60)
            {
                adreanlineCounter++;
                Main.NewText(adreanlineCounter.ToString(), Color.SeaShell);
                counter = 0;
            }

            if (adreanlineCounter >= adreanlineCounterMax)
            {
                adreanline = !adreanline;
                Main.NewText("The adrealine is at " + adreanline.ToString(), Color.Red);
                adreanlineCounter = 0;
            }
        }


        //spawn in the npcs for the server
        public override void PostUpdateEverything()
        {
            if (Main.netMode == NetmodeID.Server)
            {
                toggleAdreanline();
            }
            base.PostUpdateEverything();
        }

        //spawn in the npcs for singleplayer
        public override void PostUpdateWorld()
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                toggleAdreanline();
            }
            base.PostUpdateWorld();
        }
    }
}