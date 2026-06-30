using Game.Gameplay;
using UnityEngine.Events;

namespace Game.Events {
    public class BattleEvent {
        public event UnityAction OnStart;
        public event UnityAction OnEnd;
        
        public event UnityAction<BattleState> OnBattleStateChanged;

        public event UnityAction<SkillDataSO> OnPlayerActionSelected;
        
        public event UnityAction<PlayerCombat> OnPlayerSpawned;
        public event UnityAction<EnemyCombat> OnEnemySpawned;
        
        public event UnityAction<UnitCombatBase, float, float> OnUnitDamaged;
        public event UnityAction<UnitCombatBase, SkillDataSO> OnUnitAttacked;
        public event UnityAction<UnitCombatBase, float, float> OnUnitHealed;
        public event UnityAction<UnitCombatBase> OnUnitTurnFinished;
        public event UnityAction<UnitCombatBase> OnUnitDied;
        
        public event UnityAction<UnitCombatBase, float, float> OnUnitMPChanged;
        
        public event UnityAction<BattleData> OnWin;
        public event UnityAction OnLose;

        public void RaiseOnStart() {
            OnStart?.Invoke();
        }
        
        public void RaiseOnEnd() {
            OnEnd?.Invoke();
        }
        
        public void RaiseOnBattleStateChanged(BattleState state) {
            OnBattleStateChanged?.Invoke(state);
        }
        
        public void RaiseOnEnemySpawned(EnemyCombat enemyUnit) {
            OnEnemySpawned?.Invoke(enemyUnit);
        }

        public void RaiseOnPlayerSpawned(PlayerCombat playerUnit) {
            OnPlayerSpawned?.Invoke(playerUnit);
        }
        
        public void RaiseOnPlayerActionSelected(SkillDataSO skillData) {
            OnPlayerActionSelected?.Invoke(skillData);
        }

        public void RaiseOnUnitDamaged(UnitCombatBase damagedUnit, float currentHealth, float damageAmount) {
            OnUnitDamaged?.Invoke(damagedUnit, currentHealth, damageAmount);
        }
        
        public void RaiseOnUnitAttack(UnitCombatBase caster, SkillDataSO skillData) {
            OnUnitAttacked?.Invoke(caster, skillData);
        }
        
        public void RaiseOnUnitTurnFinished(UnitCombatBase unit) {
            OnUnitTurnFinished?.Invoke(unit);
        }

        public void RaiseOnUnitHealed(UnitCombatBase healedUnit, float currentHealth, float healedAmount) {
            OnUnitHealed?.Invoke(healedUnit, currentHealth, healedAmount);
        }
        
        public void RaiseOnUnitDied(UnitCombatBase diedUnit) {
            OnUnitDied?.Invoke(diedUnit);
        }
        
        public void RaiseOnUnitMPChanged(UnitCombatBase unit, float currentMP, float mpUsed) {
            OnUnitMPChanged?.Invoke(unit, currentMP, mpUsed);
        }

        public void RaiseOnWin(BattleData battleData) {
            OnWin?.Invoke(battleData);
        }

        public void RaiseOnLose() {
            OnLose?.Invoke();
        }
    }
}