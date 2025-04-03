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
            Walls = new List<InvisibleWall>();
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

            Walls.Add(new InvisibleWall(horizontalPosTop, horizontalSize));
            Walls.Add(new InvisibleWall(horizontalPosBot, horizontalSize));
            // left
            Walls.Add(new InvisibleWall(verticalPosLeft, verticalSize));
            // right
            Walls.Add(new InvisibleWall(verticalPosRight, verticalSize));
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
                DebugManager.AddItem(Walls[i].RigidBody.Collider);
            }
        }
        public static void DespawnWalls()
        {
            for (int i = 0; i < Walls.Count; i++)
            {
                Walls[i].IsActive = false;
                DebugManager.RemoveItem(Walls[i].RigidBody.Collider);
            }
        }
    }
}
