using System.Collections.Generic;
using System.Linq;
using Game.Managers;
using Game.Settings;
using UnityEngine;

namespace Game.Gameplay {
    public class PlayerInteraction : MonoBehaviour {
        [SerializeField] private PlayerInputReader inputReader;
        
        private readonly List<IInteractable> interactablesInRange = new List<IInteractable>();

        private int interactionLockCount = 0;
        
        private void OnEnable() {
            inputReader.Interact += TryInteract;
            
            GameEventManager.Instance.BattleEvent.OnStart += LockInteraction;
            GameEventManager.Instance.BattleEvent.OnEnd += UnlockInteraction;
            
            GameEventManager.Instance.PlayerEvent.OnInteractStarted += LockInteraction;
            GameEventManager.Instance.PlayerEvent.OnInteractEnded += UnlockInteraction;
        }
        
        private void OnDisable() {
            inputReader.Interact -= TryInteract;
            
            GameEventManager.Instance.BattleEvent.OnStart -= LockInteraction;
            GameEventManager.Instance.BattleEvent.OnEnd -= UnlockInteraction;
            
            GameEventManager.Instance.PlayerEvent.OnInteractStarted -= LockInteraction;
            GameEventManager.Instance.PlayerEvent.OnInteractEnded -= UnlockInteraction;
        }

        private void TryInteract() {
            if(interactionLockCount > 0)
                return;
            
            if(interactablesInRange.Count == 0)
                return;

            IInteractable closestInteractable = GetClosestInteractable();

            if (closestInteractable != null) {
                closestInteractable.Interact();
                GameEventManager.Instance.PlayerEvent.RaiseOnInteractStarted();
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent<IInteractable>(out IInteractable interactable))
                return;
            
            if (interactablesInRange.Contains(interactable))
                return;

            if (!interactable.IsAvailableForInteract())
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

            interactablesInRange.RemoveAll(item => item == null || item.Equals(null));

            foreach(IInteractable interactable in interactablesInRange.Where(interactable => interactable.IsAvailableForInteract())) {
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

        #region Locking

        private void LockInteraction() {
            interactionLockCount++;
        }

        private void UnlockInteraction() {
            interactionLockCount--;
            interactionLockCount = Mathf.Max(0, interactionLockCount);
        }

        #endregion
    }
}