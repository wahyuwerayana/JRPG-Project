using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Game.Settings.MainInput;

namespace Game.Settings {
    public interface IInputReader {
        public Vector2 MoveInputDirection { get; }
        public void EnablePlayerActions();
        public void DisablePlayerActions();
    }
    
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Scriptable Object/Input/Input Reader")]
    public class InputReader : ScriptableObject, IPlayerActions, IInputReader {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction Interact = delegate { };

        public MainInput inputActions;

        public Vector2 MoveInputDirection => inputActions.Player.Move.ReadValue<Vector2>();

        public void EnablePlayerActions() {
            if (inputActions == null) {
                inputActions = new MainInput();
                inputActions.Player.SetCallbacks(this);
            }
            
            inputActions.Player.Enable();
        }
        
        public void DisablePlayerActions() {
            inputActions?.Player.Disable();
        }
        
        public void OnMove(InputAction.CallbackContext context) {
            Move?.Invoke(context.ReadValue<Vector2>());
        }
        
        public void OnInteract(InputAction.CallbackContext context) {
            if (!context.performed)
                return;
            
            Interact?.Invoke();
        }
    }
}