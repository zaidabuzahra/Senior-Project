using RunTime.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace RunTime
{
    public class FalllingState : PlayerBaseState
    {
        public FalllingState(PlayerStateManager context, StateFactory stateFactory, Animator animator) : base(context, stateFactory, animator)
        {
        }

        public override void Enter()
        {
            Debug.LogWarning("Falling Entered");
            context.gravityMultiplier = context.playerData.gravityMultiplier;
            context.isFalling = true;
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {
            context.gravityMultiplier = 1;
            context.isFalling = false;
        }

        public override void FixedExecute()
        {
            //context.Rgbd.AddForce(new Vector3(0, -context.playerData.gravityPower * context.playerData.gravityMultiplier, 0), ForceMode.Impulse);
        }
    }
}