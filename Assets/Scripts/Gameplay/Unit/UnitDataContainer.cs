using UnityEngine;

namespace Game.Gameplay {
    public class UnitDataContainer : MonoBehaviour {
        [field: SerializeField] public UnitDataSO Data { get; private set; }
    }
}