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
using Microsoft.Xna.Framework;

namespace StarfighterMission
{
    class Star
    {
        private static Texture2D texture;
        private Rectangle shape;
        private int screenHeight, screenWidth;
        private int size;
        private double r, x, y, angle;
        private int maxExpireDelay = 50;
        private int expireDelay;
        private int speed = 1;
        bool alive = true;

        Game1 game;

        public Rectangle Shape
        {
            get
            {
                return shape;
            }

            set
            {
                shape = value;
            }
        }

        public bool Alive
        {
            get
            {
                return alive;
            }

            set
            {
                alive = value;
            }
        }

        public Star(int screenHeight, int screenWidth, Game1 game, double angle)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.game = game;
            size = screenWidth / 480;
            this.angle = Utils.ConvertToRadians(angle);
            expireDelay = maxExpireDelay;
            r = ((screenWidth / 2) * 1.4) / 205 * 20;
            x = (int)(Math.Cos(angle) * r + screenWidth / 2) - size / 2;
            y = (int)(Math.Sin(angle) * r + screenHeight / 2) - size / 2;
            shape = new Rectangle((int)x, (int)y, size, size);
        }

        public static void SetTexture(Game1 game)
        {
            texture = game.Content.Load<Texture2D>("white");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, shape, Color.White);
        }

        public void Expire()
        {
            if(expireDelay <= 0)
            {
                alive = false;
            }
            else if (expireDelay <= 6)
            {
                speed = 8;
                size = screenWidth / 60;
            }
            else if (expireDelay <= 13)
            {
                speed = 8;
                size = screenWidth / 69;
            }
            else if (expireDelay <= 19)
            {
                speed = 4;
                size = screenWidth / 94;
            }
            else if (expireDelay <= 25)
            {
                speed = 4;
                size = screenWidth / 137;
            }
            else if (expireDelay <= 31)
            {
                speed = 2;
                size = screenWidth / 197;
            }
            else if (expireDelay <= 37)
            {
                speed = 2;
                size = screenWidth / 274;
            }
            else if (expireDelay <= 43)
            {
                speed = 1;
                size = screenWidth / 369;
            }
            shape.Width = size;
            shape.Height = size;
            r += ((screenWidth / 2) * 1.4)/ 205 * speed;
            shape.X = (int)(Math.Cos(angle) * r + screenWidth / 2) - size / 2;
            shape.Y = (int)(Math.Sin(angle) * r + screenHeight / 2) - size / 2;
            expireDelay--;
        }
    }
}