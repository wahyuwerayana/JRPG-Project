using System;
using Game.Gameplay;
using UnityEngine.Events;

namespace Game.Events {
    public class BattleEvent {
        public event Action OnStart;
        public event Action OnEnd;
        
        public UnityAction<UnitCombatBase, float, float> OnUnitDamaged;
        public UnityAction<UnitCombatBase, SkillDataSO> OnUnitAttacked;
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

        public void RaiseOnUnitDamaged(UnitCombatBase damagedUnit, float currentHealth, float damageAmount) {
            OnUnitDamaged?.Invoke(damagedUnit, currentHealth, damageAmount);
        }
        
        public void RaiseOnUnitAttack(UnitCombatBase caster, SkillDataSO skillData) {
            OnUnitAttacked?.Invoke(caster, skillData);
        }

        public void RaiseOnUnitHealed(UnitCombatBase healedUnit, float currentHealth, float healedAmount) {
            OnUnitHealed?.Invoke(healedUnit, currentHealth, healedAmount);
        }
        
        public void RaiseOnUnitDied(UnitCombatBase diedUnit) {
            OnUnitDied?.Invoke(diedUnit);
        }

        public void RaiseOnWin() {
            OnWin?.Invoke();
        }

        public void RaiseOnLose() {
            OnLose?.Invoke();
        }
    }
}