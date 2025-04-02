using OpenTK;
using System;
using System.Collections.Generic;

namespace AIV_Engine
{
    internal class BoxCollider : Collider
    {
        protected float halfWidth;
        protected float halfHeight;

        public float Width { get { return halfWidth * 2f; } }
        public float Height { get { return halfHeight * 2f; } }

        public BoxCollider(RigidBody owner, float halfW, float halfH) : base(owner)
        {
            halfWidth = halfW;
            halfHeight = halfH;
        }

        public override bool Collides(Collider other, ref Collision collisioninfo)
        {
            return other.Collides(this, ref collisioninfo);
        }

        public override bool Collides(CircleCollider collider, ref Collision collisioninfo)
        {
            float deltaX = collider.Position.X - Math.Max(Position.X - halfWidth, Math.Min(collider.Position.X, Position.X + halfWidth));
            float deltaY = collider.Position.Y - Math.Max(Position.Y - halfHeight, Math.Min(collider.Position.Y, Position.Y + halfHeight));

            collisioninfo.Delta = new Vector2(-deltaX, -deltaY);

            return (deltaX * deltaX + deltaY * deltaY) <= (collider.Radius * collider.Radius);
        }

        public override bool Collides(BoxCollider other, ref Collision collisioninfo)
        {
            Vector2 distance = other.Position - Position;

            float deltaX = Math.Abs(distance.X) - (other.halfWidth + halfWidth);

            if(deltaX > 0)
            {
                return false;
            }

            float deltaY = Math.Abs(distance.Y) - (other.halfHeight + halfHeight);

            if (deltaY > 0)
            {
                return false;
            }

            collisioninfo.Type = CollisionType.RectsIntersection;
            collisioninfo.Delta = new Vector2(-deltaX, -deltaY);

            return true;
        }

        public override bool Collides(CompoundCollider collider, ref Collision collisioninfo)
        {
            return collider.Collides(this, ref collisioninfo);
        }
    }
}
