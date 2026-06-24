using Game.Managers;
using Game.Utils;
using UnityEngine;

namespace Game.Gameplay {
    [RequireComponent(typeof(UnitDataContainer))]
    public abstract class UnitCombatBase : MonoBehaviour, IDamagable {
        [field: SerializeField] public UnitDataSO Stats { get; private set; }
        
        private float currentHealth;
        private float currentMP;

        private void Awake() {
            currentHealth = Stats.Health;
            currentMP = Stats.MP;
        }

        public virtual void UseSkill(SkillDataSO skillData) {
            if (skillData == null) {
                Debug.LogWarning("Skill is null");
                return;
            }

            if (currentMP < skillData.MPCost) {
                Debug.LogWarning("Not enough MP to use skill");
                return;
            }

            if (currentHealth <= 0) {
                Debug.LogWarning("Unit is dead");
                return;
            }

            currentMP -= skillData.MPCost;
        }
        
        public virtual void TakeDamage(float attack) {
            float actualDamage = CombatUtils.CalculateDamage(attack, Stats.Defense);
            
            currentHealth = Mathf.Max(0, currentHealth - actualDamage);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitDamaged(this, currentHealth, actualDamage);
            
            if (currentHealth <= 0) {
                Die();
            }
        }
        
        public virtual void Heal(float healedAmount) {
            currentHealth = Mathf.Min(Stats.Health, currentHealth + healedAmount);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitHealed(this, currentHealth, healedAmount);
        }

        protected virtual void Die() {
            GameEventManager.Instance.BattleEvent.RaiseOnUnitDied(this);
            Destroy(gameObject);
        }
    }
}