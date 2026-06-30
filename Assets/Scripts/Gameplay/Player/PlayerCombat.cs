namespace Game.Gameplay {
    public class PlayerCombat : UnitCombatBase {
        public void UseItem(ItemSO item) {
            if (item is not UsableItemSO usableItem)
                return;

            foreach(Effect effect in usableItem.itemEffects) {
                effect.Execute(this, null);
            }
        }
        
        public void TryRun() {
            
        }
    }
}