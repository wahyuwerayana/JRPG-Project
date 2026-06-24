using Game.Managers;
using Game.Utils;
using UnityEngine;

namespace Game.Gameplay {
    public abstract class UnitCombatBase : MonoBehaviour, IDamagable {
        [field: SerializeField] public UnitSO Stats { get; private set; }
        
        private float CurrentHealth;
        private float CurrentMP;

        private void Awake() {
            CurrentHealth = Stats.Health;
            CurrentMP = Stats.MP;
        }

        public virtual void UseSkill(SkillSO skill) {
            if (skill == null) {
                Debug.LogWarning("Skill is null");
                return;
            }

            if (CurrentMP < skill.MPCost) {
                Debug.LogWarning("Not enough MP to use skill");
                return;
            }

            if (CurrentHealth <= 0) {
                Debug.LogWarning("Unit is dead");
                return;
            }

            CurrentMP -= skill.MPCost;
        }
        
        public virtual void TakeDamage(float attack) {
            float actualDamage = CombatUtils.CalculateDamage(attack, Stats.Defense);
            
            CurrentHealth = Mathf.Max(0, CurrentHealth - actualDamage);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitDamaged(this, CurrentHealth, actualDamage);
            
            if (CurrentHealth <= 0) {
                Die();
            }
        }
        
        public virtual void Heal(float healedAmount) {
            CurrentHealth = Mathf.Min(Stats.Health, CurrentHealth + healedAmount);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitHealed(this, CurrentHealth, healedAmount);
        }

        protected virtual void Die() {
            GameEventManager.Instance.BattleEvent.RaiseOnUnitDied(this);
            Destroy(gameObject);
        }
    }
}