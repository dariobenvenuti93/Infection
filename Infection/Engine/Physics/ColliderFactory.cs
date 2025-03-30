using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIV_Engine
{
    static class ColliderFactory
    {
        public static CircleCollider CreateCircleFor(GameObject owner, bool innerCircle = true)
        {
            float radius;

            if(innerCircle)
            {
                if (owner.HalfWidth < owner.HalfHeight)
                {
                    radius = owner.HalfWidth;
                }
                else
                {
                    radius = owner.HalfHeight;
                }
            }
            else
            {
                radius = (float)Math.Sqrt(owner.HalfWidth * owner.HalfWidth +
                                   owner.HalfHeight * owner.HalfHeight);
            }

            return new CircleCollider(owner.RigidBody, radius);
        }

        public static BoxCollider CreateBoxFor(GameObject owner)
        {
            return new BoxCollider(owner.RigidBody, owner.HalfWidth, owner.HalfHeight);
        }
    }
}
