using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIV_Engine;

namespace Infection
{
    internal class RecoveringState : InfectionState
    {
        public RecoveringState(Ball b) : base(b)    
        {
        }
        public override void Update()
        {
            List<RigidBody> collidingBodies = ball.RigidBody.IsCollidingWith;
            if (collidingBodies.Count > 0)
            {
                for (int i = 0; i < collidingBodies.Count; i++)
                {
                    if (collidingBodies[i].Type == RigidBodyType.Infection)
                    {
                        fsm.GoTo(FSMStates.InInfection);
                    }
                }
            }
            ball.Energy += Configs.RecoveryRate * Game.DeltaTime;
            if (ball.Energy >= Configs.BallEnergy)
            {
                fsm.GoTo(FSMStates.Healthy);
            }
        }
    }
}
