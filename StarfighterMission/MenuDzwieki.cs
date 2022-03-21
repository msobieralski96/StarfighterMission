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
    class MenuDzwieki
    {
        static bool active;
        int screenHeight, screenWidth;
        Game1 game;
        TouchCollection touchCollection;
        Texture2D musicON, musicOFF, soundsON, soundsOFF, back;
        Rectangle musicRect, soundsRect, backRect;
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

        public MenuDzwieki(int screenHeight, int screenWidth, Game1 game)
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
            musicON = game.Content.Load<Texture2D>("menuHud/MusicPict");
            musicOFF = game.Content.Load<Texture2D>("menuHud/MusicPict2");
            soundsON = game.Content.Load<Texture2D>("menuHud/SoundsPict");
            soundsOFF = game.Content.Load<Texture2D>("menuHud/SoundsPict2");
            back = game.Content.Load<Texture2D>("menuHud/Back");

            musicRect = Utils.GetRectangleForTexture2D(musicON, screenWidth / 2 - (((int)(textHeight * 4.75) * musicON.Width) / musicON.Height) / 2,
                screenHeight / 2 - (int)(textHeight * 7.25), (int)(textHeight * 4.75));
            soundsRect = Utils.GetRectangleForTexture2D(soundsON, screenWidth / 2 - (((int)(textHeight * 4.75) * soundsON.Width) / soundsON.Height) / 2,
                screenHeight / 2 - (int)(textHeight * 0.5), (int)(textHeight * 4.75));
            backRect = Utils.GetRectangleForTexture2D(back, screenWidth / 2 - ((textHeight * back.Width) / back.Height) / 2,
                screenHeight / 2 + (int)(textHeight * 6.25), textHeight);

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
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, musicRect))
            {
                if (Gra.MusicActive)
                {
                    Gra.MusicActive = false;
                } else
                {
                    Gra.MusicActive = true;
                }
                isInputPressed = false;
            }
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, soundsRect))
            {
                if (Gra.SoundsActive)
                {
                    Gra.SoundsActive = false;
                }
                else
                {
                    Gra.SoundsActive = true;
                }
                isInputPressed = false;
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

            if (Gra.MusicActive)
            {
                Utils.DrawText(spriteBatch, musicON, musicRect, Color.White);
            }
            else
            {
                Utils.DrawText(spriteBatch, musicOFF, musicRect, Color.Gray);
            }
            if (Gra.SoundsActive)
            {
                Utils.DrawText(spriteBatch, soundsON, soundsRect, Color.White);
            }
            else
            {
                Utils.DrawText(spriteBatch, soundsOFF, soundsRect, Color.Gray);
            }
            Utils.DrawText(spriteBatch, back, backRect, Color.White);
            spriteBatch.End();
        }
    }
}