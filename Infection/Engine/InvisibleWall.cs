using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using Infection;
using OpenTK;

namespace AIV_Engine
{
    internal class InvisibleWall : GameObject
    {
        public InvisibleWall(Vector2 position, Vector2 size)
        {
            id = Configs.GetGameObjectId();
            float spriteW = size.X;
            float spriteH = size.Y;
            sprite = new Sprite(spriteW, spriteH);
            sprite.position = position;
            cutWidth = (int)spriteW;
            cutHeight = (int)spriteH;
            HalfWidth = cutWidth * 0.5f;
            HalfHeight = cutHeight * 0.5f;

            RigidBody  = new RigidBody(this);
            RigidBody.Type = RigidBodyType.Wall;
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
        }
    }
}
