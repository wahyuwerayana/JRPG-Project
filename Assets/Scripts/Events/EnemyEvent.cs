using UnityEngine.Events;

namespace Game.Events {
    public class EnemyEvent {
        public UnityAction OnEnemySpawned;
        public UnityAction OnEnemyDefeated;

        public void RaiseOnEnemySpawned() {
            OnEnemySpawned?.Invoke();
        }

        public void RaiseOnEnemyDefeated() {
            OnEnemyDefeated?.Invoke();
        }
    }
}