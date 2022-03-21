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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarfighterMission
{
    static class Utils
    {
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double ConvertToDegrees(double angle)
        {
            return angle * 180.0 / Math.PI;
        }

        public static bool CheckIfPointIsInRect(int xTouch, int yTouch, Rectangle shape)
        {
            return shape.Contains(xTouch, yTouch);
        }

        public static void DrawText(SpriteBatch spriteBatch, Texture2D tex, Rectangle rect, Color color)
        {
            spriteBatch.Draw(tex, rect, color);
        }

        /*public static void DrawText(SpriteBatch spriteBatch, Texture2D tex, int x, int y, int textHeight, Color color, double scale)
        {
            int textWidth = (textHeight * tex.Width) / tex.Height;
            Rectangle rect = new Rectangle(x, y, (int)(scale * textWidth), (int)(scale * textHeight));
            spriteBatch.Draw(tex, rect, color);
        }*/

        public static Rectangle GetRectangleForTexture2D(Texture2D tex, int x, int y, int textHeight)
        {
            int textWidth = (textHeight * tex.Width) / tex.Height;
            Rectangle rect = new Rectangle(x, y, textWidth, textHeight);
            return rect;
        }
    }
}