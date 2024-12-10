using UnityEngine;
using UnityEngine.Events;

namespace RunTime
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        //Input events
        public UnityAction<Vector2> OnInputMoveUpdate = delegate { };
        public UnityAction<Vector2> OnInputeLookUpdate = delegate { };
        public UnityAction OnInputJumpPressed = delegate { };
        public UnityAction OnInputJumpReleased = delegate { };
        public UnityAction OnInputUseUtilityPressed = delegate { };
        public UnityAction OnInputUseUtilityReleased = delegate { };
        public UnityAction OnInputInteractPressed = delegate { };
        public UnityAction OnInputInteractReleased = delegate { };
        public UnityAction OnInputShootPressed = delegate { };
        public UnityAction OnInputShootReleased = delegate { };

        //Input state
    }
}
