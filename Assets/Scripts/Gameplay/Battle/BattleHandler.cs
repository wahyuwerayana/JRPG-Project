using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public enum BattleState {
        Starting,
        PlayerTurn,
        Attacking,
        EnemyTurn,
        Win,
        Lose,
    }
    
    public class BattleHandler : MonoBehaviour
    {
        private BattleState state;

        private void InitializeBattle() {
            state = BattleState.Starting;
            
            StartBattle();
        }
        
        private void StartBattle() {
            GameEventManager.Instance.BattleEvent.RaiseOnStart();
        }
        
        private void EndBattle(bool isWin) {
            if(isWin) {
                state = BattleState.Win;
                GameEventManager.Instance.BattleEvent.RaiseOnWin();
            } else {
                state = BattleState.Lose;
                GameEventManager.Instance.BattleEvent.RaiseOnLose();
            }
            
            GameEventManager.Instance.BattleEvent.RaiseOnEnd();
        }

        private void HandleStateChange(BattleState nextState) {
            state = nextState;
            
            
        }
    }
}
