using Game.Events;
using UnityEngine;

namespace Game.Managers {
    public class GameEventManager : MonoBehaviour {
        public static GameEventManager Instance { get; private set; }
        
        //Gameplay
        public PlayerEvent PlayerEvent { get; private set; } = new PlayerEvent();
        public BattleEvent BattleEvent { get; private set; } = new BattleEvent();
        public EnemyEvent EnemyEvent { get; private set; } = new EnemyEvent();

        private void Awake() {
            if(Instance != null) {
                Destroy(this);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}