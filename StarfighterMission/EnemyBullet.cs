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
    class EnemyBullet
    {
        private static Texture2D texture, texture2;
        private RotatingRectangle shape;
        private int screenHeight, screenWidth;
        private int size, speed;
        private double r, x, y, angle, R, rDiffR;
        private int speedTimeFullR;
        private int maxExpireDelay = 10;
        private int expireDelay;
        private int speedScale;
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



        public EnemyBullet(int screenHeight, int screenWidth, Game1 game, double angle, double X, double Y)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.game = game;
            size = screenWidth / 175;
            speed = 1;
            this.angle = angle;
            r = Math.Sqrt(Math.Pow(X - screenWidth/2 , 2) + Math.Pow(Y - screenHeight / 2, 2));
            R = (screenWidth / 2) * 1.6;
            rDiffR = R - r;
            speedTimeFullR = 120;
            maxExpireDelay = (int)(rDiffR / R * speedTimeFullR);
            expireDelay = maxExpireDelay;
            speedScale = getSpeedScale();
            x = (int)(Math.Cos(angle - Utils.ConvertToRadians(-90)) * r + screenWidth / 2) - size / 2;
            y = (int)(Math.Sin(angle - Utils.ConvertToRadians(-90)) * r + screenHeight / 2) - size / 2;

            shape = new RotatingRectangle(new Rectangle((int)x, (int)y, size, size), (float)angle);
        }

        public static void SetTexture(Game1 game)
        {
            texture = game.Content.Load<Texture2D>("enemybullet");
            texture2 = game.Content.Load<Texture2D>("enemybullet2");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle aPositionAdjusted = new Rectangle(shape.X + (shape.Width / 2), shape.Y + (shape.Height / 2), shape.Width, shape.Height);
            if ((expireDelay % 10) < 5)
            {
                spriteBatch.Draw(texture, aPositionAdjusted, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, shape.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture2, aPositionAdjusted, new Rectangle(0, 0, texture2.Width, texture2.Height), Color.White, shape.Rotation, new Vector2(texture2.Width / 2, texture2.Height / 2), SpriteEffects.None, 0f);
            }
        }

        public void Expire()
        {
            //speedTimeFullR = 120
            //speedTimeFullR/5 = 20
            if(expireDelay <= 0)
            {
                alive = false;
            }
            else if (expireDelay <= speedTimeFullR/5)
            {
                speed = 16;
                size = screenWidth / 30;
            }
            else if (expireDelay <= speedTimeFullR/5*2)
            {
                speed = 8;
                size = screenWidth / 40;
            }
            else if (expireDelay <= speedTimeFullR/5*3)
            {
                speed = 4;
                size = screenWidth / 55;
            }
            else if (expireDelay <= speedTimeFullR/5*4)
            {
                speed = 2;
                size = screenWidth / 75;
            }
            else
            {
                speed = 1;
                size = screenWidth / 105;
            }
            shape.CollisionRectangle.Width = size;
            shape.CollisionRectangle.Height = size;
            shape.Origin = new Vector2(size / 2, size / 2);
            r += rDiffR / speedScale * speed;
            shape.CollisionRectangle.X = (int)(Math.Cos(angle - Utils.ConvertToRadians(-90)) * r + screenWidth / 2) - size / 2;
            shape.CollisionRectangle.Y = (int)(Math.Sin(angle - Utils.ConvertToRadians(-90)) * r + screenHeight / 2) - size / 2;
            expireDelay--;
        }

        private int getSpeedScale() {
            int speedScale = 0;
            for(int i = 1; i <= maxExpireDelay; i++)
            {
                if (i <= speedTimeFullR / 5)
                {
                    speedScale += 16;
                } else if (i <= speedTimeFullR / 5 * 2)
                {
                    speedScale += 8;
                }
                else if (i <= speedTimeFullR / 5 * 3)
                {
                    speedScale += 4;
                }
                else if (i <= speedTimeFullR / 5 * 4)
                {
                    speedScale += 2;
                }
                else
                {
                    speedScale += 1;
                }
            }
            return speedScale;
        }
    }
}