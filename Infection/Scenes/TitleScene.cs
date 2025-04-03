
using Aiv.Fast2D;
using AIV_Engine;
using OpenTK;

namespace Infection
{
    internal class TitleScene : Scene
    {
        protected GameObject bg;
        protected string textureName;
        protected KeyCode exitKey;
        protected KeyCode playKey;
        protected TextObject playText;
        protected TextObject exitText;

        public TitleScene(string textName, KeyCode play = KeyCode.Space, KeyCode exit = KeyCode.Return)
        {
            exitKey = exit;
            playKey = play;
            textureName = textName;
        }

        public override void Start()
        {
            LoadAssets();
            bg = new GameObject("lab", DrawLayer.Background, spriteW: Game.Window.Width, spriteH: Game.Window.Height);
            bg.IsActive = true; 
            bg.Pivot = Vector2.Zero;
            DrawManager.AddItem(bg);
            playText = new TextObject(new Vector2(Game.Window.Width * 0.35f, Game.Window.Height * 0.2f), $"Press {playKey} to start!");
            playText.IsActive = true;
            exitText = new TextObject(new Vector2(Game.Window.Width * 0.35f, Game.Window.Height * 0.3f), $"Press {exitKey} to exit!");
            exitText.IsActive = true;
            base.Start();
        }

        public override void LoadAssets()
        {
            //GfxManager.AddTexture("aivBg", "Assets/Graphics/aivBG.png");
            GfxManager.AddTexture("lab", "Assets/Graphics/lab.jpg");
            //Fonts
            FontMgr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            FontMgr.AddFont("comics", "Assets/comics.png", 10, 32, 61, 65);
        }

        public override void Input()
        {
             if (Game.Window.GetKey(exitKey))
            {
                if (!NextScene.IsExitKeyPressed)
                {
                    NextScene = null;
                    IsPlaying = false;
                }
            }
            else
            {
                NextScene.IsExitKeyPressed = false;
            }
            if (Game.Window.GetKey(playKey))
            {
                IsPlaying = false;
            }
        }
        public override Scene OnExit()
        {
            bg  = null;
            playText = null;
            return base.OnExit();
        }
    }
}
