using UnityEngine;

namespace RunTime.Player
{
    public class MagnetState : PlayerBaseState
    {
        public MagnetState(PlayerStateManager context, StateFactory states, Animator animator) : base(context, states, animator)
        {
            isRoot = true;
            SetChildState(states.IdleState());
        }

        public override void Enter()
        {
            Debug.Log("magnet enter");
            animator.SetLayerWeight(animator.GetLayerIndex("Magnet"), 1f);
        }

        public override void Execute()
        {
        }

        public override void Exit()
        {

        }

        public override void FixedExecute()
        {
        }
    }
}