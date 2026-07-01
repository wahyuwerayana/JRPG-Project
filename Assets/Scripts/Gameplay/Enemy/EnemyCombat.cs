using System.Collections;
using UnityEngine;
using System.Linq;
using Game.Managers;

namespace Game.Gameplay {
    public class EnemyCombat : UnitCombatBase {
        public void ExecuteTurn(PlayerCombat playerTarget) {
            StartCoroutine(ThinkAndActRoutine(playerTarget));
        }

        private IEnumerator ThinkAndActRoutine(PlayerCombat playerTarget) {
            yield return new WaitForSeconds(1.5f);

            SkillDataSO chosenSkill = DecideSkillToUse();
            
            UseSkill(chosenSkill, playerTarget);

            yield return new WaitForSeconds(1f);

            GameEventManager.Instance.BattleEvent.RaiseOnUnitTurnFinished(this);
        }
        
        private SkillDataSO DecideSkillToUse() {
            if (Stats.Skills.Length <= 0 || Random.value < 0.5f) //Randomize using skill or basic attack
                return Stats.BasicAttack;

            SkillDataSO[] availableSkills = Stats.Skills.Where(skill => skill.MPCost <= Stats.MP).ToArray();
                
            return availableSkills.Length > 0 ? availableSkills[Random.Range(0, availableSkills.Length)] : Stats.BasicAttack;
        }
    }
}