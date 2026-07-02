using System;
using Game.Settings;
using UnityEngine;

namespace Game.Managers {
    public class InputManager : MonoBehaviour {
        public static InputManager Instance { get; private set; }

        [Header("Input Readers")]
        [SerializeField] private PlayerInputReader playerInputReader;
        [SerializeField] private UIInputReader uiInputReader;

        private MainInput mainInput;

        private void Awake() {
            if(Instance != null) {
                Destroy(this);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(this);

            mainInput = new MainInput();
            
            playerInputReader.Initialize(mainInput);
            uiInputReader.Initialize(mainInput);
        }
        
        private void OnDisable() {
            playerInputReader.DisablePlayer();
            uiInputReader.DisableUI();
        }

        private void Start() {
            SetGameplayMode();
        }

        public void SetGameplayMode() {
            uiInputReader.DisableUI();
            playerInputReader.EnablePlayer();
        }
        
        public void SetUIMode() {
            playerInputReader.DisablePlayer();
            uiInputReader.EnableUI();
        }
    }
}