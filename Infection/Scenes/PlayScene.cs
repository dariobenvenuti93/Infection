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
        protected List<Ball> balls;
        protected List<InvisibleWall> boundaries;
        protected int numBalls;
        public PlayScene(string textName, KeyCode exit = KeyCode.Return) 
        {
            exitKey = exit; 
            balls = new List<Ball>();
            boundaries = new List<InvisibleWall>();
            numBalls = Configs.NumBalls;
        }
        public override void LoadAssets()
        {
            GfxManager.AddTexture("ball", "Assets/Graphics/grey_ball.png");
        }
        public override void Start()
        {
            LoadAssets();
            for (int i = 0; i < numBalls; i++)
                balls.Add(new Ball("ball", DrawLayer.Playground, spriteW: Configs.BallSize, spriteH: Configs.BallSize)); 
            // top
            boundaries.Add(new InvisibleWall(new Vector2(Game.Window.Width * 0.5f, Configs.TopPadding + Configs.BoxThickness * 0.5f), new Vector2(Game.Window.Width, Configs.BoxThickness)));
            // bot
            boundaries.Add(new InvisibleWall(new Vector2(Game.Window.Width * 0.5f, Game.Window.Height - Configs.BoxThickness * 0.5f), new Vector2(Game.Window.Width, Configs.BoxThickness)));
            // left
            boundaries.Add(new InvisibleWall(new Vector2(Configs.BoxThickness * 0.5f, Configs.TopPadding +  Game.Window.Height * 0.5f), new Vector2(Configs.BoxThickness, Game.Window.Height - Configs.TopPadding + Configs.BoxThickness)));
            // right
            boundaries.Add(new InvisibleWall(new Vector2(Game.Window.Width - Configs.BoxThickness * 0.5f, Configs.TopPadding + Game.Window.Height * 0.5f), new Vector2(Configs.BoxThickness, Game.Window.Height - Configs.TopPadding + Configs.BoxThickness)));
            base.Start();
        }
        public override Scene OnExit()
        {
            balls.Clear();
            balls = null;
            return base.OnExit();
        }
    }
}
