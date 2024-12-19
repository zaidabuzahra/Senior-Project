using UnityEditor.Playables;
using UnityEngine;

namespace RunTime.Player
{
    public class MagnetState : PlayerBaseState
    {
        public MagnetState(PlayerStateManager context, StateFactory states, Animator animator) : base(context, states, animator)
        {
            isRoot = true;
            SetSubState(states.IdleState());
        }

        public override void Enter()
        {
            Debug.Log("magnet enter");
            animator.SetTrigger("Magnet");
            
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

        public override void OnCheckSwitchStates()
        {
            if (context.isAiming)
            {
                OnChangeState(states.AimingState());
            }
        }
    }
}