using Fungus;
using Game.Gameplay;
using UnityEngine;

namespace Game.Utils.FungusCommand {
    [CommandInfo("Game", "ResetInventoryCommand", "Reset the player's inventory.")]
    public class ResetInventoryCommand : Command {
        [SerializeField] private PlayerInventorySO playerInventory;
        
        public override void OnEnter() {
            playerInventory.ResetInventory();
            Continue();
        }
    }
}