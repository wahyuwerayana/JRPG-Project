using System.Collections;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay {
    public class EnemyAI : MonoBehaviour {
        private EnemyCombat enemyCombat;

        private void Awake() {
            enemyCombat = GetComponent<EnemyCombat>();
        }

        public void ExecuteTurn(PlayerCombat playerTarget) {
            StartCoroutine(ThinkAndActRoutine(playerTarget));
        }

        private IEnumerator ThinkAndActRoutine(PlayerCombat playerTarget) {
            yield return new WaitForSeconds(1.5f);

            SkillDataSO chosenSkill = DecideSkillToUse();
            
            enemyCombat.UseSkill(chosenSkill, playerTarget);

            yield return new WaitForSeconds(1f);

            BattleHandler.Instance.EndEnemyTurn();
        }
        
        private SkillDataSO DecideSkillToUse() {
            if (enemyCombat.Stats.Skills.Length <= 0) 
                return enemyCombat.Stats.BasicAttack;

            SkillDataSO[] availableSkills = enemyCombat.Stats.Skills.Where(skill => skill.MPCost <= enemyCombat.Stats.MP).ToArray();
                
            return availableSkills.Length > 0 ? availableSkills[Random.Range(0, availableSkills.Length)] : enemyCombat.Stats.BasicAttack;
        }
    }
}