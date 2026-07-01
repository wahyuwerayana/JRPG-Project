using System;
using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public class BattleAudioHandler : MonoBehaviour {
        [Header("Geneic Status SFX")]
        [SerializeField] private AudioClip healSFX;
        [SerializeField] private AudioClip damagedSFX;
        [SerializeField] private AudioClip mpGainSFX;

        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnUnitHealed += HandleUnitHealed;
            GameEventManager.Instance.BattleEvent.OnUnitDamaged += HandleUnitDamaged;
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged += HandleUnitMPChanged;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnUnitHealed -= HandleUnitHealed;
            GameEventManager.Instance.BattleEvent.OnUnitDamaged -= HandleUnitDamaged;
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged -= HandleUnitMPChanged;
        }
        
        private void HandleUnitDamaged(UnitCombatBase unit, float currentHealth, float healthBeforeDamaged) {
            if(damagedSFX != null)
                AudioManager.Instance.PlaySFX(damagedSFX);
        }
        
        private void HandleUnitHealed(UnitCombatBase unit, float currentHealth, float healedAmount) {
            if(healSFX != null)
                AudioManager.Instance.PlaySFX(healSFX);
        }
        
        private void HandleUnitMPChanged(UnitCombatBase unit, float currentMP, float mpBeforeUsed) {
            if (!(currentMP > mpBeforeUsed)) 
                return;

            if(mpGainSFX != null)
                AudioManager.Instance.PlaySFX(mpGainSFX);
        }
    }
}