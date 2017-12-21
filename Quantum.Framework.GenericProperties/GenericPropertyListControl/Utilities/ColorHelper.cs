using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Framework.GenericProperties.GenericPropertyListControl.Utilities
{
    public class ColorHelper
    {
        public static string ToHtml(Color color, bool withSharpCharacter = false)
        {
            return string.Format("{0}{1}{2}{3}",
                                 withSharpCharacter ? "#" : string.Empty,
                                 color.R.ToString("X2"),
                                 color.G.ToString("X2"),
                                 color.B.ToString("X2")).ToLower();
        }

        public static Color FromHtml(string htmlColor)
        {
            return FromHtml(htmlColor, Color.Black);
        }

        public static Color FromHtml(string htmlColor, Color defaultColor)
        {
            var htmlColorWithoutSharp = htmlColor.Replace("#", string.Empty);

            if (htmlColorWithoutSharp.Length != 6)
                return defaultColor;

            try
            {
                var rStr = htmlColorWithoutSharp.Substring(0, 2);
                var gStr = htmlColorWithoutSharp.Substring(2, 2);
                var bStr = htmlColorWithoutSharp.Substring(4, 2);

                int r = Convert.ToInt32(rStr, 16);
                int g = Convert.ToInt32(gStr, 16);
                int b = Convert.ToInt32(bStr, 16);

                return Color.FromArgb(r, g, b);
            }
            catch
            {
                return defaultColor;
            }
        }
    }
}
