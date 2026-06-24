using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Events
{
    public class PlayerEvent {
        public event UnityAction<Vector2> OnMove;
        public event UnityAction OnInteract;
        
        public void RaiseInteract() {
            OnInteract?.Invoke();
        }
        
        public void RaiseOnMove(InputAction.CallbackContext context) {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
