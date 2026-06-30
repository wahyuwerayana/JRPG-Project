using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay {
    [Serializable]
    public abstract class Effect {
        public abstract void Execute(UnitCombatBase caster, UnitCombatBase target);
    }

    [Serializable]
    public class DamageEffect : Effect {
        [Tooltip("Example: 1.5 means 150% of the caster's attack")]
        public float damageMultiplier;
        
        public override void Execute(UnitCombatBase caster, UnitCombatBase target) {
            float totalDamage = damageMultiplier * caster.Stats.Attack;
            target.TakeDamage(totalDamage);
        }
    }

    [Serializable]
    public class HealEffect : Effect {
        public float baseAmount;
        
        public override void Execute(UnitCombatBase caster, UnitCombatBase target) {
            caster.Heal(baseAmount);
        }
    }

    [Serializable]
    public class MPRegenEffect : Effect {
        public int minAmount;
        public int maxAmount;
        
        public override void Execute(UnitCombatBase caster, UnitCombatBase target) {
            float amount = Random.Range(minAmount, maxAmount);
            caster.RegenMP(amount);
        }
    }

    [Serializable]
    public class MPDecreaseEffect : Effect {
        public int minAmount;
        public int maxAmount;
        
        public override void Execute(UnitCombatBase caster, UnitCombatBase target) {
            float amount = Random.Range(minAmount, maxAmount);
            target.TakeMP(amount);
        }
    }
}