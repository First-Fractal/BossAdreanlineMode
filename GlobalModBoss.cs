using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BossAdreanlineMode
{
    public class GlobalModBoss : GlobalNPC
    {
        //flag for if the speed should be enabled
        static bool speed = false;

        public override bool PreAI(NPC npc)
        {
            //check if the adrenaline is enabled
            bool Adrenaline = ModContent.GetInstance<BossAdrenalineSystem>().Adrenaline;

            //get a list of boss parts
            int[] BossParts = ModContent.GetInstance<BossAdrenalineSystem>().BossParts;

            //flag for if the current npc is a boss
            bool boss = false;

            //check if adrenaline is enabled
            if (Adrenaline)
            {
                //check if the npc is a boss and active 
                if (npc.boss && npc.active)
                {
                    //set the adrenline flags to be true
                    boss = true;
                    speed = true;
                }

                //loop through all of the boss parts
                foreach(int bossPart in BossParts)
                {
                    //check if the npc is a boss part and active
                    if (npc.type == bossPart && npc.active)
                    {
                        //set the adrenline flags to be true
                        boss = true;
                        speed = true;
                    }
                }

                //check if it is a boss
                if (boss)
                {
                    //check if bosses shouldn't be allowed to despawn
                    if (BossConfig.Instance.DisableBossDespawn)
                    {
                        //flag for checking if someone is alive for the despawn to take place
                        bool someoneAlive = false;

                        //loop through all of the players
                        foreach (Player player in Main.player)
                        {
                            //check if there is a active and not dead player
                            if (player.active && !player.dead)
                            {
                                someoneAlive = true;
                            }

                            if (someoneAlive)
                            {
                                //check the distance between the npc and the player that it is targeting to be less then 6000 (16 pixels = 1 tile)
                                if (npc.Distance(Main.player[npc.target].position) < 6000)
                                {
                                    //make sure that the npc dosnt despawn when still with in range
                                    npc.DoesntDespawnToInactivity();
                                    npc.DiscourageDespawn(9999);
                                } 
                                //skeletron require a larger distance cause he's a speical boy
                                else if (npc.Distance(Main.player[npc.target].position) < 12000 && npc.type == NPCID.Skeleton)
                                {
                                    npc.DoesntDespawnToInactivity();
                                    npc.DiscourageDespawn(9999);
                                }

                                //make the night time bosses linger a bit longer before despawning when it's day time
                                else if (Main.dayTime && (npc.type == NPCID.EyeofCthulhu || npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism || npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail || npc.type == NPCID.SkeletronPrime || npc.type == NPCID.PrimeCannon || npc.type == NPCID.PrimeLaser || npc.type == NPCID.PrimeSaw || npc.type == NPCID.PrimeVice))
                                {
                                    npc.DoesntDespawnToInactivity();
                                    npc.DiscourageDespawn(55);
                                }
                            }
                        }
                    }
                }

                //check if the speed is enabled
                if (speed)
                {
                    //get the future postion of where the npc will be after adrenaline has been appiled
                    Vector2 futurePos = npc.position + (npc.velocity * BossConfig.Instance.AdrenalineMulti);


                    //check if the horzontal boss move fix is enabled for EoL and Lunatic Cultist
                    if (BossConfig.Instance.HorzontalBossMoveFix && (npc.type == NPCID.HallowBoss || npc.type == NPCID.CultistBoss) && npc.active)
                    {
                        //get the radius and the postion of the targeted player
                        int radius = 2000;
                        Vector2 PlayerPos = Main.player[npc.target].position;

                        //force them to be no farther away from the player then allowed
                        futurePos.X = MathHelper.Clamp(futurePos.X, PlayerPos.X - radius, PlayerPos.X + radius);
                        futurePos.Y = MathHelper.Clamp(futurePos.Y, PlayerPos.Y - radius, PlayerPos.Y + radius);
                    }

                    //set the future postions to be in tiles unit
                    int futurePosX = (int)futurePos.X / 16;
                    int futurePosY = (int)(futurePos.Y + npc.height) / 16;  

                    //check if the npc is not allowed to go through walls
                    if (npc.noTileCollide == false)
                    {
                        //check if the future postion isnt inside a tile 
                        if ((Main.tile[futurePosX, futurePosY].HasTile == true && Main.tileSolid[Main.tile[futurePosX, futurePosY].TileType]) == false)
                        {
                            //set the postion to be at the future postion
                            npc.position = futurePos;
                        }
                    } else
                    {
                        //set the postion to be at the future postion
                        npc.position = futurePos;
                    }
                    
                    //tell TML to sync the npc AI
                    npc.netUpdate = true;

                    //make sure to sync the data with everyone else (may or may not stolen this from fargo ;| )
                    if (Main.netMode == NetmodeID.Server && --npc.netSpam < 0)
                    {
                        npc.netSpam = 2;
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
                    }
                }
            }
            return base.PreAI(npc);
        }
    }
}
