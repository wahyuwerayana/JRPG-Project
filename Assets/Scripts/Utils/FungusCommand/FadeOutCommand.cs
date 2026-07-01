using System.Collections;
using Fungus;
using Game.UI;
using UnityEngine;

namespace Game.Utils.FungusCommand {
    [CommandInfo("UI", "Fade Out", "Fades out the UI.")]
    public class FadeOutCommand : Command {
        [SerializeField] private FadeOverlayHandler fader;
        [SerializeField] private bool waitForFade;
        [SerializeField] private float fadeDuration = 1f;
        
        public override void OnEnter() {
            if (waitForFade) {
                StartCoroutine(FadeOutWithWait());
                return;
            }
                
            fader.FadeOut(fadeDuration);
            Continue();
        }
        
        private IEnumerator FadeOutWithWait() {
            yield return fader.FadeOutAsync(fadeDuration);
            Continue();
        }
    }
}