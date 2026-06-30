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

            int rowCount = currentWave.enemyInfos.Length;
            const float zSpacing = 3f;
            const float xSpacing = 3f;

            for(int i = 0; i < rowCount; i++) {
                EnemyWave enemyWave = currentWave.enemyInfos[i];
                float zModifier = (i - (rowCount - 1) / 2f) * zSpacing;
                
                for(int j = 0; j < enemyWave.enemyCount; j++) {
                    Vector3 spawnPosition = enemySpawnTransform.position;
                    
                    float xModifier = (j - (enemyWave.enemyCount - 1) / 2f) * xSpacing;
                    
                    spawnPosition += enemySpawnTransform.right * xModifier;
                    spawnPosition += -enemySpawnTransform.forward * zModifier;
                    
                    EnemyCombat enemyUnit = Instantiate(enemyWave.enemy, spawnPosition, enemySpawnTransform.rotation).GetComponent<EnemyCombat>();
                    
                    GameEventManager.Instance.BattleEvent.RaiseOnEnemySpawned(enemyUnit);
                }
            }
        }
    }
}