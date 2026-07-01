using DG.Tweening;
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
            GameEventManager.Instance.BattleEvent.OnUnitDamaged += HandleUnitHPChanged;
            GameEventManager.Instance.BattleEvent.OnUnitHealed += HandleUnitHPChanged;
            
            GameEventManager.Instance.BattleEvent.OnUnitDied += HandleUnitDied;
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged += HandleUnitMPChanged;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnUnitDamaged -= HandleUnitHPChanged;
            GameEventManager.Instance.BattleEvent.OnUnitHealed -= HandleUnitHPChanged;
            
            GameEventManager.Instance.BattleEvent.OnUnitDied -= HandleUnitDied;
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged -= HandleUnitMPChanged;
        }
        
        private void HandleUnitHPChanged(UnitCombatBase unit, float currentHealth, float healthBeforeDamaged) {
            if (unit != enemyUnit)
                return;
                
            float tweenHealth = healthBeforeDamaged;
            
            DOTween.To(() => tweenHealth, 
                x => tweenHealth = x, 
                currentHealth, 
                Const.Tween.FADE_DURATION)
                .SetEase(Ease.OutQuad)
                .OnUpdate(() => {
                    enemyHealthText.text = $"{tweenHealth:F0} / {unit.Stats.Health:F0}";
                });
        }

        private void HandleUnitDied(UnitCombatBase unit) {
            if (unit != enemyUnit)
                return;
            
            Destroy(gameObject);
        }

        private void HandleUnitMPChanged(UnitCombatBase unit, float currentMP, float mpBefore) {
            if (unit != enemyUnit)
                return;

            float tweenMp = mpBefore;
            
            DOTween.To(() => tweenMp, 
                x => tweenMp = x,
                currentMP, 
                Const.Tween.FADE_DURATION)
                .SetEase(Ease.OutQuad)
                .OnUpdate(() => {
                    enemyManaText.text = $"{tweenMp:F0} / {unit.Stats.MP:F0}";
                });
        }
    }
}