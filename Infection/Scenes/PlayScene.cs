using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using AIV_Engine;
using Infection.Scenes;
using OpenTK;

namespace Infection
{
    internal class PlayScene : Scene
    {
        protected KeyCode exitKey;
        protected Background bg;
        protected TextObject ballsText;
        public PlayScene(string textName, KeyCode exit = KeyCode.Return) 
        {
            exitKey = exit; 
        }
        public override void LoadAssets()
        {
            //GfxManager.AddTexture("ball", "Assets/Graphics/grey_ball.png");
            GfxManager.AddTexture("virusIdle1", "Assets/Graphics/coronavirus-classic-idle1-sheet.png");
            GfxManager.AddTexture("virusIdle2", "Assets/Graphics/coronavirus-classic-idle2-sheet.png");
            GfxManager.AddTexture("virusHit", "Assets/Graphics/coronavirus-classic-hit-sheet.png");
            GfxManager.AddTexture("virusAttack", "Assets/Graphics/coronavirus-classic-attack-sheet.png");
            GfxManager.AddTexture("lab", "Assets/Graphics/lab.jpg");
            //Fonts
            FontMgr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            FontMgr.AddFont("comics", "Assets/comics.png", 10, 32, 61, 65);
        }
        public override void Start()
        {
            LoadAssets();

            base.Start();

            bg = new Background("lab", DrawLayer.Background);

            ballsText = new TextObject(new Vector2(Configs.BoxThickness, Configs.TopPadding * 0.25f), $"Infected: {BallManager.InfectedBallCount} / {BallManager.BallCount + BallManager.InfectedBallCount}");
            ballsText.IsActive = true;

            InvisibleWallsManager.SpawnWalls();
            BallManager.SpawnBalls();

        }
        public override void Update()
        {
            base.Update();
            ballsText.Text = $"Infected: {BallManager.InfectedBallCount} / {BallManager.BallCount + BallManager.InfectedBallCount}";
            if ( BallManager.InfectedBallCount == Configs.NumBalls )
            {
                Scene oldNextScene = NextScene;
                NextScene = new GameOverScene("GameOverScreen", KeyCode.Return);
                NextScene.NextScene = oldNextScene;
                IsPlaying = false;
            }
            else if ( BallManager.InfectedBallCount == 0 )
            {
                Scene oldNextScene = NextScene;
                NextScene = new VictoryScene("VictoryScreen", KeyCode.Return);
                NextScene.NextScene = oldNextScene; 
                IsPlaying = false;
            }
        }
        public override void Input()
        {
            base.Input();
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
            
            if (!(NextScene is GameOverScene) || !(NextScene is VictoryScene))
                BallManager.DespawnBalls();
            InvisibleWallsManager.DespawnWalls();
            bg = null;
            return base.OnExit();
        }
    }
}
