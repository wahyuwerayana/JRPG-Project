using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Skill", menuName = "Scriptable Object/SkillDataSO")]
    public class SkillDataSO : ScriptableObject {
        [Header("General")]
        public string Name;
        [TextArea(3, 10)]
        public string Description;
        
        [Header("Skill Attributes")]
        public float MPCost;
        [SerializeReference] public List<Effect> effects;
        
        [Header("Visual")]
        public GameObject skillVFX;
        
        private void OnEnable() {
            if (string.IsNullOrEmpty(Name)) Name = name;
            effects ??= new List<Effect>();
        }
    }
}