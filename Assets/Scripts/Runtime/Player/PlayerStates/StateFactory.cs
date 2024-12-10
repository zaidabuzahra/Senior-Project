using RunTime.Player;
using UnityEngine;

namespace RunTime
{
    public class StateFactory
    {
        private readonly PlayerStateManager context;
        private readonly Animator animator;
        public StateFactory(PlayerStateManager context, Animator animator)
        {
            this.context = context;
            this.animator = animator;
        }

        public PlayerBaseState IdleState() => new IdleState(context, this, animator);
        public PlayerBaseState FullBodyState() => new FullBodyState(context, this, animator);
        public PlayerBaseState MagnetState() => new MagnetState(context, this, animator);
        public PlayerBaseState WalkingState() => new WalkingState(context, this, animator);
    }
}
