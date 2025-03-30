using OpenTK;
using System;
using Aiv.Fast2D;

namespace AIV_Engine
{
    internal class ProgressBar : GameObject
    {
        protected Vector2 barOffset;
        protected Sprite barSprite;
        protected Texture barTexture;
        protected int barWidth;

        public override Vector2 Position { get => base.Position; set { base.Position = value; barSprite.position = value + barOffset; } }

        public ProgressBar(string frameTextureName, string barTextureName, Vector2 offset) : base(frameTextureName, DrawLayer.GUI)
        {
            sprite.pivot = Vector2.Zero;
            barTexture = GfxManager.GetTexture(barTextureName);
            barSprite = new Sprite(Game.PixelsToUnits(barTexture.Width), Game.PixelsToUnits(barTexture.Height));
            barSprite.Camera = sprite.Camera;

            barOffset = offset;
            barWidth = (int)sprite.Width;

            IsActive = true;

            DrawManager.AddItem(this);
        }

        public void Scale(float scale)
        {
            scale = MathHelper.Clamp(scale, 0, 1);

            barSprite.scale.X = scale;
            barWidth = (int)(barTexture.Width * barSprite.scale.X);

            barSprite.SetMultiplyTint((1 - scale) * 50, scale * 2, scale, 1);
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Draw();
                barSprite.DrawTexture(barTexture,0,0,barWidth,barTexture.Height);
            }
        }
    }
}
