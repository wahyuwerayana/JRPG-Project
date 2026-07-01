using Game.Gameplay;
using Game.Managers;
using UnityEngine;

namespace Game.UI {
    public abstract class UIHandler : MonoBehaviour {
        [SerializeField] private CanvasGroup UICanvasGroup;

        protected virtual void OnEnable() {
            GameEventManager.Instance.PlayerEvent.OnPlayerActionSelected += HandlePlayerActionSelected;
        }
        
        protected virtual void OnDisable() {
            GameEventManager.Instance.PlayerEvent.OnPlayerActionSelected -= HandlePlayerActionSelected;
        }
        
        private void HandlePlayerActionSelected(SkillDataSO skill) {
            ToggleUI(false);
        }

        public void ToggleUI(bool isInteractable) {
            UICanvasGroup.alpha = isInteractable ? 1f : 0f;
            UICanvasGroup.interactable = isInteractable;
            UICanvasGroup.blocksRaycasts = isInteractable;
        }
    }
}