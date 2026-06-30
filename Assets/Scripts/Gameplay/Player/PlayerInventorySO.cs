using System.Collections.Generic;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Player Inventory", menuName = "Scriptable Object/Player Inventory")]
    public class PlayerInventorySO : ScriptableObject {
        public List<ItemSO> items;
        
        public void AddItem(ItemSO item) {
            items.Add(item);
            
            GameEventManager.Instance.PlayerEvent.RaiseOnInventoryChanged();
        }
        
        public void RemoveItem(ItemSO item) {
            if(!items.Contains(item))
                return;
            
            items.Remove(item);
            
            GameEventManager.Instance.PlayerEvent.RaiseOnInventoryChanged();
        }
    }
}