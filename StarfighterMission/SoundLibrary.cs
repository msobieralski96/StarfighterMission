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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace StarfighterMission
{
    public static class SoundLibrary
    {
        private static SoundEffect alarm, alarm2, expl1, expl2, expl3;
        private static SoundEffect expl4, expl5, powerup, shot, shot2;
        private static SoundEffect sound1, torpedo, heavyhit;
        private static SoundEffect hit1, hit2, hit3, hit4, hit5, hit6;
        private static SoundEffectInstance alarmInstance;
        private static Song bachRemix;


        public static void LoadContent(Game1 game)
        {
            alarm = game.Content.Load<SoundEffect>("sounds/alarm");
            alarm2 = game.Content.Load<SoundEffect>("sounds/alarm2");
            expl1 = game.Content.Load<SoundEffect>("sounds/expl1");
            expl2 = game.Content.Load<SoundEffect>("sounds/expl2");
            expl3 = game.Content.Load<SoundEffect>("sounds/expl3");
            expl4 = game.Content.Load<SoundEffect>("sounds/expl4");
            expl5 = game.Content.Load<SoundEffect>("sounds/expl5");
            heavyhit = game.Content.Load<SoundEffect>("sounds/heavyhit");
            hit1 = game.Content.Load<SoundEffect>("sounds/hit1");
            hit2 = game.Content.Load<SoundEffect>("sounds/hit2");
            hit3 = game.Content.Load<SoundEffect>("sounds/hit3");
            hit4 = game.Content.Load<SoundEffect>("sounds/hit4");
            hit5 = game.Content.Load<SoundEffect>("sounds/hit5");
            hit6 = game.Content.Load<SoundEffect>("sounds/hit6");
            powerup = game.Content.Load<SoundEffect>("sounds/powerup");
            shot = game.Content.Load<SoundEffect>("sounds/shot");
            shot2 = game.Content.Load<SoundEffect>("sounds/shot2");
            sound1 = game.Content.Load<SoundEffect>("sounds/sound1");
            torpedo = game.Content.Load<SoundEffect>("sounds/torpedo");

            bachRemix = game.Content.Load<Song>("sounds/bachremix");

            alarmInstance = alarm.CreateInstance();
            alarmInstance.IsLooped = true;

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
        }

        public static void PlayerShot()
        {
            if (Gra.SoundsActive)
            {
                shot.Play(1.0f, 0.0f, 0.0f);
            }
        }

        public static void EnemyShot()
        {
            if (Gra.SoundsActive)
            {
                shot2.Play(0.7f, 0.0f, 0.0f);
            }
        }

        public static void PlayerExplode(int rnd)
        {
            if (Gra.SoundsActive)
            {
                if (rnd == 0)
                {
                    expl4.Play(1.0f, 0.0f, 0.0f);
                }
                else
                {
                    expl5.Play(1.0f, 0.0f, 0.0f);
                }
            }
        }

        public static void EnemyExplode(int rnd)
        {
            if (Gra.SoundsActive)
            {
                if (rnd == 0)
                {
                    expl1.Play(0.5f, 0.0f, 0.0f);
                }
                else if (rnd == 1)
                {
                    expl2.Play(0.05f, 0.0f, 0.0f);
                }
                else
                {
                    expl3.Play(0.5f, 0.0f, 0.0f);
                }
            }
        }

        public static void AlarmStart()
        {
            if (Gra.SoundsActive)
            {
                if (Gra.MusicActive)
                {
                    alarmInstance.Volume = 0.2f;
                }
                else
                {
                    alarmInstance.Volume = 0.05f;
                }
                if (alarmInstance.State == SoundState.Stopped)
                {
                    alarmInstance.Play();
                }
            }
        }

        public static void AlarmStop()
        {
            if (Gra.SoundsActive)
            {
                if (alarmInstance.State == SoundState.Playing)
                {
                    alarmInstance.Stop();
                }
            }
        }

        public static void PlayerHit(int rnd)
        {
            if (Gra.SoundsActive)
            {
                if (rnd == 0)
                {
                    hit1.Play(0.3f, 0.0f, 0.0f);
                }
                else if (rnd == 1)
                {
                    hit2.Play(0.6f, 0.0f, 0.0f);
                }
                else if (rnd == 2)
                {
                    hit3.Play(0.6f, 0.0f, 0.0f);
                }
                else if (rnd == 3)
                {
                    hit4.Play(0.6f, 0.0f, 0.0f);
                }
                else if (rnd == 4)
                {
                    hit5.Play(0.75f, 0.0f, 0.0f);
                }
                else
                {
                    hit6.Play(0.15f, 0.0f, 0.0f);
                }
            }
        }

        public static void PlayerHeavyHit(int rnd)
        {
            if (Gra.SoundsActive)
            {
                if (rnd == 0)
                {
                    hit1.Play(0.67f, 0.0f, 0.0f);
                }
                else
                {
                    heavyhit.Play(0.67f, 0.0f, 0.0f);
                }
            }
        }

        public static void StartMusic()
        {
            if (Gra.MusicActive && Gra.Active && MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(bachRemix);
            }
        }

        public static void PauseMusic()
        {
            if (Gra.MusicActive && MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
        }

        public static void StopMusic()
        {
            if (Gra.MusicActive && MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
        }

        public static void ResumeMusic()
        {
            if (Gra.MusicActive && Gra.Active && MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
        }
    }
}