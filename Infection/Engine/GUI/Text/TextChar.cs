using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIV_Engine
{
    internal class TextChar : GameObject
    {
        protected Font font;
        protected char character;

        public char Character {  get { return character; } set { character = value; ComputeOffset(); } }

        public TextChar(Vector2 spritePosition, char character, Font f) : base(f.TextureName, DrawLayer.GUI, spriteW: Game.PixelsToUnits(f.CharacterWidth), spriteH: Game.PixelsToUnits(f.CharacterHeight))
        {
            sprite.position = spritePosition;

            sprite.pivot = Vector2.Zero;
            font = f;
            spriteWidth = f.CharacterWidth;
            spriteHeight = f.CharacterHeight;
            Character = character;
            IsActive = true;

            DrawManager.AddItem(this);
        }

        protected void ComputeOffset()
        {
            Vector2 textureOffset = font.GetOffset(character);

            textureOffsetX = (int)textureOffset.X;
            textureOffsetY = (int)textureOffset.Y;
        }
    }
}
