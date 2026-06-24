using UnityEngine;

namespace Game.Gameplay {
    public abstract class SkillDataSO : ScriptableObject {
        [Header("General")]
        public string Name;
        [TextArea(3, 10)]
        public string Description;
        
        [Header("Skill Attributes")]
        public float MPCost;

        [Header("Visuals")]
        public string AnimationTrigger;

        public abstract void ExecuteSkill(UnitCombatBase caster, UnitCombatBase target);
    }
}