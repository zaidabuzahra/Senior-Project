using Cinemachine;
using UnityEngine;

namespace RunTime.Player
{
    public class SprintingState : PlayerBaseState
    {
        public SprintingState(PlayerStateManager context, StateFactory stateFactory, Animator animator) : base(context, stateFactory, animator)
        {
        }

        private Vector3 _moveDir;

        public override void Enter()
        {
            Debug.Log("SPRINTING");
            animator.SetBool("Sprinting", true);
            context.speed = context.playerData.sprintSpeed;
        }

        public override void Execute()
        {
            OnCheckSwitchStates();
            context.CalculatePlayerRotation(context.cam, out _moveDir);
            if (context.isAiming || _moveDir == Vector3.zero) return;
            context.meshObject.transform.rotation = Quaternion.Slerp(context.meshObject.transform.rotation, Quaternion.LookRotation(new Vector3(_moveDir.x, 0, _moveDir.z)), context.playerData.turnSpeed * Time.deltaTime);
            context.followObject.transform.rotation = Quaternion.Euler(context.followObject.transform.rotation.eulerAngles.x, context.cam.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Follow.transform.rotation.eulerAngles.y, context.followObject.transform.rotation.eulerAngles.z);
        }

        public override void Exit()
        {
            animator.SetBool("Sprinting", false);
            context.speed = context.playerData.walkingSpeed;
        }
        public override void FixedExecute()
        {
            context.Rgbd.AddForce(new Vector3(_moveDir.x, 0, _moveDir.z) * (context.speed * Time.fixedDeltaTime));
        }

        public override void OnCheckSwitchStates()
        {
            if (context.isSprinting) return;
            if (context.MoveValue == Vector2.zero)
            {
                OnChangeState(states.IdleState());
            }
            else
            {
                OnChangeState(states.WalkingState());
            }
        }
    }
}