using Eflatun.SceneReference;
using Game.Gameplay;
using Game.UI;
using UnityEngine;

namespace Gameplay.Environment {
    public class EnvironmentInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private SceneReference sceneToLoad;
        [SerializeField] private FadeOverlayHandler fader;

        public void Interact() {
            _ = SceneController.LoadSceneWithFade(sceneToLoad, fader);
        }
        
        public bool IsAvailableForInteract() {
            return sceneToLoad != null;
        }
    }
}