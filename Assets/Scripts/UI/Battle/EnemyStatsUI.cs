using Game.Gameplay;
using Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class EnemyStatsUI : MonoBehaviour {
        [SerializeField] private TMP_Text enemyHealthText;
        [SerializeField] private TMP_Text enemyManaText;
        [SerializeField] private Image enemyPortrait;
        
        private EnemyCombat enemyUnit;
        
        public void Init(EnemyCombat spawnedEnemy) {
            enemyHealthText.text = $"{spawnedEnemy.Stats.Health} / {spawnedEnemy.Stats.Health}";
            enemyManaText.text = $"{spawnedEnemy.Stats.MP} / {spawnedEnemy.Stats.MP}";
            enemyPortrait.sprite = spawnedEnemy.Stats.portrait;
            
            enemyUnit = spawnedEnemy;
        }

        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnUnitDamaged += HandleUnitDamaged;
            GameEventManager.Instance.BattleEvent.OnUnitDied += HandleUnitDied;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnUnitDamaged -= HandleUnitDamaged;
            GameEventManager.Instance.BattleEvent.OnUnitDied -= HandleUnitDied;
        }
        
        private void HandleUnitDamaged(UnitCombatBase unit, float currentHealth, float damageAmount) {
            if (unit != enemyUnit)
                return;
                
            enemyHealthText.text = $"{currentHealth} / {unit.Stats.Health}";
            
        }

        private void HandleUnitDied(UnitCombatBase unit) {
            if (unit != enemyUnit)
                return;
            
            Destroy(gameObject);
        }
    }
}