using System;
using Game.Gameplay;
using UnityEngine.Events;

namespace Game.Events {
    public class BattleEvent {
        public event Action OnStart;
        public event Action OnEnd;

        public UnityAction<UnitCombatBase, float> OnUnitAttack;
        public UnityAction<UnitCombatBase, float, float> OnUnitDamaged;
        public UnityAction<UnitCombatBase, float, float> OnUnitHealed;
        public UnityAction<UnitCombatBase> OnUnitDied;
        
        public event Action OnWin;
        public event Action OnLose;

        public void RaiseOnStart() {
            OnStart?.Invoke();
        }
        
        public void RaiseOnEnd() {
            OnEnd?.Invoke();
        }
        
        public void RaiseOnUnitAttack(UnitCombatBase unitCombat, float attack) {
            OnUnitAttack?.Invoke(unitCombat, attack);
        }

        public void RaiseOnUnitDamaged(UnitCombatBase unitCombat, float currentHealth, float damage) {
            OnUnitDamaged?.Invoke(unitCombat, currentHealth, damage);
        }

        public void RaiseOnUnitHealed(UnitCombatBase unitCombat, float currentHealth, float healedAmount) {
            OnUnitHealed?.Invoke(unitCombat, currentHealth, healedAmount);
        }
        
        public void RaiseOnUnitDied(UnitCombatBase unitCombat) {
            OnUnitDied?.Invoke(unitCombat);
        }

        public void RaiseOnWin() {
            OnWin?.Invoke();
        }

        public void RaiseOnLose() {
            OnLose?.Invoke();
        }
    }
}