using Game.Gameplay;
using Game.Managers;
using UnityEngine;

namespace Game.UI {
    public abstract class UIHandler : MonoBehaviour {
        [SerializeField] private CanvasGroup UICanvasGroup;

        protected virtual void OnEnable() {
            GameEventManager.Instance.PlayerEvent.OnPlayerActionSelected += HandlePlayerActionSelected;
            GameEventManager.Instance.PlayerEvent.OnPlayerItemSelected += HandlePlayerItemSelected;
        }
        
        protected virtual void OnDisable() {
            GameEventManager.Instance.PlayerEvent.OnPlayerActionSelected -= HandlePlayerActionSelected;
            GameEventManager.Instance.PlayerEvent.OnPlayerItemSelected -= HandlePlayerItemSelected;
        }
        
        private void HandlePlayerActionSelected(SkillDataSO skill) {
            ToggleUI(false);
        }

        private void HandlePlayerItemSelected(ItemSO item) {
            ToggleUI(false);
        }

        public void ToggleUI(bool isInteractable) {
            UICanvasGroup.alpha = isInteractable ? 1f : 0f;
            UICanvasGroup.interactable = isInteractable;
            UICanvasGroup.blocksRaycasts = isInteractable;
        }
        
        public bool IsInteractable => UICanvasGroup.interactable;
    }
}