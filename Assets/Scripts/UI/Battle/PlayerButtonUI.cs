using Game.Gameplay;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class PlayerButtonUI : MonoBehaviour {
        [SerializeField] private CanvasGroup buttonCanvasGroup;
        
        [Header("Buttons")]
        [SerializeField] private Button attackButton;
        [SerializeField] private Button skillButton;
        [SerializeField] private Button itemButton;
        [SerializeField] private Button runButton;

        private void Awake() {
            //GameEventManager.Instance.BattleEvent.OnUnitDamaged += UpdateHealthUI;
            //GameEventManager.Instance.BattleEvent.OnUnitHealed += UpdateHealthUI;
        }

        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned += Init;
        }

        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned -= Init;
        }

        private void Init(PlayerCombat player) {
            //healthSlider.maxValue = player
            
            attackButton.onClick.AddListener(() => GameEventManager.Instance.BattleEvent.RaiseOnPlayerActionSelected(player.Stats.BasicAttack));
            //skillButton.onClick.AddListener(() => player.UseSkill());
            //itemButton.onClick.AddListener(() => player.UseItem());
            runButton.onClick.AddListener(player.TryRun);
        }

        public void ToggleButtons(bool isInteractable) {
            buttonCanvasGroup.interactable = isInteractable;
            buttonCanvasGroup.blocksRaycasts = !isInteractable;
        }
    }
}