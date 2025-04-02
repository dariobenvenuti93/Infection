using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Infection
{
    internal static class Configs
    {
        public static float BallSpeed;
        public static float InfectionRadius;
        public static float BoxThickness;
        public static float BallEnergy;

        private static int currentGameObjectId;

        public static int BallSize;
        public static int NumBalls;
        public static int WindowWidth;
        public static int WindowHeight;
        public static int InitialGameObjectId;
        public static int TopPadding;

        public static bool Debug;

        public static void SetDebug(bool value)
        {
            Debug = value;
        }
        static Configs()
        {
            BallSpeed = 150.0f;
            BallEnergy = 100;
            NumBalls = 30;
            Debug = true;
            WindowWidth = 1250;
            WindowHeight = 600;
            BoxThickness = 22.5f;
            BallSize = (int)(WindowWidth * 0.02f);
            InfectionRadius = BallSize * 2.0f;
            InitialGameObjectId = 0;
            currentGameObjectId = InitialGameObjectId;
            TopPadding = 50;
        }
        public static int GetGameObjectId()
        {
            return currentGameObjectId++;
        }
    }
}
