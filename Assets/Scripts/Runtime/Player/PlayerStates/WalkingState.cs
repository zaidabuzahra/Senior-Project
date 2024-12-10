using RunTime.Player;
using UnityEditor.Rendering;
using UnityEngine;

namespace RunTime
{
    public class WalkingState : PlayerBaseState
    {
        public WalkingState(PlayerStateManager context, StateFactory stateFactory, Animator animator) : base(context, stateFactory, animator)
        {
        }

        private Vector3 _moveDir;
        public override void Enter()
        {
            animator.SetBool("Walking", true);
        }

        public override void Execute()
        {
            OnCheckSwitchStates();
            var camForward = context.Cam.transform.forward;
            var camRight = context.Cam.transform.right;
            camForward.y = camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            var forwardRelative = camForward * context.MoveValue.y;
            var rightRelative = camRight * context.MoveValue.x;
            _moveDir = forwardRelative + rightRelative;
            if (context.MoveValue == Vector2.zero) return;
            context.meshObject.transform.rotation = Quaternion.Slerp(context.meshObject.transform.rotation, Quaternion.LookRotation(new Vector3(_moveDir.x, 0, _moveDir.z)), context.turnSpeed);
        }

        public override void OnCheckSwitchStates()
        {
            if (context.MoveValue == Vector2.zero)
            {
                OnChangeState(states.IdleState());
            }
        }

        public override void Exit()
        {
            animator.SetBool("Walking", false);
        }

        public override void FixedExecute()
        {
            context.Rgbd.linearVelocity = new Vector3(_moveDir.x, context.Rgbd.linearVelocity.y, _moveDir.z) * context.speed;
        }
    }
}