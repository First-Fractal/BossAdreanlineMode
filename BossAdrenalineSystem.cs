using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.IO;

namespace BossAdreanlineMode
{
    public class BossAdrenalineSystem : ModSystem
    {
        //flags for tracking the adrenaline counter
        public bool Adrenaline = false;
        public int counter = 0;
        public int AdrenalineCounter = 0;
        public int AdrenalineCounterMax = BossConfig.Instance.AdrenalineCooldown;

        //flag for boss is alive
        public static bool boss = false;

        //list of boss parts
        public int[] BossParts = { NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail, NPCID.Creeper, NPCID.SkeletronHand, NPCID.SkeletronHead, NPCID.WallofFleshEye, NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail, NPCID.Probe, NPCID.PrimeCannon, NPCID.PrimeLaser, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.PlanterasHook, NPCID.PlanterasTentacle, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.GolemHead, NPCID.GolemHeadFree, NPCID.CultistBossClone, NPCID.MoonLordCore, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye, NPCID.MoonLordLeechBlob };

        //function for talking to everyone on the world
        public void Talk(string message, Color color)
        {
            //check if its singleplayer or multiplayer
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                //display the message into the chat with the speific color
                Main.NewText(message, color);
            }
            else
            {
                //brodcast the message to everyone in the world
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), color);
            }
        }

        //function for managing the adrenaline toggle
        public void toggleAdrenaline()
        {
            //if the adrenaline is enabled, then set the counter to the duration, or else set it to the cooldown
            if (Adrenaline)
            {
                AdrenalineCounterMax = BossConfig.Instance.AdrenalineDuration;
            } else
            {
                AdrenalineCounterMax = BossConfig.Instance.AdrenalineCooldown;
            }

            //set the boss flag to false before checking if there is one
            boss = false;

            //loop through all of the npcs in the world
            for (int i = 0; i < Main.npc.Length; i++)
            {
                //check if the npc is a boss and active
                if (Main.npc[i].boss && Main.npc[i].active == true)
                {
                    //set the boss to be true and exit
                    boss = true;
                    break;
                }

                //loop through all of the boss parts
                foreach (int bossPart in BossParts)
                {
                    //check if the npc is a boss part and active
                    if (Main.npc[i].type == bossPart && Main.npc[i].active == true)
                    {
                        //set the boss to be true and exit
                        boss = true;
                        break;
                    }
                }
            }

            //check if there is a boss alive
            if (boss)
            {

                //check if adrenaline is off the cooldown/duration
                if (AdrenalineCounter > AdrenalineCounterMax)
                {
                    //check if the counter was for turning on/off adreanline
                    if (Adrenaline == false)
                    {
                        //tell them that adrenaline is enabled
                        Talk(Language.GetTextValue("Mods.BossAdreanlineMode.Chat.AdrenalineEnabled"), new Color(255, 0, 0));

                        //set adrenaline to true
                        Adrenaline = true;

                        //reset the counter to the duration
                        AdrenalineCounterMax = BossConfig.Instance.AdrenalineDuration;
                    } else
                    {
                        //tell them that adrenaline is disabled
                        Talk(Language.GetTextValue("Mods.BossAdreanlineMode.Chat.AdrenalineDisabled"), new Color(0, 225, 0));

                        //disable adrenaline
                        Adrenaline = false;

                        //reset the counter to the cooldown
                        AdrenalineCounterMax = BossConfig.Instance.AdrenalineCooldown;
                    }
                    AdrenalineCounter = 0;
                }

                //increase the tick counter
                counter++;

                //check if the tick counter is above 60 (60 ticks = one second)
                if (counter >= 60)
                {
                    //increase the adrenaline counter
                    AdrenalineCounter++;

                    //reset the tick counter
                    counter = 0;
                }
            }
            else
            {
                //reset the values when there is no boss alive
                counter = 0;
                AdrenalineCounter = 0;
                Adrenaline = false;
            }

            if (Main.dedServ)
            {
                NetMessage.SendData(MessageID.WorldData);
            }
        }
        
        //run the toggle adrenaline script after every tick has passed in the world
        public override void PostUpdateWorld()
        {
            toggleAdrenaline();
            base.PostUpdateWorld();
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(boss);
            writer.Write(Adrenaline);
            writer.Write(AdrenalineCounter);
            writer.Write(AdrenalineCounterMax);

            
            base.NetSend(writer);
        }

        public override void NetReceive(BinaryReader reader)
        {
            boss = reader.ReadBoolean();
            Adrenaline = reader.ReadBoolean();
            AdrenalineCounter = reader.ReadInt32();
            AdrenalineCounterMax = reader.ReadInt32();

            base.NetReceive(reader);
        }
    }
}
