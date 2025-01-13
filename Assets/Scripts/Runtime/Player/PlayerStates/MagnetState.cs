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

        private MagnetPole currentPole = MagnetPole.Red;
        public override void Enter()
        {
            Debug.Log("magnet enter");
            animator.SetTrigger("Magnet");
            //context.speed = context.normalSpeed;
            //context.rotationPowerX = context.normalRotationPowerX;
            //context.rotationPowerY = context.normalRotationPowerY;

            InputSignals.Instance.OnInputUseUtilityPressed = Remote;
            InputSignals.Instance.OnInputFlipUtilityPressed = FlipUtility;
            InputSignals.Instance.OnInputShootPressed = ShootPod;
            //subscribe to flip and utility methods
        }

        public override void Execute()
        {
            OnCheckSwitchStates();
        }

        public override void Exit()
        {
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
            if (context.aimedAtObject.CompareTag("Magnet")) context.aimedAtObject.GetComponent<IMagnetizable>().Interact(context.transform.position, currentPole);
        }

        public override void OnCheckSwitchStates()
        {
            if (context.isAiming)
            {
                InputSignals.Instance.OnInputUseUtilityPressed = Manual;
                OnChangeState(states.AimingState());
            }
        }
    }
}