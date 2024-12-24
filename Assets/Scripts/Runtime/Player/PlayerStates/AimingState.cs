using Cinemachine;
using System.ComponentModel.Design.Serialization;
using UnityEditor;
using UnityEngine;

namespace RunTime.Player
{
    public class AimingState : PlayerBaseState
    {
        private Vector3 _mousePos = Vector3.zero;
        private Vector2 _screenCenter = new (Screen.width/2, Screen.height/2);
        private Ray _ray;

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

            context.speed = context.aimingSpeed;
            context.rotationPowerX = context.aimRotationPowerX;
            context.rotationPowerY = context.aimRotationPowerY;
            //Speed will be reset on other states
            //play aiming animation
            //play aiming sound
        }

        //player rotates with camera rotation
        public override void Execute()
        {
            OnCheckSwitchStates();
            _ray = context.Cam.ScreenPointToRay(_screenCenter);
            if (Physics.Raycast(_ray, out context.Hit, 999f, context.layerMask))
            {
                _mousePos = context.Hit.point;
                if (context.Hit.transform.gameObject != context.aimedAtObject)
                {
                    if (context.aimedAtObject.CompareTag("Magnet")) context.aimedAtObject.GetComponent<IMagnetizable>().GrayoutTarget();
                    context.aimedAtObject = context.Hit.transform.gameObject;
                }
                if (context.aimedAtObject.CompareTag("Magnet"))
                {
                    context.aimedAtObject.GetComponent<IMagnetizable>()?.HighlightTarget();
                }
            }
            //Debug sphere
            context.sphere.transform.position = _mousePos;
            context.meshObject.transform.rotation = Quaternion.Euler(0, context.followTransform.transform.rotation.eulerAngles.y, 0);
        }

        public override void Exit()
        {
            //animator.SetBool("Aim", false);
            context.aimVirtaulCamera.gameObject.SetActive(false);
            //play aiming animation
            //play aiming sound
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