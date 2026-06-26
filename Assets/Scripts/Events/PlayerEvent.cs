using Game.Gameplay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Events
{
    public class PlayerEvent {
        public event UnityAction OnMoveStarted;
        public event UnityAction OnMoveEnded;
        
        /// <summary>
        /// Raised when the player successfully interacted to an IInteractable object
        /// </summary>
        public event UnityAction<IInteractable> OnInteractStarted;
        
        /// <summary>
        /// Raised when the player stopped interacting to an IInteractable object
        /// </summary>
        public event UnityAction<IInteractable> OnInteractEnded;
        
        public void RaiseOnInteractStarted(IInteractable interactable) {
            OnInteractStarted?.Invoke(interactable);
        }
        
        public void RaiseOnInteractEnd(IInteractable interactable) {
            OnInteractEnded?.Invoke(interactable);
        }
        
        public void RaiseOnMoveStarted() {
            OnMoveStarted?.Invoke();
        }
        
        public void RaiseOnMoveEnded() {
            OnMoveEnded?.Invoke();
        }
    }
}
