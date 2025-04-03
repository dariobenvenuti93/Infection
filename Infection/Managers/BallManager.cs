using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using AIV_Engine;
using OpenTK;

namespace Infection
{
    static class BallManager
    {
        private static List<Ball> Balls;
        private static List<Ball> InfectedBalls;
        public static int BallCount { get { return Balls.Count; } }
        public static int InfectedBallCount { get { return InfectedBalls.Count; } }
        static BallManager()
        {
            Balls = new List<Ball>(Configs.NumBalls);
            InfectedBalls = new List<Ball>(Configs.NumInfectedBalls);
            CreateBalls();
        }
        public static void CreateBalls()
        {
            for (int i = 0; i < Configs.NumBalls; i++)
                Balls.Add(new Ball("ball", DrawLayer.Playground, spriteW: Configs.BallSize, spriteH: Configs.BallSize));
        }
        public static void DeleteBalls()
        {
            Balls.Clear();
            InfectedBalls.Clear();
        }
        public static void Infect(Ball b)
        {
            InfectedBalls.Add(b);
            Balls.Remove(b);
        }
        public static void Recover(Ball b)
        {
            InfectedBalls.Remove(b);
            Balls.Add(b);
        }
        static public void SpawnBalls()
        {
            float boxThickness = Configs.BoxThickness * 1.1f + Configs.BallSize * 1.0f;
            float maxPosX = Game.Window.Width - boxThickness;
            float minPosX = boxThickness;
            float maxPosY = Game.Window.Height - boxThickness;
            float minPosY = Configs.TopPadding + boxThickness;

            float posX;
            float posY;

            for (int i = 0; i < BallCount; i++)
            {
                posX = RandomGenerator.GetRandomFloat() * maxPosX;
                posY = RandomGenerator.GetRandomFloat() * maxPosY;
                if (posX < minPosX)
                    posX = minPosX;
                if (posY < minPosY)
                    posY = minPosY;
                Balls[i].Sprite.position = new Vector2(posX, posY);

                Balls[i].IsActive = true;
                DrawManager.AddItem(Balls[i]);
                UpdateManager.AddItem(Balls[i]);

                Balls[i].DirectionX = RandomGenerator.GetRandomFloat();
                Balls[i].DirectionY = 1 - Balls[i].DirectionX;
                if (RandomGenerator.GetRandomBool())
                    Balls[i].DirectionX *= -1;
                if (RandomGenerator.GetRandomBool())
                    Balls[i].DirectionY *= -1;
            }
            for (int i = 0; i < Configs.NumInfectedBalls; i++)
            {
                Balls[i].Fsm.GoTo(FSMStates.Infected);
            }
        }
        public static void ResetBall(Ball ball)
        {
            ball.Energy = Configs.BallEnergy;
            ball.Fsm.GoTo(FSMStates.Healthy);
        }
        public static void DespawnBalls()
        {
            for (int i = 0; i < InfectedBallCount; i++)
            {
                Balls.Add(InfectedBalls[i]);
            }
            InfectedBalls.Clear();
            for (int i = 0; i < BallCount; i++)
            {
                ResetBall(Balls[i]);
                Balls[i].IsActive = false;
                DrawManager.RemoveItem(Balls[i]);
                UpdateManager.RemoveItem(Balls[i]);
            }
        }
    }
}
