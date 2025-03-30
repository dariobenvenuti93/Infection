using System;
using System.Collections.Generic;
using System.Linq;

namespace AIV_Engine
{
    enum FSMStates
    {
        Walk, Follow, Shoot
    }

    abstract class State
    {
        protected StateMachine fsm;

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void Update() { }

        public void SetStateMachine(StateMachine fsm)
        {
            this.fsm = fsm;
        }
    }
}
