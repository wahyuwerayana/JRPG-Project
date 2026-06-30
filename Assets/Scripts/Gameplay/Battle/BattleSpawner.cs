using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public class BattleSpawner : MonoBehaviour {
        [Header("Battle Spawn Positions")]
        [SerializeField] private Transform playerSpawnTransform;
        [SerializeField] private Transform enemySpawnTransform;
        
        [Header("Player Battle Prefab")]
        [SerializeField] private GameObject playerPrefab;

        [Header("Global Context")]
        [field: SerializeField] public BattleContextSO BattleContext { get; private set; }
        
        public void SpawnPlayerUnit() {
            if (playerSpawnTransform == null || playerPrefab == null)
                return;
            
            PlayerCombat playerUnit = Instantiate(playerPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation).GetComponent<PlayerCombat>();
            
            GameEventManager.Instance.BattleEvent.RaiseOnPlayerSpawned(playerUnit);
        }

        public void SpawnEnemyUnits(int waveIdx) {
            if (waveIdx >= BattleContext.CurrentBattleData.waves.Length)
                return;

            Wave currentWave = BattleContext.CurrentBattleData.waves[waveIdx];

            foreach(EnemyWave enemyWave in currentWave.enemyInfos) {
                for(int i = 0; i < enemyWave.enemyCount; i++) {
                    Vector3 spawnPosition = enemySpawnTransform.position;
                    spawnPosition.x += i * 1.5f;
                    spawnPosition.z += i * 1.5f;
                    
                    EnemyCombat enemyUnit = Instantiate(enemyWave.enemy, spawnPosition, enemySpawnTransform.rotation).GetComponent<EnemyCombat>();
                    
                    GameEventManager.Instance.BattleEvent.RaiseOnEnemySpawned(enemyUnit);
                }
            }
        }
    }
}