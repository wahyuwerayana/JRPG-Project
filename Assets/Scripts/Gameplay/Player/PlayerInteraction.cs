using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public class PlayerInteraction : MonoBehaviour {
        private void OnEnable() {
            GameEventManager.Instance.PlayerEvent.OnInteract += TryInteract;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.PlayerEvent.OnInteract -= TryInteract;
        }

        private void TryInteract() {
            
        }
    }
}