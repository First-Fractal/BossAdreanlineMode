using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BossAdreanlineMode
{
    public class GlobalModBoss : GlobalNPC
    {
        static bool speed = false;
        static int[] BossParts = {NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail, NPCID.Creeper, NPCID.SkeletronHand, NPCID.SkeletronHead, NPCID.WallofFleshEye, NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail, NPCID.Probe, NPCID.PrimeCannon, NPCID.PrimeLaser, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.PlanterasHook, NPCID.PlanterasTentacle, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.GolemHead, NPCID.GolemHeadFree, NPCID.CultistTablet, NPCID.CultistBossClone, NPCID.LunarTowerNebula, NPCID.LunarTowerSolar, NPCID.LunarTowerVortex, NPCID.LunarTowerStardust, NPCID.MoonLordCore, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye, NPCID.MoonLordLeechBlob };

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
                    Vector2 futurePos = npc.position + (npc.velocity * BossConfig.Instance.AdreanlineMulti);
                    int futurePosX = (int)futurePos.X / 16;
                    int futurePosY = (int)(futurePos.Y + npc.height) / 16;


                    if (npc.noTileCollide == false)
                    {
                        if ((Main.tile[futurePosX, futurePosY].HasTile == true && Main.tileSolid[(int)Main.tile[futurePosX, futurePosY].TileType]) == false)
                        {
                            npc.position = futurePos;
                        }
                    } else
                    {
                        npc.position = futurePos;
                    }
                    

                    npc.netUpdate = true;
                    if (Main.netMode == NetmodeID.Server && --npc.netSpam < 0)
                    {
                        npc.netSpam = 5;
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
                    }
                }
            }
            base.PostAI(npc);
        }
    }
}
