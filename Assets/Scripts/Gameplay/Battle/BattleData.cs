using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Battle Data", menuName = "Scriptable Object/Battle Data", order = 0)]
    public class BattleData : ScriptableObject {
        [field: SerializeField] public Wave[] waves { get; private set; }
    }
}