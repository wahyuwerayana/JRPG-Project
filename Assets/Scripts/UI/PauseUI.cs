using System;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class PauseUI : MonoBehaviour {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button exitButton;
        private void OnEnable() {
            GameEventManager.Instance.GameStateEvent.OnPauseToggled += HandlePauseToggle;
        }

        private void OnDisable() {
            GameEventManager.Instance.GameStateEvent.OnPauseToggled -= HandlePauseToggle;
        }

        private void HandlePauseToggle(bool isPaused) {
            
        }

        public void ResumeGame() {
            Debug.Log("Resuming game...");
            GameEventManager.Instance.GameStateEvent.RaiseOnPauseToggled(false);
        }
        
        public void ExitGame() {
            Debug.Log("Exiting game...");
        }
    }
}