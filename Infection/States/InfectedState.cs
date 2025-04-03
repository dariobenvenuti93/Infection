using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AIV_Engine;

namespace Infection
{
    internal class InfectedState : InfectionState
    {
        protected Stopwatch recoveryTimer;
        public InfectedState(Ball b) : base(b)
        {
            recoveryTimer = new Stopwatch();
        }
        public override void OnEnter()
        {
            if (ball.Energy != 0.0f)
            {
                ball.Energy = 0.0f;
            }
            BallManager.Infect(ball);
        }
        public override void Update()
        {
            bool collidingWithBall = false;
            List<RigidBody> collidingBodies = ball.InfectionRigidBody.IsCollidingWith;
            if (collidingBodies.Count > 0)
            {
                for ( int i = 0; i < collidingBodies.Count; i++ )
                {
                    if (collidingBodies[i].Type == RigidBodyType.Ball)
                    {
                        collidingWithBall = true;
                        try
                        {
                            Ball b = (Ball) collidingBodies[i].GameObject;
                            if (b.Energy > 0.0f)
                                b.Energy -= Configs.InfectionRate * Game.DeltaTime;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            if (collidingWithBall)
            {
                recoveryTimer.Reset();
            }
            else
            {
                if (!recoveryTimer.IsRunning)
                {
                    recoveryTimer.Restart();
                }
                if (recoveryTimer.ElapsedMilliseconds > Configs.RecoveryTime)
                {
                    recoveryTimer.Reset();
                    ball.Fsm.GoTo(FSMStates.InRecovery);
                    BallManager.Recover(ball);
                }
            }
        }
    }
}
