using System.Collections;
using System.Collections.Generic;
using Game.Managers;
using Game.UI;
using UnityEngine;

namespace Game.Gameplay {
    public enum BattleState {
        Initializing,
        Player_Turn,
        Selecting_Target,
        Executing,
        Enemy_Turn,
        Win,
        Lose,
    }
    
    public class BattleHandler : MonoBehaviour {
        public static BattleHandler Instance { get; private set; }
        
        public BattleState CurrentState { get; private set; } = BattleState.Initializing;

        private List<EnemyCombat> activeEnemies = new List<EnemyCombat>();
        private int currentEnemyTurnIdx = 0;
        
        private PlayerCombat playerTarget;

        private BattleSpawner spawner;

        private int currentWaveIdx = 0;
        
        private FadeOverlayHandler fader;
        
        private void Awake() {
            Instance = this;
            
            spawner = GetComponent<BattleSpawner>();
        }

        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned += HandlePlayerSpawned;

            GameEventManager.Instance.BattleEvent.OnEnemySpawned += HandleEnemySpawned;
            GameEventManager.Instance.BattleEvent.OnUnitDied += HandleUnitDied;
            
            GameEventManager.Instance.BattleEvent.OnUnitTurnFinished += HandleUnitTurnFinished;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned -= HandlePlayerSpawned;
            
            GameEventManager.Instance.BattleEvent.OnEnemySpawned -= HandleEnemySpawned;
            GameEventManager.Instance.BattleEvent.OnUnitDied -= HandleUnitDied;
            
            GameEventManager.Instance.BattleEvent.OnUnitTurnFinished -= HandleUnitTurnFinished;
        }
        
        private void Start() {
            InitializeBattle();
        }
        
        private void HandlePlayerSpawned(PlayerCombat player) {
            playerTarget = player;
        }
        
        private void HandleEnemySpawned(EnemyCombat enemy) {
            if(!activeEnemies.Contains(enemy))
                activeEnemies.Add(enemy);
        }

        private void HandleUnitDied(UnitCombatBase unit) {
            switch (unit) {
                case EnemyCombat enemy:
                {
                    activeEnemies.Remove(enemy);

                    if (activeEnemies.Count == 0) {
                        currentWaveIdx++;
                        
                        if(currentWaveIdx >= spawner.BattleContext.CurrentBattleData.waves.Length) {
                            EndBattle(true);
                            return;
                        }
                        
                        spawner.SpawnEnemyUnits(currentWaveIdx);
                    }

                    break;
                }
                case PlayerCombat:
                    EndBattle(false);
                    break;
            }
        }
        
        private void HandleUnitTurnFinished(UnitCombatBase finishedUnit) {
            if(CurrentState is BattleState.Win or BattleState.Lose)
                return;
            
            switch (finishedUnit) {
                case PlayerCombat:
                    StartCoroutine(EnemyTurnTransition());
                    break;
                case EnemyCombat:
                    EndEnemyTurn();
                    break;
            }
        }

        private async void InitializeBattle() {
            spawner.SpawnPlayerUnit();
            spawner.SpawnEnemyUnits(currentWaveIdx);
            
            fader = FindAnyObjectByType<FadeOverlayHandler>();
            
            if(fader != null)
                await fader.FadeInAsync();
            
            GameEventManager.Instance.BattleEvent.RaiseOnStarted();
            
            ChangeState(BattleState.Player_Turn);
        }
        
        private void EndBattle(bool isWin) {
            if(isWin) {
                ChangeState(BattleState.Win);
                GameEventManager.Instance.BattleEvent.RaiseOnWin(spawner.BattleContext.CurrentBattleData);
            } else {
                ChangeState(BattleState.Lose);
                GameEventManager.Instance.BattleEvent.RaiseOnLose();
            }
            
            EndBattleSequenceAsync();
        }
        
        private async void EndBattleSequenceAsync() {
            await Awaitable.WaitForSecondsAsync(1f);
            
            AudioManager.Instance.ResumePreviousBGM();
            GameEventManager.Instance.BattleEvent.RaiseOnEnded();

            if (fader != null) {
                await SceneController.UnloadSceneWithFade(SceneController.GetCurrentActiveScene(), fader);
            }
            else {
                await SceneController.UnloadScene(SceneController.GetCurrentActiveScene());
            }
        }

        public void ChangeState(BattleState nextState) {
            CurrentState = nextState;
            GameEventManager.Instance.BattleEvent.RaiseOnBattleStateChanged(nextState);
        }

        private IEnumerator EnemyTurnTransition() {
            yield return new WaitForSeconds(1f);
            ChangeState(BattleState.Enemy_Turn);

            currentEnemyTurnIdx = 0;
            ExecuteNextEnemyTurn();
        }

        private void ExecuteNextEnemyTurn() {
            if(CurrentState is BattleState.Win or BattleState.Lose)
                return;
            
            if(currentEnemyTurnIdx >= activeEnemies.Count) {
                ChangeState(BattleState.Player_Turn);
                return;
            }

            EnemyCombat currentEnemy = activeEnemies[currentEnemyTurnIdx];
            if (currentEnemy != null && playerTarget != null) {
                currentEnemy.ExecuteTurn(playerTarget);
            } else 
                EndEnemyTurn();
        }

        private void EndEnemyTurn() {
            if (CurrentState != BattleState.Enemy_Turn)
                return;

            currentEnemyTurnIdx++;
            ExecuteNextEnemyTurn();
        }
        
        //TODO: Delete
        [ContextMenu("Debug: Force Win")]
        private void ForceWin() {
            EndBattle(true);
        }
    }
}
