using System;

namespace Game.Events {
    public class BattleEvent {
        public event Action OnBattleStart;
        public event Action OnBattleEnd;

        public void RaiseOnBattleStart() {
            OnBattleStart?.Invoke();
        }
        
        public void RaiseOnBattleEnd() {
            OnBattleEnd?.Invoke();
        }
    }
}