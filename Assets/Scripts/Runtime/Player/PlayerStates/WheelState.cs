using RunTime.Player;
using UnityEngine;
using static RunTime.WheelManager;

namespace RunTime
{
    public class WheelState : PlayerBaseState
    {
        public WheelState(PlayerStateManager context, StateFactory stateFactory, Animator animator) : base(context, stateFactory, animator)
        {
            isRoot = true;
            //SetSubState(stateFactory.IdleState());
        }

        public override void Enter()
        {
            context.Rgbd.mass = context.wheelMass;
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {
            context.Rgbd.mass = 1;
        }

        public override void FixedExecute()
        {
            // Apply motor power
            context.wheelManager.wheels[1].wheelCollider.motorTorque = context.MoveValue.y * context.maxSpead;

            // Steer the front wheel
            context.wheelManager.wheels[0].wheelCollider.steerAngle = context.MoveValue.y * context.turnRate;
        }

        public override void OnCheckSwitchStates()
        {

        }
    }
}
