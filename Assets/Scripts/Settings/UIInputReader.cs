using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Game.Settings.MainInput;

namespace Game.Settings {
    [CreateAssetMenu(fileName = "New UI Input Reader", menuName = "Scriptable Object/Input/UI Input Reader")]
    public class UIInputReader : ScriptableObject, IUIActions {
        public event UnityAction<Vector2> Navigate = delegate { };
        public event UnityAction Submit = delegate { };
        public event UnityAction Cancel = delegate { };

        private MainInput inputActions;

        public void Initialize(MainInput input) {
            inputActions = input;
            inputActions.UI.SetCallbacks(this);
        }

        public void EnableUI() => inputActions.UI.Enable();
        public void DisableUI() => inputActions.UI.Disable();

        public void OnNavigate(InputAction.CallbackContext context) {
            Navigate?.Invoke(context.ReadValue<Vector2>());
        }
        
        public void OnSubmit(InputAction.CallbackContext context) {
            if(context.performed)
                Submit?.Invoke();
        }
        
        public void OnCancel(InputAction.CallbackContext context) {
            if(context.performed)
                Cancel?.Invoke();
        }
        
        public void OnPoint(InputAction.CallbackContext context) { }
        public void OnClick(InputAction.CallbackContext context) { }
        public void OnRightClick(InputAction.CallbackContext context) { }
        public void OnMiddleClick(InputAction.CallbackContext context) { }
        public void OnScrollWheel(InputAction.CallbackContext context) { }
        public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }
        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }
    }
}