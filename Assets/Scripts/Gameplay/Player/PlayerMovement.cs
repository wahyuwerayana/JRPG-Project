using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public class PlayerMovement : MonoBehaviour {
        private Vector2 moveInput;

        private void OnEnable() {
            GameEventManager.Instance.PlayerEvent.OnMove += ReadMoveInput;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.PlayerEvent.OnMove -= ReadMoveInput;
        }

        private void ReadMoveInput(Vector2 direction) => moveInput = direction;

        private void Update() {
            Move();
        }

        private void Move() {
            if (moveInput == Vector2.zero) 
                return;
            
            Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
            // transform.Translate(direction * (player.Unit.MoveSpeed * Time.deltaTime));
        }
    }
}