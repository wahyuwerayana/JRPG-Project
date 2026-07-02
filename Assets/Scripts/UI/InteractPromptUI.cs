using System.Collections.Generic;
using DG.Tweening;
using Game.Gameplay;
using Game.Managers;
using UnityEngine;

namespace Game.UI {
    public class InteractPromptUI : MonoBehaviour {
        private CanvasGroup canvasGroup;

        private int interactableCount = 0;
        private bool isInteracting = false;
        
        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
        }

        private void OnEnable() {
            GameEventManager.Instance.PlayerEvent.OnInteractableInRangeChanged += HandleInteractableInRangeChanged;
            GameEventManager.Instance.PlayerEvent.OnInteractStarted += HandleInteractStarted;
            GameEventManager.Instance.PlayerEvent.OnInteractEnded += HandleInteractEnded;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.PlayerEvent.OnInteractableInRangeChanged -= HandleInteractableInRangeChanged;
            GameEventManager.Instance.PlayerEvent.OnInteractStarted -= HandleInteractStarted;
            GameEventManager.Instance.PlayerEvent.OnInteractEnded -= HandleInteractEnded;

            canvasGroup.DOKill();
        }

        private void HandleInteractableInRangeChanged(List<IInteractable> interactables) {
            interactableCount = interactables.Count;

            RefreshUI();
        }
        
        private void HandleInteractStarted() {
            isInteracting = true;
            RefreshUI();
        }
        
        private void HandleInteractEnded() {
            isInteracting = false;
            RefreshUI();
        }
        
        private void RefreshUI() {
            bool shouldShow = interactableCount > 0 && !isInteracting;
            SetInteractUI(shouldShow);
        }

        private void SetInteractUI(bool isEnabled) {
            canvasGroup.DOKill();
            
            canvasGroup.DOFade(isEnabled ? 1f : 0f, Game.Const.Tween.FADE_INTERACT_DURATION)
                .SetEase(Ease.InOutQuad);
        }
    }
}