using System.Collections;
using System.Collections.Generic;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public enum BattleState {
        Initializing,
        PlayerTurn,
        SelectingTarget,
        Attacking,
        EnemyTurn,
        Win,
        Lose,
    }
    
    public class BattleHandler : MonoBehaviour {
        public static BattleHandler Instance { get; private set; }
        
        public BattleState CurrentState { get; private set; } = BattleState.Initializing;

        private EnemyAI currentEnemyTurn;
        private PlayerCombat playerTarget;
        
        private void Awake() {
            Instance = this;
        }

        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned += HandlePlayerSpawned;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned -= HandlePlayerSpawned;
        }
        
        private void HandlePlayerSpawned(PlayerCombat player) {
            playerTarget = player;
        }

        private void Start() {
            InitializeBattle();
        }
        
        private void InitializeBattle() {
            SceneController.SetActiveScene(SceneController.GetCurrentActiveScene());
            GameEventManager.Instance.BattleEvent.RaiseOnStart();
            
            ChangeState(BattleState.PlayerTurn);
        }
        
        private void EndBattle(bool isWin) {
            GameEventManager.Instance.BattleEvent.RaiseOnEnd();
            
            if(isWin) {
                CurrentState = BattleState.Win;
                StartCoroutine(WinBattleCoroutine());
                GameEventManager.Instance.BattleEvent.RaiseOnWin();
            } else {
                CurrentState = BattleState.Lose;
                GameEventManager.Instance.BattleEvent.RaiseOnLose();
            }
        }
        
        private IEnumerator WinBattleCoroutine() {
            yield return new WaitForSeconds(1f);
            SceneController.UnloadScene(SceneController.GetCurrentActiveScene());
        }

        public void ChangeState(BattleState nextState) {
            CurrentState = nextState;
            GameEventManager.Instance.BattleEvent.RaiseOnBattleStateChanged(nextState);
        }

        private IEnumerator EnemyTurnTransition() {
            yield return new WaitForSeconds(1f);
            ChangeState(BattleState.EnemyTurn);
            
            
        }

        public void EndEnemyTurn() {
            if (CurrentState != BattleState.EnemyTurn)
                return;
            
            ChangeState(BattleState.PlayerTurn);
        }
    }
}
