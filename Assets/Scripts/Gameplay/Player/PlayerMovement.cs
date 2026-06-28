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
        private Transform mainCameraTransform;

        private void Awake() {
            playerData = GetComponent<UnitDataContainer>().Data;
            characterController = GetComponent<CharacterController>();
            mainCameraTransform = Camera.main?.transform;
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
            ApplyGravity();
            Move();
        }

        private void Move() {
            if (currentDirection == Vector2.zero) 
                return;
            
            Vector3 cameraForward = mainCameraTransform.forward;
            Vector3 cameraRight = mainCameraTransform.right;
            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();
            
            Vector3 moveDirection = cameraForward * currentDirection.y + cameraRight * currentDirection.x;
            
            characterController.Move(moveDirection * (playerData.MoveSpeed * Time.deltaTime));
            
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerData.RotationSpeed * Time.deltaTime);
        }
        
        private void ApplyGravity() {
            if(!characterController.isGrounded)
                characterController.Move(Physics.gravity * Time.deltaTime);
        }
    }
}