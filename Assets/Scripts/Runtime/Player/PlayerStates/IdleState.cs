using UnityEngine;

namespace RunTime.Player
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerStateManager context, StateFactory states, Animator animator) : base(context, states, animator) { }

        public override void Enter()
        {
            //play animation
            context.StopCharacter();
        }

        public override void Execute()
        {
            OnCheckSwitchStates();
            //multiple animations for idle based on waiting time?
        }

        public override void Exit() { }

        public override void FixedExecute() { }

        public override void OnCheckSwitchStates()
        {
            if (context.MoveValue != Vector2.zero)
            {
                OnChangeState(states.WalkingState());
            }
        }
    }
}
