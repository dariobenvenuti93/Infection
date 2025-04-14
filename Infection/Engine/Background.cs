using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;
using Infection;

namespace AIV_Engine
{
    internal class Background : I_Drawable
    {
        private Sprite sprite;
        private Texture texture;

        public DrawLayer Layer { get; protected set; }

        public Background(string textureName, DrawLayer layer)
        {
            texture = GfxManager.GetTexture(textureName);
            sprite = new Sprite(Game.Window.Width, Game.Window.Height);

            Layer = layer;

            DrawManager.AddItem(this);
        }
        public void SetColor(Vector4 color)
        {
            sprite.SetMultiplyTint(color);
        }
        public void Draw()
        {
            sprite.DrawTexture(texture);
        }
    }
}
