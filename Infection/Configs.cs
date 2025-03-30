using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infection
{
    internal static class Configs
    {
        public static float ballSpeed;
        public static int numBalls;
        public static bool debug;
        public static void SetDebug(bool value)
        {
            debug = value;
        }
        static Configs()
        {
            ballSpeed = 1.0f;
            numBalls = 70;
            debug = true;
        }
    }
}
