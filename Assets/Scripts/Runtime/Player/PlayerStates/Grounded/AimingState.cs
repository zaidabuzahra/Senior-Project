using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem.Android;

namespace RunTime.Player
{
    public class AimingState : PlayerBaseState
    {
        private Vector2 _screenCenter = new (Screen.width/2, Screen.height/2);
        private Ray _ray;
        private PlayerBaseState _oldState;
        private readonly Camera _cam = Camera.main;

        public AimingState(PlayerStateManager context, StateFactory states, Animator animator) : base(context, states, animator) 
        {
            isRoot = true;
            SetSubState(states.IdleState());
        }

        public override void Enter()
        {
            //animator.SetBool("Aim", true);
            base.Enter();
            _oldState = context.currentState;
            CameraSignals.Instance.OnSwitchCamera?.Invoke(Cam.CameraEnum.Aim);
            context.speed = context.playerData.aimingSpeed;
            //play aiming animation
            //play aiming sound
        }

        //player rotates with camera rotation
        public override void Execute()
        {
            OnCheckSwitchStates();
            _ray = _cam.ScreenPointToRay(_screenCenter);
            if (Physics.Raycast(_ray, out context.Hit, 999f, context.layerMask))
            {
                if (context.Hit.transform.gameObject != context.aimedAtObject)
                {
                    if (context.aimedAtObject.CompareTag("Magnet")) context.aimedAtObject.GetComponent<Magnetizable>().GrayoutTarget();
                    context.aimedAtObject = context.Hit.transform.gameObject;
                }
                if (context.aimedAtObject.CompareTag("Magnet"))
                {
                    context.aimedAtObject.GetComponent<Magnetizable>().HighlightTarget();
                }
            }
            //Debug sphere
            //context.sphere.transform.position = _mousePos;
            float currentY = context.meshObject.transform.rotation.eulerAngles.y;
            float targetY = _cam.GetComponent<CinemachineBrain>()
                               .ActiveVirtualCamera.Follow.transform.rotation.eulerAngles.y;

            // Interpolate the Y angle (adjust 'rotationSpeed' as needed)
            float newY = Mathf.LerpAngle(currentY, targetY, Time.deltaTime * context.playerData.aimTurnSpeed);

            // Apply the new rotation
            context.meshObject.transform.rotation = Quaternion.Euler(0, newY, 0);
            context.followObject.transform.rotation = Quaternion.Euler(_cam.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Follow.transform.rotation.eulerAngles);
        }

        public override void Exit()
        {
            //animator.SetBool("Aim", false);
            //play aiming animation
            //play aiming sound
            base.Exit();
            CameraSignals.Instance.OnSwitchCamera?.Invoke(Cam.CameraEnum.Standard);
            context.speed = context.playerData.walkingSpeed;
        }

        public override void OnCheckSwitchStates()
        {
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