using Game.Events;
using Game.Settings;
using UnityEngine;

namespace Game.Managers {
    public class GameEventManager : MonoBehaviour {
        public static GameEventManager Instance { get; private set; }
        
        //Gameplay
        public BattleEvent BattleEvent { get; private set; } = new BattleEvent();
        public PlayerEvent PlayerEvent { get; private set; } = new PlayerEvent();
        public EnemyEvent EnemyEvent { get; private set; } = new EnemyEvent();
        
        public MainInput Input { get; private set; }

        private void Awake() {
            if(Instance != null) {
                Destroy(this);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(this);
            
            Input = new MainInput();
        }

        private void OnEnable() {
            Input.Enable();
        }
        
        private void OnDisable() {
            Input.Disable();
        }
    }
}