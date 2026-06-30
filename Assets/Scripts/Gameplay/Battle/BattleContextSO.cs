using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Battle Context", menuName = "Scriptable Object/Gameplay/Battle Context", order = 0)]
    public class BattleContextSO : ScriptableObject {
        [Header("Active Battle State")]
        public BattleData CurrentBattleData;
    }
}