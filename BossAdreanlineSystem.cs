using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BossAdreanlineMode
{
    public class BossAdreanlineSystem : ModSystem
    {
        public bool adreanline = false;
        public int counter = 0;
        public int adreanlineCounter = 0;
        public int adreanlineCounterMax = BossConfig.Instance.AdreanlineCooldown;
        public void toggleAdreanline()
        {
            adreanlineCounterMax = BossConfig.Instance.AdreanlineCooldown;
            bool boss = false;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].boss == true && Main.npc[i].active == true)
                {
                    boss = true;
                }
            }

            if (boss)
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
            else
            {
                counter = 0;
                adreanlineCounter = 0;
                adreanline = false;
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
