using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Events
{
    public class PlayerEvent {
        public UnityAction OnInteract;
        public UnityAction<Vector2> OnMove;
        
        public void RaiseInteract() {
            OnInteract?.Invoke();
        }
        
        public void RaiseMoveStarted(InputAction.CallbackContext context) {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
