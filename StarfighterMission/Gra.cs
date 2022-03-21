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
using Android.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input.Touch;
using StarfighterMission;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework.Input;

namespace StarfighterMission
{
    class Gra
    {
        static bool active;
        Player player;
        List<Bullet> bullets;
        List<Star> stars;
        List<Enemy> enemies;
        List<EnemyBullet> enemyBullets;
        private SpriteFont txt;
        TouchingText lost1, lost2, lost3, lost4, lost5, lost6;
        HUD hud;
        int screenHeight, screenWidth;
        Game1 game;
        Texture2D white, buttonsteer, buttonfire, buttonfire2;
        Rectangle buttonSteerRect, buttonSteerTouchRect, buttonFireRect, buttonFireTouchRect/*, rect*/;
        Rectangle screenButt;
        Rectangle topLayout, bottomLayout;
        int buttonWidth, buttonHeight, steerButtonWidth;
        TouchCollection touchCollection;
        static Accelerometer accelerometer;
        static Vector2 GravityDirection;
        //static float Z;
        static bool controls;//false - klasyczne; true - obrotowe
        static bool soundsActive;
        static bool musicActive;
        static bool ableToContinue;
        bool gamePaused;
        bool unlockTouchAfterLoose;
        private Random rnd;
        int spawner;
        int enemyStartAngle, enemyTrack, EnemyStartDirection;//temporary
        static int score, lives;
        int pointsToBonusLife, diffPointsToGetBonusLife;
        List<Explosion> explosions;
        List<PowerUp> powerUps;
        List<Meteorite> meteorites;
        int ManualShootFlag;
        bool LeftCannon;

        public static bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        public static bool Controls
        {
            get
            {
                return controls;
            }

            set
            {
                controls = value;
            }
        }

        public static bool MusicActive
        {
            get
            {
                return musicActive;
            }

            set
            {
                musicActive = value;
            }
        }

        public static bool SoundsActive
        {
            get
            {
                return soundsActive;
            }

            set
            {
                soundsActive = value;
            }
        }

        public static bool AbleToContinue
        {
            get
            {
                return ableToContinue;
            }

            set
            {
                ableToContinue = value;
            }
        }

        public static int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        public static int Lives
        {
            get
            {
                return lives;
            }

            set
            {
                lives = value;
            }
        }

        public Gra(int screenHeight, int screenWidth, Game1 game)
        {
            this.game = game;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            Initialize();
        }

        public void Initialize()
        {
            rnd = new Random();
            player = new Player(screenHeight, screenWidth, game);
            bullets = new List<Bullet>();
            enemyBullets = new List<EnemyBullet>();
            stars = new List<Star>();
            //rect = new Rectangle(800, 800, 400, 300);
            steerButtonWidth = (int)(screenWidth / 8 * 3.5);
            buttonWidth = screenWidth / 8;
            buttonHeight = screenHeight / 12;
            /*buttonLeftRect = new Rectangle(screenWidth / 5 - buttonWidth / 2,
                screenWidth + (screenHeight - screenWidth) / 4 * 3 - buttonHeight / 2,
                buttonWidth, buttonHeight);
            buttonRightRect = new Rectangle(screenWidth / 5 * 4 - buttonWidth / 2,
                screenWidth + (screenHeight - screenWidth) / 4 * 3 - buttonHeight / 2,
                buttonWidth, buttonHeight);*/
            buttonSteerRect = new Rectangle(screenWidth / 10 * 3 - steerButtonWidth / 2,
            screenWidth + (screenHeight - screenWidth) / 4 * 3 - buttonHeight / 2,
            steerButtonWidth, buttonHeight);
            buttonSteerTouchRect = new Rectangle(0, screenWidth + (screenHeight - screenWidth) / 4 * 3 - buttonHeight,
            steerButtonWidth + screenWidth / 160 * 26, buttonHeight * 2);
            buttonFireRect = new Rectangle(screenWidth / 5 * 4 - buttonWidth / 2,
                screenWidth + (screenHeight - screenWidth) / 4 * 3 - buttonHeight / 2,
                buttonWidth, buttonHeight);
            buttonFireTouchRect = new Rectangle(screenWidth / 5 * 4 - buttonWidth,
                screenWidth + (screenHeight - screenWidth) / 4 * 3 - buttonHeight,
                buttonWidth * 2, buttonHeight * 2);
            screenButt = new Rectangle(0, screenHeight / 2 - screenWidth / 2, screenWidth, screenWidth);
            topLayout = new Rectangle(0, 0, screenWidth, screenHeight / 2 - screenWidth / 2);
            bottomLayout = new Rectangle(0, screenHeight / 2 + screenWidth / 2, screenWidth, screenHeight / 2);
            unlockTouchAfterLoose = false;
            enemies = new List<Enemy>();
            spawner = 0;
            lost1 = new TouchingText(screenHeight, screenWidth, game, "Przegra³eœ!");
            lost2 = new TouchingText(screenHeight, screenWidth, game, "Dotknij ekran, ¿eby");
            lost3 = new TouchingText(screenHeight, screenWidth, game, "powróciæ do menu.");
            lost4 = new TouchingText(screenHeight, screenWidth, game, "Wybuch³eœ!");
            lost5 = new TouchingText(screenHeight, screenWidth, game, "Dotknij ekran, ¿eby");
            lost6 = new TouchingText(screenHeight, screenWidth, game, "wznowiæ grê.");
            hud = new HUD(screenHeight, screenWidth, game);
            score = 0;
            lives = 2;
            gamePaused = false;
            explosions = new List<Explosion>();
            Bullet.Level = 1;
            diffPointsToGetBonusLife = 40000;
            pointsToBonusLife = diffPointsToGetBonusLife;
            powerUps = new List<PowerUp>();
            meteorites = new List<Meteorite>();
            ManualShootFlag = 3;
            LeftCannon = true;
        }

