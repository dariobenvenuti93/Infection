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
        public static float InfectionRate;
        public static float RecoveryRate;
        public static float RecoveryTime;

        private static int currentGameObjectId;

        public static int BallSize;
        public static int NumBalls;
        public static int NumInfectedBalls;
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
            BallSpeed = 100.0f;
            BallEnergy = 100;
            NumBalls = 30;
            NumInfectedBalls = 1;
            Debug = true;
            WindowWidth = 1250;
            WindowHeight = 600;
            BoxThickness = 12.5f;
            BallSize = (int)(WindowWidth * 0.02f);
            InfectionRadius = BallSize * 3.5f;
            InitialGameObjectId = 0;
            currentGameObjectId = InitialGameObjectId;
            TopPadding = 30;
            RecoveryRate = 15.0f;  // energy each second
            InfectionRate = 30.0f; // energy each second
            RecoveryTime = 4000.0f; // milliseconds
        }
        public static int GetGameObjectId()
        {
            return currentGameObjectId++;
        }
    }
}
