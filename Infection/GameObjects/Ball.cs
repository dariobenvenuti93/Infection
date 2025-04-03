
using System;
using AIV_Engine;
using OpenTK;
using OpenTK.Graphics.ES11;

namespace Infection
{
    internal class Ball : GameObject
    {
        protected Vector2 direction;
        public float DirectionX { get { return direction.X; } set { direction.X = value; } }
        public float DirectionY { get { return direction.Y; } set { direction.Y = value; } }
        protected float energy;
        protected StateMachine fsm;
        public StateMachine Fsm { get { return fsm; } }
        public float Energy { get { return energy;} set { energy = value; } }

        protected RigidBody infectionRigidBody;
        public RigidBody InfectionRigidBody { get { return infectionRigidBody; } }
        public Ball(string textureName, DrawLayer layer = DrawLayer.Playground, int textOffsetX = 0, int textOffsetY = 0, int spriteW = 0, int spriteH  = 0) : base(textureName, layer, textOffsetX, textOffsetY, spriteW, spriteH)
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
            if (collisionInfo.RigidBody.Type == RigidBodyType.Infection)
            {
                //Console.WriteLine($"Infection OnCollide {id}");
            }
            else if (collisionInfo.RigidBody.Type == RigidBodyType.Ball) {
                //Console.WriteLine($"Ball OnCollide {id}");
                if (collisionInfo.Collider is InvisibleWall)
                {
                    if (sprite.position.X - HalfWidth < Configs.BoxThickness || sprite.position.X + HalfWidth > Game.Window.Width - Configs.BoxThickness)
                    {
                        // left || right wall
                        direction.X *= -1.0f;
                    }
                    if (sprite.position.Y - HalfHeight < Configs.BoxThickness + Configs.TopPadding || sprite.position.Y + HalfHeight > Game.Window.Height - Configs.BoxThickness)
                    {
                        // top || bot wall
                        direction.Y *= -1.0f;
                    }
                    base.OnCollide(collisionInfo);
                }
            }
        } 
    }
}
