using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Skill", menuName = "Scriptable Object/Gameplay/Skill", order = 0)]
    public class SkillSO : ScriptableObject {
        [Header("General")]
        public string SkillName;
        
        [Header("Skill Attributes")]
        public float SkillDamage;
        public float MPCost;
    }
}