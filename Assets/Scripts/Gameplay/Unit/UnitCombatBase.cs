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
            if (skillData == null)
                return;

            if (currentMP < skillData.MPCost)
                return;

            foreach(Effect effect in skillData.effects) {
                effect.Execute(this, target);
            }
            
            if (skillData.MPCost > 0) {
                float mpBeforeUsed = currentMP;
                currentMP -= skillData.MPCost;
                
                GameEventManager.Instance.BattleEvent.RaiseOnUnitMPChanged(this, currentMP, mpBeforeUsed);
            }
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitAttack(this, target, skillData);
        }
        
        public virtual void TakeDamage(float damageAmount) {
            float actualDamage = CombatUtils.CalculateDamage(damageAmount, Stats.Defense);
            
            float healthBeforeDamaged = currentHealth;
            currentHealth = Mathf.Clamp(currentHealth - actualDamage, 0f, Stats.Health);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitDamaged(this, currentHealth, healthBeforeDamaged);
            
            if (currentHealth <= 0f) {
                Die();
            }
        }
        
        public virtual void Heal(float healAmount) {
            currentHealth = Mathf.Clamp(currentHealth + healAmount, 0f, Stats.Health);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitHealed(this, currentHealth, healAmount);
        }

        public virtual void RegenMP(float mpAmount) {
            float mpBeforeChange = currentMP;
            
            currentMP = Mathf.Clamp(currentMP + mpAmount, 0f, Stats.MP);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitMPChanged(this, currentMP, mpBeforeChange);
        }
        
        public virtual void TakeMP(float mpAmount) {
            float mpBeforeChange = currentMP;
            
            currentMP = Mathf.Clamp(currentMP - mpAmount, 0f, Stats.MP);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitMPChanged(this, currentMP, mpBeforeChange);
        }

        protected virtual void Die() {
            GameEventManager.Instance.BattleEvent.RaiseOnUnitDied(this);
            Destroy(gameObject);
        }
    }
}