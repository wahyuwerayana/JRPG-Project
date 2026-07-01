using System.Collections.Generic;
using Fungus;
using Game.Gameplay;
using UnityEngine;

namespace Game.Utils.FungusCommand {
    [CommandInfo("Game", "AddItemsToInventoryCommand", "Add items to the player's inventory.")]
    public class AddItemsToInventoryCommand : Command {
        [SerializeField] private PlayerInventorySO playerInventory;
        [SerializeReference] private List<ItemSO> itemsToAdd;
        
        public override void OnEnter() {
            foreach(ItemSO itemToAdd in itemsToAdd) {
                playerInventory.AddItem(itemToAdd);
            }
            
            Continue();
        }
    }
}