using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Damage Skill", menuName = "Scriptable Object/Gameplay/Skill/Damage Skill", order = 0)]
    public class DamageSkillSO : SkillDataSO {
        [Header("Damage Skill Attributes")]
        public float Damage;

        public override void ExecuteSkill(UnitCombatBase caster, UnitCombatBase target) {
            
        }
    }
}