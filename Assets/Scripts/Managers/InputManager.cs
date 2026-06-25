using Game.Settings;
using UnityEngine;

namespace Game.Managers {
    public class InputManager : MonoBehaviour {
        public static InputManager Instance { get; private set; }

        [SerializeField] private PlayerInputReader playerInputReader;

        private void Awake() {
            if(Instance != null) {
                Destroy(this);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void OnEnable() {
            SetPlayerInput(true);
        }
        
        private void OnDisable() {
            SetPlayerInput(false);
        }

        public void SetPlayerInput(bool isEnabled) {
            if(isEnabled)
                playerInputReader.EnablePlayerActions();
            else
                playerInputReader.DisablePlayerActions();
        }
    }
}