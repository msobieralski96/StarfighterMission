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
    class Bullet
    {
        private static Texture2D texture;
        private RotatingRectangle shape;
        private int screenHeight, screenWidth;
        private int size;
        private double r, x, y, angle;
        private static int reloadDelay = 0;
        private int maxExpireDelay = 15;
        private int expireDelay;
        private static int level;
        private int dmgMultiplier;
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

        public static int ReloadDelay
        {
            get
            {
                return reloadDelay;
            }

            set
            {
                reloadDelay = value;
            }
        }

        public static int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        public Bullet(int screenHeight, int screenWidth, Game1 game, double angle, int dmgMultiplier)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.game = game;
            size = screenWidth / 60;
            this.angle = angle;
            this.dmgMultiplier = dmgMultiplier;
            expireDelay = maxExpireDelay;
            r = (screenWidth / 2) - screenHeight / 20 * 1.5;
            x = (int)(Math.Cos(angle - Utils.ConvertToRadians(-90)) * r + screenWidth / 2) - size / 2;
            y = (int)(Math.Sin(angle - Utils.ConvertToRadians(-90)) * r + screenHeight / 2) - size / 2;


            //x = screenWidth / 2 - width / 2;
            //y = screenHeight / 2 + (int)r - height/2;
            shape = new RotatingRectangle(new Rectangle((int)x, (int)y, size, size), (float)angle);
        }

        public static void SetTexture(Game1 game)
        {
            texture = game.Content.Load<Texture2D>("bullet");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle aPositionAdjusted = new Rectangle(shape.X + (shape.Width / 2), shape.Y + (shape.Height / 2), shape.Width, shape.Height);
            //spriteBatch.Draw(texture, aPositionAdjusted, new Rectangle(0, 0, width, height), Color.White, shape.Rotation, new Vector2(width / 2, height / 2), SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, aPositionAdjusted, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, shape.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);

            //spriteBatch.Draw(texture, shape.CollisionRectangle, Microsoft.Xna.Framework.Color.White);
        }

        public static void Reload()
        {
            if(reloadDelay > 0)
            {
                reloadDelay--;
            }
        }

        public void Expire()
        {
            if(expireDelay <= 0)
            {
                alive = false;
            }
            else if (expireDelay <= 3)
            {
                size = screenWidth / 175;
            }
            else if (expireDelay <= 6)
            {
                size = screenWidth / 135;
            }
            else if (expireDelay <= 9)
            {
                size = screenWidth / 105;
            }
            else if (expireDelay <= 12)
            {
                size = screenWidth / 80;
            }
            shape.CollisionRectangle.Width = size;
            shape.CollisionRectangle.Height = size;
            shape.Origin = new Vector2(size / 2, size / 2);
            r -= ((screenWidth / 2) - screenHeight / 20 * 1.5)/maxExpireDelay;
            shape.CollisionRectangle.X = (int)(Math.Cos(angle - Utils.ConvertToRadians(-90)) * r + screenWidth / 2) - size / 2;
            shape.CollisionRectangle.Y = (int)(Math.Sin(angle - Utils.ConvertToRadians(-90)) * r + screenHeight / 2) - size / 2;
            expireDelay--;
        }

        public static int setReloadTimeAfterShoot()
        {
            switch (level)
            {
                case 1:
                    return 20;
                case 2:
                    return 12;
                case 3:
                    return 25;
                case 4:
                    return 17;
                case 5:
                    return 17;
                default:
                    return 20;
            }
        }

        public static void Improve()
        {
            level++;
            if (level > 5)
            {
                level = 5;
            }
        }
    }
}