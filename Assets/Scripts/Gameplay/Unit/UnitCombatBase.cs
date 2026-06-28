using Game.Managers;
using Game.Utils;
using UnityEngine;

namespace Game.Gameplay {
    [RequireComponent(typeof(UnitDataContainer))]
    public abstract class UnitCombatBase : MonoBehaviour, IDamageable {                      
        public UnitDataSO Stats { get; private set; }
        private float currentHealth;
        private float currentMP;

        private void Awake() {
            Stats = GetComponent<UnitDataContainer>().Data;
            currentHealth = Stats.Health;
            currentMP = Stats.MP;
        }

        public virtual void UseSkill(SkillDataSO skillData, UnitCombatBase target) {
            if (skillData == null) {
                Debug.LogWarning("Skill is null");
                return;
            }

            if (currentMP < skillData.MPCost) {
                Debug.LogWarning("Not enough MP to use skill");
                return;
            }

            foreach(SkillEffect effect in skillData.effects) {
                effect.Execute(this, target);
            }

            currentMP -= skillData.MPCost;
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitAttack(this, skillData);
        }
        
        public virtual void TakeDamage(float damageAmount) {
            float actualDamage = CombatUtils.CalculateDamage(damageAmount, Stats.Defense);
            
            currentHealth = Mathf.Max(0f, currentHealth - actualDamage);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitDamaged(this, currentHealth, actualDamage);
            
            if (currentHealth <= 0f) {
                Die();
            }
        }
        
        public virtual void Heal(float healAmount) {
            currentHealth = Mathf.Min(Stats.Health, currentHealth + healAmount);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitHealed(this, currentHealth, healAmount);
        }
        
        public virtual void RestoreMP(float mpAmount) {
            currentMP = Mathf.Min(Stats.MP, currentMP + mpAmount);
        }

        protected virtual void Die() {
            GameEventManager.Instance.BattleEvent.RaiseOnUnitDied(this);
            Destroy(gameObject);
        }
    }
}