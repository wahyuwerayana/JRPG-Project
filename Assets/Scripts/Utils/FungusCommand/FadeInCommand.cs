using System.Collections;
using Fungus;
using Game.UI;
using UnityEngine;

namespace Game.Utils.FungusCommand {
    [CommandInfo("UI", "Fade In", "Fades in the UI.")]
    public class FadeInCommand : Command {
        [SerializeField] private FadeOverlayHandler fader;
        [SerializeField] private bool waitForFade;
        [SerializeField] private float fadeDuration = 1f;
        
        public override void OnEnter() {
            if (waitForFade) {
                StartCoroutine(FadeInWithWait());
                return;
            }
            
            fader.FadeIn(fadeDuration);
            Continue();
        }
        
        private IEnumerator FadeInWithWait() {
            yield return fader.FadeInAsync(fadeDuration);
            Continue();
        }
    }
}