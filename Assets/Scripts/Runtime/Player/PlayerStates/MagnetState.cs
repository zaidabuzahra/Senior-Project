using RunTime.Cam;
using UnityEngine;

namespace RunTime.Player
{
    public class MagnetState : PlayerBaseState
    {
        public MagnetState(PlayerStateManager context, StateFactory states, Animator animator) : base(context, states, animator)
        {
            isRoot = true;
            //Debug.LogError("FullBody");
            SetSubState(states.IdleState());
        }

        private bool _isHeld = false;
        private bool _isShooting = false;
        private Vector2 _screenCenter = new(Screen.width / 2, Screen.height / 2);
        private MagnetPole currentPole = MagnetPole.Red;
        private readonly Camera _cam = Camera.main;

        public override void Enter()
        {
            animator.SetTrigger("Magnet");

            InputSignals.Instance.OnInputUseMainUtility = Pressed;
            InputSignals.Instance.OnInputFlipUtility = FlipUtility;
            InputSignals.Instance.OnInputShootPressed = ShootPod;
            //
        }

        private void Pressed(bool state)
        {
            _isHeld = state;
            if (!_isHeld) _isShooting = false;
        }

        public override void Execute()
        {
            OnCheckSwitchStates();
            Ray _ray = _cam.ScreenPointToRay(_screenCenter);
            if (Physics.Raycast(_ray, out context.Hit, 999f, context.layerMask))
            {
                if (!context.Hit.transform.gameObject.CompareTag("Magnet") && !_isShooting)
                {
                    if (context.aimedAtObject) context.aimedAtObject.GetComponent<Magnetizable>().GrayoutTarget();
                    context.aimedAtObject = null;
                    context.lineRenderer.enabled = _isShooting;
                    return;
                }
                if (context.Hit.transform.gameObject != context.aimedAtObject && !_isShooting)
                {
                    if (!context.Hit.transform.gameObject.CompareTag("Magnet")) return;
                    if (context.aimedAtObject)
                    {
                        context.aimedAtObject.GetComponent<Magnetizable>().GrayoutTarget();
                    }
                    context.aimedAtObject = context.Hit.transform.gameObject;
                    context.lineRenderer.enabled = true;
                    context.aimedAtObject.GetComponent<Magnetizable>().HighlightTarget();
                }
            }
            context.lineRenderer.SetPosition(0, context.followObject.transform.position);
            if (context.aimedAtObject) context.lineRenderer.SetPosition(1, context.aimedAtObject.transform.position);
            if (_isHeld && context.aimedAtObject)
            {
                Manual();
            }
        }

        public override void Exit()
        {
            context.lineRenderer.enabled = true;
        }

        public override void FixedExecute()
        {
        }
        private void FlipUtility()
        {
            currentPole = currentPole == MagnetPole.Red ? MagnetPole.Blue : MagnetPole.Red;
            Debug.Log(context.lineRenderer.material);
            if (currentPole == MagnetPole.Red)
            {
                context.lineRenderer.material.SetFloat("_RedValue", 1f);
                context.lineRenderer.material.SetFloat("_BlueValue", 0.1f);
            }
            else
            {
                context.lineRenderer.material.SetFloat("_RedValue", 0.1f);
                context.lineRenderer.material.SetFloat("_BlueValue", 1f);
            }
            //magnetColor.GetComponent<MeshRenderer>().material = currentPole == MagnetPole.Red ? red : blue;
        }

        private void Remote()
        {

        }

        private void ShootPod()
        {
            if (context.isAiming && context.Hit.transform != null)
            {
                //context.Hit
                MagnetPodManager.Instance.LaunchPod(context.Hit.point);
            }
        }

        private void Manual()
        {
            _isShooting = true;
            context.aimedAtObject.GetComponent<Magnetizable>().Interact(context.transform.position, currentPole);
        }

        public override void OnCheckSwitchStates()
        {
            if (context.isAiming)
            {
                OnChangeState(states.AimingState());
            }
        }
    }
}