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
        private readonly float _stepHeight = 0.3f; 
        private readonly float _stepCheckDistance = 0.5f; 
        private readonly float _stepSmooth = 5f;
        private readonly Camera _cam = Camera.main;

        public override void Enter()
        {
            animator.SetBool("Walking", true);
        }

        public override void Execute()
        {
            OnCheckSwitchStates();
            context.CalculatePlayerRotation(_cam, out _moveDir);
            if (context.isAiming || _moveDir == Vector3.zero) return;
            context.meshObject.transform.rotation = Quaternion.Slerp(context.meshObject.transform.rotation, Quaternion.LookRotation(new Vector3(_moveDir.x, 0, _moveDir.z)), context.playerData.turnSpeed * Time.deltaTime);
            context.followObject.transform.rotation = Quaternion.Euler(0, _cam.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Follow.transform.rotation.eulerAngles.y, 0);
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
            if (DetectStep(out Vector3 stepUp))
            {
                // Smoothly step up
                Vector3 targetPosition = context.Rgbd.position + stepUp;
                context.Rgbd.MovePosition(Vector3.Lerp(context.Rgbd.position, targetPosition, _stepSmooth * Time.fixedDeltaTime));
            }

            //context.Rgbd.AddForce(new Vector3(_moveDir.x, 0, _moveDir.z) * (context.speed * Time.fixedDeltaTime));
            context.Rgbd.AddForce(new Vector3(_moveDir.x, 0, _moveDir.z) * (context.playerData.speed * Time.fixedDeltaTime));
        }
        private bool DetectStep(out Vector3 stepUp)
        {
            stepUp = Vector3.zero;

            // Cast a ray forward to detect obstacle
            if (Physics.Raycast(context.transform.position + Vector3.up * 0.1f, _moveDir, out RaycastHit hit, _stepCheckDistance))
            {
                // Check the height of the obstacle
                if (hit.point.y - context.transform.position.y <= _stepHeight)
                {
                    stepUp = Vector3.up * (hit.point.y - context.transform.position.y);
                    return true;
                }
            }

            return false;
        }

        public override void Exit()
        {
            animator.SetBool("Walking", false);
        }
    }
}