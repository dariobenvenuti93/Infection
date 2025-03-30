
using AIV_Engine;
using OpenTK;

namespace Infection
{
    internal class Ball : GameObject
    {
        protected Vector2 direction;
        public Ball(string textureName, DrawLayer layer = DrawLayer.Playground, int textOffsetX = 0, int textOffsetY = 0, int spriteW = 0, int spriteH  = 0) : base(textureName, layer, textOffsetX, textOffsetY, spriteW, spriteH)
        {
            maxSpeed = Configs.ballSpeed;

            sprite.position = new Vector2(RandomGenerator.GetRandomFloat() * Game.Window.OrthoWidth, RandomGenerator.GetRandomFloat() * Game.Window.OrthoHeight);
            sprite.SetMultiplyTint(0, 1.0f, 0.0f, 1.0f);

            RigidBody = new RigidBody(this);
            RigidBody.Type = RigidBodyType.Ball;
            RigidBody.AddCollisionType(RigidBodyType.Ball);
            RigidBody.AddCollisionType(RigidBodyType.Wall);
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);
            DebugManager.AddItem(RigidBody.Collider);

            direction.X = RandomGenerator.GetRandomFloat();
            direction.Y = 1 - direction.X;
            if (RandomGenerator.GetRandomBool())
                direction.X *= -1;
            if (RandomGenerator.GetRandomBool())
                direction.Y *= -1;
            RigidBody.Velocity = direction * maxSpeed;

            IsActive = true;

            DrawManager.AddItem(this);
            UpdateManager.AddItem(this);
        }
        public override void Update()
        {
            base.Update();
            //rotation based on velocity
            if (IsActive && RigidBody.Velocity != Vector2.Zero)
            {
                Forward = RigidBody.Velocity;
            }
            sprite.Rotation += 1.0f;
        }
        public override void OnCollide(Collision collisionInfo)
        {
            if (collisionInfo.Collider is InvisibleWall)
            {

            }
        }
    }
}
