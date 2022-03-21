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
    public class Explosion
    {
        public static Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int maxExplTime, explTime;
        private int totalFrames;
        private int screenHeight, screenWidth;
        private int x, y, size;
        private float angle;
        private int height, width;
        public bool Alive { get; set; } = true;
        public bool PlayerExplosion { get; set; }
        Game1 game;

        public Explosion(int screenHeight, int screenWidth, Game1 game, int x, int y, float angle, int size, int explTime, bool playerExplosion)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.game = game;
            this.x = x;
            this.y = y;
            this.angle = angle;
            setSize(size);
            Rows = 5;
            Columns = 5;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            maxExplTime = explTime;
            this.explTime = explTime;
            PlayerExplosion = playerExplosion;
        }

        public static void SetTexture(Game1 game)
        {
            Texture = game.Content.Load<Texture2D>("explosion");
        }

        public void Update()
        {
            explTime--;
            if (explTime <= 0)
            {
                currentFrame++;
                explTime = maxExplTime;
            }
            if (currentFrame == totalFrames)
                Alive = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int texWidth = Texture.Width / Columns;
            int texHeight = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(texWidth * column, texHeight * row, texWidth, texHeight);
            Rectangle destinationRectangle = new Rectangle(x , y , width, (int)(height * 1.25));
            
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, angle, new Vector2(texWidth / 2, texHeight / 2), SpriteEffects.None, 0f);
        }

        private void setSize(int size)
        {
            switch (size)
            {
                case 5:
                    height = (int)((screenWidth / 6.2) * 1.5);
                    width = (int)((screenHeight / 9.25) * 1.5);
                    break;
                case 4:
                    height = (int)(screenWidth / 8 * 1.5);
                    width = (int)(screenHeight / 12 * 1.5);
                    break;
                case 3:
                    height = (int)((screenWidth / 13.58) * 1.5);
                    width = (int)((screenHeight / 20.325) * 1.5);
                    break;
                case 2:
                    height = (int)((screenWidth / 30.185) * 1.5);
                    width = (int)((screenHeight / 45.24) * 1.5);
                    break;
                case 1:
                    height = (int)(screenWidth / 80 * 1.5);
                    width = (int)(screenHeight / 120 * 1.5);
                    break;
            }
            //shape.CollisionRectangle.Height = height;
            //shape.CollisionRectangle.Width = width;
            //shape.Origin = new Vector2(width / 2, height / 2);
        }

    }
}