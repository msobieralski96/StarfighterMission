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
    class Enemy
    {
        private static Texture2D texture;
        private RotatingRectangle shape;
        private int screenHeight, screenWidth;
        private int width, height;
        private double r, x, y;
        private int counter;
        private int size;
        private int enemyTrack;
        private int reloadDelay;
        private int reloadDelayTime;
        private int enemyPoints;
        private bool grantPowerUp;
        private int hp;

        private float speed = 2;
        bool visible;
        bool alive = true;
        bool canShoot;
        bool canCollide;

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

        public bool Visible
        {
            get
            {
                return visible;
            }

            set
            {
                visible = value;
            }
        }

        public bool CanShoot
        {
            get
            {
                return canShoot;
            }

            set
            {
                canShoot = value;
            }
        }

        public bool CanCollide
        {
            get
            {
                return canCollide;
            }

            set
            {
                canCollide = value;
            }
        }

        public int ReloadDelay
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

        public int ReloadDelayTime
        {
            get
            {
                return reloadDelayTime;
            }

            set
            {
                reloadDelayTime = value;
            }
        }

        public int EnemyPoints
        {
            get
            {
                return enemyPoints;
            }

            set
            {
                enemyPoints = value;
            }
        }

        public int Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
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

        public bool GrantPowerUp
        {
            get
            {
                return grantPowerUp;
            }

            set
            {
                grantPowerUp = value;
            }
        }

        public Enemy(int screenHeight, int screenWidth, Game1 game, double angle, int enemyTrack, int startDirection,
            int enemyPoints, int reloadDelay, int hp, bool grantPowerUp)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.game = game;
            this.enemyPoints = enemyPoints;
            this.reloadDelay = 0;
            this.grantPowerUp = grantPowerUp;
            this.hp = hp;
            reloadDelayTime = reloadDelay;
            width = screenWidth/8;
            height = screenHeight/12;
            setEnemyTrack(enemyTrack);
            setTrackDirection(startDirection);
            visible = true;
            canShoot = false;
            canCollide = false;
            r = (screenWidth / 2) * 1.6;
            x = (int)(Math.Cos(Utils.ConvertToRadians(angle+90)) * r + screenWidth/2) - width / 2;
            y = (int)(Math.Sin(Utils.ConvertToRadians(angle+90)) * r + screenHeight/2) - height / 2;

            shape = new RotatingRectangle(new Rectangle((int) x, (int) y, width, height), (float) Utils.ConvertToRadians(angle));
        }

        public static void SetTexture(Game1 game)
        {
            texture = game.Content.Load<Texture2D>("alienship1");
            //texture = game.Content.Load<Texture2D>("white");
        }

        private void Move()
        {
            shape.Rotation += (float) Utils.ConvertToRadians(speed);
            if (shape.Rotation >= (float) Utils.ConvertToRadians(360))
            {
                shape.Rotation -= (float) Utils.ConvertToRadians(360);
            }
            if (shape.Rotation < (float) Utils.ConvertToRadians(0))
            {
                shape.Rotation += (float) Utils.ConvertToRadians(360);
            }
            UpdatePosition();
        }

        public void Reload()
        {
            if (reloadDelay > 0)
            {
                reloadDelay--;
            }
        }

        public void Fire()
        {
        }

        public void UpdatePosition()
        {
            shape.CollisionRectangle.X = (int)(Math.Cos(shape.Rotation - Utils.ConvertToRadians(-90)) * r + screenWidth / 2) - width / 2;
            shape.CollisionRectangle.Y = (int)(Math.Sin(shape.Rotation - Utils.ConvertToRadians(-90)) * r + screenHeight / 2) - height / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle aPositionAdjusted = new Rectangle(shape.X + (shape.Width / 2), shape.Y + (shape.Height / 2), shape.Width, shape.Height);
            spriteBatch.Draw(texture, aPositionAdjusted, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, shape.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);
        }

        private void setEnemyTrack(int enemyTrack)
        {
            this.enemyTrack = enemyTrack;
            switch (enemyTrack) {
                case 1:
                    counter = 1080;
                    break;
                case 2:
                    counter = 1440;
                    break;
                case 3:
                    counter = 1440;
                    break;
                default:
                    counter = 1080;
                    break;
            }
        }

        public void MoveTrack()
        {
            switch (enemyTrack)
            {
                case 1:
                    EnemyTrack1();
                    break;
                case 2:
                    EnemyTrack2();
                    break;
                case 3:
                    EnemyTrack3();
                    break;
                case 4:
                    EnemyTrack4();
                    break;
                default:
                    EnemyTrack1();
                    break;
            }
        }

        private void setTrackDirection(int startDirection)
        {
            if (startDirection == 1)
            {
                speed *= (-1);
            }
        }

        private void EnemyTrack1()
        {
            //MAX COUNTER = 1080*2;
            //START COUNTER = 1080;
            if (counter > 1044 && counter <= 1080)
            {
                visible = true;
                canShoot = false;
                canCollide = false;
                r -= ((screenWidth / 2) * 1.0) / ((1080 - 900) / (int)Math.Abs(speed)) / 3 * 5;
                size = 5;
                changeSize();
            }
            else if (counter > 1008 && counter <= 1044)
            {
                r -= ((screenWidth / 2) * 1.0) / ((1080 - 900) / (int)Math.Abs(speed)) / 3 * 4;
                size = 5;
                changeSize();
            }
            else if (counter > 972 && counter <= 1008)
            {
                r -= ((screenWidth / 2) * 1.0) / ((1080 - 900) / (int)Math.Abs(speed));
                size = 4;
                changeSize();
            }
            else if (counter > 936 && counter <= 972)
            {
                r -= ((screenWidth / 2) * 1.0) / ((1080 - 900) / (int)Math.Abs(speed)) / 3 * 2;
                size = 4;
                changeSize();
            }
            else if (counter > 900 && counter <= 936)
            {
                r -= ((screenWidth / 2) * 1.0) / ((1080 - 900) / (int)Math.Abs(speed)) / 3;
                size = 3;
                changeSize();
            }
            else if (counter > 180 && counter <= 900)
            {
                canShoot = true;
                canCollide = true;
            }
            else if (counter > 144 && counter <= 180)
            {
                canShoot = false;
                r += ((screenWidth / 2) * 1.0) / ((180) / (int)Math.Abs(speed)) / 3;
                size = 3;
                changeSize();
            }
            else if (counter > 108 && counter <= 144)
            {
                r += ((screenWidth / 2) * 1.0) / ((180) / (int)Math.Abs(speed)) / 3 * 2;
                size = 4;
                changeSize();
            }
            else if (counter > 72 && counter <= 108)
            {
                r += ((screenWidth / 2) * 1.0) / ((180) / (int)Math.Abs(speed));
                size = 4;
                changeSize();
            }
            else if (counter > 36 && counter <= 72)
            {
                r += ((screenWidth / 2) * 1.0) / ((180) / (int)Math.Abs(speed)) / 3 * 4;
                size = 5;
                changeSize();
            }
            else if (counter > 0 && counter <= 36)
            {
                r += ((screenWidth / 2) * 1.0) / ((180) / (int)Math.Abs(speed)) / 3 * 5;
                size = 5;
                changeSize();
            }
            else if (counter == 0)
            {
                visible = false;
                canCollide = false;
                r = (screenWidth / 2) * 1.6;
                //counter = 1080 * 2;
                counter = 1080 + 360;
            }
            //Console.WriteLine(r);
            Move();
            counter -= (int)Math.Abs(speed);
        }

        private void EnemyTrack2()
        {
            //MAX COUNTER = 1440*2;
            //START COUNTER = 1440;
            if (counter > 1368 && counter <= 1440)
            {
                visible = true;
                canShoot = false;
                canCollide = false;
                r -= ((screenWidth / 2) * 1.5) / ((1440 - 1080) / (int)Math.Abs(speed)) / 3 * 5;
                size = 5;
                changeSize();
            }
            else if (counter > 1296 && counter <= 1368)
            {
                r -= ((screenWidth / 2) * 1.5) / ((1440 - 1080) / (int)Math.Abs(speed)) / 3 * 4;
                size = 4;
                changeSize();
            }
            else if (counter > 1224 && counter <= 1296)
            {
                canShoot = true;
                r -= ((screenWidth / 2) * 1.5) / ((1440 - 1080) / (int)Math.Abs(speed));
                size = 3;
                changeSize();
            }
            else if (counter > 1152 && counter <= 1224)
            {
                r -= ((screenWidth / 2) * 1.5) / ((1440 - 1080) / (int)Math.Abs(speed)) / 3 * 2;
                size = 2;
                changeSize();
            }
            else if (counter > 1080 && counter <= 1152)
            {
                r -= ((screenWidth / 2) * 1.5) / ((1440 - 1080) / (int)Math.Abs(speed)) / 3;
                size = 1;
                changeSize();
            }
            else if (counter > 288 && counter <= 360)
            {
                canCollide = true;
                r += ((screenWidth / 2) * 1.5) / ((360) / (int)Math.Abs(speed)) / 3;
                size = 1;
                changeSize();
            }
            else if (counter > 216 && counter <= 288)
            {
                r += ((screenWidth / 2) * 1.5) / ((360) / (int)Math.Abs(speed)) / 3 * 2;
                size = 2;
                changeSize();
            }
            else if (counter > 144 && counter <= 216)
            {
                r += ((screenWidth / 2) * 1.5) / ((360) / (int)Math.Abs(speed));
                size = 3;
                changeSize();
            }
            else if (counter > 72 && counter <= 144)
            {
                canShoot = false;
                r += ((screenWidth / 2) * 1.5) / ((360) / (int)Math.Abs(speed)) / 3 * 4;
                size = 4;
                changeSize();
            }
            else if (counter > 0 && counter <= 72)
            {
                r += ((screenWidth / 2) * 1.5) / ((360) / (int)Math.Abs(speed)) / 3 * 5;
                size = 5;
                changeSize();
            }
            else if (counter == 0)
            {
                visible = false;
                canCollide = false;
                r = (screenWidth / 2) * 1.6;
                //counter = 1440 * 2;
                counter = 1440 + 360;
            }
            //Console.WriteLine(r);
            Move();
            counter -= (int)Math.Abs(speed);
        }

        private void EnemyTrack3()
        {
            //MAX COUNTER = 1440*2;
            //START COUNTER = 1440;
            if (counter > 1422 && counter <= 1440)
            {
                visible = true;
                canCollide = false;
                canShoot = false;
                r -= ((screenWidth / 2) * 1.0) / ((1440 - 1350) / (int)Math.Abs(speed)) / 3 * 5;
                size = 5;
                changeSize();
            }
            else if (counter > 1404 && counter <= 1422)
            {
                r -= ((screenWidth / 2) * 1.0) / ((1440 - 1350) / (int)Math.Abs(speed)) / 3 * 4;
                size = 5;
                changeSize();
            }
            else if (counter > 1386 && counter <= 1404)
            {
                r -= ((screenWidth / 2) * 1.0) / ((1440 - 1350) / (int)Math.Abs(speed));
                size = 4;
                changeSize();
            }
            else if (counter > 1368 && counter <= 1386)
            {
                r -= ((screenWidth / 2) * 1.0) / ((1440 - 1350) / (int)Math.Abs(speed)) / 3 * 2;
                size = 4;
                changeSize();
            }
            else if (counter > 1350 && counter <= 1368)
            {
                canShoot = true;
                r -= ((screenWidth / 2) * 1.0) / ((1440 - 1350) / (int)Math.Abs(speed)) / 3;
                size = 3;
                changeSize();
            }
            else if (counter > 990 && counter <= 1350)
            {
                canCollide = true;
            }
            else if (counter > 936 && counter <= 990)
            {
                r -= ((screenWidth / 2) * 0.6) / ((990 - 720) / (int)Math.Abs(speed)) / 3 * 5;
                size = 3;
                changeSize();
            }
            else if (counter > 882 && counter <= 936)
            {
                r -= ((screenWidth / 2) * 0.6) / ((990 - 720) / (int)Math.Abs(speed)) / 3 * 4;
                size = 2;
                changeSize();
            }
            else if (counter > 828 && counter <= 882)
            {
                r -= ((screenWidth / 2) * 0.6) / ((990 - 720) / (int)Math.Abs(speed));
                size = 1;
                changeSize();
            }
            else if (counter > 774 && counter <= 828)
            {
                visible = false;
                canShoot = false;
                canCollide = false;
                r -= ((screenWidth / 2) * 0.6) / ((990 - 720) / (int)Math.Abs(speed)) / 3 * 2;
                size = 1;
                changeSize();
            }
            else if (counter > 720 && counter <= 774)
            {
                r -= ((screenWidth / 2) * 0.6) / ((990 - 720) / (int)Math.Abs(speed)) / 3;
                size = 1;
                changeSize();
            }
            else if (counter > 666 && counter <= 720)
            {
                if(counter == 720)
                {
                    speed *= -1;
                    shape.Rotation += (float) Utils.ConvertToRadians(180);
                    if(shape.Rotation >= 360)
                    {
                        shape.Rotation -= 360;
                    }
                }
                r += ((screenWidth / 2) * 0.6) / ((720 - 450) / (int)Math.Abs(speed)) / 3;
                size = 1;
                changeSize();
            }
            else if (counter > 612 && counter <= 666)
            {
                r += ((screenWidth / 2) * 0.6) / ((720 - 450) / (int)Math.Abs(speed)) / 3 * 2;
                size = 1;
                changeSize();
            }
            else if (counter > 558 && counter <= 612)
            {
                visible = true;
                canShoot = true;
                canCollide = true;
                r += ((screenWidth / 2) * 0.6) / ((720 - 450) / (int)Math.Abs(speed));
                size = 1;
                changeSize();
            }
            else if (counter > 504 && counter <= 558)
            {
                r += ((screenWidth / 2) * 0.6) / ((720 - 450) / (int)Math.Abs(speed)) / 3 * 4;
                size = 2;
                changeSize();
            }
            else if (counter > 450 && counter <= 504)
            {
                r += ((screenWidth / 2) * 0.6) / ((720 - 450) / (int)Math.Abs(speed)) / 3 * 5;
                size = 3;
                changeSize();
            }
            else if (counter > 72 && counter <= 90)
            {
                r += ((screenWidth / 2) * 1.0) / ((90) / (int)Math.Abs(speed)) / 3;
                size = 3;
                changeSize();
            }
            else if (counter > 54 && counter <= 72)
            {
                canShoot = false;
                r += ((screenWidth / 2) * 1.0) / ((90) / (int)Math.Abs(speed)) / 3 * 2;
                size = 4;
                changeSize();
            }
            else if (counter > 36 && counter <= 54)
            {
                r += ((screenWidth / 2) * 1.0) / ((90) / (int)Math.Abs(speed));
                size = 4;
                changeSize();
            }
            else if (counter > 18 && counter <= 36)
            {
                r += ((screenWidth / 2) * 1.0) / ((90) / (int)Math.Abs(speed)) / 3 * 4;
                size = 5;
                changeSize();
            }
            else if (counter > 0 && counter <= 18)
            {
                r += ((screenWidth / 2) * 1.0) / ((90) / (int)Math.Abs(speed)) / 3 * 5;
                size = 5;
                changeSize();
            }
            else if (counter == 0)
            {
                canCollide = false;
                visible = false;
                r = (screenWidth / 2) * 1.6;
                //counter = 1440 * 2;
                counter = 1440 + 360;
            }
            //Console.WriteLine(r);
            Move();
            counter -= (int)Math.Abs(speed);
        }

        private void EnemyTrack4()
        {
        }

        private void changeSize()
        {
            switch (size)
            {
                case 5:
                    height = (int)(screenWidth / 6.2);
                    width = (int)(screenHeight / 9.25);
                    break;
                case 4:
                    height = screenWidth / 8;
                    width = screenHeight / 12;
                    break;
                case 3:
                    height = (int)(screenWidth / 13.58);
                    width = (int)(screenHeight / 20.325);
                    break;
                case 2:
                    height = (int)(screenWidth / 30.185);
                    width = (int)(screenHeight / 45.24);
                    break;
                case 1:
                    height = screenWidth / 80;
                    width = screenHeight / 120;
                    break;
            }
            shape.CollisionRectangle.Height = height;
            shape.CollisionRectangle.Width = width;
            shape.Origin = new Vector2(width / 2, height / 2);
        }
    }
}