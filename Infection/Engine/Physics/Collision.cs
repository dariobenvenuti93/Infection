using OpenTK;
using System;
using System.Collections.Generic;

namespace AIV_Engine
{
    enum CollisionType
    {
        None, RectsIntersection, CirclesIntersection, CircleRectIntersection
    }

    struct Collision
    {
        public Vector2 Delta;
        public GameObject Collider;
        public RigidBody RigidBody;
        public CollisionType Type;
    }
}
