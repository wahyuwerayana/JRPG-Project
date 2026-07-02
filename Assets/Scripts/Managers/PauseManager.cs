using System;
using Game.Settings;
using UnityEngine;

namespace Game.Managers {
    public class PauseManager : MonoBehaviour {
        [Header("Readers")]
        [SerializeField] private PlayerInputReader playerInputReader;
        [SerializeField] private UIInputReader uiInputReader;

        private bool isPaused = false;

        private void OnEnable() {
            playerInputReader.Pause += TogglePause;
            uiInputReader.Cancel += HandleUICancel;
        }

        private void OnDisable() {
            playerInputReader.Pause -= TogglePause;
            uiInputReader.Cancel -= HandleUICancel;
        }

        private void Start() {
            isPaused = false;
            Time.timeScale = 1f;
            
            InputManager.Instance.SetGameplayMode();
        }

        private void HandleUICancel() {
            if (!isPaused)
                return;
            
            TogglePause();
        }

        public void TogglePause() {
            isPaused = !isPaused;
            
            Time.timeScale = isPaused ? 0f : 1f;

            if (isPaused) {
                InputManager.Instance.SetUIMode();
            }
            else {
                InputManager.Instance.SetGameplayMode();
            }
            
            GameEventManager.Instance.GameStateEvent.RaiseOnPauseToggled(isPaused);
        }
    }
}