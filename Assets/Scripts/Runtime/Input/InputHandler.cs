using UnityEngine;
using UnityEngine.InputSystem;

namespace RunTime.Input
{
    public class InputHandler : MonoBehaviour
    {
        public void OnMoveUpdate(InputAction.CallbackContext context)
        {
            InputSignals.Instance.OnInputMoveUpdate?.Invoke(context.ReadValue<Vector2>());
        }
        public void OnLookUpdate(InputAction.CallbackContext context)
        {
            InputSignals.Instance.OnInputeLookUpdate?.Invoke(context.ReadValue<Vector2>());
        }
        public void OnUseUtility(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputUseUtilityPressed?.Invoke(); }
            else if (context.canceled) { InputSignals.Instance.OnInputUseUtilityReleased?.Invoke(); }
        }
        public void OnOpenWheelPress(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputUtilityWheelOpen?.Invoke(); }
            //else if (context.canceled) { InputSignals.Instance.OnInputUtilityWheelClose?.Invoke(); }
        }
        public void OnCloseWheelPress(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputUtilityWheelClose?.Invoke(); }
        }
        public void OnShootPress(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputShootPressed?.Invoke(); }
            else if (context.canceled) { InputSignals.Instance.OnInputShootReleased?.Invoke(); }
        }
        public void OnInteractPress(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputInteractPressed?.Invoke(); }
            else if (context.canceled) { InputSignals.Instance.OnInputInteractReleased?.Invoke(); }
        }
        public void OnJumpPress(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputJumpPressed?.Invoke(); }
            else if (context.canceled) { InputSignals.Instance.OnInputJumpReleased?.Invoke(); }
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputAimPressed?.Invoke(); }
            else if (context.canceled) { InputSignals.Instance.OnInputAimReleased?.Invoke(); }
        }

        public void OnFlipUtility(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputFlipUtilityPressed?.Invoke(); }
            else if (context.canceled) { InputSignals.Instance.OnInputFlipUtilityReleased?.Invoke();}
        }

        public void OnRetrieveMagnets(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputRetrieveMagnetPodsPressed?.Invoke(); }
        }
    }
}