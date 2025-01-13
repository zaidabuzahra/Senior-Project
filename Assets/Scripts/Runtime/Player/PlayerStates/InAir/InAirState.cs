using UnityEngine;

namespace RunTime.Player
{
    public class InAirState : PlayerBaseState
    {
        public InAirState(PlayerStateManager context, StateFactory stateFactory, Animator animator) : base(context, stateFactory, animator)
        {
            isRoot = true;
            if (context.jumpPressed) { Debug.LogWarning("SpawnJump"); SetSubState(states.JumpState()); }
            else { Debug.LogWarning("SpawnFall"); SetSubState(states.FallingState()); }
        }

        private Vector3 _moveDir;
        private readonly Camera _cam = Camera.main;

        public override void Enter()
        {
            //can dash
            //Debug.LogError("Air");
            base.Enter();
        }

        public override void Execute()
        {
            //empty
            OnCheckSwitchStates();
            context.CalculatePlayerRotation(_cam, out  _moveDir);
            if (context.isAiming || _moveDir == Vector3.zero) return;
            context.followObject.transform.rotation = Quaternion.Slerp(context.followObject.transform.rotation, Quaternion.LookRotation(new Vector3(_moveDir.x, 0, _moveDir.z)), context.playerData.turnSpeed * Time.deltaTime);
        }

        public override void Exit()
        {
            //landing
            context.jumpPressed = false;
            base.Exit();
        }

        public override void FixedExecute()
        {
            //apply gravity
            //apply air movement
            context.Rgbd.AddForce(new Vector3(_moveDir.x, 0, _moveDir.z) * (context.playerData.speed * context.playerData.airControl * Time.fixedDeltaTime));
            //context.Rgbd.linearVelocity += new Vector3(_moveDir.x, -context.playerData.gravityPower, _moveDir.z) * (context.playerData.speed * context.playerData.airControl);
            context.Rgbd.AddForce(new Vector3(0, -context.playerData.gravityPower * context.gravityMultiplier * Time.fixedDeltaTime, 0));
        }

        public override void OnCheckSwitchStates()
        {
            if (context.Grounded() && context.isFalling)
            {
                //landing buffer
                OnChangeState(states.GroundedState());
            }
        }
    }
}