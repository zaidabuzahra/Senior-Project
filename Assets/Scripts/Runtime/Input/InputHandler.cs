using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;

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
        public void OnJumpPress(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputJumpPressed?.Invoke(true); }
            else if (context.canceled) { InputSignals.Instance.OnInputJumpPressed?.Invoke(false); }
        }

        public void OnSprinting(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputSprintPressed?.Invoke(true); }
            else if (context.canceled) { InputSignals.Instance.OnInputSprintPressed?.Invoke(false); }
        }

        public void OnUseMainUtility(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputUseMainUtility?.Invoke(true); }
            else if (context.canceled) { InputSignals.Instance.OnInputUseMainUtility?.Invoke(false); }
        }

        public void OnUseSideUtility(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputUseSideUtility?.Invoke(true); }
            else if (context.canceled) { InputSignals.Instance.OnInputUseSideUtility?.Invoke(false); }
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputAimPressed?.Invoke(true); }
            else if (context.canceled) { InputSignals.Instance.OnInputAimPressed?.Invoke(false); }
        }

        public void OnShootPress(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputShootPressed?.Invoke(); }
            else if (context.canceled) { InputSignals.Instance.OnInputShootReleased?.Invoke(); }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed) InputSignals.Instance.OnInputDash?.Invoke();
        }

        public void OnInteractPress(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputInteract?.Invoke(); }
            //else if (context.canceled) { InputSignals.Instance.OnInputInteractReleased?.Invoke(); }
        }

        public void OnSwitchMagnet(InputAction.CallbackContext context)
        {
            if (context.performed) InputSignals.Instance.OnInputSwitchMagnet?.Invoke();
        }

        public void OnSwitchElectiricty(InputAction.CallbackContext context)
        {
            if (context.performed) InputSignals.Instance.OnInputSwitchElectricity?.Invoke();
        }

        public void OnFlipUtility(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputFlipUtility?.Invoke(); }
            //else if (context.canceled) { InputSignals.Instance.OnInputFlipUtilityReleased?.Invoke();}
        }

        public void OnRetrieveMagnets(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputRetrieveMagnetPods?.Invoke(); }
        }
        /*public void OnOpenWheelPress(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputUtilityWheelOpen?.Invoke(); }
            //else if (context.canceled) { InputSignals.Instance.OnInputUtilityWheelClose?.Invoke(); }
        }*/
        /*public void OnCloseWheelPress(InputAction.CallbackContext context)
        {
            if (context.performed) { InputSignals.Instance.OnInputUtilityWheelClose?.Invoke(); }
        }*/
    }
}