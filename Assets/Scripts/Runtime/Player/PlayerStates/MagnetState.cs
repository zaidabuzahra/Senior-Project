using UnityEngine;

namespace RunTime.Player
{
    public class MagnetState : PlayerBaseState
    {
        public MagnetState(PlayerStateManager context, StateFactory states, Animator animator) : base(context, states, animator)
        {
            isRoot = true;
            Debug.LogError("FullBody");
            SetSubState(states.IdleState());
        }
        private Vector3 _mousePos = Vector3.zero;
        private Vector2 _screenCenter = new(Screen.width / 2, Screen.height / 2);
        private Ray _ray;
        private MagnetPole currentPole = MagnetPole.Red;
        private readonly Camera _cam = Camera.main;
        public override void Enter()
        {
            Debug.Log("magnet enter");
            animator.SetTrigger("Magnet");

            InputSignals.Instance.OnInputUseUtilityPressed = Manual;
            InputSignals.Instance.OnInputFlipUtilityPressed = FlipUtility;
            InputSignals.Instance.OnInputShootPressed = ShootPod;
            //
        }

        public override void Execute()
        {
            OnCheckSwitchStates();
            Ray _ray = _cam.ScreenPointToRay(_screenCenter);
            if (Physics.Raycast(_ray, out context.Hit, 999f, context.layerMask))
            {
                _mousePos = context.Hit.point;
                if (context.Hit.transform.gameObject != context.aimedAtObject)
                {
                    if (context.aimedAtObject.CompareTag("Magnet")) context.aimedAtObject.GetComponent<Magnetizable>().GrayoutTarget();
                    context.aimedAtObject = context.Hit.transform.gameObject;
                }
                if (context.aimedAtObject.CompareTag("Magnet"))
                {
                    context.lineRenderer.enabled = true;
                    context.aimedAtObject.GetComponent<Magnetizable>()?.HighlightTarget();
                    context.lineRenderer.SetPosition(0, context.transform.position);
                    context.lineRenderer.SetPosition(1, context.aimedAtObject.transform.position);
                    if (context.isHeld)
                    {
                        InputSignals.Instance.OnInputUseUtilityPressed?.Invoke();
                        Debug.Log("WHY");
                    }
                }
                else
                {
                    context.lineRenderer.enabled = false;
                }
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
                Debug.Log(context.Hit.point);
                MagnetPodManager.Instance.LaunchPod(context.Hit.point);
            }
        }

        private void Manual()
        {
            context.isHeld = true;
            if (context.aimedAtObject.CompareTag("Magnet")) context.aimedAtObject.GetComponent<Magnetizable>().Interact(context.transform.position, currentPole);
        }

        public override void OnCheckSwitchStates()
        {
            if (context.isAiming)
            {
                //InputSignals.Instance.OnInputUseUtilityPressed = Manual;
                OnChangeState(states.AimingState());
            }
        }
    }
}