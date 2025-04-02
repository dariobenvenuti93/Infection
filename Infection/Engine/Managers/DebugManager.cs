using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace AIV_Engine
{
    internal class DebugManager
    {
        static List<Collider> items;
        static List<Sprite> sprites;

        static Vector4 greenColor;
        static Vector4 redColor;
        static Texture circleTexture;
        static Texture circleTextureRed;

        static bool isActive;
        static bool isButtonPressed;

        static DebugManager()
        {
            items = new List<Collider>();
            sprites = new List<Sprite>();

            greenColor = new Vector4(0f, 1f, 0f, 1f);
            redColor = new Vector4(1f, 0f, 0f, 1f);
            circleTexture = new Texture("Assets/Graphics/circle.png");
            circleTextureRed = new Texture("Assets/Graphics/circle-red.png");

            isActive = true;
        }

        public static void AddItem(Collider c)
        {
            items.Add(c);

            if (c is BoxCollider)
            {
                BoxCollider b = (BoxCollider)c;
                Sprite s = new Sprite(b.Width, b.Height);
                s.pivot = new Vector2(s.Width * 0.5f, s.Height * 0.5f);
                sprites.Add(s);
            }
            else
            {
                float size = ((CircleCollider)c).Radius * 2f;
                Sprite s = new Sprite(size, size);
                s.pivot = new Vector2(size * 0.5f);
                sprites.Add(s);
            }
            Console.WriteLine($"Added new collider, new count: {items.Count()}");
        }

        public static void RemoveItem(Collider c)
        {
            int colliderIndex = items.IndexOf(c);
            Console.WriteLine($"Removing collider at index {colliderIndex}");
            sprites.RemoveAt(colliderIndex);
            items.RemoveAt(colliderIndex);
            Console.WriteLine($"New colliders count: {items.Count()}");
        }

        public static void ClearAll()
        {
            items.Clear();
            sprites.Clear();
        }

        public static void Draw()
        {
            if (Game.Window.GetKey(KeyCode.F1))
            {
                if (!isButtonPressed)
                {
                    isButtonPressed = true;
                    isActive = !isActive;
                }
            }
            else
            {
                isButtonPressed = false;
            }

            if (!isActive)
                return;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].RigidBody.IsActive)
                {
                    sprites[i].position = items[i].Position;//update sprite position

                    if (items[i] is BoxCollider)
                    {
                        if (items[i].RigidBody.IsCollidingWith.Count > 0)
                        {
                            sprites[i].DrawWireframe(redColor); 
                        }
                        else
                        {
                            sprites[i].DrawWireframe(greenColor);
                        }
                    }
                    else
                    {
                        if (items[i].RigidBody.IsCollidingWith.Count > 0)
                        {
                            sprites[i].DrawTexture(circleTextureRed);
                        }
                        else
                        {
                            sprites[i].DrawTexture(circleTexture);
                        }
                    }
                }
            }
        }
    }
}
