using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIV_Engine;

namespace Infection
{
    internal class HealthyState : InfectionState
    {
        protected Animation anim;
        public HealthyState(Ball b) : base(b)
        {
            anim = new Animation(ball, 27, 27, loop: true);
        }
        public override void OnEnter()
        {
            ball.SetTexture("virusIdle1");
            ball.Animation = anim;
            ball.Animation.Play();
            if (ball.Energy != Configs.BallEnergy)
            {
                ball.Energy = Configs.BallEnergy;
            }
        }
        public override void OnExit()
        {
            ball.Animation.Stop();
        }
        public override void Update()
        {
            if (ball.Energy < Configs.BallEnergy)
            {
                fsm.GoTo(FSMStates.InInfection);
            }
        }
    }
}