        static public void AccelerometerInit()
        {
            GravityDirection = new Vector2();
            if (accelerometer == null)
            {
                accelerometer = new Accelerometer();
            }
            accelerometer.CurrentValueChanged += accelerometerValueChanged;

            if (accelerometer.State != SensorState.Ready)
            {
                accelerometer.Start();
            }
        }

        static public void AccelerometerStop()
        {
            if (accelerometer != null)
            {
                accelerometer.Stop();
            }
        }

        public void LoadContent()
        {
            player.SetTexture();
            txt = game.Content.Load<SpriteFont>("font");
            //buttonleft = game.Content.Load<Texture2D>("buttonleft");
            //buttonright = game.Content.Load<Texture2D>("buttonright");
            buttonsteer = game.Content.Load<Texture2D>("steerbutton");
            buttonfire = game.Content.Load<Texture2D>("firebutton");
            buttonfire2 = game.Content.Load<Texture2D>("firebutton2");
            white = game.Content.Load<Texture2D>("white");
            Bullet.SetTexture(game);
            EnemyBullet.SetTexture(game);
            Star.SetTexture(game);
            lost1.SetXANDY(txt, screenWidth / 2, screenHeight / 2 - 60);
            lost2.SetXANDY(txt, screenWidth / 2, screenHeight / 2 + 10);
            lost3.SetXANDY(txt, screenWidth / 2, screenHeight / 2 + 55);
            lost4.SetXANDY(txt, screenWidth / 2, screenHeight / 2 - 60);
            lost5.SetXANDY(txt, screenWidth / 2, screenHeight / 2 + 10);
            lost6.SetXANDY(txt, screenWidth / 2, screenHeight / 2 + 55);
            Enemy.SetTexture(game);
            hud.LoadContent();
            SoundLibrary.LoadContent(game);
            Explosion.SetTexture(game);
            PowerUp.SetTexture(game);
            Meteorite.SetTexture(game);
            SoundLibrary.StartMusic();
        }

        static void accelerometerValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            //http://community.monogame.net/t/how-to-use-the-accelerometer-in-android/347
            //need to consider orientation here,if support only landscape might be like this
            /*if (Settings.IsLandscape)
            {
                GravityDirection.Y = -(float)e.SensorReading.Acceleration.Y * gravityCoeff;
                GravityDirection.X = -(float)e.SensorReading.Acceleration.X * gravityCoeff;
            }
            else*/
            {
                GravityDirection.Y = -(float)e.SensorReading.Acceleration.X;
                GravityDirection.X = (float)e.SensorReading.Acceleration.Y;
                //Z = (float)e.SensorReading.Acceleration.Z;
                //Console.WriteLine("x = " + GravityDirection.X);
                //Console.WriteLine("y = " + GravityDirection.Y);
            }

        }

