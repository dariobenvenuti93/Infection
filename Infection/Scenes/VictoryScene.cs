using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using AIV_Engine;
using OpenTK;

namespace Infection.Scenes
{
    internal class VictoryScene : Scene
    {
        protected KeyCode exitKey;
        protected Background bg;
        protected TextObject text1;
        protected TextObject text2;
        public VictoryScene(string textName, KeyCode exit = KeyCode.Return)
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
            GfxManager.AddTexture("greyLab", "Assets/Graphics/lab-greyscale.jpg");
            //GfxManager.AddTexture("lab", "Assets/Graphics/lab.jpg");
            //Fonts
            FontMgr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            FontMgr.AddFont("comics", "Assets/comics.png", 10, 32, 61, 65);
        }
        public override void Start()
        {
            LoadAssets();

            bg = new Background("greyLab", DrawLayer.Background);
            bg.SetColor(new Vector4(0.0f, 1.0f, 0.0f, 1.0f));

            text1 = new TextObject(new Vector2(Game.Window.Width * 0.3f, Game.Window.Height * 0.3f), $"The virus has been contained!");
            text1.IsActive = true;

            text2 = new TextObject(new Vector2(Game.Window.Width * 0.3f, Game.Window.Height * 0.4f), $"Press {exitKey} to exit!");
            text2.IsActive = true;

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
            bg = null;
            text1 = null;
            text2 = null;
            return base.OnExit();
        }
    }
}
