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
