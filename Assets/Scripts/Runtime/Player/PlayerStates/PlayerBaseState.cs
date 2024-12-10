using RunTime.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace RunTime
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateManager context;
        protected StateFactory states;
        protected Animator animator;

        protected bool isRoot;
        protected PlayerBaseState currentSuperState;
        protected PlayerBaseState currentSubState;

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

        public void UpdateStates()
        {
            Execute();
            currentSubState?.UpdateStates();
        }

        public void FixedUpdateStates()
        {
            FixedExecute();
            currentSubState?.FixedUpdateStates();
        }
        protected void SetChildState(PlayerBaseState newChildState)
        {
            currentSubState = newChildState;
            newChildState.SetSuperState(this);
        }

        protected void SetSuperState(PlayerBaseState newSuperState)
        {
            currentSuperState = newSuperState;
        }

        protected void OnChangeState(PlayerBaseState newState)
        {
            Exit();
            newState.Enter();
            if (isRoot) { context.currentState = newState; }
            else { currentSuperState?.SetChildState(newState); }
        }
        public virtual void OnCheckSwitchStates() { }
    }
}