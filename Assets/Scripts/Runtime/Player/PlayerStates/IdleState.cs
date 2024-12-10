using UnityEngine;

namespace RunTime.Player
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerStateManager context, StateFactory states, Animator animator) : base(context, states, animator) { }

        public override void Enter()
        {
            //play animation
            animator.SetBool("Idle", true);
            StopCharacter();
        }

        public override void Execute()
        {
            OnCheckSwitchStates();
            //multiple animations for idle based on waiting time?
        }

        public override void Exit() 
        {
            animator.SetBool("Idle", false);
        }

        public override void FixedExecute() { }

        public override void OnCheckSwitchStates()
        {
            if (context.MoveValue != Vector2.zero)
            {
                OnChangeState(states.WalkingState());
            }
        }

        public void StopCharacter()
        {
            context.Rgbd.linearVelocity = Vector3.zero;
        }
    }
}
