using Aiv.Fast2D;
using Infection;
using OpenTK;
using OpenTK.Graphics.ES20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIV_Engine
{
    internal class GameObject: I_Updatable, I_Drawable
    {
        protected int id;
        public int Id { get { return id; } }

        protected Texture texture;
        protected Sprite sprite;

        public RigidBody RigidBody;
        public bool IsActive;

        protected float maxSpeed;
        //protected Vector2 velocity;

        public virtual Vector2 Position { get { return sprite.position; } set { sprite.position = value; } }
        public float X { get { return Position.X; } set { sprite.position.X = value; } }
        public float Y { get { return Position.Y; } set { sprite.position.Y = value; } }
        public float HalfWidth { get; protected set; }
        public float HalfHeight { get; protected set; }

        public DrawLayer Layer { get; protected set; }

        public Vector2 Pivot { get { return sprite.pivot; } set { sprite.pivot = value; } }

        //point to start cut texture
        protected int textureOffsetX;
        protected int textureOffsetY;

        //size of cut
        protected int spriteWidth;
        protected int spriteHeight;


        public Vector2 Forward {
            get
            {
                return new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation));
            }
            set
            {
                sprite.Rotation = (float)Math.Atan2(value.Y,value.X);
            }
        }
        public GameObject()
        {
        }
        public GameObject(string textureName, DrawLayer layer = DrawLayer.Playground, int textOffsetX=0, int textOffsetY=0, float spriteW=0, float spriteH=0)
        {
            texture = GfxManager.GetTexture(textureName);
            id = Configs.GetGameObjectId();

            //float _spriteW = spriteW > 0 ? spriteW : Game.PixelsToUnits(texture.Width);
            //float _spriteH = spriteH > 0 ? spriteH : Game.PixelsToUnits(texture.Height);
            float _spriteW = spriteW > 0 ? spriteW : texture.Width;
            float _spriteH = spriteH > 0 ? spriteH : texture.Height;

            sprite = new Sprite(_spriteW, _spriteH);

            spriteWidth = texture.Width;
            spriteHeight = texture.Height;

            textureOffsetX = textOffsetX;
            textureOffsetY = textOffsetY;

            HalfWidth = sprite.Width * 0.5f;
            HalfHeight = sprite.Height * 0.5f;
            
            sprite.pivot = new Vector2(HalfWidth, HalfHeight);

            Layer = layer;
        }

        public virtual void Update()
        {
            if(IsActive)
            {
               Position += RigidBody.Velocity * Game.DeltaTime;
            }
        }

        public virtual void OnCollide(Collision collisionInfo)
        {
            sprite.position -= collisionInfo.Delta;
        }

        public virtual void Draw()
        {
            if(IsActive)
            {
                sprite.DrawTexture(texture,textureOffsetX,textureOffsetY,spriteWidth,spriteHeight);
            }
        }
    }
}
