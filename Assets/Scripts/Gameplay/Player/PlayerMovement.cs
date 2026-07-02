using Game.Managers;
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

        private bool isMoving = false;
        private int movementLockCount = 0;

        private void Awake() {
            playerData = GetComponent<UnitDataContainer>().Data;
            characterController = GetComponent<CharacterController>();
            mainCameraTransform = Camera.main?.transform;
        }

        private void OnEnable() {
            inputReader.Move += ReadMoveInput;

            GameEventManager.Instance.BattleEvent.OnStarted += LockMovement;
            GameEventManager.Instance.BattleEvent.OnEnded += UnlockMovement;

            GameEventManager.Instance.PlayerEvent.OnInteractStarted += LockMovement;
            GameEventManager.Instance.PlayerEvent.OnInteractEnded += UnlockMovement;
        }
        
        private void OnDisable() {
            inputReader.Move -= ReadMoveInput;
            
            GameEventManager.Instance.BattleEvent.OnStarted -= LockMovement;
            GameEventManager.Instance.BattleEvent.OnEnded -= UnlockMovement;

            GameEventManager.Instance.PlayerEvent.OnInteractStarted -= LockMovement;
            GameEventManager.Instance.PlayerEvent.OnInteractEnded -= UnlockMovement;
        }

        private void ReadMoveInput(Vector2 direction) {
            if(movementLockCount > 0)
                return;

            currentDirection = direction;
        }

        private void Update() {
            ApplyGravity();
            Move();
        }

        private void Move() {
            if (currentDirection == Vector2.zero || movementLockCount > 0) {
                if (isMoving)
                    StopMovementAndAnimation();
                
                return;
            }
            
            if(!isMoving){
                isMoving = true;
                GameEventManager.Instance.PlayerEvent.RaiseOnMoveStarted();
            }
            
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

        private void StopMovementAndAnimation() {
            currentDirection = Vector2.zero;

            if (isMoving) {
                isMoving = false;
                GameEventManager.Instance.PlayerEvent.RaiseOnMoveEnded();
            }
        }

        #region Locking

        private void LockMovement() {
            movementLockCount++;
            StopMovementAndAnimation();
        }

        private void UnlockMovement() {
            movementLockCount--;
            movementLockCount = Mathf.Max(0, movementLockCount);
        }

        #endregion

    }
}