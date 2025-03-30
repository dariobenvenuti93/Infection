using System;
using System.Collections.Generic;
using OpenTK;

namespace AIV_Engine
{
    abstract class Collider
    {
        public RigidBody RigidBody;
        public Vector2 Offset;

        public Vector2 Position { get { return RigidBody.Position + Offset; } }

        public Collider(RigidBody owner)
        {
            RigidBody = owner;
            Offset = Vector2.Zero;
        }
        public abstract bool Collides(Collider collider, ref Collision collisioninfo);

        public abstract bool Collides(CircleCollider collider, ref Collision collisioninfo);

        public abstract bool Collides(BoxCollider collider, ref Collision collisioninfo);
        public abstract bool Collides(CompoundCollider collider, ref Collision collisioninfo);
    }
}
