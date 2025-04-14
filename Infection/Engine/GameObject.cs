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
        public Vector2 Offset
        {
            get => new Vector2(textureOffsetX, textureOffsetY);
            set
            {
                textureOffsetX = (int)value.X;
                textureOffsetY = (int)value.Y;
            }
        }
        public int Id { get { return id; } }

        protected Texture texture;
        protected Sprite sprite;
        public Sprite Sprite { get { return sprite; } }

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

        //size of frame
        public int FrameWidth;
        public int FrameHeight;

        //size of cut
        protected int cutWidth;
        protected int cutHeight;


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
        public void SetTexture(string textureName)
        {
            texture = GfxManager.GetTexture(textureName);
        }
        public GameObject()
        {

        }
        public GameObject(string textureName, DrawLayer layer = DrawLayer.Playground, int textOffsetX = 0, int textOffsetY = 0, int spriteWidth = 0, int spriteHeight = 0, bool lockedRatio = false, int numFrames = 1)
        {
            texture = GfxManager.GetTexture(textureName);
            id = Configs.GetGameObjectId();

            int spriteW;
            int spriteH;

            // remember that the spriteSheet needs to be horizontal only
            FrameWidth = texture.Width / numFrames;
            FrameHeight = texture.Height;

            if (lockedRatio)
            {
                if (spriteWidth > 0 && spriteHeight == 0)
                {
                    // if I give a new width and want to keep the texture ratio
                    spriteW = spriteWidth;
                    spriteH = spriteWidth * FrameHeight / FrameWidth;
                }
                else if (spriteHeight > 0 && spriteWidth == 0 || (spriteWidth > 0 && spriteHeight > 0))
                {
                    // if I give a new height and want to keep the texture ratio
                    // or if I give both new measure and want to keep the texture ratio
                    spriteH = spriteHeight;
                    spriteW = spriteHeight * FrameWidth / FrameHeight;
                }
                else
                {
                    // if I want to keep the texture ratio but do not give values
                    spriteW = FrameWidth;
                    spriteH = FrameHeight;
                }
            }
            else
            {
                // if I do not want to keep the texture ratio
                spriteW = spriteWidth > 0 ? spriteWidth : FrameWidth;
                spriteH = spriteHeight > 0 ? spriteHeight : FrameHeight;
            }
            sprite = new Sprite(spriteW, spriteH);

            cutWidth = FrameWidth;
            cutHeight = FrameHeight;

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
            Console.WriteLine($"Position collider: {collisionInfo.Collider.Position}");
            Console.WriteLine($"Position rigidBody: {collisionInfo.RigidBody.Position}");
            Console.WriteLine($"Delta: {collisionInfo.Delta}");
        }

        public virtual void Draw()
        {
            if(IsActive)
            {
                sprite.DrawTexture(texture,textureOffsetX,textureOffsetY,cutWidth,cutHeight);
            }
        }
    }
}
