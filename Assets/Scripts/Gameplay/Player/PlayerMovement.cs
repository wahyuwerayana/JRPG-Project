using Game.Settings;
using UnityEngine;

namespace Game.Gameplay {
    [RequireComponent(typeof(UnitDataContainer))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] private PlayerInputReader inputReader;
        
        private UnitDataSO playerData;
        private Vector2 currentDirection;
        private CharacterController characterController;
        private Camera mainCamera;

        private void Awake() {
            playerData = GetComponent<UnitDataContainer>().UnitData;
            characterController = GetComponent<CharacterController>();
            mainCamera = Camera.main;
        }

        private void OnEnable() {
            inputReader.Move += ReadMoveInput;
        }
        
        private void OnDisable() {
            inputReader.Move -= ReadMoveInput;
        }

        private void ReadMoveInput(Vector2 direction) {
            currentDirection = direction;
        }

        private void Update() {
            Move();
        }

        private void Move() {
            if (currentDirection == Vector2.zero) 
                return;
            
            Vector3 direction = new Vector3(currentDirection.x, 0f, currentDirection.y).normalized;
            
            characterController.Move(direction * (playerData.MoveSpeed * Time.deltaTime));
        }
    }
}