using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using System.Collections.Generic;
using Terraria.ID;
using BossAdreanlineMode;

namespace BossAdrenalineMode
{
    public class AdrenalineUI : UIState
    {
        public ReLogic.Content.Asset<Texture2D> imageBar = ModContent.Request<Texture2D>("BossAdreanlineMode/bar");
        public UIElement panel;
        public UIImage bar;
        public UIText title;

        public override void OnInitialize()
        {
            panel = new UIElement();
            panel.Width.Set(imageBar.Width(), 0);
            panel.Height.Set(imageBar.Height(), 0);
            Append(panel);

            bar = new UIImage(imageBar);
            panel.Append(bar);

            title = new UIText("Boss Adrenaline Bar");
            panel.Append(title);

            base.OnInitialize();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (BossGUIConfig.Instance.DisplayBar)
            {
                if (Main.netMode == NetmodeID.SinglePlayer && ModContent.GetInstance<BossAdrenalineSystem>().boss == false)
                {
                    return;
                }
                else if (Main.netMode == NetmodeID.MultiplayerClient && Main.LocalPlayer.GetModPlayer<BossAdrenalinePlayer>().boss == false)
                {
                    return;
                }
            } else
            {
                return;
            }
            
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            panel.Left.Set((int)(Main.MenuUI.GetDimensions().Width * (BossGUIConfig.Instance.AdreanlineBarX * 0.01)) - imageBar.Width() / 2, 0);
            panel.Top.Set((int)(Main.MenuUI.GetDimensions().Height * (BossGUIConfig.Instance.AdreanlineBarY * 0.01)) - imageBar.Height() / 2, 0);

            title.Left.Set(imageBar.Width() - imageBar.Width() / 2, 0);
            title.Top.Set(imageBar.Height() + title.Height.Pixels + 10, 0);
            title.HAlign = 0.5f;

            float quotient = 0;
            BossAdrenalineSystem system = ModContent.GetInstance<BossAdrenalineSystem>();
            BossAdrenalinePlayer player = Main.LocalPlayer.GetModPlayer<BossAdrenalinePlayer>();

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                quotient = (float)system.AdrenalineCounter / system.AdrenalineCounterMax;
            }
            else
            {
                quotient = (float)player.adrenalineCounter / player.adrenalineCounterMax;
            }

            quotient = Utils.Clamp(quotient, 0f, 1f);
            int offet = 4;

            Rectangle progressBar = new Rectangle((int)panel.Left.Pixels + offet, (int)panel.Top.Pixels + offet, imageBar.Width() - offet*2, imageBar.Height() - offet*2);
            progressBar.Width = (int)(progressBar.Width * quotient);

