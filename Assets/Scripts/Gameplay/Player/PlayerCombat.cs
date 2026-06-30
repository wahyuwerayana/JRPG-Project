using System.Collections;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public class PlayerCombat : UnitCombatBase {
        public void ExecuteSkillAction(SkillDataSO skill, UnitCombatBase target) {
            StartCoroutine(PlayerActionRoutine(skill, target));
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
            
            GameEventManager.Instance.BattleEvent.RaiseOnUnitTurnFinished(this);
        }
        
        public void TryRun() {
            
        }
    }
}