using System;
using System.Collections.Generic;

namespace AIV_Engine
{
    internal class StateMachine
    {
        private Dictionary<FSMStates, State> states;
        private State currentState;

        public StateMachine()
        {
            states = new Dictionary<FSMStates, State>();
            currentState = null;
        }

        public void AddState(FSMStates key, State value )
        {
            states[key] = value;
            value.SetStateMachine(this);
        }

        public void GoTo(FSMStates key)
        {
            if(currentState != null)
            {
                currentState.OnExit();
            }

            currentState = states[key];
            currentState.OnEnter();
        }

        public void Update()
        {
            currentState.Update();
        }
    }
}
