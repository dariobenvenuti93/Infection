using Aiv.Fast2D;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infection;

namespace AIV_Engine
{
    struct CameraLimits
    {
        public float MaxX;
        public float MaxY;
        public float MinX;
        public float MinY;

        public CameraLimits(float maxX, float minX, float maxY, float minY)
        {
            MaxX = maxX;
            MinX = minX;
            MaxY = maxY;
            MinY = minY;
        }
    }

    static class CameraMgr
    {
        private static Dictionary<string, Tuple<Camera, float>> cameras;

        public static Camera MainCamera { get; private set; }

        public static GameObject Target;
        public static float CameraSpeed = 5;
        public static Vector2 TargetOffset;
        public static CameraLimits CameraLimits;

        public static float HalfDiagonalSqr { get; private set; }

        public static void Init(GameObject target, CameraLimits cameraLimits)
        {
            MainCamera = new Camera(Game.ScreenCenterX, Game.ScreenCenterY);
            MainCamera.pivot = new Vector2(Game.ScreenCenterX, Game.ScreenCenterY);
            Target = target;
            TargetOffset = new Vector2(0, 0);
            CameraLimits = cameraLimits;

            HalfDiagonalSqr = (float)(Math.Pow(MainCamera.pivot.X, 2) + Math.Pow(MainCamera.pivot.Y, 2));

            cameras = new Dictionary<string, Tuple<Camera, float>>();
        }

        public static void Update()
        {
            if(Target == null)
                return;
            
            Vector2 oldCameraPos = MainCamera.position;
            Vector2 targetPosition = Target.Position + TargetOffset;
            //smooth lerp
            MainCamera.position = Vector2.Lerp(MainCamera.position, targetPosition, Game.Window.DeltaTime * CameraSpeed);

            FixPosition();

            Vector2 cameraDelta = MainCamera.position - oldCameraPos;

            if (cameraDelta == Vector2.Zero)
                return;

            //camera moved
            foreach (var item in cameras)
            {
                //camera.position += cameraDelta * cameraSpeed
                //Tuple<Camera, float> cameraTuple = item.Value;
                item.Value.Item1.position += cameraDelta * item.Value.Item2;
            }
        }

        private static void FixPosition()
        {
            MainCamera.position.X = MathHelper.Clamp(MainCamera.position.X, CameraLimits.MinX, CameraLimits.MaxX);
            MainCamera.position.Y = MathHelper.Clamp(MainCamera.position.Y, CameraLimits.MinY, CameraLimits.MaxY);
        }

        public static void AddCamera(string cameraName, Camera camera = null, float cameraSpeed = 0)
        {
            if (camera == null)
            {
                camera = new Camera(MainCamera.position.X, MainCamera.position.Y);
                camera.pivot = MainCamera.pivot;
            }

            cameras[cameraName] = new Tuple<Camera, float>(camera, cameraSpeed);
        }

        public static Camera GetCamera(string cameraName)
        {
            if (cameras.ContainsKey(cameraName))
            {
                return cameras[cameraName].Item1;
            }

            return null;
        }

    }
}
