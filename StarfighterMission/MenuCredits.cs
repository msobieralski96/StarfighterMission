using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using StarfighterMission;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Input;

namespace StarfighterMission
{
    class MenuCredits
    {
        static bool active;
        int screenHeight, screenWidth;
        Game1 game;
        TouchCollection touchCollection;
        Texture2D txt;
        Rectangle txtRect;
        bool isInputPressed = false;
        bool blockUntilReleased = true;
        int textHeight;

        public static bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        public MenuCredits(int screenHeight, int screenWidth, Game1 game)
        {
            this.game = game;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            textHeight = screenHeight / 40;
        }

        public void LoadContent()
        {
            txt = game.Content.Load<Texture2D>("menuHud/Credits2");

            txtRect = Utils.GetRectangleForTexture2D(txt, screenWidth / 2 - (((int)(textHeight * 16.5) * txt.Width) / txt.Height) / 2,
                screenHeight / 2 - (int)(textHeight * 8.25), (int)(textHeight * 16.5));
        }

        public void Update(Game1 game1)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                ApplicationManager.callMenu();

            touchCollection = TouchPanel.GetState();
            int xTouch = 0;
            int yTouch = 0;
            if (touchCollection.Count >= 1)
            {
                var touch = touchCollection[0];
                xTouch = (int)touch.Position.X;
                yTouch = (int)touch.Position.Y;

                if (!blockUntilReleased)
                {
                    blockUntilReleased = true;
                    isInputPressed = touch.State == TouchLocationState.Pressed || touch.State == TouchLocationState.Moved;
                }
                if (touch.State == TouchLocationState.Released)
                {
                    blockUntilReleased = false;
                }
            }
            if (isInputPressed)
            {
                isInputPressed = false;
                ApplicationManager.callMenu();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            Utils.DrawText(spriteBatch, txt, txtRect, Color.White);
            spriteBatch.End();
        }
    }
}