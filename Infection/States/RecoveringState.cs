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
        protected Animation anim;
        public RecoveringState(Ball b) : base(b)    
        {
            anim = new Animation(ball, 7, 7, loop: true);
        }
        public override void OnEnter()
        {
            ball.SetTexture("virusIdle2");
            ball.Animation = anim;
            ball.Animation.Play();
        }
        public override void OnExit()
        {
            ball.Animation.Stop();
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
