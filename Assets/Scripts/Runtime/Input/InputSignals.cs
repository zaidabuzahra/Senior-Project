using UnityEngine;
using UnityEngine.Events;

namespace RunTime
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        //Movement & combat Input events
        public UnityAction<Vector2> OnInputMoveUpdate = delegate { };
        public UnityAction<Vector2> OnInputeLookUpdate = delegate { };
        public UnityAction<bool> OnInputJumpPressed = delegate { };
        public UnityAction<bool> OnInputSprintPressed = delegate { };
        public UnityAction<bool> OnInputUseMainUtility = delegate { };
        public UnityAction<bool> OnInputUseSideUtility = delegate { };
        public UnityAction<bool> OnInputAimPressed = delegate { };
        public UnityAction OnInputShootPressed = delegate { };
        public UnityAction OnInputShootReleased = delegate { };
        public UnityAction OnInputDash = delegate { };
        public UnityAction OnInputInteract = delegate { };
        public UnityAction OnInputSwitchMagnet = delegate { };
        public UnityAction OnInputSwitchElectricity = delegate { };
        public UnityAction OnInputFlipUtility = delegate { };
        public UnityAction OnInputRetrieveMagnetPods = delegate { };
        //public UnityAction OnInputJumpReleased = delegate { };
        //public UnityAction OnInputSprintReleased = delegate { };
        //public UnityAction OnInputUseUtilityReleased = delegate { };
        //public UnityAction OnInputFlipUtilityReleased = delegate { };
        //public UnityAction OnInputInteractReleased = delegate { };
        //public UnityAction OnInputAimReleased = delegate { };

        //UI Input
        //public UnityAction OnInputUtilityWheelOpen = delegate { };
        //public UnityAction OnInputUtilityWheelClose = delegate { };
    }
}
