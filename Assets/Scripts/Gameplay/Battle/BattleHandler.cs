using System.Collections;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public enum BattleState {
        Initializing,
        PlayerTurn,
        Attacking,
        EnemyTurn,
        Win,
        Lose,
    }
    
    public class BattleHandler : MonoBehaviour {
        [Header("Battle Spawn Positions")]
        [SerializeField] private Transform playerSpawnTransform;
        [SerializeField] private Transform enemySpawnTransform;

        [Header("Player Battle Prefab")]
        [SerializeField] private GameObject playerPrefab;
        
        private BattleState state = BattleState.Initializing;
        [SerializeField] //Remove
        private BattleData battleData;
        private int currentWaveIdx = 0;

        private Enemy currentSelectedEnemy;

        private void Start() {
            //InitializeBattle();
        }

        [ContextMenu("Initialize Battle")]
        private void InitializeBattle() {
            SceneController.SetActiveScene(SceneController.GetCurrentActiveScene());
            
            SpawnPlayerUnit();
            SpawnEnemyUnits(battleData, currentWaveIdx);
            
            StartBattle();
        }
        
        private void StartBattle() {
            GameEventManager.Instance.BattleEvent.RaiseOnStart();
            
            HandleStateChange(BattleState.PlayerTurn);
        }
        
        private void EndBattle(bool isWin) {
            GameEventManager.Instance.BattleEvent.RaiseOnEnd();
            
            if(isWin) {
                state = BattleState.Win;
                StartCoroutine(WinBattleCoroutine());
                GameEventManager.Instance.BattleEvent.RaiseOnWin();
            } else {
                state = BattleState.Lose;
                GameEventManager.Instance.BattleEvent.RaiseOnLose();
            }
        }
        
        private IEnumerator WinBattleCoroutine() {
            yield return new WaitForSeconds(1f);
            SceneController.UnloadScene(SceneController.GetCurrentActiveScene());
        }

        private void HandleStateChange(BattleState nextState) {
            state = nextState;
            
            
        }
        
        private void SpawnPlayerUnit() {
            if(playerSpawnTransform == null || playerPrefab == null) 
                return;
            
            Instantiate(playerPrefab, playerSpawnTransform.position, Quaternion.identity);
        }

        private void SpawnEnemyUnits(BattleData battleData, int waveIdx) {
            if(waveIdx >= battleData.waves.Length)
                return;
            
            Wave currentWave = battleData.waves[waveIdx];
            foreach(EnemyWave enemyWave in currentWave.enemyInfos) {
                for(int i = 0; i < enemyWave.enemyCount; i++) {
                    Vector3 spawnPosition = enemySpawnTransform.position;
                    spawnPosition.x += i * 1.5f;
                    spawnPosition.z += i * 1.5f;
                    Instantiate(enemyWave.enemy, spawnPosition, enemySpawnTransform.rotation);
                }
            }
        }
    }
}
