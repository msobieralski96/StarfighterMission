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
    class MenuSterowanie
    {
        static bool active;
        int screenHeight, screenWidth;
        Game1 game;
        //private SpriteFont txt;
        TouchCollection touchCollection;
        Texture2D classic, rotatable, back;
        Rectangle classicRect, rotatableRect, backRect;
        //TouchingText klasyczne, obrotowe;
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

        public MenuSterowanie(int screenHeight, int screenWidth, Game1 game)
        {
            this.game = game;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            //Initialize();
            textHeight = screenHeight / 25;
        }

        /*public void Initialize()
        {
            klasyczne = new TouchingText(screenHeight, screenWidth, game, "Klasyczne");
            obrotowe = new TouchingText(screenHeight, screenWidth, game, "Obrotowe");
        }*/

        public void LoadContent()
        {
            classic = game.Content.Load<Texture2D>("menuHud/ClassicPict");
            rotatable = game.Content.Load<Texture2D>("menuHud/RotatablePict");
            back = game.Content.Load<Texture2D>("menuHud/Back");

            classicRect = Utils.GetRectangleForTexture2D(classic, screenWidth / 2 - (((int)(textHeight * 5.1) * classic.Width) / classic.Height) / 2,
                screenHeight / 2 - (int)(textHeight * 7.6), (int)(textHeight * 5.1));
            rotatableRect = Utils.GetRectangleForTexture2D(rotatable, screenWidth / 2 - (((int)(textHeight * 5.1) * rotatable.Width) / rotatable.Height) / 2,
                screenHeight / 2 - (int)(textHeight * 0.5), (int)(textHeight * 5.1));
            backRect = Utils.GetRectangleForTexture2D(back, screenWidth / 2 - ((textHeight * back.Width) / back.Height) / 2,
                screenHeight / 2 + (int)(textHeight * 6.6), textHeight);

            /*txt = game.Content.Load<SpriteFont>("bigfont");
            klasyczne.SetXANDY(txt, screenWidth / 2, screenHeight / 2 - 60);
            obrotowe.SetXANDY(txt, screenWidth / 2, screenHeight / 2 + 60);*/
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
            //if (isInputPressed && klasyczne.CheckIfPointIsInRect(xTouch, yTouch))
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, classicRect))
            {
                isInputPressed = false;
                Gra.Controls = false;
                Gra.AccelerometerStop();
                //ApplicationManager.callMenu();
            }
            //if (isInputPressed && obrotowe.CheckIfPointIsInRect(xTouch, yTouch))
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, rotatableRect))
            {
                isInputPressed = false;
                Gra.Controls = true;
                Gra.AccelerometerInit();
                //ApplicationManager.callMenu();
            }
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, backRect))
            {
                isInputPressed = false;
                ApplicationManager.callMenu();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //klasyczne.Draw(spriteBatch);
            //obrotowe.Draw(spriteBatch);
            if (Gra.Controls)
            {
                Utils.DrawText(spriteBatch, classic, classicRect, Color.Gray);
                Utils.DrawText(spriteBatch, rotatable, rotatableRect, Color.White);
            }
            else
            {
                Utils.DrawText(spriteBatch, classic, classicRect, Color.White);
                Utils.DrawText(spriteBatch, rotatable, rotatableRect, Color.Gray);
            }
            Utils.DrawText(spriteBatch, back, backRect, Color.White);
            spriteBatch.End();
        }
    }
}