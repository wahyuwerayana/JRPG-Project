using Eflatun.SceneReference;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    [RequireComponent(typeof(CanvasGroup))]
    public class PauseUI : MonoBehaviour {
        [Header("UI References")]
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button exitButton;
        
        [Header("Scene Reference")]
        [SerializeField] private SceneReference mainMenuScene;
        
        private CanvasGroup canvasGroup;
        private PauseManager pauseManager;

        private bool isExiting = false;
        
        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
            pauseManager = GetComponent<PauseManager>();
        }
        
        private void OnEnable() {
            GameEventManager.Instance.GameStateEvent.OnPauseToggled += HandlePauseToggle;
        }

        private void OnDisable() {
            GameEventManager.Instance.GameStateEvent.OnPauseToggled -= HandlePauseToggle;
        }

        private void Start() {
            SetUI(false);
            
            resumeButton.onClick.AddListener(ResumeGame);
            exitButton.onClick.AddListener(ExitGame);
        }

        private void HandlePauseToggle(bool isPaused) {
            SetUI(isPaused);
        }

        private void SetUI(bool isVisible) {
            canvasGroup.alpha = isVisible ? 1f : 0f;
            canvasGroup.interactable = isVisible;
            canvasGroup.blocksRaycasts = isVisible;
        }

        private void ResumeGame() {
            pauseManager.TogglePause();
        }
        
        private void ExitGame() {
            if (isExiting)
                return;
            
            isExiting = true;
            
            Time.timeScale = 1f;
            
            FadeOverlayHandler fader = FindAnyObjectByType<FadeOverlayHandler>();
            
            _ = fader != null ? SceneController.LoadSceneWithFade(mainMenuScene, fader) : SceneController.LoadSceneAndSetActive(mainMenuScene);
            
            SetUI(false);
        }
    }
}