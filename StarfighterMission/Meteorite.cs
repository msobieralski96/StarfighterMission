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
    class Meteorite
    {
        private static Texture2D texture;
        private RotatingRectangle shape;
        private int screenHeight, screenWidth;
        private int size;
        private double r, x, y;
        private int maxExpireDelay = 100;
        private int expireDelay;
        private int speed = 1;
        bool alive = true;

        Game1 game;

        public RotatingRectangle Shape
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

        public Meteorite(int screenHeight, int screenWidth, Game1 game, double angle)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.game = game;
            size = screenWidth / 60;
            expireDelay = maxExpireDelay;
            r = ((screenWidth / 2) * 1.4) / 410 * 20;
            x = (int)(Math.Cos(angle) * r + screenWidth / 2) - size / 2;
            y = (int)(Math.Sin(angle) * r + screenHeight / 2) - size / 2;

            shape = new RotatingRectangle(new Rectangle((int)x, (int)y, size, size), (float)Utils.ConvertToRadians(angle));
        }

        public static void SetTexture(Game1 game)
        {
            texture = game.Content.Load<Texture2D>("meteorite");
            //texture = game.Content.Load<Texture2D>("white");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle aPositionAdjusted = new Rectangle(shape.X + (shape.Width / 2), shape.Y + (shape.Height / 2), shape.Width, shape.Height);
            spriteBatch.Draw(texture, aPositionAdjusted, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, shape.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);
        }

        public void Expire()
        {
            if (expireDelay <= 0)
            {
                alive = false;
            }
            else if (expireDelay <= 12)
            {
                speed = 8;
                size = screenWidth / 7;
            }
            else if (expireDelay <= 26)
            {
                speed = 8;
                size = screenWidth / 9;
            }
            else if (expireDelay <= 38)
            {
                speed = 4;
                size = screenWidth / 12;
            }
            else if (expireDelay <= 50)
            {
                speed = 4;
                size = screenWidth / 17;
            }
            else if (expireDelay <= 62)
            {
                speed = 2;
                size = screenWidth / 25;
            }
            else if (expireDelay <= 74)
            {
                speed = 2;
                size = screenWidth / 34;
            }
            else if (expireDelay <= 86)
            {
                speed = 1;
                size = screenWidth / 46;
            }
            shape.CollisionRectangle.Width = size;
            shape.CollisionRectangle.Height = size;
            shape.Origin = new Vector2(size / 2, size / 2);
            r += ((screenWidth / 2) * 1.4)/ 410 * speed;
            shape.CollisionRectangle.X = (int)(Math.Cos(shape.Rotation) * r + screenWidth / 2) - size / 2;
            shape.CollisionRectangle.Y = (int)(Math.Sin(shape.Rotation) * r + screenHeight / 2) - size / 2;
            expireDelay--;
        }
    }
}