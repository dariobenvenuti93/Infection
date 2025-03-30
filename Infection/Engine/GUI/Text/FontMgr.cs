using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIV_Engine
{
    static class FontMgr
    {
        private static Dictionary<string, Font> fonts;
        private static Font defaultFont;

        static FontMgr()
        {
            fonts = new Dictionary<string, Font>();
        }

        public static Font AddFont(string fontName, string texturePath, int numColumns, int firstCharASCIIvalue, int charWidth, int charHeight)
        {
            Font f;

            if (!fonts.ContainsKey(fontName))
            {
                f = new Font(fontName, texturePath, numColumns, firstCharASCIIvalue, charWidth, charHeight);
                fonts.Add(fontName, f);

                if (defaultFont == null)
                {
                    defaultFont = f;
                }
            }
            else
            {
                f = fonts[fontName];
            }

            return f;
        }

        public static Font Getfont(string fontName = "")
        {
            if (fontName != "" && fonts.ContainsKey(fontName))
            {
                return fonts[fontName];
            }

            return defaultFont;
        }
    }
}
