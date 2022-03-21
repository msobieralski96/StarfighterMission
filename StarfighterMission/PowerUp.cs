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
    class PowerUp
    {
        private static Texture2D texture;
        private RotatingRectangle shape;
        private int screenHeight, screenWidth;
        private int width, height;
        private double r, x, y;
        private int expireTime;
        private int points;
        private int hp;

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

        public int Points
        {
            get
            {
                return points;
            }

            set
            {
                points = value;
            }
        }

        public int ExpireTime
        {
            get
            {
                return expireTime;
            }

            set
            {
                expireTime = value;
            }
        }

        public int Hp
        {
            get
            {
                return hp;
            }

            set
            {
                hp = value;
            }
        }

        public PowerUp(int screenHeight, int screenWidth, Game1 game, double angle, int points, int expireTime)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.game = game;
            this.points = points;
            this.expireTime = expireTime;
            hp = 1;
            width = screenWidth / 12;
            height = screenHeight / 45;
            r = (screenWidth / 2) * 0.5;
            x = (int)(Math.Cos(Utils.ConvertToRadians(angle+90)) * r + screenWidth / 2) - width / 2;
            y = (int)(Math.Sin(Utils.ConvertToRadians(angle+90)) * r + screenHeight / 2) - height / 2;

            shape = new RotatingRectangle(new Rectangle((int)x, (int)y, width, height), (float)Utils.ConvertToRadians(angle));
        }

        public static void SetTexture(Game1 game)
        {
            texture = game.Content.Load<Texture2D>("powerups/powerupGun");
        }

        public void Expire()
        {
            expireTime--;
            if (expireTime <= 0)
            {
                alive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle aPositionAdjusted = new Rectangle(shape.X + (shape.Width / 2), shape.Y + (shape.Height / 2), shape.Width, shape.Height);
            spriteBatch.Draw(texture, aPositionAdjusted, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, shape.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);
        }
    }
}