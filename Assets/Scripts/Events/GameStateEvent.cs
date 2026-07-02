using UnityEngine.Events;

namespace Game.Events {
    public class GameStateEvent {
        public event UnityAction<bool> OnPauseToggled;
        
        public void RaiseOnPauseToggled(bool isPaused) {
            OnPauseToggled?.Invoke(isPaused);
        }
    }
}