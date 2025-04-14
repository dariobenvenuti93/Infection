
using System;
using AIV_Engine;
using OpenTK;
using OpenTK.Graphics.ES11;

namespace Infection
{
    internal class Ball : GameObject
    {
        protected Vector2 direction;
        protected Animation animation;
        protected float energy;
        protected StateMachine fsm;
        protected RigidBody infectionRigidBody;

        public Animation Animation { get { return animation; } set { animation = value; } }
        public float DirectionX { get { return direction.X; } set { direction.X = value; } }
        public float DirectionY { get { return direction.Y; } set { direction.Y = value; } }
        public StateMachine Fsm { get { return fsm; } }
        public float Energy { get { return energy;} set { energy = value; } }
        public RigidBody InfectionRigidBody { get { return infectionRigidBody; } }
        
        public Ball(string textureName, DrawLayer layer = DrawLayer.Playground, int textOffsetX = 0, int textOffsetY = 0, int spriteW = 0, int spriteH  = 0, bool lockedRatio = false, bool sheet = false, int numFrames = 1) : base(textureName, layer, textOffsetX, textOffsetY, spriteW, spriteH, lockedRatio, numFrames)
        {
            maxSpeed = Configs.BallSpeed;
            energy = Configs.BallEnergy;

            fsm = new InfectionStateMachine(this);

            RigidBody = new RigidBody(this);
            RigidBody.Type = RigidBodyType.Ball;
            RigidBody.AddCollisionType(RigidBodyType.Ball);
            RigidBody.AddCollisionType(RigidBodyType.Wall);
            RigidBody.AddCollisionType(RigidBodyType.Infection);
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);

            infectionRigidBody = new RigidBody(this);
            infectionRigidBody.Type = RigidBodyType.Infection;
            infectionRigidBody.AddCollisionType(RigidBodyType.Ball);
            infectionRigidBody.Collider = new CircleCollider(infectionRigidBody, Configs.InfectionRadius);

            DebugManager.AddItem(RigidBody.Collider);
            DebugManager.AddItem(infectionRigidBody.Collider);
        }
        public override void Update()
        {
            fsm.Update();
            RigidBody.Velocity = direction * maxSpeed;
            base.Update();
            //rotation based on velocity
            if (IsActive && RigidBody.Velocity != Vector2.Zero)
            {
                Forward = RigidBody.Velocity;
            }
        }
        public override void Draw()
        {
            base.Draw();
            float rComponent = (Configs.BallEnergy - energy) * 0.01f;
            float gComponent = energy * 0.01f;
            sprite.SetMultiplyTint(new Vector4(rComponent, gComponent, 0.0f, 1.0f));
        }
        public override void OnCollide(Collision collisionInfo)
        {
            if (collisionInfo.RigidBody.Type == RigidBodyType.Ball) {
                //Console.WriteLine($"Ball OnCollide {id}");
                if (collisionInfo.Collider is InvisibleWall)
                {
                    base.OnCollide(collisionInfo);
                    if (collisionInfo.Delta.Y == 0.0f)
                    {
                        // left || right wall
                        direction.X *= -1.0f;
                        sprite.position.X += collisionInfo.Delta.X;
                    }
                    else if (collisionInfo.Delta.X == 0.0f)
                    {
                        // top || bot wall
                        direction.Y *= -1.0f;
                        sprite.position.Y += collisionInfo.Delta.Y;
                    }
                    Console.WriteLine($"New position: {sprite.position}");
                }
            }
        }
    }
}
