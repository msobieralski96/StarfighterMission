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

namespace StarfighterMission
{
    class ApplicationManager
    {
        Gra gra;
        Menu menu;
        MenuSterowanie sterowanie;
        MenuDzwieki dzwieki;
        MenuCredits credits;

        public ApplicationManager(int screenHeight, int screenWidth, Game1 game)
        {
            Gra.Controls = false;
            Gra.MusicActive = true;
            Gra.SoundsActive = true;
            Gra.AbleToContinue = false;
            gra = new Gra(screenHeight, screenWidth, game);
            menu = new Menu(screenHeight, screenWidth, game);
            sterowanie = new MenuSterowanie(screenHeight, screenWidth, game);
            dzwieki = new MenuDzwieki(screenHeight, screenWidth, game);
            credits = new MenuCredits(screenHeight, screenWidth, game);
            Menu.Active = true;
            Gra.Active = false;
            MenuSterowanie.Active = false;
            MenuDzwieki.Active = false;
            MenuCredits.Active = false;
        }

        public void Initialize()
        {

        }

        public void LoadContent()
        {
            gra.LoadContent();
            menu.LoadContent();
            sterowanie.LoadContent();
            dzwieki.LoadContent();
            credits.LoadContent();
        }

        public void Update(Game1 game1)
        {
            if (Menu.Active)
            {
                menu.Update(game1);
            }
            if (Gra.Active)
            {
                gra.Update();
            }
            if (MenuSterowanie.Active)
            {
                sterowanie.Update(game1);
            }
            if (MenuDzwieki.Active)
            {
                dzwieki.Update(game1);
            }
            if (MenuCredits.Active)
            {
                credits.Update(game1);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Menu.Active)
            {
                menu.Draw(spriteBatch);
            }
            else if (Gra.Active)
            {
                gra.Draw(spriteBatch);
            }
            else if (MenuSterowanie.Active)
            {
                sterowanie.Draw(spriteBatch);
            }
            else if (MenuDzwieki.Active)
            {
                dzwieki.Draw(spriteBatch);
            }
            else if (MenuCredits.Active)
            {
                credits.Draw(spriteBatch);
            }
        }

        public static void callMenu()
        {
            switchMenu();
            Menu.Active = true;
        }

        public static void callGra()
        {
            switchMenu();
            Gra.Active = true;
            Gra.AbleToContinue = true;
        }

        public void prepareNewGame()
        {
            gra.Initialize();
            gra.LoadContent();
        }

        public static void callKontynuujGre()
        {
            switchMenu();
            Gra.Active = true;
        }

        public static void callSterowanie()
        {
            switchMenu();
            MenuSterowanie.Active = true;
        }

        public static void callDzwieki()
        {
            switchMenu();
            MenuDzwieki.Active = true;
        }

        public static void callCredits()
        {
            switchMenu();
            MenuCredits.Active = true;
        }

        public static void switchMenu()
        {
            Menu.Active = false;
            Gra.Active = false;
            MenuSterowanie.Active = false;
            MenuDzwieki.Active = false;
            MenuCredits.Active = false;
        }
    }
}