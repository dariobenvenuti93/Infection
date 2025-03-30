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
                        if (items[j].IsActive && items[j].IsCollisionAffected)
                        {
                            bool firstCheck = items[i].CollisionTypeMatches(items[j].Type);
                            bool secondCheck = items[j].CollisionTypeMatches(items[i].Type);

                            if ((firstCheck || secondCheck) && items[i].Collides(items[j], ref CollisionInfo))
                            {
                                // Collision is Happening!!!
                                if (firstCheck)
                                {
                                    CollisionInfo.Collider = items[j].GameObject;
                                    items[i].GameObject.OnCollide(CollisionInfo); 
                                }

                                if (secondCheck)
                                {
                                    CollisionInfo.Collider = items[i].GameObject;
                                    items[j].GameObject.OnCollide(CollisionInfo); 
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
