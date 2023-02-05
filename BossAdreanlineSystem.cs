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
            if (adreanline)
            {
                adreanlineCounterMax = BossConfig.Instance.AdreanlineDuration;
            } else
            {
                adreanlineCounterMax = BossConfig.Instance.AdreanlineCooldown;
            }


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
                    if (adreanline == false)
                    {
                        Talk(Language.GetTextValue("Mods.BossAdreanlineMode.Chat.AdreanlineEnabled"), new Color(255, 0, 0));
                        adreanline = true;
                        adreanlineCounterMax = BossConfig.Instance.AdreanlineDuration;
                    } else
                    {
                        Talk(Language.GetTextValue("Mods.BossAdreanlineMode.Chat.AdreanlineDisabled"), new Color(0, 225, 0));
                        adreanline = false;
                        adreanlineCounterMax = BossConfig.Instance.AdreanlineCooldown;
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

        public override void PostUpdateWorld()
        {
            toggleAdreanline();
            base.PostUpdateWorld();
        }
    }
}
