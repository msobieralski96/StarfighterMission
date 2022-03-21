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
    class TouchingText
    {
        private Rectangle shape;
        private int screenHeight, screenWidth;
        private float width, height;
        private SpriteFont txt;
        private int x, y;
        private string text;

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

        public float Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public float Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public TouchingText(int screenHeight, int screenWidth, Game1 game, String text)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.game = game;
            this.text = text;
        }

        public void SetXANDY(SpriteFont txt, int centerX, int centerY)
        {
            this.txt = txt;
            width = txt.MeasureString(text).X;
            height = txt.MeasureString(text).Y;
            x = centerX - (int) (width/2);
            y = centerY - (int) (height/2);
            //Console.WriteLine(this.GetType().Name);
            //Console.WriteLine("x = " + x);
            //Console.WriteLine("y = " + y);
            //Console.WriteLine("width = " + width);
            //Console.WriteLine("height = " + height);
            shape = new Rectangle(x, y, (int)width, (int)height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(txt, text, new Vector2(x, y), Color.White);
        }

        public bool CheckIfPointIsInRect(int xTouch, int yTouch)
        {
            //Console.WriteLine(x + "<" + xTouch + "<" + (x + width));
            //Console.WriteLine(y + "<" + yTouch + "<" + (y + height));
            //Console.WriteLine(shape.Contains(xTouch, yTouch));
            return shape.Contains(xTouch, yTouch);
        }
    }
}