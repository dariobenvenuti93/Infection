using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIV_Engine;

namespace Infection
{
    internal class SickeningState : InfectionState
    {
        protected Animation anim;
        public SickeningState(Ball b) : base(b)
        {
            anim = new Animation(ball, 10, 10, loop: true);
        }
        public override void OnEnter()
        {
            ball.SetTexture("virusHit");
            ball.Animation = anim;
            ball.Animation.Play();
        }
        public override void OnExit()
        {
            ball.Animation.Stop();
        }
        public override void Update()
        {
            if (ball.Fsm.CurrentState == this)
                ball.Animation.Update();
            if (ball.Energy <= 0)
            {
                fsm.GoTo(FSMStates.Infected);
            }
            bool collidingWithInfection = false;
            List<RigidBody> collidingBodies = ball.RigidBody.IsCollidingWith;
            if (collidingBodies.Count > 0)
            {
                for (int i = 0; i < collidingBodies.Count; i++)
                {
                    if (collidingBodies[i].Type == RigidBodyType.Infection)
                    {
                        collidingWithInfection = true;
                    }
                }
            }
            if (!collidingWithInfection)
            {
                fsm.GoTo(FSMStates.InRecovery);
            }
        }
    }
}
