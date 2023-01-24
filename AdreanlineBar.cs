using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using System.Collections.Generic;
using SteelSeries.GameSense;

namespace BossAdreanlineMode
{
    internal class AdreanlineBar : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
        private UIElement area;
        private UIImage barFrame;

        public override void OnInitialize()
        {
            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding.
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - BossConfig.Instance.AdreanlineBarX, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(-area.Height.Pixels + BossConfig.Instance.AdreanlineBarY - 60f, 0f); // Placing it just a bit below the top of the screen.
            area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(60, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("BossAdreanlineMode/bar")); // Frame of our resource bar
            barFrame.Left.Set(22, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(34, 0f);

            

            area.Append(barFrame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ModContent.GetInstance<BossAdreanlineSystem>().boss == false)
            {
                return;
            }

            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var system = ModContent.GetInstance<BossAdreanlineSystem>();
            // Calculate quotient
            float quotient = (float)system.adreanlineCounter / system.adreanlineCounterMax; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 8;
            hitbox.Height -= 16;

            area.Left.Set(-area.Width.Pixels - BossConfig.Instance.AdreanlineBarX, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(area.Height.Pixels + BossConfig.Instance.AdreanlineBarY - 60f, 0f); // Placing it just a bit below the top of the screen.

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                float percent = (float)i / (right - left);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(new Color(78, 224, 42), new Color(0, 225, 0), percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (ModContent.GetInstance<BossAdreanlineSystem>().boss == false)
            {
                return;
            }
            base.Update(gameTime);
        }
    }

    class ExampleResourseUISystem : ModSystem
    {
        private UserInterface adreanlineBarUserInterface;

        internal AdreanlineBar adreanlineBar;

        public override void Load()
        {
            // All code below runs only if we're not loading on a server
            if (!Main.dedServ)
            {
                adreanlineBar = new();
                adreanlineBarUserInterface = new();
                adreanlineBarUserInterface.SetState(adreanlineBar);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            adreanlineBarUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Example Resource Bar",
                    delegate {
                        adreanlineBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
