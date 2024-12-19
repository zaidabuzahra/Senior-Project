using UnityEngine;

namespace RunTime.Player
{
    public class AimingState : PlayerBaseState
    {
        public AimingState(PlayerStateManager context, StateFactory states, Animator animator) : base(context, states, animator) 
        {
            isRoot = true;
            SetSubState(states.IdleState());
        }

        PlayerBaseState _oldState;
        public override void Enter()
        {
            //animator.SetBool("Aim", true);
            _oldState = context.currentState;
            context.aimVirtaulCamera.gameObject.SetActive(true);
            //play aiming animation
            //play aiming sound
            //switch cameras
            //adjust speed
        }

        public override void Execute()
        {
            //player rotates with camera rotation
            OnCheckSwitchStates();
            Vector3 mousePos = Vector3.zero;
            Vector2 screenCenter = new(Screen.width/2, Screen.height/2);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out RaycastHit hit, 999f))
            {
                mousePos = hit.point;
            }
            //mousePos.y = context.meshObject.transform.position.y;
            context.sphere.transform.position = mousePos;
            context.meshObject.transform.rotation = Quaternion.Euler(0, context.followTransform.transform.rotation.eulerAngles.y, 0);
        }

        public override void Exit()
        {
            //animator.SetBool("Aim", false);
            context.aimVirtaulCamera.gameObject.SetActive(false);
            //play aiming animation
            //play aiming sound
            //switch cameras
            //reset speed
        }

        public override void OnCheckSwitchStates()
        {
            //base.OnCheckSwitchStates();
            if (!context.isAiming)
            {
                OnChangeState(_oldState);
            }
        }

        public override void FixedExecute() 
        {
        }
    }
}
