using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIV_Engine;

namespace Infection
{
    abstract class InfectionState : State
    {
        protected Ball ball;
        public InfectionState(Ball b)
        {
            this.ball = b;
        }
    }
}
