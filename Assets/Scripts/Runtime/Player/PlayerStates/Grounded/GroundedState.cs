using RunTime.Player;
using UnityEngine;

namespace RunTime.Player
{
    public class GroundedState : PlayerBaseState
    {
        public GroundedState(PlayerStateManager context, StateFactory stateFactory, Animator animator) : base(context, stateFactory, animator)
        {
            isRoot = true;
            SetSubState(stateFactory.IdleState());
        }

        public override void Enter()
        {
            Debug.LogError("Grounded");
            base.Enter();
            animator.SetTrigger("FullBody");
        }

        public override void Execute()
        {
            OnCheckSwitchStates();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void FixedExecute()
        {

        }
        public override void OnCheckSwitchStates()
        {
            if (context.isAiming)
            {
                OnChangeState(states.AimingState());
            }
            if (!context.Grounded() || context.jumpPressed)
            {
                OnChangeState(states.InAirState());
            }
        }
    }
}