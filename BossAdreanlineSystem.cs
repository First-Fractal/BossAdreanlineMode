using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace BossAdreanlineMode
{
    public class BossAdreanlineSystem : ModSystem
    {
        public bool adreanline = false;
        public int counter = 0;
        public int adreanlineCounter = 0;
        public int adreanlineCounterMax = BossConfig.Instance.AdreanlineCooldown;
        public bool boss = false;

        public void Talk(string message, Color color)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(message, color);
            }
            else
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), color);
            }
        }

        public void toggleAdreanline()
        {
            adreanlineCounterMax = BossConfig.Instance.AdreanlineCooldown;
            boss = false;
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
                    counter = 0;
                }

                if (adreanlineCounter > adreanlineCounterMax)
                {
                    adreanline = !adreanline;
                    if (adreanline == true)
                    {
                        Talk(Language.GetTextValue("Mods.BossAdreanlineMode.Chat.AdreanlineEnabled"), new Color(255, 0, 0));
                    } else
                    {
                        Talk(Language.GetTextValue("Mods.BossAdreanlineMode.Chat.AdreanlineDisabled"), new Color(0, 225, 0));
                    }
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
