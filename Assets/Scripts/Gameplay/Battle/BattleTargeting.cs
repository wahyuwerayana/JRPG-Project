using Game.Managers;
using Game.Settings;
using UnityEngine;

namespace Game.Gameplay {
    public class BattleTargeting : MonoBehaviour {
        [Header("Targeting System")]
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private LayerMask enemyLayer;

        private PlayerCombat playerUnit;
        private SkillDataSO queuedSkill;

        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned += OnPlayerSpawned;
            GameEventManager.Instance.BattleEvent.OnPlayerActionSelected += HandlePlayerActionSelected;
            inputReader.Click += HandleTargetClick;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned -= OnPlayerSpawned;
            GameEventManager.Instance.BattleEvent.OnPlayerActionSelected -= HandlePlayerActionSelected;
            inputReader.Click -= HandleTargetClick;
        }

        private void OnPlayerSpawned(PlayerCombat spawnedPlayer) {
            playerUnit = spawnedPlayer;
        }
        
        private void HandlePlayerActionSelected(SkillDataSO selectedSkill) {
            if(BattleHandler.Instance.CurrentState != BattleState.PlayerTurn)
                return;
            
            BattleHandler.Instance.ChangeState(BattleState.SelectingTarget);
            queuedSkill = selectedSkill;
        }

        public void HandleEnemyAction() {
            if(BattleHandler.Instance.CurrentState != BattleState.EnemyTurn)
                return;

            
        }

        private void HandleTargetClick() {
            if(BattleHandler.Instance.CurrentState != BattleState.SelectingTarget)
                return;

            Vector2 screenPos = inputReader.PointerPosition;
            Ray ray = Camera.main.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, enemyLayer)) {
                if (hit.collider.TryGetComponent<EnemyCombat>(out EnemyCombat targetEnemy)) {
                    BattleHandler.Instance.ChangeState(BattleState.Attacking);
                    
                    playerUnit.UseSkill(queuedSkill, targetEnemy);
                    
                    // Wait for the skill to finish
                }
            }
        }
    }
}