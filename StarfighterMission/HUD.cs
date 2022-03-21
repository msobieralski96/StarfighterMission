using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace StarfighterMission
{
    internal class HUD
    {
        Game1 game;
        int screenHeight, screenWidth;
        Texture2D zero, one, two, three, four, five, six, seven, eight, nine;
        Texture2D percent, leftbracket, rightbracket;
        Texture2D score, lives, level, hp, enemies;
        Texture2D torpedoes, bombs, shields, stealth, r2d2;
        Texture2D torpedoes2, bombs2, shields2, stealth2, r2d22;
        Texture2D live;
        int textHeight, spaceWidth, digitWidth;


        public HUD(int screenHeight, int screenWidth, Game1 game)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.game = game;
            Initialize();
        }

        public void Initialize()
        {
            textHeight = screenHeight / 50;
            spaceWidth = screenWidth / 40;
        }

        public void LoadContent()
        {
            zero = game.Content.Load<Texture2D>("hud/0");
            digitWidth = (int) (1.2 * ((screenHeight / 50 * zero.Width) / zero.Height));
            one = game.Content.Load<Texture2D>("hud/1");
            two = game.Content.Load<Texture2D>("hud/2");
            three = game.Content.Load<Texture2D>("hud/3");
            four = game.Content.Load<Texture2D>("hud/4");
            five = game.Content.Load<Texture2D>("hud/5");
            six = game.Content.Load<Texture2D>("hud/6");
            seven = game.Content.Load<Texture2D>("hud/7");
            eight = game.Content.Load<Texture2D>("hud/8");
            nine = game.Content.Load<Texture2D>("hud/9");
            percent = game.Content.Load<Texture2D>("hud/percent");
            leftbracket = game.Content.Load<Texture2D>("hud/leftbracket");
            rightbracket = game.Content.Load<Texture2D>("hud/rightbracket");
            score = game.Content.Load<Texture2D>("hud/score");
            lives = game.Content.Load<Texture2D>("hud/lives");
            level = game.Content.Load<Texture2D>("hud/level");
            hp = game.Content.Load<Texture2D>("hud/hp");
            enemies = game.Content.Load<Texture2D>("hud/enemies");
            torpedoes = game.Content.Load<Texture2D>("hud/torpedoes");
            torpedoes2 = game.Content.Load<Texture2D>("hud/torpedoes2");
            bombs = game.Content.Load<Texture2D>("hud/bombs");
            bombs2 = game.Content.Load<Texture2D>("hud/bombs2");
            shields = game.Content.Load<Texture2D>("hud/shields");
            shields2 = game.Content.Load<Texture2D>("hud/shields2");
            stealth = game.Content.Load<Texture2D>("hud/stealth");
            stealth2 = game.Content.Load<Texture2D>("hud/stealth2");
            r2d2 = game.Content.Load<Texture2D>("hud/r2d2");
            r2d22 = game.Content.Load<Texture2D>("hud/r2d22");
            live = game.Content.Load<Texture2D>("delta7");
        }

        public void Update()
        {
            UpdateHP();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawScore(spriteBatch);
            DrawHP(spriteBatch);
            DrawLives(spriteBatch);
        }

        private void UpdateHP()
        {

        }

        private void DrawScore(SpriteBatch spriteBatch)
        {
            DrawText(spriteBatch, score, screenWidth / 20, screenHeight / 50, Color.White);
            DrawScoreNumber(spriteBatch, screenWidth / 20, screenHeight / 50, Color.White);
        }

        private void DrawHP(SpriteBatch spriteBatch)
        {
            DrawText(spriteBatch, hp, screenWidth / 20, screenHeight / 50 * 3, Color.White);
            DrawHPNumber(spriteBatch, screenWidth / 20, screenHeight / 50 * 3, Color.White);
        }

        private void DrawLives(SpriteBatch spriteBatch)
        {
            int livesTextWidth = (screenHeight / 50 * lives.Width) / lives.Height;
            int livePicWidth = (int) (1.7 * (screenHeight / 50 * live.Width) / live.Height);
            DrawText(spriteBatch, lives, screenWidth / 20, screenHeight / 50 * 5, Color.White);
            for(int i = 0; i < Gra.Lives; i++)
            {
                DrawText(spriteBatch, live, 
                    screenWidth / 20 + livesTextWidth + spaceWidth + (i * livePicWidth), 
                    screenHeight / 50 * 5, Color.White, 1.5);
            }
        }

        private void DrawScoreNumber(SpriteBatch spriteBatch, int x, int y, Color color)
        {
            int scoreTextWidth = (screenHeight / 50 * score.Width) / score.Height;
            int digits = Gra.Score.ToString().Length;
            int number = Gra.Score;
            int digit;
            for (int i = 0; i < digits; i++)
            {
                digit = number % 10;
                DrawDigit(spriteBatch, x + scoreTextWidth + spaceWidth + (digits - (i + 1)) * digitWidth, y, color, digit);
                number = number / 10;
            }
        }

        private void DrawDigit(SpriteBatch spriteBatch, int x, int y, Color color, int digit)
        {
            switch (digit)
            {
                case 0:
                    DrawText(spriteBatch, zero, x, y, color);
                    break;
                case 1:
                    DrawText(spriteBatch, one, x, y, color);
                    break;
                case 2:
                    DrawText(spriteBatch, two, x, y, color);
                    break;
                case 3:
                    DrawText(spriteBatch, three, x, y, color);
                    break;
                case 4:
                    DrawText(spriteBatch, four, x, y, color);
                    break;
                case 5:
                    DrawText(spriteBatch, five, x, y, color);
                    break;
                case 6:
                    DrawText(spriteBatch, six, x, y, color);
                    break;
                case 7:
                    DrawText(spriteBatch, seven, x, y, color);
                    break;
                case 8:
                    DrawText(spriteBatch, eight, x, y, color);
                    break;
                case 9:
                    DrawText(spriteBatch, nine, x, y, color);
                    break;
                default:
                    break;
            }
        }

        private void DrawHPNumber(SpriteBatch spriteBatch, int x, int y, Color color)
        {
            int hpTextWidth = (screenHeight / 50 * hp.Width) / hp.Height;
            switch (Player.HP)
            {
                case 0:
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    break;
                case 1:
                    DrawText(spriteBatch, one, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + 2 * digitWidth, y, color);
                    break;
                case 2:
                    DrawText(spriteBatch, two, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + 2 * digitWidth, y, color);
                    break;
                case 3:
                    DrawText(spriteBatch, three, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + 2 * digitWidth, y, color);
                    break;
                case 4:
                    DrawText(spriteBatch, four, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + 2 * digitWidth, y, color);
                    break;
                case 5:
                    DrawText(spriteBatch, five, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + 2 * digitWidth, y, color);
                    break;
                case 6:
                    DrawText(spriteBatch, six, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + 2 * digitWidth, y, color);
                    break;
                case 7:
                    DrawText(spriteBatch, seven, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + 2 * digitWidth, y, color);
                    break;
                case 8:
                    DrawText(spriteBatch, eight, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + 2 * digitWidth, y, color);
                    break;
                case 9:
                    DrawText(spriteBatch, nine, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + 2 * digitWidth, y, color);
                    break;
                case 10:
                    DrawText(spriteBatch, one, x + hpTextWidth + spaceWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + digitWidth, y, color);
                    DrawText(spriteBatch, zero, x + hpTextWidth + spaceWidth + 2 * digitWidth, y, color);
                    DrawText(spriteBatch, percent, x + hpTextWidth + spaceWidth + 3 * digitWidth, y, color);
                    break;
                default:
                    break;
            }
        }

        private void DrawText(SpriteBatch spriteBatch, Texture2D tex, int x, int y, Color color)
        {
            int textWidth = (textHeight * tex.Width) / tex.Height;
            spriteBatch.Draw(tex, new Rectangle(x, y, textWidth, textHeight), color);
            //System.Console.WriteLine("X = " + screenWidth / 20);
            //System.Console.WriteLine("Y = " + screenHeight / 50);
            //System.Console.WriteLine("Width = " + textWidth);
            //System.Console.WriteLine("Height = " + textHeight);
            //System.Console.WriteLine("tex.Height = " + tex.Height);
            //System.Console.WriteLine("tex.Width = " + tex.Width);
        }

        private void DrawText(SpriteBatch spriteBatch, Texture2D tex, int x, int y, Color color, double scale)
        {
            int textWidth = (textHeight * tex.Width) / tex.Height;
            spriteBatch.Draw(tex, new Rectangle(x, y, (int)(scale * textWidth), (int)(scale * textHeight)), color);
            //System.Console.WriteLine("X = " + screenWidth / 20);
            //System.Console.WriteLine("Y = " + screenHeight / 50);
            //System.Console.WriteLine("Width = " + textWidth);
            //System.Console.WriteLine("Height = " + textHeight);
            //System.Console.WriteLine("tex.Height = " + tex.Height);
            //System.Console.WriteLine("tex.Width = " + tex.Width);
        }
    }
}