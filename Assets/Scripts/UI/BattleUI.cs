using System;
using Game.Gameplay;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class BattleUI : MonoBehaviour {
        [Header("Player Stats")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Slider manaSlider;

        [Header("Enemy Stats")]
        [SerializeField] private GameObject enemyInfoUI;
        
        [Header("Buttons")]
        [SerializeField] private Button attackButton;
        [SerializeField] private Button skillButton;
        [SerializeField] private Button itemButton;
        [SerializeField] private Button runButton;

        private void Awake() {
            //GameEventManager.Instance.BattleEvent.OnUnitDamaged += UpdateHealthUI;
            //GameEventManager.Instance.BattleEvent.OnUnitHealed += UpdateHealthUI;
        }

        public void InitializeUI(PlayerCombat player) {
            //healthSlider.maxValue = player
            
            //attackButton.onClick.AddListener(() => player.Attack());
            //skillButton.onClick.AddListener(() => player.UseSkill());
            //itemButton.onClick.AddListener(() => player.UseItem());
            runButton.onClick.AddListener(player.TryRun);
        }
    }
}