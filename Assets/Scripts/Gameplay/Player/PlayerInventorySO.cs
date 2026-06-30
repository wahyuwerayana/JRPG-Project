using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Player Inventory", menuName = "Scriptable Object/Player Inventory")]
    public class PlayerInventorySO : ScriptableObject {
        public List<ItemSO> items;
    }
}