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
        
        private void EndBattle() {
            GameEventManager.Instance.BattleEvent.RaiseOnEnd();
        }
    }
}
