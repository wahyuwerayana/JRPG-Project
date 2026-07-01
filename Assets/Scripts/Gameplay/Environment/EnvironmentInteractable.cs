using System;
using Eflatun.SceneReference;
using Game.Gameplay;
using Game.UI;
using UnityEngine;

namespace Gameplay.Environment {
    public class EnvironmentInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private SceneReference sceneToLoad;
        [SerializeField] private FadeOverlayHandler fader;

        public async void Interact() {
            try {
                await SceneController.LoadSceneWithFade(sceneToLoad, fader);
            } catch (Exception e) {
                throw; // TODO handle exception
            }
        }
        
        public bool IsAvailableForInteract() {
            return sceneToLoad != null;
        }
    }
}