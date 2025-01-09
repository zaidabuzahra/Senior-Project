using UnityEngine;

namespace RunTime.Player
{
    public class JumpState : PlayerBaseState
    {
        private float _jumpTime;

        public JumpState(PlayerStateManager context, StateFactory factory, Animator animator) : base(context, factory, animator)
        {
        }

        public override void Enter()
        {
            Debug.LogWarning("JumpEnter");
            context.Rgbd.AddForce(Vector3.up * context.playerData.jumpPower, ForceMode.Impulse);
        }

        public override void Execute()
        {
            OnCheckSwitchStates();
        }

        public override void Exit()
        {
            // Exit jump logic if needed
        }

        public override void FixedExecute()
        {

        }

        public override void OnCheckSwitchStates()
        {
            if (context.Rgbd.linearVelocity.y < 0)
            {
                Debug.LogWarning("JumpExit " + context.Rgbd.linearVelocity.y);
                OnChangeState(states.FallingState());
            }
        }
    }
}
