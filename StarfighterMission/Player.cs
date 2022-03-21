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
    class Player
    {
        private Texture2D /*texture,*/ shieldtxt1, shieldtxt2, shieldtxt3;
        private Texture2D texture1, texture2, texture3, texture4, texture5;
        private RotatingRectangle shape;
        private int screenHeight, screenWidth;
        private int width, height;
        private static int hp;
        private double r, x, y, angle;
        private double frame1X, frame1Y, frame2X, frame2Y, frame3X, frame3Y;
        private float previousRotation;

        private float speed = 2;
        private static int shields;
        private int counter;
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

        public static int HP
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

        public static int Shields
        {
            get
            {
                return shields;
            }

            set
            {
                shields = value;
            }
        }

        //public double Angle
        //{
        //get
        //{
        //return shape.R;
        //}

        //set
        //{
        //angle = value;
        //}
        //}

        public Player(int screenHeight, int screenWidth, Game1 game)
        {
            this.screenHeight = screenHeight;//1776 on phone
            this.screenWidth = screenWidth;//1080 on phone
            this.game = game;
            width = screenWidth/8;
            height = screenHeight/20;
            //angle = 90;
            //angle2 = 0;
            r = (screenWidth / 2) - height * 0.7;
            x = (int)(Math.Cos(Utils.ConvertToRadians(90)) * r + screenWidth/2) - width / 2;
            y = (int)(Math.Sin(Utils.ConvertToRadians(90)) * r + screenHeight/2) - height / 2;
            previousRotation = (float) Utils.ConvertToRadians(0);
            hp = 10;
            shields = 300;
            counter = 20;

            //x = screenWidth / 2 - width / 2;
            //y = screenHeight / 2 + (int)r - height/2;
            shape = new RotatingRectangle(new Rectangle((int) x, (int) y, width, height), (float) Utils.ConvertToRadians(0));
        }

        public void SetTexture()
        {
            texture1 = game.Content.Load<Texture2D>("delta7a");
            texture2 = game.Content.Load<Texture2D>("delta7b");
            texture3 = game.Content.Load<Texture2D>("delta7c");
            texture4 = game.Content.Load<Texture2D>("delta7d");
            texture5 = game.Content.Load<Texture2D>("delta7e");
            //texture = game.Content.Load<Texture2D>("delta7");
            //texture = game.Content.Load<Texture2D>("white");
            shieldtxt1 = game.Content.Load<Texture2D>("shield1");
            shieldtxt2 = game.Content.Load<Texture2D>("shield2");
            shieldtxt3 = game.Content.Load<Texture2D>("shield3");
        }

        //public void MoveLeft()
        //{
        //    angle += speed;
        //    if (angle > 359)
        //    {
        //        angle = 0;
        //    }
        //    angle2 = angle - 90;
        //    if (angle < 90)
        //    {
        //        angle2 = 359 - (90 - angle);
        //    }
        //    UpdatePosition();
        //    /*shape.X += speed;
        //    if (shape.X > 720)
        //    {
        //        shape.X = 720;
        //    }*/
        //}

        //public void MoveRight()
        //{
        //    angle -= speed;
        //    if (angle < 0)
        //    {
        //        angle = 359;
        //    }
        //    angle2 = angle - 90;
        //    if (angle < 90)
        //    {
        //        angle2 = 359 - (90 - angle);
        //    }
        //    UpdatePosition();
        //    /*shape.X -= speed;
        //    if (shape.X < 0)
        //    {
        //        shape.X = 0;
        //    }*/
        //}

        public void Move(float x, float width)
        {
            if(x < 0)
            {
                x = 0;
            }
            else if(x > width)
            {
                x = width;
            }
            if (x < width / 2)
            {
                speed = (float) Utils.ConvertToRadians(4) * ((width / 2) - x) / (width / 2);
            } else if (x > width / 2.0)
            {
                speed = (float) Utils.ConvertToRadians(-4) * (x - (width / 2)) / (width / 2);
            }
            shape.Rotation += (float) speed;
            if (shape.Rotation >= (float) Utils.ConvertToRadians(360))
            {
                shape.Rotation -= (float) Utils.ConvertToRadians(360);
            }
            if (shape.Rotation < (float) Utils.ConvertToRadians(0))
            {
                shape.Rotation += (float) Utils.ConvertToRadians(360);
            }
            //System.Console.WriteLine(shape.Rotation);
            //System.Console.WriteLine(Utils.ConvertToDegrees(speed));
            UpdatePosition();
        }

        public void Fire()
        {
        }

        public void Update()
        {
            if (shields > 0)
            {
                shields--;
            }
            counter--;
            if (counter <= 0)
            {
                counter = 20;
            }
        }

        public void UpdatePosition()
        {
            //shape.Rotation = 0.0f;
            shape.CollisionRectangle.X = (int)(Math.Cos(shape.Rotation - Utils.ConvertToRadians(-90)) * r + screenWidth / 2) - width / 2;
            shape.CollisionRectangle.Y = (int)(Math.Sin(shape.Rotation - Utils.ConvertToRadians(-90)) * r + screenHeight / 2) - height / 2;
        }

        /// <summary>
        /// Rotate a point from a given location and adjust using the Origin we
        /// are rotating around
        /// </summary>
        /// <param name="thePoint"></param>
        /// <param name="theOrigin"></param>
        /// <param name="theRotation"></param>
        /// <returns></returns>
        //private Vector2 RotatePoint(Vector2 thePoint, Vector2 theOrigin, float theRotation)
        //{
        //    Vector2 aTranslatedPoint = new Vector2();
        //    aTranslatedPoint.X = (float)(theOrigin.X + (thePoint.X - theOrigin.X) * Math.Cos(theRotation)
        //        - (thePoint.Y - theOrigin.Y) * Math.Sin(theRotation));
        //    aTranslatedPoint.Y = (float)(theOrigin.Y + (thePoint.Y - theOrigin.Y) * Math.Cos(theRotation)
        //        + (thePoint.X - theOrigin.X) * Math.Sin(theRotation));
        //    return aTranslatedPoint;
        //}

        //old version, which not contains max speed
        /*public void UpdatePosition(Vector2 GravityDirection)
        {
            if (frame3X.Equals(null))
            {
                frame3X = GravityDirection.X * 90;
                frame2X = GravityDirection.X * 90;
                frame3Y = GravityDirection.Y * 90;
                frame2Y = GravityDirection.Y * 90;
            }
            float x, y, tg;
            x = GravityDirection.X * 90;
            y = GravityDirection.Y * 90;
            //Math.Atan2() wyznacza k¹t na podstawie boków (tangens)
            tg = (float) (Math.Atan2((frame3Y + frame2Y + y)/3.0, (frame3X + frame2X + x)/3.0));
            shape.Rotation = tg;
            if (shape.Rotation >= (float)Utils.ConvertToRadians(360))
            {
                shape.Rotation -= (float)Utils.ConvertToRadians(360);
            }
            if (shape.Rotation < (float)Utils.ConvertToRadians(0))
            {
                shape.Rotation += (float)Utils.ConvertToRadians(360);
            }
            //System.Console.WriteLine(shape.Rotation);
            shape.CollisionRectangle.X = (int)(-Math.Cos(shape.Rotation - Utils.ConvertToRadians(-90)) * r + screenWidth / 2) - width / 2;
            shape.CollisionRectangle.Y = (int)(Math.Sin(shape.Rotation - Utils.ConvertToRadians(-90)) * r + screenHeight / 2) - height / 2;
            //System.Console.WriteLine(Utils.ConvertToDegrees(shape.Rotation));
            shape.Rotation = -1.0f * shape.Rotation;
            //angle2 = (-1)*(Utils.ConvertToDegrees(tg)) + 90;
            //if (Utils.ConvertToDegrees(tg) < -90)
            //{
            //    angle2 = 360 - (Utils.ConvertToDegrees(tg)) + 90;
            //}
            //System.Console.WriteLine((int)ConvertToDegrees(tg));
            //System.Console.WriteLine(angle2);
            frame3X = frame2X;
            frame3Y = frame2Y;
            frame2X = frame1X;
            frame2Y = frame1Y;
            frame1X = x;
            frame1Y = y;
        }*/

        public void UpdatePosition(Vector2 GravityDirection)
        {
            previousRotation = -1.0f * shape.Rotation;
            calculate_shapeRotation(GravityDirection);
            changePreviousRotationIfAngles0And360();
            speed = shape.Rotation - previousRotation;
            //Console.WriteLine("speed = " + Utils.ConvertToDegrees(speed));
            //Console.WriteLine("Utils.ConvertToDegrees(speed) > 4 : " + (Utils.ConvertToDegrees(speed) > 4));
            if (Utils.ConvertToDegrees(speed) > 4)
            {
                if (!checkIfTwoRotationsDiffersMoreThan180()) {
                    shape.Rotation = previousRotation + (float)Utils.ConvertToRadians(4);
                }
                else
                {
                    shape.Rotation = previousRotation + (float)Utils.ConvertToRadians(-4);
                }
            }
            else if (Utils.ConvertToDegrees(speed) < (-4))
            {
                if (!checkIfTwoRotationsDiffersMoreThan180())
                {
                    shape.Rotation = previousRotation + (float)Utils.ConvertToRadians(-4);
                }
                else
                {
                    shape.Rotation = previousRotation + (float)Utils.ConvertToRadians(4);
                }
            }
            if (shape.Rotation >= (float)Utils.ConvertToRadians(360))
            {
                shape.Rotation -= (float)Utils.ConvertToRadians(360);
            }
            if (shape.Rotation < (float)Utils.ConvertToRadians(0))
            {
                shape.Rotation += (float)Utils.ConvertToRadians(360);
            }
            shape.CollisionRectangle.X = (int)(-Math.Cos(shape.Rotation - Utils.ConvertToRadians(-90)) * r + screenWidth / 2) - width / 2;
            shape.CollisionRectangle.Y = (int)(Math.Sin(shape.Rotation - Utils.ConvertToRadians(-90)) * r + screenHeight / 2) - height / 2;
            shape.Rotation = -1.0f * shape.Rotation;
        }

        private void changePreviousRotationIfAngles0And360()
        {
            if (previousRotation > (float)Utils.ConvertToRadians(356) && shape.Rotation < (float)Utils.ConvertToRadians(4))
            {
                previousRotation -= (float)Utils.ConvertToRadians(360);
            }
            else if (previousRotation < (float)Utils.ConvertToRadians(4) && shape.Rotation > (float)Utils.ConvertToRadians(356))
            {
                previousRotation += (float)Utils.ConvertToRadians(360);
            }
        }

        public void calculate_shapeRotation(Vector2 GravityDirection)
        {
            if (frame3X.Equals(null))
            {
                frame3X = GravityDirection.X * 90;
                frame2X = GravityDirection.X * 90;
                frame3Y = GravityDirection.Y * 90;
                frame2Y = GravityDirection.Y * 90;
            }
            float x, y, tg;
            x = GravityDirection.X * 90;
            y = GravityDirection.Y * 90;
            //Math.Atan2() wyznacza k¹t na podstawie boków (tangens)
            tg = (float)(Math.Atan2((frame3Y + frame2Y + y) / 3.0, (frame3X + frame2X + x) / 3.0));
            shape.Rotation = tg;
            if (shape.Rotation >= (float)Utils.ConvertToRadians(360))
            {
                shape.Rotation -= (float)Utils.ConvertToRadians(360);
            }
            if (shape.Rotation < (float)Utils.ConvertToRadians(0))
            {
                shape.Rotation += (float)Utils.ConvertToRadians(360);
            }
            frame3X = frame2X;
            frame3Y = frame2Y;
            frame2X = frame1X;
            frame2Y = frame1Y;
            frame1X = x;
            frame1Y = y;
        }

        public bool checkIfTwoRotationsDiffersMoreThan180()
        {
            //Because speed is (shape.Rotation - previousRotation):
            return (Utils.ConvertToDegrees(speed) > 180 || Utils.ConvertToDegrees(speed) < (-180));
        }

        //public void Update()
        //{
        //    MoveRight();
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle aPositionAdjusted = new Rectangle(shape.X + (shape.Width / 2), shape.Y + (shape.Height / 2), shape.Width, shape.Height);
            Rectangle bPositionAdjusted = new Rectangle(shape.X + (shape.Width / 2), shape.Y + (shape.Height / 2), (int)(1.2 * shape.Width), (int)(1.2 * shape.Width));
            //spriteBatch.Draw(texture, aPositionAdjusted, new Rectangle(0, 0, width, height), Color.White, shape.Rotation, new Vector2(width / 2, height / 2), SpriteEffects.None, 0f);

            //spriteBatch.Draw(texture, aPositionAdjusted, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, shape.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);
            if (counter > 16)
            {
                spriteBatch.Draw(texture1, aPositionAdjusted, new Rectangle(0, 0, texture1.Width, texture1.Height), Color.White, shape.Rotation, new Vector2(texture1.Width / 2, texture1.Height / 2), SpriteEffects.None, 0f);
            }
            else if (counter > 12)
            {
                spriteBatch.Draw(texture2, aPositionAdjusted, new Rectangle(0, 0, texture2.Width, texture2.Height), Color.White, shape.Rotation, new Vector2(texture2.Width / 2, texture2.Height / 2), SpriteEffects.None, 0f);
            }
            else if (counter > 8)
            {
                spriteBatch.Draw(texture3, aPositionAdjusted, new Rectangle(0, 0, texture3.Width, texture3.Height), Color.White, shape.Rotation, new Vector2(texture3.Width / 2, texture3.Height / 2), SpriteEffects.None, 0f);
            }
            else if (counter > 4)
            {
                spriteBatch.Draw(texture4, aPositionAdjusted, new Rectangle(0, 0, texture4.Width, texture4.Height), Color.White, shape.Rotation, new Vector2(texture4.Width / 2, texture4.Height / 2), SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture5, aPositionAdjusted, new Rectangle(0, 0, texture5.Width, texture5.Height), Color.White, shape.Rotation, new Vector2(texture5.Width / 2, texture5.Height / 2), SpriteEffects.None, 0f);
            }

            if (shields>0 && shields % 18 < 6)
            {
                spriteBatch.Draw(shieldtxt1, bPositionAdjusted, new Rectangle(0, 0, shieldtxt1.Width, shieldtxt1.Height), Color.White, shape.Rotation, new Vector2(shieldtxt1.Width / 2, shieldtxt1.Height / 2), SpriteEffects.None, 0f);
            }
            else if (shields > 0 && shields % 18 >= 6 && shields % 18 < 12)
            {
                spriteBatch.Draw(shieldtxt2, bPositionAdjusted, new Rectangle(0, 0, shieldtxt2.Width, shieldtxt2.Height), Color.White, shape.Rotation, new Vector2(shieldtxt2.Width / 2, shieldtxt2.Height / 2), SpriteEffects.None, 0f);
            }
            else if (shields > 0 && shields % 18 >= 12)
            {
                spriteBatch.Draw(shieldtxt3, bPositionAdjusted, new Rectangle(0, 0, shieldtxt3.Width, shieldtxt3.Height), Color.White, shape.Rotation, new Vector2(shieldtxt3.Width / 2, shieldtxt3.Height / 2), SpriteEffects.None, 0f);
            }
        }
    }
}