using Game.Gameplay;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class PlayerButtonUIHandler : UIHandler {
        [Header("Buttons")]
        [SerializeField] private Button attackButton;
        [SerializeField] private Button skillButton;
        [SerializeField] private Button itemButton;
        [SerializeField] private Button runButton;
        
        [Header("References")]
        [SerializeField] private SkillUIHandler skillUIHandler;
        [SerializeField] private ItemUIHandler itemUIHandler;

        protected override void OnEnable() {
            base.OnEnable();
            
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned += Init;
            GameEventManager.Instance.BattleEvent.OnBattleStateChanged += OnPlayerTurn;
        }

        protected override void OnDisable() {
            base.OnDisable();
            
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned -= Init;
            GameEventManager.Instance.BattleEvent.OnBattleStateChanged -= OnPlayerTurn;
        }

        private void Init(PlayerCombat player) {
            attackButton.onClick.AddListener(() => GameEventManager.Instance.BattleEvent.RaiseOnPlayerActionSelected(player.Stats.BasicAttack));
            skillButton.onClick.AddListener(() => skillUIHandler.ToggleUI(true));
            itemButton.onClick.AddListener(() => itemUIHandler.ToggleUI(true));
            runButton.onClick.AddListener(player.TryRun);
        }

        private void OnPlayerTurn(BattleState state) {
            if(state != BattleState.PlayerTurn)
                return;
            
            skillUIHandler.ToggleUI(false);
            itemUIHandler.ToggleUI(false);
            ToggleUI(true);
        }
    }
}