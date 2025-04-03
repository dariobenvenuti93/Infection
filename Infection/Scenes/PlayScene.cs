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
    internal class PlayScene : Scene
    {
        protected KeyCode exitKey;
        protected List<InvisibleWall> boundaries;
        public PlayScene(string textName, KeyCode exit = KeyCode.Return) 
        {
            exitKey = exit; 
            boundaries = new List<InvisibleWall>();
        }
        public override void LoadAssets()
        {
            GfxManager.AddTexture("ball", "Assets/Graphics/grey_ball.png");
        }
        public override void Start()
        {
            LoadAssets();
            BallManager.SpawnBalls();
            InvisibleWallsManager.SpawnWalls();
            base.Start();
        }
        public override void Input()
        {
            if (Game.Window.GetKey(exitKey))
            {
                if (!IsExitKeyPressed)
                {
                    
                    IsExitKeyPressed = true;
                    IsPlaying = false;
                }
            }
            else
            {
                IsExitKeyPressed = false;
            }
        }
        public override Scene OnExit()
        {
            BallManager.DespawnBalls();
            InvisibleWallsManager.DespawnWalls();
            return base.OnExit();
        }
    }
}
