using System;
using Game.Settings;
using UnityEngine;

namespace Game.Managers {
    public class PauseManager : MonoBehaviour {
        [SerializeField] private PlayerInputReader inputReader;
        
        private bool isPaused = false;

        private void OnEnable() {
            
        }

        private void OnDisable() {
            
        }

        public void TogglePause() {
            isPaused = !isPaused;
            
            Time.timeScale = isPaused ? 0f : 1f;
            
            GameEventManager.Instance.GameStateEvent.RaiseOnPauseToggled(isPaused);
        }

        public void ResumeGame() {
            if(isPaused)
                TogglePause();
        }
    }
}