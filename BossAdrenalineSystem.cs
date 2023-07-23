using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace BossAdreanlineMode
{
    public class BossAdrenalineSystem : ModSystem
    {
        public bool Adrenaline = false;
        public int counter = 0;
        public int AdrenalineCounter = 0;
        public int AdrenalineCounterMax = BossConfig.Instance.AdrenalineCooldown;
        public bool boss = false;
        public int[] BossParts = { NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail, NPCID.Creeper, NPCID.SkeletronHand, NPCID.SkeletronHead, NPCID.WallofFleshEye, NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail, NPCID.Probe, NPCID.PrimeCannon, NPCID.PrimeLaser, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.PlanterasHook, NPCID.PlanterasTentacle, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.GolemHead, NPCID.GolemHeadFree, NPCID.CultistBossClone, NPCID.MoonLordCore, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye, NPCID.MoonLordLeechBlob };

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

        public void toggleAdrenaline()
        {
            if (Adrenaline)
            {
                AdrenalineCounterMax = BossConfig.Instance.AdrenalineDuration;
            } else
            {
                AdrenalineCounterMax = BossConfig.Instance.AdrenalineCooldown;
            }


            boss = false;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].boss && Main.npc[i].active == true)
                {
                    boss = true;
                }

                foreach (int bossPart in BossParts)
                {
                    if (Main.npc[i].type == bossPart && Main.npc[i].active == true)
                    {
                        boss = true;
                    }
                }
            }

            if (boss)
            {
                counter++;
                if (counter >= 60)
                {
                    AdrenalineCounter++;
                    counter = 0;
                }

                if (AdrenalineCounter > AdrenalineCounterMax)
                {
                    if (Adrenaline == false)
                    {
                        Talk(Language.GetTextValue("Mods.BossAdreanlineMode.Chat.AdrenalineEnabled"), new Color(255, 0, 0));
                        Adrenaline = true;
                        AdrenalineCounterMax = BossConfig.Instance.AdrenalineDuration;
                    } else
                    {
                        Talk(Language.GetTextValue("Mods.BossAdreanlineMode.Chat.AdrenalineDisabled"), new Color(0, 225, 0));
                        Adrenaline = false;
                        AdrenalineCounterMax = BossConfig.Instance.AdrenalineCooldown;
                    }
                    AdrenalineCounter = 0;
                }
            }
            else
            {
                counter = 0;
                AdrenalineCounter = 0;
                Adrenaline = false;
            }
        }

        public override void PostUpdateWorld()
        {
            toggleAdrenaline();
            base.PostUpdateWorld();
        }
    }
}
