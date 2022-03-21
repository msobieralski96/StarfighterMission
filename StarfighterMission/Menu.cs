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
    class Menu
    {
        static bool active;
        int screenHeight, screenWidth;
        Game1 game;
        //private SpriteFont txt;
        TouchCollection touchCollection;
        Texture2D continueGame, newGame, controls, equipment;
        Texture2D sounds, highScores, credits, exit;
        Rectangle continueRect, newGameRect, controlsRect, equipmentRect;
        Rectangle soundsRect, highScoresRect, creditsRect, exitRect;
        //TouchingText nowaGra, sterowanie, wyjscie;
        bool isInputPressed = false;
        bool blockUntilReleased = false;
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

        public Menu(int screenHeight, int screenWidth, Game1 game)
        {
            this.game = game;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            //Initialize();
            textHeight = screenHeight / 25;
        }

        /*public void Initialize()
        {
            nowaGra = new TouchingText(screenHeight, screenWidth, game, "Nowa Gra");
            sterowanie = new TouchingText(screenHeight, screenWidth, game, "Sterowanie");
            wyjscie = new TouchingText(screenHeight, screenWidth, game, "Wyjœcie");
        }*/

        public void LoadContent()
        {
            continueGame = game.Content.Load<Texture2D>("menuHud/Continue");
            newGame = game.Content.Load<Texture2D>("menuHud/NewGame");
            controls = game.Content.Load<Texture2D>("menuHud/Controls");
            equipment = game.Content.Load<Texture2D>("menuHud/Equipment");
            sounds = game.Content.Load<Texture2D>("menuHud/Sounds");
            highScores = game.Content.Load<Texture2D>("menuHud/HighScores");
            credits = game.Content.Load<Texture2D>("menuHud/Credits");
            exit = game.Content.Load<Texture2D>("menuHud/Exit");

            continueRect = Utils.GetRectangleForTexture2D(continueGame, screenWidth / 2 - ((textHeight * continueGame.Width) / continueGame.Height) / 2,
                screenHeight / 2 - textHeight / 2 * 15, textHeight);
            newGameRect = Utils.GetRectangleForTexture2D(newGame, screenWidth / 2 - ((textHeight * newGame.Width) / newGame.Height) / 2,
                screenHeight / 2 - textHeight / 2 * 11, textHeight);
            controlsRect = Utils.GetRectangleForTexture2D(controls, screenWidth / 2 - ((textHeight * controls.Width) / controls.Height) / 2,
                screenHeight / 2 - textHeight / 2 * 7, textHeight);
            equipmentRect = Utils.GetRectangleForTexture2D(equipment, screenWidth / 2 - (((int)(textHeight * 1.25) * equipment.Width) / equipment.Height) / 2,
                screenHeight / 2 - textHeight / 2 * 3, (int)(textHeight * 1.25));
            soundsRect = Utils.GetRectangleForTexture2D(sounds, screenWidth / 2 - ((textHeight * sounds.Width) / sounds.Height) / 2,
                screenHeight / 2 + textHeight / 2, textHeight);
            highScoresRect = Utils.GetRectangleForTexture2D(highScores, screenWidth / 2 - (((int)(textHeight * 1.25) * highScores.Width) / highScores.Height) / 2,
                screenHeight / 2 + textHeight / 2 * 5, (int)(textHeight * 1.25));
            creditsRect = Utils.GetRectangleForTexture2D(credits, screenWidth / 2 - ((textHeight * credits.Width) / credits.Height) / 2,
                screenHeight / 2 + textHeight / 2 * 9, textHeight);
            exitRect = Utils.GetRectangleForTexture2D(exit, screenWidth / 2 - ((textHeight * exit.Width) / exit.Height) / 2,
                screenHeight / 2 + textHeight / 2 * 13, textHeight);

            /*txt = game.Content.Load<SpriteFont>("bigfont");
            nowaGra.SetXANDY(txt, screenWidth / 2, screenHeight / 2 - 120);
            sterowanie.SetXANDY(txt, screenWidth / 2, screenHeight / 2);
            wyjscie.SetXANDY(txt, screenWidth / 2, screenHeight / 2 + 120);*/
        }

        public void Update(Game1 game1)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                game1.Quit();

            //https://gamedev.stackexchange.com/questions/56479/how-to-create-draw-clickable-object-such-as-button-in-winform-in-xna-wp8

            //old links:
            //https://adamboyne.wordpress.com/2013/07/02/dealing-with-rejection-windows-store-style/
            //http://www.plungeinteractive.com/blog/2012/12/19/getting-started-with-monogame-for-android-on-windows/

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
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, continueRect) && Gra.AbleToContinue)
            {
                isInputPressed = false;
                ApplicationManager.callGra();
            }
            //if (isInputPressed && nowaGra.CheckIfPointIsInRect(xTouch, yTouch))
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, newGameRect))
            {
                isInputPressed = false;
                ApplicationManager.callGra();
                game1.AppManager.prepareNewGame();
            }
            //if (isInputPressed && sterowanie.CheckIfPointIsInRect(xTouch, yTouch))
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, controlsRect))
            {
                isInputPressed = false;
                ApplicationManager.callSterowanie();
            }
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, soundsRect))
            {
                isInputPressed = false;
                ApplicationManager.callDzwieki();
            }
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, creditsRect))
            {
                isInputPressed = false;
                ApplicationManager.callCredits();
            }
            //if (isInputPressed && wyjscie.CheckIfPointIsInRect(xTouch, yTouch))
            if (isInputPressed && Utils.CheckIfPointIsInRect(xTouch, yTouch, exitRect))
            {
                isInputPressed = false;
                game1.Quit();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            /*nowaGra.Draw(spriteBatch);
            sterowanie.Draw(spriteBatch);
            wyjscie.Draw(spriteBatch);*/

            if (Gra.AbleToContinue)
            {
                Utils.DrawText(spriteBatch, continueGame, continueRect, Color.White);
            }
            else
            {
                Utils.DrawText(spriteBatch, continueGame, continueRect, Color.Gray);
            }
            Utils.DrawText(spriteBatch, newGame, newGameRect, Color.White);
            Utils.DrawText(spriteBatch, controls, controlsRect, Color.White);
            Utils.DrawText(spriteBatch, equipment, equipmentRect, Color.Gray);
            Utils.DrawText(spriteBatch, sounds, soundsRect, Color.White);
            Utils.DrawText(spriteBatch, highScores, highScoresRect, Color.Gray);
            Utils.DrawText(spriteBatch, credits, creditsRect, Color.White);
            Utils.DrawText(spriteBatch, exit, exitRect, Color.White);
            spriteBatch.End();
        }
    }
}