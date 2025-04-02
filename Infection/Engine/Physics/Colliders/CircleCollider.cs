using OpenTK;
using System;
using System.Collections.Generic;

namespace AIV_Engine
{
    internal class CircleCollider : Collider
    {
        public float Radius;

        public CircleCollider(RigidBody owner, float radius) : base(owner)
        {
            Radius = radius;
        }

        public override bool Collides(Collider collider, ref Collision collisioninfo)
        {
            return collider.Collides(this, ref collisioninfo);
        }

        public override bool Collides(CircleCollider collider, ref Collision collisioninfo)
        {
            Vector2 distance = collider.Position - Position;
            collisioninfo.Delta = -distance;
            return distance.LengthSquared <= Math.Pow(collider.Radius + Radius, 2);
        }

        public override bool Collides(BoxCollider collider, ref Collision collisioninfo)
        {
            return collider.Collides(this, ref collisioninfo);
        }

        public override bool Collides(CompoundCollider collider, ref Collision collisioninfo)
        {
            return collider.Collides(this, ref collisioninfo);
        }
    }
}
