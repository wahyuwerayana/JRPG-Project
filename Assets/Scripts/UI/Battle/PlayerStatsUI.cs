using System;
using Game.Gameplay;
using Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Game.UI {
    public class PlayerStatsUI : MonoBehaviour {
        [Header("Health")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TMP_Text healthText;
        
        [Header("Mana")]
        [SerializeField] private Slider manaSlider;
        [SerializeField] private TMP_Text manaText;

        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnUnitDamaged += UpdateHealthUI;
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged += UpdateManaUI;
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned += Init;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnUnitDamaged -= UpdateHealthUI;
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged -= UpdateManaUI;
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned -= Init;
        }

        public void Init(PlayerCombat playerUnit) {
            healthSlider.maxValue = playerUnit.Stats.Health;
            healthSlider.value = playerUnit.Stats.Health;

            manaSlider.maxValue = playerUnit.Stats.MP;
            manaSlider.value = playerUnit.Stats.MP;

            UpdateText(playerUnit.Stats.Health, playerUnit.Stats.MP);
        }
        
        private void UpdateText(float statsHealth, float statsMp) {
            healthText.text = $"{statsHealth:F0} / {healthSlider.maxValue:F0}";
            manaText.text = $"{statsMp:F0} / {manaSlider.maxValue:F0}";
        }

        private void UpdateHealthUI(UnitCombatBase unit, float currentHealth, float damageAmount) {
            if (unit is not PlayerCombat)
                return;
            
            healthSlider.value = currentHealth;
            UpdateText(currentHealth, manaSlider.value);
        }
        
        private void UpdateManaUI(UnitCombatBase unit, float currentMana, float manaUsed) {
            if (unit is not PlayerCombat)
                return;

            manaSlider.value = currentMana;
            UpdateText(healthSlider.value, currentMana);
        }
    }
}