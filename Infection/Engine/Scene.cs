using Aiv.Fast2D;
using Infection;
using OpenTK;

namespace AIV_Engine
{
    abstract class Scene
    {
        public bool IsPlaying { get; protected set; }
        protected bool isF1Pressed;
        public Scene NextScene;

        public virtual void Start()
        {
            IsPlaying = true;
            isF1Pressed = false;
        }

        public virtual void LoadAssets()
        {

        }

        public virtual void Input()
        {
            if (Game.Window.GetKey(KeyCode.F1))
            {
                if (!isF1Pressed)
                {
                    isF1Pressed = true;
                    Configs.SetDebug(!Configs.debug);
                }
                else
                {
                    isF1Pressed = false;
                }
            }
        }

        public virtual void Update()
        {
            UpdateManager.Update();
            PhysicsManager.CheckCollisions();
        }

        public virtual void Draw()
        {
            DrawManager.Draw();
            if (Configs.debug) 
            { 
                DebugManager.Draw();
            }
        }

        public virtual Scene OnExit()
        {
            IsPlaying = false;
            DrawManager.ClearAll();
            UpdateManager.ClearAll();
            return NextScene;
        }
    }
}
