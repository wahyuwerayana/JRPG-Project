using DG.Tweening;
using UnityEngine;

namespace Game.UI {
    public class FadeOverlayHandler : MonoBehaviour {
        [SerializeField] private CanvasGroup fadeOverlay;
        [SerializeField] private bool fadeOnStart = true;
        
        private void Start() {
            if (fadeOnStart)
                FadeIn();
        }

        public void FadeIn(float duration = Game.Const.Tween.FADE_GENERAL_DURATION) {
            fadeOverlay.alpha = 1f;
            fadeOverlay.gameObject.SetActive(true);
            fadeOverlay.DOFade(0f, duration).OnComplete(() => fadeOverlay.gameObject.SetActive(false));
        }

        public void FadeOut(float duration = Game.Const.Tween.FADE_GENERAL_DURATION) {
            fadeOverlay.alpha = 0f;
            fadeOverlay.gameObject.SetActive(true);
            fadeOverlay.DOFade(1f, duration);
        }

        public async Awaitable FadeInAsync(float duration = Game.Const.Tween.FADE_GENERAL_DURATION) {
            fadeOverlay.alpha = 1f;
            fadeOverlay.gameObject.SetActive(true);
            await fadeOverlay.DOFade(0f, duration).AsyncWaitForCompletion();
            fadeOverlay.gameObject.SetActive(false);
        }

        public async Awaitable FadeOutAsync(float duration = Game.Const.Tween.FADE_GENERAL_DURATION) {
            fadeOverlay.alpha = 0f;
            fadeOverlay.gameObject.SetActive(true);
            await fadeOverlay.DOFade(1f, duration).AsyncWaitForCompletion();
        }
    }
}