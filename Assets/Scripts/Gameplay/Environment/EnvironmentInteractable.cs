using Eflatun.SceneReference;
using Game.Gameplay;
using UnityEngine;

namespace Gameplay.Environment {
    public class EnvironmentInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private SceneReference sceneToLoad;
        
        public void Interact() {
            SceneController.LoadScene(sceneToLoad);
        }
    }
}