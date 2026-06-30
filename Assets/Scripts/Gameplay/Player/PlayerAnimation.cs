using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public class PlayerAnimation : MonoBehaviour {
        private Animator animator;

        private readonly int walkHash = Animator.StringToHash("isWalking");

        private void Start() {
            animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable() {
            GameEventManager.Instance.PlayerEvent.OnMoveStarted += StartWalkAnimation;
            GameEventManager.Instance.PlayerEvent.OnMoveEnded += StopWalkAnimation;
        }

        private void OnDisable() {
            GameEventManager.Instance.PlayerEvent.OnMoveStarted -= StartWalkAnimation;
            GameEventManager.Instance.PlayerEvent.OnMoveEnded -= StopWalkAnimation;
        }

        private void StartWalkAnimation() {
            animator.SetBool(walkHash, true);
        }
        
        private void StopWalkAnimation() {
            animator.SetBool(walkHash, false);
        }
    }
}