using Game.Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Game.Settings.MainInput;

namespace Game.Settings {
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Scriptable Object/Input/Player Input Reader")]
    public class PlayerInputReader : ScriptableObject, IPlayerActions {
        /// <summary>
        /// Raised when the player used the move button on the controller they are using
        /// </summary>
        public event UnityAction<Vector2> Move = delegate { };
        
        /// <summary>
        /// Raised when the player press the interact button on the controller they are using
        /// </summary>
        public event UnityAction Interact = delegate { };
        
        /// <summary>
        /// Raised when the player press the click button on the controller they are using
        /// </summary>
        public event UnityAction Click = delegate { };
        public event UnityAction Pause = delegate { };

        public MainInput inputActions;

        public Vector2 PointerPosition => inputActions.Player.PointerPosition.ReadValue<Vector2>();

        public void Initialize(MainInput input) {
            inputActions = input;
            inputActions.Player.SetCallbacks(this);
        }
        
        public void EnablePlayer() => inputActions.Player.Enable();
        public void DisablePlayer() => inputActions.Player.Disable();
        
        public void OnMove(InputAction.CallbackContext context) {
            Move?.Invoke(context.ReadValue<Vector2>());
        }
        
        public void OnInteract(InputAction.CallbackContext context) {
            if (!context.performed)
                return;
            
            Interact?.Invoke();
        }
        
        public void OnClick(InputAction.CallbackContext context) {
            if (!context.performed)
                return;
                
            Click?.Invoke();
        }

        public void OnPause(InputAction.CallbackContext context) {
            if (!context.performed)
                return;
            
            Pause?.Invoke();
        }
        
        public void OnPointerPosition(InputAction.CallbackContext context) { }
    }
}