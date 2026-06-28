using System.Collections.Generic;
using Game.Managers;
using Game.Settings;
using UnityEngine;

namespace Game.Gameplay {
    public class PlayerInteraction : MonoBehaviour {
        [SerializeField] private PlayerInputReader inputReader;
        
        private readonly List<IInteractable> interactablesInRange = new List<IInteractable>();
        
        private void OnEnable() {
            inputReader.Interact += TryInteract;
        }
        
        private void OnDisable() {
            inputReader.Interact -= TryInteract;
        }

        private void TryInteract() {
            if(interactablesInRange.Count == 0)
                return;

            IInteractable closestInteractable = GetClosestInteractable();
            closestInteractable.Interact();
            GameEventManager.Instance.PlayerEvent.RaiseOnInteractStarted(closestInteractable);
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent<IInteractable>(out IInteractable interactable))
                return;
            
            if (interactablesInRange.Contains(interactable))
                return;
            
            interactablesInRange.Add(interactable);
        }

        private void OnTriggerExit(Collider other) {
            if (!other.TryGetComponent<IInteractable>(out IInteractable interactable))
                return;
            
            if (!interactablesInRange.Contains(interactable))
                return;

            interactablesInRange.Remove(interactable);
        }
        
        private IInteractable GetClosestInteractable() {
            IInteractable closestInteractable = null;
            float closestDistance = float.MaxValue;
            Vector3 playerPosition = transform.position;

            foreach (IInteractable interactable in interactablesInRange) {
                if(interactable is not MonoBehaviour interactableMB)
                    continue;
                
                float distance = Vector3.SqrMagnitude(playerPosition - interactableMB.transform.position);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }

            return closestInteractable;
        }
    }
}