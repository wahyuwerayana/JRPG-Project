using Game.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Game.Settings.MainInput;

namespace Game.Settings {
    public interface IInputReader {
        public void EnablePlayerActions();
        public void DisablePlayerActions();
    }
    
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Scriptable Object/Input/Input Reader")]
    public class PlayerInputReader : ScriptableObject, IPlayerActions, IInputReader {
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

        public MainInput inputActions;

        public Vector2 PointerPosition => inputActions.Player.PointerPosition.ReadValue<Vector2>();

        public void EnablePlayerActions() {
            if (inputActions == null) {
                inputActions = new MainInput();
                inputActions.Player.SetCallbacks(this);
            }
            
            inputActions.Player.Enable();
        }
        
        public void DisablePlayerActions() {
            inputActions.Player.Disable();
        }
        
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
        
        public void OnPointerPosition(InputAction.CallbackContext context) { }
    }
}