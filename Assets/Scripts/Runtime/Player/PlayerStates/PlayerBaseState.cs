using RunTime.Player;
using UnityEngine;

namespace RunTime
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateManager context;
        protected StateFactory states;
        protected Animator animator;
        public PlayerBaseState (PlayerStateManager context, StateFactory stateFactory, Animator animator)
        {
            this.context = context;
            states = stateFactory;
            this.animator = animator;
        }

        public abstract void Enter();
        public abstract void Execute();
        public abstract void FixedExecute();
        public abstract void Exit();
        protected void OnChangeState(PlayerBaseState newState)
        {
            Exit();
            context.currentState = newState;
            newState.Enter();
        }
        public virtual void OnCheckSwitchStates() { }
    }
}
