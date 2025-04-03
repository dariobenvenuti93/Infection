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
        protected GameObject bg;
        public PlayScene(string textName, KeyCode exit = KeyCode.Return) 
        {
            exitKey = exit; 
        }
        public override void LoadAssets()
        {
            GfxManager.AddTexture("ball", "Assets/Graphics/grey_ball.png");
            GfxManager.AddTexture("lab", "Assets/Graphics/lab.jpg");
            //Fonts
            FontMgr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            FontMgr.AddFont("comics", "Assets/comics.png", 10, 32, 61, 65);
        }
        public override void Start()
        {
            LoadAssets();
            bg = new GameObject("lab", DrawLayer.Background, spriteW: Game.Window.Width, spriteH: Game.Window.Height);
            bg.IsActive = true;
            bg.Pivot = Vector2.Zero;
            DrawManager.AddItem(bg);
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
            bg = null;
            return base.OnExit();
        }
    }
}
