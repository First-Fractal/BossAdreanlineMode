using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.ModBrowser;

namespace BossAdrenalineMode
{
    public class GlobalModBoss : GlobalNPC
    {
        static bool speed = false;

        public override void PostAI(NPC npc)
        {
            bool Adrenaline = ModContent.GetInstance<BossAdrenalineSystem>().Adrenaline;
            int[] BossParts = ModContent.GetInstance<BossAdrenalineSystem>().BossParts;
            if (Adrenaline)
            {
                if (npc.boss && npc.active)
                {
                    speed = true;
                }

                foreach(int bossPart in BossParts)
                {
                    if (npc.type == bossPart && npc.active)
                    {
                        speed = true;
                    }
                }

                if (speed)
                {
                    Vector2 futurePos = npc.position + (npc.velocity * BossConfig.Instance.AdrenalineMulti);
                    int futurePosX = (int)futurePos.X / 16;
                    int futurePosY = (int)(futurePos.Y + npc.height) / 16;

                    if (BossConfig.Instance.ForceEOLToStayNearby && npc.type == NPCID.HallowBoss && npc.active)
                    {
                        int radius = 4000;
                        Vector2 PlayerPos = Main.player[npc.target].position;
                        futurePos.X = MathHelper.Clamp(futurePos.X, PlayerPos.X - radius, PlayerPos.X + radius);
                        futurePos.Y = MathHelper.Clamp(futurePos.Y, PlayerPos.Y - radius, PlayerPos.Y + radius);
                    }    


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
