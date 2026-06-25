using Game.Managers;
using Game.Settings;
using UnityEngine;

namespace Game.Gameplay {
    public class PlayerInteraction : MonoBehaviour {
        [SerializeField] private PlayerInputReader inputReader;
        
        private readonly Collider[] hitColliders = new Collider[1];
        
        private void OnEnable() {
            inputReader.Interact += TryInteract;
        }
        
        private void OnDisable() {
            inputReader.Interact -= TryInteract;
        }

        private void TryInteract() {
            Physics.OverlapSphereNonAlloc(transform.position, 2f, hitColliders);

            if (hitColliders[0] == null)
                return;

            if (hitColliders[0].TryGetComponent<IInteractable>(out IInteractable interactable)) {
                interactable.Interact();
                GameEventManager.Instance.PlayerEvent.RaiseOnInteractStarted(interactable);
            }
        }

        private void OnTriggerEnter(Collider other) {
            Debug.Log($"Player entered trigger with {other.gameObject.name}");
        }
        
        private void OnTriggerExit(Collider other) {
            Debug.Log($"Player exited trigger with {other.gameObject.name}");
        }
    }
}