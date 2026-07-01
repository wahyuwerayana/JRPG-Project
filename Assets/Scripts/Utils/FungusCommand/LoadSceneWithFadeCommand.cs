using Eflatun.SceneReference;
using Fungus;
using Game.UI;
using UnityEngine;

namespace Game.Utils.FungusCommand {
    [CommandInfo("Scene", "Load Scene With Fade", "Loads a scene with fade.")]
    public class LoadSceneWithFadeCommand : Command {
        [SerializeField] private SceneReference sceneToLoad;
        [SerializeField] private FadeOverlayHandler fader;

        public override void OnEnter() {
            _ = SceneController.LoadSceneWithFade(sceneToLoad, fader);
            Continue();
        }
    }
}