            Color color = Color.Red;
            bool adren = false;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                adren = system.Adrenaline;
            }
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                adren = player.adrenaline;
            }

            if (adren)
            {
                color = Color.LimeGreen;
            }
            else
            {
                color = Color.Red;
            }

            spriteBatch.Draw(TextureAssets.MagicPixel.Value, progressBar, color);

            base.DrawSelf(spriteBatch);
        }
    }

    public class AdrenalineUISystem : ModSystem
    {
        internal UserInterface adrenalineInterface;
        internal AdrenalineUI AUI;
        private GameTime lastGameTime;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                adrenalineInterface = new UserInterface();
                AUI = new AdrenalineUI();
                AUI.Activate();
            }
            base.Load();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            lastGameTime = gameTime;
            if ( adrenalineInterface?.CurrentState != null)
            {
                adrenalineInterface.Update(gameTime);
            }

            adrenalineInterface?.SetState(AUI);
            base.UpdateUI(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Boss Adrenaline Counter",
                    delegate
                    {
                        if (lastGameTime != null && adrenalineInterface?.CurrentState != null)
                        {
                            adrenalineInterface.Draw(Main.spriteBatch, lastGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
            base.ModifyInterfaceLayers(layers);
        }
    }
}



    //internal class AdreanlineBar : UIState
    //{
    //    private UIElement area;
    //    private UIImage barFrame;

    //    public override void OnInitialize()
    //    {
    //        area = new UIElement();
    //        area.Left.Set(-area.Width.Pixels - (area.Width.Pixels * (BossGUIConfig.Instance.AdreanlineBarX * 0.01f)), 0f);
    //        area.Top.Set(area.Height.Pixels + (area.Width.Pixels * (BossGUIConfig.Instance.AdreanlineBarY * 0.01f)), 0f);
    //        area.Width.Set(Main.MenuUI.GetDimensions().Width, 0f);
    //        area.Height.Set(Main.MenuUI.GetDimensions().Height, 0f);

    //        barFrame = new UIImage(ModContent.Request<Texture2D>("BossAdreanlineMode/bar")); // Frame of our resource bar
    //        barFrame.Left.Set(22, 0f);
    //        barFrame.Top.Set(0, 0f);
    //        barFrame.Width.Set(138, 0f);
    //        barFrame.Height.Set(34, 0f);



    //        area.Append(barFrame);
    //        Append(area);
    //    }

    //    public override void Draw(SpriteBatch spriteBatch)
    //    {
    //        if (Main.netMode == NetmodeID.SinglePlayer && ModContent.GetInstance<BossAdrenalineSystem>().boss == false)
    //        {
    //            return;
    //        } else if (Main.netMode == NetmodeID.MultiplayerClient && Main.LocalPlayer.GetModPlayer<BossAdrenalinePlayer>().boss == false) 
    //        {
    //            return;
    //        }

    //        base.Draw(spriteBatch);
    //    }

    //    protected override void DrawSelf(SpriteBatch spriteBatch)
    //    {
    //        base.DrawSelf(spriteBatch);

    //        var system = ModContent.GetInstance<BossAdrenalineSystem>();

    //        float quotient = 0f;
    //        if (Main.netMode == NetmodeID.SinglePlayer)
    //        {
    //            quotient = (float)system.AdrenalineCounter / system.AdrenalineCounterMax;
    //        } else
    //        {
    //            quotient = (float)Main.LocalPlayer.GetModPlayer<BossAdrenalinePlayer>().adrenalineCounter/ Main.LocalPlayer.GetModPlayer<BossAdrenalinePlayer>().adrenalineCounterMax;
    //        }
    //        quotient = Utils.Clamp(quotient, 0.01f, 0.9888f);

    //        Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
    //        hitbox.X += 12;
    //        hitbox.Width -= 24;
    //        hitbox.Y += 8;
    //        hitbox.Height -= 16;

    //        //area.Left.Set(-area.Width.Pixels - (area.Width.Pixels * (BossGUIConfig.Instance.AdreanlineBarX * 0.01f)), 1f);
    //        //area.Top.Set(area.Height.Pixels + (area.Width.Pixels * (BossGUIConfig.Instance.AdreanlineBarY * 0.01f)), 0f);

    //        area.Left.Set(-area.Width.Pixels - (area.Width.Pixels * (BossGUIConfig.Instance.AdreanlineBarX * 0.01f)), 0f);
    //        area.Top.Set(area.Height.Pixels + (area.Width.Pixels * (BossGUIConfig.Instance.AdreanlineBarY * 0.01f)), 0f);

    //        int left = hitbox.Left;
    //        int right = hitbox.Right;
    //        int steps = (int)((right - left) * quotient);
    //        for (int i = 0; i < steps; i += 1)
    //        {
    //            float percent = (float)i / (right - left);
    //            bool adren = false;
    //            if (Main.netMode == NetmodeID.SinglePlayer)
    //            {
    //                adren = system.Adrenaline;
    //            } else if (Main.netMode == NetmodeID.MultiplayerClient) 
    //            { 
    //                adren = Main.LocalPlayer.GetModPlayer<BossAdrenalinePlayer>().adrenaline;
    //            }
    //            if (adren)
    //            {
    //                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(new Color(0, 225, 0), new Color(0, 225, 0), percent));
    //            }
    //            else
    //            {
    //                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(new Color(255, 0, 0), new Color(255, 0, 0), percent));
    //            }
    //        }
    //    }

    //    public override void Update(GameTime gameTime)
    //    {
    //        if (Main.netMode == NetmodeID.SinglePlayer && ModContent.GetInstance<BossAdrenalineSystem>().boss == false)
    //        {
    //            return;
    //        }
    //        else if (Main.netMode == NetmodeID.MultiplayerClient && Main.LocalPlayer.GetModPlayer<BossAdrenalinePlayer>().boss == false)
    //        {
    //            return;
    //        }

    //        base.Update(gameTime);
    //    }
    //}

    //class ExampleResourseUISystem : ModSystem
    //{
    //    private UserInterface adreanlineBarUserInterface;

    //    internal AdreanlineBar adreanlineBar;

    //    public override void Load()
    //    {
    //        if (!Main.dedServ)
    //        {
    //            adreanlineBar = new();
    //            adreanlineBarUserInterface = new();
    //            adreanlineBarUserInterface.SetState(adreanlineBar);
    //        }
    //    }

    //    public override void UpdateUI(GameTime gameTime)
    //    {
    //        adreanlineBarUserInterface?.Update(gameTime);
    //    }

    //    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    //    {
    //        int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
    //        if (resourceBarIndex != -1)
    //        {
    //            layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
    //                "BossAdrenalineMode: Boss Adrenaline Timer",
    //                delegate {
    //                    adreanlineBarUserInterface.Draw(Main.spriteBatch, new GameTime());
    //                    return true;
    //                },
    //                InterfaceScaleType.UI)
    //            );
    //        }
    //    }
    //}
//}
