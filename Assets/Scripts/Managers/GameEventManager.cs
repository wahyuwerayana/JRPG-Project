using Game.Events;
using UnityEngine;

namespace Game.Managers {
    public class GameEventManager : MonoBehaviour {
        public static GameEventManager Instance { get; private set; }
        
        //Gameplay
        public BattleEvent BattleEvent { get; private set; } = new BattleEvent();
        public PlayerEvent PlayerEvent { get; private set; } = new PlayerEvent();
        public EnemyEvent EnemyEvent { get; private set; } = new EnemyEvent();
        
        //GameState
        public GameStateEvent GameStateEvent { get; private set; } = new GameStateEvent();

        private void Awake() {
            if(Instance != null) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}