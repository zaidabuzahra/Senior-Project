using UnityEngine;

namespace RunTime.Player
{
    public class FullBodyState : PlayerBaseState
    {
        public FullBodyState(PlayerStateManager context, StateFactory states, Animator animator) : base(context, states, animator) { }
        public override void Enter()
        {   
            //Play animation
            //Play sound effect
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
