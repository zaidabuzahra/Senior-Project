using Cinemachine;
using RunTime.Player;
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
            context.CalculatePlayerRotation(context.cam, out _moveDir);
            if (context.isAiming || _moveDir == Vector3.zero) return;
            context.meshObject.transform.rotation = Quaternion.Slerp(context.meshObject.transform.rotation, Quaternion.LookRotation(new Vector3(_moveDir.x, 0, _moveDir.z)), context.playerData.turnSpeed * Time.deltaTime);
            context.followObject.transform.rotation = Quaternion.Euler(context.followObject.transform.rotation.eulerAngles.x, context.cam.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Follow.transform.rotation.eulerAngles.y, context.followObject.transform.rotation.eulerAngles.z);
        }
        
        public override void OnCheckSwitchStates()
        {
            if (context.MoveValue == Vector2.zero)
            { 
                OnChangeState(states.IdleState());
            }
            else if (context.isSprinting && !context.isAiming)
            {
                OnChangeState(states.SprintingState());
            }
        }

        public override void FixedExecute()
        {
            context.Rgbd.AddForce(new Vector3(_moveDir.x, 0, _moveDir.z) * (context.playerData.walkingSpeed * Time.fixedDeltaTime));
        }

        public override void Exit()
        {
            animator.SetBool("Walking", false);
        }
    }
}