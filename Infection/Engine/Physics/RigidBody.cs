using OpenTK;
using System;
using System.Collections.Generic;

namespace AIV_Engine
{
    enum RigidBodyType { Ball = 1, Wall = 2, Infection = 4}

    internal class RigidBody
    {
        protected uint collisionMask;

        public GameObject GameObject;
        public Collider Collider;

        public bool IsGravityAffected;
        public bool IsCollisionAffected;
        public List<RigidBody> IsCollidingWith;
        public float Friction;

        public Vector2 Velocity;
        public Vector2 Position { get { return GameObject.Position; } }

        public bool IsActive { get { return GameObject.IsActive; } }

        public RigidBodyType Type;

        public RigidBody(GameObject owner)
        {
            GameObject = owner;
            PhysicsManager.AddItem(this);
            IsCollisionAffected = true;
            IsCollidingWith = new List<RigidBody>();
        }

        public void Update()
        {
            if (IsGravityAffected)
            {
                Velocity.Y += PhysicsManager.G * Game.DeltaTime;
            }

            ApplyFriction();

            GameObject.Position += Velocity * Game.DeltaTime;
        }

        protected void ApplyFriction()
        {
            if (Friction == 0 || Velocity == Vector2.Zero)
                return;

            float fAmount = Friction * Game.DeltaTime;//velocity to subtract
            float newVelocityLength = Velocity.Length - fAmount;

            if(newVelocityLength < 0)
            {
                Velocity = Vector2.Zero;
                return;
            }

            Velocity = Velocity.Normalized() * newVelocityLength;//assign new velocity
        }

        public bool Collides(RigidBody other, ref Collision collisionInfo)
        {
            return Collider.Collides(other.Collider, ref collisionInfo);
        }

        public void AddCollisionType(RigidBodyType type)
        {
            collisionMask |= (uint)type;//collisionMask = collisionMask | (uint)type
        }

        public void AddCollisionType(uint type)
        {
            collisionMask |= type;//collisionMask = collisionMask | type
        }

        public bool CollisionTypeMatches(RigidBodyType type)
        {
            return ((uint)type & collisionMask) != 0;
        }
    }
}
