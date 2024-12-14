using UnityEngine;

namespace RunTime.Player
{
    public class FullBodyState : PlayerBaseState
    {
        public FullBodyState(PlayerStateManager context, StateFactory states, Animator animator) : base(context, states, animator)
        {
            isRoot = true;
            SetSubState(states.IdleState()); /*this will go to an animation function that will be invoked when the transition ends*/
        }

        public override void Enter()
        {   
            //Play animation if needed
            //Equip utility
            //Set aim accuracy
            //Subscribe to input events
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