        public void Update()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                SoundLibrary.AlarmStop();
                SoundLibrary.PauseMusic();
                ApplicationManager.callMenu();
                unlockTouchAfterLoose = false;
                if (player != null && player.Alive)
                {
                    gamePaused = true;
                }
                //Initialize();
                //LoadContent();
            }

            //https://gamedev.stackexchange.com/questions/56479/how-to-create-draw-clickable-object-such-as-button-in-winform-in-xna-wp8
            bool isInputPressed = false;
            touchCollection = TouchPanel.GetState();
            List<int> xTouchList = new List<int>();
            List<int> yTouchList = new List<int>();

            if (touchCollection.Count >= 1)
            {
                isInputPressed = touchCollection[0].State == TouchLocationState.Pressed ||
                    touchCollection[0].State == TouchLocationState.Moved;
            }

            if (active)
            {
                if (player != null && player.Alive && !gamePaused)
                {
                    spawner--;
                    if (/*spawner <= 0 && */enemies.Count == 0)
                    {
                        enemyStartAngle = rnd.Next(0, 360);//temporary
                        enemyTrack = rnd.Next(1, 4);//temporary
                        EnemyStartDirection = rnd.Next(0, 2);//temporary
                        spawner = 1440;
                        enemies.Add(new Enemy(screenHeight, screenWidth, game, enemyStartAngle, enemyTrack, EnemyStartDirection, 100, 20, 1, enemyGrantPowerUp()));
                        //enemies.Add(new Enemy(screenHeight, screenWidth, game, rnd.Next(0, 360), rnd.Next(1, 4), rnd.Next(0, 2), 100, 20, 1, enemyGrantPowerUp()));
                    }
                    else if (spawner == 1432 || spawner == 1424 || spawner == 1416 || spawner == 1408 ||
                        spawner == 1400 || spawner == 1392 || spawner == 1384 || spawner == 1376 || spawner == 1368)
                    {
                        enemies.Add(new Enemy(screenHeight, screenWidth, game, enemyStartAngle, enemyTrack, EnemyStartDirection, 100, 20, 1, enemyGrantPowerUp()));
                        //enemies.Add(new Enemy(screenHeight, screenWidth, game, rnd.Next(0, 360), rnd.Next(1, 4), rnd.Next(0, 2), 100, 20, 1, enemyGrantPowerUp()));
                    }
                    if (enemies.Count > 0)
                    {
                        foreach (Enemy enemy in enemies)
                        {
                            enemy.MoveTrack();
                            enemy.Reload();
                            if ((enemy.CanShoot && enemy.ReloadDelay == 0 && IfEnemySeePlayer(enemy.Shape.Rotation, player.Shape.Rotation)
                                && rnd.Next(0, 20 + (int)(Math.Pow(20, enemyBullets.Count))) == (20 + (int)(Math.Pow(20, enemyBullets.Count)) - 1)))
                            {
                                enemyBullets.Add(new EnemyBullet(screenHeight, screenWidth, game, enemy.Shape.Rotation, enemy.Shape.X, enemy.Shape.Y));
                                SoundLibrary.EnemyShot();
                                enemy.ReloadDelay = enemy.ReloadDelayTime;
                            }
                        }
                    }
                    Bullet.Reload();
                    if (bullets.Count > 0)
                    {
                        foreach (Bullet bullet in bullets)
                        {
                            bullet.Expire();
                        }
                        if (bullets.Count > 1 && bullets[1].Alive == false)
                        {
                            bullets.RemoveAt(1);
                        }
                        if (bullets[0].Alive == false)
                        {
                            bullets.RemoveAt(0);
                        }
                    }
                    if (enemyBullets.Count > 0)
                    {
                        for (int b = enemyBullets.Count - 1; b >= 0; b--)
                        {
                            enemyBullets[b].Expire();
                            if (!enemyBullets[b].Alive)
                            {
                                enemyBullets.RemoveAt(b);
                            }
                        }
                    }
                    if (powerUps.Count > 0)
                    {
                        for (int b = powerUps.Count - 1; b >= 0; b--)
                        {
                            powerUps[b].Expire();
                            if (!powerUps[b].Alive)
                            {
                                powerUps.RemoveAt(b);
                            }
                        }
                    }
                    spawnMeteorite();
                    if (meteorites.Count > 0)
                    {
                        for (int b = meteorites.Count - 1; b >= 0; b--)
                        {
                            meteorites[b].Expire();
                            if (!meteorites[b].Alive)
                            {
                                meteorites.RemoveAt(b);
                            }
                        }
                    }
                    if (stars.Count < 40 && rnd.Next(0, 2) == 0)
                    {
                        int starAngle = rnd.Next(0, 360);
                        stars.Add(new Star(screenHeight, screenWidth, game, starAngle));
                    }
                    if (stars.Count > 0)
                    {
                        foreach (Star star in stars)
                        {
                            star.Expire();
                        }
                        if (stars[0].Alive == false)
                        {
                            stars.RemoveAt(0);
                        }
                    }
                    if (controls)
                    {
                        player.UpdatePosition(GravityDirection);
                        checkPlayerGunReload(touchCollection);
                        foreach (TouchLocation touch in touchCollection)
                        {
                            //if (isInputPressed && screenButt.Contains(touch.Position.X, touch.Position.Y))
                            if (isInputPressed && fireButtonPressed(touch))
                            {
                                playerShoot();
                            }
                        }
                    }
                    else
                    {
                        /*if (isInputPressed && buttonLeftRect.Contains(xTouch, yTouch)) {
                            player.MoveLeft();
                        }
                        if (isInputPressed && buttonRightRect.Contains(xTouch, yTouch)) {
                            player.MoveRight();
                        }*/
                        checkPlayerGunReload(touchCollection);
                        foreach (TouchLocation touch in touchCollection)
                        {
                            if (isInputPressed && buttonSteerTouchRect.Contains(touch.Position.X, touch.Position.Y))
                            {
                                player.Move(touch.Position.X - buttonSteerRect.X, buttonSteerRect.Width);
                            }
                            if (isInputPressed && fireButtonPressed(touch))
                            {
                                playerShoot();
                            }
                        }
                    }
                    if (bullets.Count > 0 && meteorites.Count > 0)
                    {
                        for (int i = meteorites.Count - 1; i >= 0; i--)
                        {
                            for (int j = bullets.Count - 1; j >= 0; j--)
                            {
                                if (meteorites[i].Shape.Intersects(bullets[j].Shape))
                                {
                                    bullets[j].Alive = false;
                                    bullets.RemoveAt(j);
                                    if (bullets.Count > 0)
                                    {
                                        j = bullets.Count - 1;
                                    }
                                }
                            }
                        }
                    }
                    if (bullets.Count > 0 && enemies.Count > 0)
                    {
                        for (int i = enemies.Count - 1; i >= 0; i--)
                        {
                            if (enemies[i].Visible)
                            {
                                for (int j = bullets.Count - 1; j >= 0; j--)
                                {
                                    if (enemies[i].Shape.Intersects(bullets[j].Shape))
                                    {
                                        if (enemies[i].GrantPowerUp)
                                        {
                                            powerUps.Add(new PowerUp(screenHeight, screenWidth, game, rnd.Next(0, 360),
                                                500, 320));
                                        }
                                        enemies[i].Alive = false;
                                        bullets[j].Alive = false;
                                        score += enemies[i].EnemyPoints;
                                        explosions.Add(new Explosion(screenHeight, screenWidth, game,
                                            enemies[i].Shape.X + enemies[i].Shape.Width / 2,
                                            enemies[i].Shape.Y + enemies[i].Shape.Height / 2,
                                            (float)Utils.ConvertToRadians(rnd.Next(0, 360)), enemies[i].Size, 2, false));
                                        enemies.RemoveAt(i);
                                        SoundLibrary.EnemyExplode(rnd.Next(0, 3));
                                        bullets.RemoveAt(j);
                                        if (enemies.Count > 0)
                                        {
                                            i = enemies.Count - 1;
                                        }
                                        if (bullets.Count > 0)
                                        {
                                            j = bullets.Count - 1;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (bullets.Count > 0 && powerUps.Count > 0)
                    {
                        for (int i = powerUps.Count - 1; i >= 0; i--)
                        {
                            for (int j = bullets.Count - 1; j >= 0; j--)
                            {
                                if (powerUps[i].Shape.Intersects(bullets[j].Shape))
                                {
                                    powerUps[i].Alive = false;
                                    bullets[j].Alive = false;
                                    score += powerUps[i].Points;
                                    explosions.Add(new Explosion(screenHeight, screenWidth, game,
                                        powerUps[i].Shape.X + powerUps[i].Shape.Width / 2,
                                        powerUps[i].Shape.Y + powerUps[i].Shape.Height / 2,
                                        (float)Utils.ConvertToRadians(rnd.Next(0, 360)), 3, 2, false));
                                    powerUps.RemoveAt(i);
                                    SoundLibrary.EnemyExplode(rnd.Next(0, 3));
                                    bullets.RemoveAt(j);
                                    Bullet.Improve();
                                    if (powerUps.Count > 0)
                                    {
                                        i = powerUps.Count - 1;
                                    }
                                    if (bullets.Count > 0)
                                    {
                                        j = bullets.Count - 1;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    if (enemyBullets.Count > 0)
                    {
                        for (int i = enemyBullets.Count - 1; i >= 0; i--)
                        {
                            if (enemyBullets[i].Shape.Intersects(player.Shape) && Player.Shields == 0)
                            {
                                enemyBullets[i].Alive = false;
                                enemyBullets.RemoveAt(i);
                                Player.HP -= 2;
                                SoundLibrary.PlayerHit(rnd.Next(0, 6));
                            }
                        }
                    }
                    if (meteorites.Count > 0)
                    {
                        for (int i = meteorites.Count - 1; i >= 0; i--)
                        {
                            if (meteorites[i].Shape.Intersects(player.Shape) && Player.Shields == 0)
                            {
                                meteorites[i].Alive = false;
                                explosions.Add(new Explosion(screenHeight, screenWidth, game,
                                    meteorites[i].Shape.X + meteorites[i].Shape.Width / 2,
                                    meteorites[i].Shape.Y + meteorites[i].Shape.Height / 2,
                                    (float)Utils.ConvertToRadians(rnd.Next(0, 360)), 4, 2, false));
                                meteorites.RemoveAt(i);
                                SoundLibrary.EnemyExplode(rnd.Next(0, 3));
                                Player.HP -= 8;
                                SoundLibrary.PlayerHeavyHit(rnd.Next(0, 2));
                                if (meteorites.Count > 0)
                                {
                                    i = meteorites.Count - 1;
                                }
                            }
                        }
                    }
                    if (enemies.Count > 0)
                    {
                        for (int i = enemies.Count - 1; i >= 0; i--)
                        {
                            if (enemies[i].Visible && enemies[i].CanCollide)
                            {
                                if (enemies[i].Shape.Intersects(player.Shape) && Player.Shields == 0)
                                {
                                    if (enemies[i].GrantPowerUp)
                                    {
                                        powerUps.Add(new PowerUp(screenHeight, screenWidth, game, rnd.Next(0, 360),
                                            500, 320));
                                    }
                                    enemies[i].Alive = false;
                                    score += enemies[i].EnemyPoints;
                                    explosions.Add(new Explosion(screenHeight, screenWidth, game,
                                        enemies[i].Shape.X + enemies[i].Shape.Width / 2,
                                        enemies[i].Shape.Y + enemies[i].Shape.Height / 2,
                                        (float)Utils.ConvertToRadians(rnd.Next(0, 360)), enemies[i].Size, 2, false));
                                    enemies.RemoveAt(i);
                                    SoundLibrary.EnemyExplode(rnd.Next(0, 3));
                                    Player.HP -= 5;
                                    SoundLibrary.PlayerHeavyHit(rnd.Next(0, 2));
                                    if (enemies.Count > 0)
                                    {
                                        i = enemies.Count - 1;
                                    }
                                }
                            }
                        }
                    }
                    getExtraLife();
                    if (Player.HP <= 0)
                    {
                        Player.HP = 0;
                        player.Alive = false;
                        SoundLibrary.PlayerExplode(rnd.Next(0, 2));
                        SoundLibrary.AlarmStop();
                        SoundLibrary.StopMusic();
                        explosions.Add(new Explosion(screenHeight, screenWidth, game,
                            player.Shape.X + player.Shape.Width / 2,
                            player.Shape.Y + player.Shape.Height / 2,
                            (float)Utils.ConvertToRadians(rnd.Next(0, 360)), 5, 5, true));
                    }
                    else if (Player.HP <= 2)
                    {
                        SoundLibrary.AlarmStart();
                    }
                    if (explosions.Count > 0)
                    {
                        for (int i = explosions.Count - 1; i >= 0; i--)
                        {
                            if (!(explosions[i].PlayerExplosion))
                            {
                                explosions[i].Update();
                                if (!explosions[i].Alive)
                                {
                                    explosions.RemoveAt(i);
                                }
                            }
                        }
                    }
                    hud.Update();
                    player.Update();
                }
                else if (gamePaused)
                {
                    if (touchCollection.Count == 0)
                    {
                        unlockTouchAfterLoose = true;
                    }
                    foreach (TouchLocation touch in touchCollection)
                    {
                        if (unlockTouchAfterLoose && isInputPressed && screenButt.Contains(touch.Position.X, touch.Position.Y))
                        {
                            unlockTouchAfterLoose = false;
                            gamePaused = false;
                            SoundLibrary.ResumeMusic();
                        }
                    }
                }
                else if (lives > 0)
                {
                    if (touchCollection.Count == 0)
                    {
                        unlockTouchAfterLoose = true;
                    }
                    player = null;
                    foreach (TouchLocation touch in touchCollection)
                    {
                        if (unlockTouchAfterLoose && isInputPressed && screenButt.Contains(touch.Position.X, touch.Position.Y))
                        {
                            unlockTouchAfterLoose = false;
                            lives--;
                            player = new Player(screenHeight, screenWidth, game);
                            player.SetTexture();
                            Bullet.Level = 1;
                            SoundLibrary.StartMusic();
                        }
                    }
                }
                else
                {
                    if (touchCollection.Count == 0)
                    {
                        unlockTouchAfterLoose = true;
                    }
                    player = null;
                    foreach (TouchLocation touch in touchCollection)
                    {
                        if (unlockTouchAfterLoose && isInputPressed && screenButt.Contains(touch.Position.X, touch.Position.Y))
                        {
                            unlockTouchAfterLoose = false;
                            ApplicationManager.callMenu();
                            ableToContinue = false;
                            //Initialize();
                            //LoadContent();
                        }
                    }
                }
                if (explosions.Count > 0)
                {
                    for (int i = explosions.Count - 1; i >= 0; i--)
                    {
                        if (explosions[i].PlayerExplosion)
                        {
                            explosions[i].Update();
                            if (!explosions[i].Alive)
                            {
                                explosions.RemoveAt(i);
                            }
                        }
                    }
                }
            }
        }

        public void spawnMeteorite()
        {
            if (meteorites.Count < 2)
            {
                if (rnd.Next(0, 100) == 99)
                {
                    meteorites.Add(new Meteorite(screenHeight, screenWidth, game, rnd.Next(0, 360)));
                    //Console.WriteLine("Meteorite spawn!");
                }
            }
        }

        public bool enemyGrantPowerUp()
        {
            if (rnd.Next(0, 20) == 19)
            {
                return true;
            }
            return false;
        }

        public void getExtraLife()
        {
            if (score >= pointsToBonusLife)
            {
                pointsToBonusLife += diffPointsToGetBonusLife;
                if (lives < 5)
                {
                    lives++;
                }
            }
        }

        public bool fireButtonPressed(TouchLocation touch)
        {
            if (controls)
            {
                return buttonFireTouchRect.Contains(touch.Position.X, touch.Position.Y) ||
                                screenButt.Contains(touch.Position.X, touch.Position.Y);
            }
            else
            {
                return buttonFireTouchRect.Contains(touch.Position.X, touch.Position.Y);
            }
        }

        public void checkPlayerGunReload(TouchCollection touchCollection)
        {
            if (Bullet.ReloadDelay == 0)
            {
                ManualShootFlag = 3;
            }
            else
            {
                if (ManualShootFlag % 2 == 0)
                {
                    bool flagaChange = true;
                    foreach (TouchLocation touch in touchCollection)
                    {
                        if (fireButtonPressed(touch))
                        {
                            flagaChange = false;
                        }
                    }
                    if (flagaChange)
                    {
                        ManualShootFlag++;
                    }
                }
                else if (ManualShootFlag % 2 == 1)
                {
                    bool flagaChange = false;
                    foreach (TouchLocation touch in touchCollection)
                    {
                        if (fireButtonPressed(touch))
                        {
                            flagaChange = true;
                        }
                    }
                    if (flagaChange)
                    {
                        ManualShootFlag++;
                    }
                }
            }
        }

        public void playerShoot()
        {
            int dmgMultiplier = 1;
            if (Bullet.Level == 5)
            {
                dmgMultiplier = 2;
            }
            if (Bullet.Level == 1 && (Bullet.ReloadDelay == 0 || ManualShootFlag == 4))
            {
                bullets.Add(new Bullet(screenHeight, screenWidth, game, player.Shape.Rotation, dmgMultiplier));
                SoundLibrary.PlayerShot();
                Bullet.ReloadDelay = Bullet.setReloadTimeAfterShoot();
                ManualShootFlag = 0;
                LeftCannon = !LeftCannon;
            }
            else if (Bullet.Level == 2 && (Bullet.ReloadDelay == 0 || ManualShootFlag == 2))
            {
                bullets.Add(new Bullet(screenHeight, screenWidth, game, player.Shape.Rotation, dmgMultiplier));
                SoundLibrary.PlayerShot();
                Bullet.ReloadDelay = Bullet.setReloadTimeAfterShoot();
                ManualShootFlag = 0;
                LeftCannon = !LeftCannon;
            }
            else if (Bullet.Level == 3 && Bullet.ReloadDelay == 0)
            {
                bullets.Add(new Bullet(screenHeight, screenWidth, game,
                    player.Shape.Rotation - (float)Utils.ConvertToRadians(5), dmgMultiplier));
                bullets.Add(new Bullet(screenHeight, screenWidth, game,
                    player.Shape.Rotation + (float)Utils.ConvertToRadians(5), dmgMultiplier));
                SoundLibrary.PlayerShot();
                Bullet.ReloadDelay = Bullet.setReloadTimeAfterShoot();
                ManualShootFlag = 0;
                Console.WriteLine("Both");
            }
            else if (Bullet.Level == 3 && ManualShootFlag == 2)
            {
                if (LeftCannon)
                {
                    bullets.Add(new Bullet(screenHeight, screenWidth, game,
                        player.Shape.Rotation + (float)Utils.ConvertToRadians(5), dmgMultiplier));
                    Console.WriteLine("Left");
                } else {
                    bullets.Add(new Bullet(screenHeight, screenWidth, game,
                        player.Shape.Rotation - (float)Utils.ConvertToRadians(5), dmgMultiplier));
                    Console.WriteLine("Right");
                }
                SoundLibrary.PlayerShot();
                Bullet.ReloadDelay = Bullet.setReloadTimeAfterShoot();
                ManualShootFlag = 0;
                LeftCannon = !LeftCannon;
            }
            else if (Bullet.Level >= 4 && (Bullet.ReloadDelay == 0 || ManualShootFlag >= 1))
            {
                bullets.Add(new Bullet(screenHeight, screenWidth, game,
                    player.Shape.Rotation - (float)Utils.ConvertToRadians(5), dmgMultiplier));
                bullets.Add(new Bullet(screenHeight, screenWidth, game,
                    player.Shape.Rotation + (float)Utils.ConvertToRadians(5), dmgMultiplier));
                SoundLibrary.PlayerShot();
                Bullet.ReloadDelay = Bullet.setReloadTimeAfterShoot();
                ManualShootFlag = 0;
            }
        }

        private bool IfEnemySeePlayer(float enemyRotation, float playerRotation)
        {
            float eRot = enemyRotation;
            float pRot = playerRotation;
            float halfOfSeeingAngle = 60;
            if (eRot >= (float)Utils.ConvertToRadians(360 - halfOfSeeingAngle) &&
                pRot <= (float)Utils.ConvertToRadians(halfOfSeeingAngle))
            {
                eRot -= (float)Utils.ConvertToRadians(360);
            }
            else if (eRot <= (float)Utils.ConvertToRadians(halfOfSeeingAngle) &&
                pRot >= (float)Utils.ConvertToRadians(360 - halfOfSeeingAngle))
            {
                eRot += (float)Utils.ConvertToRadians(360);
            }

            return (eRot + (float)Utils.ConvertToRadians(halfOfSeeingAngle) >= pRot &&
                eRot - (float)Utils.ConvertToRadians(halfOfSeeingAngle) <= pRot);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawGame(spriteBatch);
        }

        public void DrawGame(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (Star star in stars)
            {
                star.Draw(spriteBatch);
            }
            if (explosions.Count > 0)
            {
                foreach (Explosion explosion in explosions)
                {
                    explosion.Draw(spriteBatch);
                }
            }
            if (powerUps.Count > 0)
            {
                foreach (PowerUp powerup in powerUps)
                {
                    powerup.Draw(spriteBatch);
                }
            }
            if (bullets.Count > 0)
            {
                foreach (Bullet bullet in bullets)
                {
                    bullet.Draw(spriteBatch);
                }
            }
            if (enemyBullets.Count > 0)
            {
                foreach (EnemyBullet bullet in enemyBullets)
                {
                    bullet.Draw(spriteBatch);
                }
            }
            if (enemies.Count > 0)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.Visible)
                    {
                        enemy.Draw(spriteBatch);
                    }
                }
            }
            if (meteorites.Count > 0)
            {
                foreach (Meteorite meteorite in meteorites)
                {
                    meteorite.Draw(spriteBatch);
                }
            }
            if (player != null && player.Alive && !gamePaused)
            {
                player.Draw(spriteBatch);
            }
            else if (gamePaused)
            {
                player.Draw(spriteBatch);
                lost5.Draw(spriteBatch);
                lost6.Draw(spriteBatch);
            }
            else if (lives > 0)
            {
                //Vector2 Center1 = new Vector2(screenWidth / 2 - 30, screenHeight / 2 - 10);
                //Vector2 Center2 = new Vector2(screenWidth / 2 - 120, screenHeight / 2 + 10);
                lost4.Draw(spriteBatch);
                lost5.Draw(spriteBatch);
                lost6.Draw(spriteBatch);
            }
            else
            {
                //Vector2 Center1 = new Vector2(screenWidth / 2 - 30, screenHeight / 2 - 10);
                //Vector2 Center2 = new Vector2(screenWidth / 2 - 120, screenHeight / 2 + 10);
                lost1.Draw(spriteBatch);
                lost2.Draw(spriteBatch);
                lost3.Draw(spriteBatch);
            }


            spriteBatch.Draw(white, topLayout, Microsoft.Xna.Framework.Color.Black);
            spriteBatch.Draw(white, bottomLayout, Microsoft.Xna.Framework.Color.Black);

            spriteBatch.Draw(white, new Rectangle(0, screenHeight / 2 - screenWidth / 2 - 4,
                screenWidth, 1), Microsoft.Xna.Framework.Color.White);
            spriteBatch.Draw(white, new Rectangle(0, screenHeight / 2 - screenWidth / 2,
                screenWidth, 1), Microsoft.Xna.Framework.Color.White);
            spriteBatch.Draw(white, new Rectangle(0, screenHeight / 2 + screenWidth / 2,
                screenWidth, 1), Microsoft.Xna.Framework.Color.White);
            spriteBatch.Draw(white, new Rectangle(0, screenHeight / 2 + screenWidth / 2 + 4,
                screenWidth, 1), Microsoft.Xna.Framework.Color.White);


            //spriteBatch.Draw(white, new Rectangle(screenWidth / 2, screenHeight / 2 - screenWidth / 2,
            //    1, screenWidth), Microsoft.Xna.Framework.Color.White);
            //spriteBatch.Draw(white, new Rectangle(0, screenHeight / 2,
            //    screenWidth, 1), Microsoft.Xna.Framework.Color.White);
            //spriteBatch.Draw(white, rect, Microsoft.Xna.Framework.Color.White);
            if (!controls)
            {
                //spriteBatch.Draw(buttonleft, buttonLeftRect, Microsoft.Xna.Framework.Color.White);
                //spriteBatch.Draw(buttonright, buttonRightRect, Microsoft.Xna.Framework.Color.White);
                spriteBatch.Draw(buttonsteer, buttonSteerRect, Microsoft.Xna.Framework.Color.White);
                /*
                if (Bullet.Level == 1 && !gunReloaded)
                {
                    spriteBatch.Draw(buttonfire2, buttonFireRect, Microsoft.Xna.Framework.Color.White);
                }
                else
                {
                    spriteBatch.Draw(buttonfire, buttonFireRect, Microsoft.Xna.Framework.Color.White);
                }
                */
            }
            if ((Bullet.Level == 1 && ManualShootFlag < 3) || (Bullet.Level >= 2 && ManualShootFlag < 1))
            {
                spriteBatch.Draw(buttonfire2, buttonFireRect, Microsoft.Xna.Framework.Color.White);
            }
            else
            {
                spriteBatch.Draw(buttonfire, buttonFireRect, Microsoft.Xna.Framework.Color.White);
            }

            hud.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}