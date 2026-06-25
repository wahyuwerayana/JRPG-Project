using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Object/Gameplay/Unit")]
    public class UnitDataSO : ScriptableObject {
        [field: Header("General")]
        [field: SerializeField] public string Name { get; private set; }
        
        [field: Header("Movement")]
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        
        [field: Header("Combat")]
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Attack { get; private set; }
        [field: SerializeField] public float MP { get; private set; }
        [field: SerializeField] public float Defense { get; private set; }
        [field: SerializeField] public SkillDataSO[] Skills;

        private void OnValidate() {
            name = name.Trim();
            Name = name;
        }
    }
}