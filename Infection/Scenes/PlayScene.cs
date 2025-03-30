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
            numBalls = Configs.numBalls;
        }
        public override void LoadAssets()
        {
            GfxManager.AddTexture("ball", "Assets/Graphics/grey_ball.png");
        }
        public override void Start()
        {
            LoadAssets();
            for (int i = 0; i < numBalls; i++)
                balls.Add(new Ball("ball", DrawLayer.Playground));
            boundaries.Add(new InvisibleWall(Vector2.Zero, new Vector2(Game.Window.Width, 1.0f)));
            boundaries.Add(new InvisibleWall(new Vector2(0.0f, Game.Window.OrthoHeight), new Vector2(Game.Window.Width, 1.0f)));
            boundaries.Add(new InvisibleWall(Vector2.Zero, new Vector2(1.0f, Game.Window.Height)));
            boundaries.Add(new InvisibleWall(new Vector2(Game.Window.OrthoWidth, 0.0f), new Vector2(1.0f, Game.Window.Height)));
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
