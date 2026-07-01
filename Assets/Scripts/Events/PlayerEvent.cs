using Game.Gameplay;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Events
{
    public class PlayerEvent {
        public event UnityAction OnMoveStarted;
        public event UnityAction OnMoveEnded;
        
        /// <summary>
        /// Raised when the player successfully interacted to an IInteractable object
        /// </summary>
        public event UnityAction OnInteractStarted;
        
        /// <summary>
        /// Raised when the player stopped interacting to an IInteractable object
        /// </summary>
        public event UnityAction OnInteractEnded;

        public event UnityAction OnInventoryChanged;
        
        // Battle
        public event UnityAction<SkillDataSO> OnPlayerActionSelected;
        public event UnityAction<ItemSO> OnPlayerItemSelected;
        
        public void RaiseOnInteractStarted() {
            OnInteractStarted?.Invoke();
        }
        
        public void RaiseOnInteractEnded() {
            OnInteractEnded?.Invoke();
        }
        
        public void RaiseOnMoveStarted() {
            OnMoveStarted?.Invoke();
        }
        
        public void RaiseOnMoveEnded() {
            OnMoveEnded?.Invoke();
        }
        
        public void RaiseOnInventoryChanged() {
            OnInventoryChanged?.Invoke();
        }
        
        public void RaiseOnPlayerActionSelected(SkillDataSO skillData) {
            OnPlayerActionSelected?.Invoke(skillData);
        }

        public void RaiseOnPlayerItemSelected(ItemSO item) {
            OnPlayerItemSelected?.Invoke(item);
        }
    }
}
