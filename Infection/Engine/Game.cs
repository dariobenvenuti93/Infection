using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using Infection;

namespace AIV_Engine
{
    static class Game
    {
        // Variables
        public static Window Window;

        private static KeyboardController keyboardCtrl;
        private static List<Controller> controllers;

        public static float HalfDiagonalSquared;
        public static Vector2 ScreenCenter;


        // Properties
        public static float DeltaTime { get { return Window.DeltaTime; } }
        public static float ScreenCenterX { get { return Window.OrthoWidth * 0.5f; } }
        public static float ScreenCenterY { get { return Window.OrthoHeight * 0.5f; } }

        public static Scene CurrentScene { get; private set; }

        public static float UnitSize { get; private set; }
        public static float OptimalScreenHeight { get; private set; } = 1080;//Full HD

        public static float OptimalUnitSize { get; private set; }

        public static void Init()
        {
            Window = new Window(1250, 680, "Infection");
            Window.Position = Vector2.Zero;
            Window.SetDefaultViewportOrthographicSize(10);

            HalfDiagonalSquared = (float)(Math.Pow(Window.OrthoWidth, 2) + Math.Pow(Window.OrthoHeight, 2)) * 0.5f;
            ScreenCenter = new Vector2(ScreenCenterX, ScreenCenterY);

            UnitSize = Window.Height / Window.OrthoHeight;//72 (1280X720 HD)
            OptimalUnitSize = OptimalScreenHeight / Window.OrthoHeight;//108 (1080/10)

            TitleScene titleScene = new TitleScene("titleScreen");
            PlayScene playScene = new PlayScene("playScreen");
            titleScene.NextScene = playScene;

            CurrentScene = titleScene;

            // Controllers
            // Always create a keyboard controller (init at 0 cause we only have 1 keyboard)
            List<KeyCode> keys = new List<KeyCode>();
            keys.Add(KeyCode.W);
            keys.Add(KeyCode.S);
            keys.Add(KeyCode.D);
            keys.Add(KeyCode.A);
            keys.Add(KeyCode.Space);

            KeysList keysList = new KeysList(keys);

            keyboardCtrl = new KeyboardController(0, keysList);
            controllers = new List<Controller>();

            string[] joysticks = Window.Joysticks;

            for (int i = 0; i < joysticks.Length; i++)
            {
                Console.WriteLine(Window.JoystickDebug(i));
                Console.WriteLine(joysticks[i]);

                if (joysticks[i] != null && joysticks[i] != "Unmapped Controller")
                {
                    controllers.Add(new JoypadController(i));
                }
            }
        }

        public static Controller GetController(int index)
        {
            Controller ctrl = keyboardCtrl;

            if(index < controllers.Count)
            {
                ctrl = controllers[index];
            }

            return ctrl;
        }

        public static float PixelsToUnits(float pixelsSize)
        {
            return pixelsSize / OptimalUnitSize;//108 if OptimalScreenHeight is 1080 and ortoSize is 10
        }

        public static void Play()
        {
            CurrentScene.Start();

            while(Window.IsOpened)
            {
                Window.SetTitle($"FPS: {1.0f / Window.DeltaTime}");

                // INPUT
                if(Window.GetKey(KeyCode.Esc))
                {
                    return;
                }

                if(!CurrentScene.IsPlaying)
                {
                    Scene nextScene = CurrentScene.OnExit();

                    if (nextScene != null)
                    {
                        CurrentScene = nextScene;
                        CurrentScene.Start();
                    }
                    else
                    {
                        return;
                    }
                }

                CurrentScene.Input();

                // UPDATE
                CurrentScene.Update();

                // DRAW
                CurrentScene.Draw();

                Window.Update();
            }
        }
    }
}
