using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIV_Engine;

using OpenTK;

namespace Infection
{
    static class InvisibleWallsManager
    {
        public static List<InvisibleWall> Walls;
        static InvisibleWallsManager()
        {
            Walls = new List<InvisibleWall>(4);
            CreateWalls();
        }
        public static void CreateWalls()
        {
            Vector2 horizontalSize = new Vector2(Game.Window.Width, Configs.BoxThickness);
            Vector2 verticalSize = new Vector2(Configs.BoxThickness, Game.Window.Height - Configs.TopPadding + Configs.BoxThickness);

            Vector2 horizontalPosTop = new Vector2(Game.Window.Width * 0.5f, Configs.TopPadding + Configs.BoxThickness * 0.5f);
            Vector2 horizontalPosBot = new Vector2(Game.Window.Width * 0.5f, Game.Window.Height - Configs.BoxThickness * 0.5f);

            Vector2 verticalPosLeft = new Vector2(Configs.BoxThickness * 0.5f, Configs.TopPadding + Game.Window.Height * 0.5f);
            Vector2 verticalPosRight = new Vector2(Game.Window.Width - Configs.BoxThickness * 0.5f, Configs.TopPadding + Game.Window.Height * 0.5f);

            InvisibleWall top = new InvisibleWall(horizontalPosTop, horizontalSize);
            Walls.Add(top);
            DebugManager.AddItem(top.RigidBody.Collider);

            InvisibleWall bot = new InvisibleWall(horizontalPosBot, horizontalSize);
            Walls.Add(bot);
            DebugManager.AddItem(bot.RigidBody.Collider);

            InvisibleWall left = new InvisibleWall(verticalPosLeft, verticalSize);
            Walls.Add(left);
            DebugManager.AddItem(left.RigidBody.Collider);

            InvisibleWall right = new InvisibleWall(verticalPosRight, verticalSize);
            Walls.Add(right);
            DebugManager.AddItem(right.RigidBody.Collider);
        }
        public static void DeleteWalls()
        {
            Walls.Clear();
        }
        public static void SpawnWalls()
        {
            for (int i = 0; i < Walls.Count; i++)
            {
                Walls[i].IsActive = true;
            }
        }
        public static void DespawnWalls()
        {
            for (int i = 0; i < Walls.Count; i++)
            {
                Walls[i].IsActive = false;
            }
        }
    }
}
