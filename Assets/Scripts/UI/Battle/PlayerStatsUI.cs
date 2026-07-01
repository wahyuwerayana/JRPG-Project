using System;
using DG.Tweening;
using Game.Audio;
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
            GameEventManager.Instance.BattleEvent.OnUnitHealed += UpdateHealthUI;
            
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged += UpdateManaUI;
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned += Init;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnUnitDamaged -= UpdateHealthUI;
            GameEventManager.Instance.BattleEvent.OnUnitHealed -= UpdateHealthUI;
            
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
            DOTween.To(() => healthSlider.value, 
                x => healthText.text = $"{x:F0} / {healthSlider.maxValue:F0}", 
                statsHealth, 
                Const.Tween.DURATION)
                .OnUpdate(() =>
            {
                healthText.text = $"{healthSlider.value:F0} / {healthSlider.maxValue:F0}";
            });
            
            DOTween.To(() => manaSlider.value, 
                x => manaText.text = $"{x:F0} / {manaSlider.maxValue:F0}", 
                statsMp, 
                Const.Tween.DURATION)
                .OnUpdate(() =>
            {
                manaText.text = $"{manaSlider.value:F0} / {manaSlider.maxValue:F0}";
            });
        }

        private void UpdateHealthUI(UnitCombatBase unit, float currentHealth, float healthBefore) {
            if (unit is not PlayerCombat)
                return;
            
            healthSlider.DOValue(currentHealth, Const.Tween.DURATION).SetEase(Ease.OutQuad);
            UpdateText(currentHealth, manaSlider.value);
        }
        
        private void UpdateManaUI(UnitCombatBase unit, float currentMana, float mpBefore) {
            if (unit is not PlayerCombat)
                return;
            
            manaSlider.DOValue(currentMana, Const.Tween.DURATION).SetEase(Ease.OutQuad);
            UpdateText(healthSlider.value, currentMana);
        }
    }
}