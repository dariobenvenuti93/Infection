using System;
using System.Collections.Generic;

namespace AIV_Engine
{
    static class PhysicsManager
    {
        private static List<RigidBody> items;

        public static float G = 9f;
        public static Collision CollisionInfo;


        static PhysicsManager()
        {
            items = new List<RigidBody>();
        }

        public static void AddItem(RigidBody item)
        {
            items.Add(item);
        }

        public static void RemoveItem(RigidBody item)
        {
            items.Remove(item);
        }

        public static void ClearAll()
        {
            items.Clear();
        }

        public static void Update()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].IsActive)
                {
                    items[i].Update();
                }
            }
        }

        public static void CheckCollisions()
        {
            for (int i = 0; i < items.Count - 1; i++)
            {
                if (items[i].IsActive && items[i].IsCollisionAffected)
                {
                    for (int j = i + 1; j < items.Count; j++)
                    {
                        if (items[j].IsActive && items[j].IsCollisionAffected && items[i].GameObject.Id != items[j].GameObject.Id)
                        {
                            bool firstCheck = items[i].CollisionTypeMatches(items[j].Type);
                            bool secondCheck = items[j].CollisionTypeMatches(items[i].Type);
                            bool collisionHappening = items[i].Collides(items[j], ref CollisionInfo);

                            if (collisionHappening)
                            {
                                // Collision is Happening!!!
                                //Console.WriteLine($"Collision: {items[i].Type} {items[i].GameObject.Id}, {items[j].Type} {items[j].GameObject.Id}, {firstCheck}, {secondCheck}, {i}, {j}");
                                if (firstCheck)
                                {
                                    CollisionInfo.Collider = items[j].GameObject;
                                    CollisionInfo.RigidBody = items[i];
                                    items[i].GameObject.OnCollide(CollisionInfo);
                                    if (!items[i].IsCollidingWith.Contains(items[j]))
                                    {
                                        items[i].IsCollidingWith.Add(items[j]);
                                    }
                                }
                                if (secondCheck)
                                {
                                    CollisionInfo.Collider = items[i].GameObject;
                                    CollisionInfo.RigidBody = items[j];
                                    items[j].GameObject.OnCollide(CollisionInfo);
                                    if (!items[j].IsCollidingWith.Contains(items[i]))
                                    {
                                        items[j].IsCollidingWith.Add(items[i]);
                                    }
                                }
                            }
                            else 
                            {
                                items[i].IsCollidingWith.Remove(items[j]);
                                items[j].IsCollidingWith.Remove(items[i]);
                            }
                        }
                    }
                }
            }
        }
    }
}
