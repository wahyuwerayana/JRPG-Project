using UnityEngine;

namespace Game.Gameplay {
    public class UnitDataContainer : MonoBehaviour {
        [SerializeField] private UnitDataSO unitData;
        
        public UnitDataSO UnitData => unitData;
    }
}