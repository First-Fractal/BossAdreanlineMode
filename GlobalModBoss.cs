using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BossAdreanlineMode
{
    public class GlobalModBoss : GlobalNPC
    {
        static bool speed = false;

        public override void PostAI(NPC npc)
        {
            bool Adrenaline = ModContent.GetInstance<BossAdrenalineSystem>().Adrenaline;
            int[] BossParts = ModContent.GetInstance<BossAdrenalineSystem>().BossParts;
            bool boss = false;
            if (Adrenaline)
            {
                if (npc.boss && npc.active)
                {
                    boss = true;
                    speed = true;
                }

                foreach(int bossPart in BossParts)
                {
                    if (npc.type == bossPart && npc.active)
                    {
                        boss = true;
                        speed = true;
                    }
                }

                if (boss)
                {
                    if (BossConfig.Instance.DisableBossDespawn)
                    {
                        foreach (Player player in Main.player)
                        {
                            bool someoneAlive = false;
                            if (player.active && !player.dead)
                            {
                                someoneAlive = true;
                            }

                            if (someoneAlive)
                            {
                                if (npc.Distance(Main.player[npc.target].position) < 3000)
                                {
                                    npc.DoesntDespawnToInactivity();
                                    npc.DiscourageDespawn(9999);
                                } else if (Main.dayTime && (npc.type == NPCID.EyeofCthulhu || npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism || npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail || npc.type == NPCID.SkeletronPrime || npc.type == NPCID.PrimeCannon || npc.type == NPCID.PrimeLaser || npc.type == NPCID.PrimeSaw || npc.type == NPCID.PrimeVice))
                                {
                                    npc.DoesntDespawnToInactivity();
                                    npc.DiscourageDespawn(55);
                                }
                            }
                        }
                    }
                }

                if (speed)
                {
                    Vector2 futurePos = npc.position + (npc.velocity * BossConfig.Instance.AdrenalineMulti);


                    if (BossConfig.Instance.HorzontalBossMoveFix && (npc.type == NPCID.HallowBoss || npc.type == NPCID.CultistBoss) && npc.active)
                    {
                        int radius = 2000;
                        Vector2 PlayerPos = Main.player[npc.target].position;
                        futurePos.X = MathHelper.Clamp(futurePos.X, PlayerPos.X - radius, PlayerPos.X + radius);
                        futurePos.Y = MathHelper.Clamp(futurePos.Y, PlayerPos.Y - radius, PlayerPos.Y + radius);
                    }


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
