using UnityEngine;

namespace RunTime.Player
{
    public class JumpState : PlayerBaseState
    {
        public JumpState(PlayerStateManager context, StateFactory factory, Animator animator) : base(context, factory, animator)
        {
        }

        public override void Enter()
        {
            context.Rgbd.linearVelocity = new Vector3(context.Rgbd.linearVelocity.x, 0, context.Rgbd.linearVelocity.z);
            Vector3 forwardPower = context.MoveValue == Vector2.zero ? Vector3.zero : context.meshObject.transform.forward.normalized * context.playerData.forwardJumpPower;
            context.Rgbd.AddForce(forwardPower + Vector3.up * context.playerData.verticalJumpPower, ForceMode.Impulse);
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
