using System;
using Game.Gameplay;
using Game.Managers;
using UnityEngine;

namespace Game.UI {
    public class EnemyStatsUIHandler : MonoBehaviour {
        [SerializeField] private GameObject enemyStatsPrefab;
        [SerializeField] private RectTransform enemyStatsParent;

        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnEnemySpawned += CreateEnemyStatsUI;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnEnemySpawned -= CreateEnemyStatsUI;
        }

        private void CreateEnemyStatsUI(EnemyCombat enemyUnit) {
            EnemyStatsUI enemyStatsUI = Instantiate(enemyStatsPrefab, enemyStatsParent).GetComponent<EnemyStatsUI>();
            enemyStatsUI.Init(enemyUnit);
        }
    }
}