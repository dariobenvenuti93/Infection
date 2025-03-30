using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIV_Engine
{
    internal class TextObject
    {
        protected List<TextChar> sprites;
        protected string text;
        protected Font font;
        protected bool isActive;
        protected float hSpace;//horizontal space between characters

        protected Vector2 position;

        public string Text
        {
            get { return text; }
            set { SetText(value); }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; UpdateCharStatus(); }
        }

        public TextObject(Vector2 position, string textString = "", Font font = null, float horizontalSpacing = 0)
        {
            this.position = position;
            this.hSpace = horizontalSpacing;

            if (font == null)
            {
                font = FontMgr.Getfont("");//default font
            }

            this.font = font;

            sprites = new List<TextChar>();

            if (textString != "")
            {
                SetText(textString);
            }
        }

        public void SetText(string newText)
        {
            if (newText == text)
            {
                return;
            }

            text = newText;
            int numChars = text.Length;

            float charX = position.X;
            float charY = position.Y;

            for (int i = 0; i < numChars; i++)
            {
                char c = text[i];//string as char array

                if (i >= sprites.Count)
                {//i is greater than last char index
                    TextChar tc = new TextChar(new Vector2(charX, charY), c, font);
                    tc.IsActive = isActive;
                    sprites.Add(tc);
                }
                else if (c != sprites[i].Character)
                {//change character
                    sprites[i].Character = c;
                }

                charX += sprites[i].HalfWidth * 2 + hSpace;//compute next TextChar position
            }

            //check for extra TextChar to remove

            if(sprites.Count > numChars)
            {
                int charsToRemoveCount = sprites.Count - numChars;
                int startCut = numChars;

                sprites.RemoveRange(startCut, charsToRemoveCount);
            }
        }

        protected void UpdateCharStatus()
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].IsActive = isActive;
            }
        }
    }
}
