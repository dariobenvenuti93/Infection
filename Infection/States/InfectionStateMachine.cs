using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIV_Engine;

namespace Infection
{
    internal class InfectionStateMachine : StateMachine
    {
        protected Ball b;
        public InfectionStateMachine(Ball b) : base() 
        {
            this.b = b;
            this.AddState(FSMStates.Healthy, new HealthyState(b));
            this.AddState(FSMStates.Infected, new InfectedState(b));
            this.AddState(FSMStates.InInfection, new SickeningState(b));
            this.AddState(FSMStates.InRecovery, new RecoveringState(b));
            this.GoTo(FSMStates.Healthy);
        }
    }
}
