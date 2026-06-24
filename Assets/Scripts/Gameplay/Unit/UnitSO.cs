using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Object/Gameplay/Unit")]
    public class UnitSO : ScriptableObject {
        [Header("General")]
        public string Name;
        
        [Header("Movement")]
        public float MoveSpeed;
        
        [Header("Combat")]
        public float Health;
        public float Attack;
        public float MP;
        public float Defense;
        public SkillSO[] Skills;

        private void OnValidate() {
            name = name.Trim();
            Name = name;
        }
    }
}