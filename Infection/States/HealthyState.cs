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
        public HealthyState(Ball b) : base(b)
        {
        }
        public override void OnEnter()
        {
            if (ball.Energy != Configs.BallEnergy)
            {
                ball.Energy = Configs.BallEnergy;
            }
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
