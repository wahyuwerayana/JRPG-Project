using System.Collections;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public class PlayerCombat : UnitCombatBase {
        [SerializeField] private PlayerInventorySO inventory;
        
        public void ExecuteSkillAction(SkillDataSO skill, UnitCombatBase target) {
            StartCoroutine(PlayerActionRoutine(skill, target));
        }
        
        public void ExecuteItemAction(ItemSO item) {
            StartCoroutine(PlayerItemRoutine(item));
        }

        private IEnumerator PlayerItemRoutine(ItemSO item) {
            UseItem(item);
            
            yield return new WaitForSeconds(1.5f);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitTurnFinished(this);
        }

        private IEnumerator PlayerActionRoutine(SkillDataSO skill, UnitCombatBase target) {
            UseSkill(skill, target);
            
            yield return new WaitForSeconds(1.5f);
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitTurnFinished(this);
        }
        
        public void UseItem(ItemSO item) {
            if (item is not UsableItemSO usableItem)
                return;

            foreach(Effect effect in usableItem.itemEffects) {
                effect.Execute(this, null);
            }
            
            inventory.RemoveItem(item);
            
        }
        
        public void TryRun() {
            SceneController.UnloadScene(SceneController.GetCurrentActiveScene());
        }
    }
}