using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay {
    [Serializable]
    public abstract class SkillEffect {
        public abstract void Execute(UnitCombatBase caster, UnitCombatBase target);
    }

    [Serializable]
    public class DamageEffect : SkillEffect {
        [Tooltip("Example: 1.5 means 150% of the caster's attack")]
        public float damageMultiplier;
        
        public override void Execute(UnitCombatBase caster, UnitCombatBase target) {
            float totalDamage = damageMultiplier * caster.Stats.Attack;
            target.TakeDamage(totalDamage);
        }
    }

    [Serializable]
    public class HealEffect : SkillEffect {
        public float baseAmount;
        
        public override void Execute(UnitCombatBase caster, UnitCombatBase target) {
            target.Heal(baseAmount);
        }
    }

    [Serializable]
    public class MPEffect : SkillEffect {
        public int minAmount;
        public int maxAmount;
        
        public override void Execute(UnitCombatBase caster, UnitCombatBase target) {
            float amount = Random.Range(minAmount, maxAmount);
            target.RestoreMP(amount);
        }
    }
}