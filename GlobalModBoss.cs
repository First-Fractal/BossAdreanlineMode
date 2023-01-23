using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BossAdreanlineMode
{
    public class GlobalModBoss : GlobalNPC
    {
        static bool speed = false;
        static int[] BossParts = { NPCID.SlimeSpiked, NPCID.ServantofCthulhu, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail, NPCID.Creeper, NPCID.SkeletronHand, NPCID.SkeletronHead, NPCID.WallofFleshEye, NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail, NPCID.Probe, NPCID.PrimeCannon, NPCID.PrimeLaser, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.PlanterasHook, NPCID.PlanterasTentacle, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.GolemHead, NPCID.GolemHeadFree, NPCID.CultistTablet, NPCID.CultistBossClone, NPCID.LunarTowerNebula, NPCID.LunarTowerSolar, NPCID.LunarTowerVortex, NPCID.LunarTowerStardust, NPCID.MoonLordCore, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye, NPCID.MoonLordLeechBlob };

        public override void PostAI(NPC npc)
        {
            bool adreanline = ModContent.GetInstance<BossAdreanlineSystem>().adreanline; 
            if (adreanline)
            {
                if (npc.boss)
                {
                    speed = true;
                }

                foreach(int bossPart in BossParts)
                {
                    if (npc.type == bossPart)
                    {
                        speed = true;
                    }
                }

                if (speed)
                {
                    npc.position += npc.velocity * 1.5f;
                }
            }
            base.PostAI(npc);
        }
    }
}
