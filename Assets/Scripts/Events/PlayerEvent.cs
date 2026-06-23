using System;

namespace Game.Events
{
    public class PlayerEvent {
        public event Action OnInteract;
        
        public event Action OnMoveStarted;
        public event Action OnMoveEnded;

        // Battle Events
        public event Action OnAttack;
        public event Action<float> OnDamaged;
        public event Action<float> OnHealed;
        
        public void RaiseInteract() {
            OnInteract?.Invoke();
        }
        
        public void RaiseMoveStarted() {
            OnMoveStarted?.Invoke();
        }

        public void RaiseMoveEnded() {
            OnMoveEnded?.Invoke();
        }

        public void RaiseOnAttack() {
            OnAttack?.Invoke();
        }
        
        public void RaiseOnDamaged(float damage) {
            OnDamaged?.Invoke(damage);
        }
        
        public void RaiseOnHealed(float healedAmount) {
            OnHealed?.Invoke(healedAmount);
        }
    }
}
