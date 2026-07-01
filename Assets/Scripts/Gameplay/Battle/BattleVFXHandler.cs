using Game.Managers;
using UnityEngine;

namespace Game.Gameplay {
    public class BattleVFXHandler : MonoBehaviour {
        [Header("VFX References")]
        [SerializeField] private GameObject defaultDamagedVFX;
        [SerializeField] private GameObject healedVFX;
        [SerializeField] private GameObject mpGainedVFX;
        
        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnUnitAttacked += HandleUnitAttacked;
            GameEventManager.Instance.BattleEvent.OnUnitDamaged += HandleUnitDamaged;
            GameEventManager.Instance.BattleEvent.OnUnitHealed += HandleUnitHealed;
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged += HandleUnitMPChanged;
        }
        
        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnUnitAttacked -= HandleUnitAttacked;
            GameEventManager.Instance.BattleEvent.OnUnitDamaged -= HandleUnitDamaged;
            GameEventManager.Instance.BattleEvent.OnUnitHealed -= HandleUnitHealed;
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged -= HandleUnitMPChanged;
        }
        
        private void SpawnVFX(GameObject vfxPrefab, Vector3 position) {
            if (vfxPrefab == null)
                return;
            
            GameObject vfx = Instantiate(vfxPrefab, position, Quaternion.identity);
            Destroy(vfx, Const.Battle.VFX_DURATION);
        }
        
        private void HandleUnitAttacked(UnitCombatBase unit, UnitCombatBase target, SkillDataSO skillData) {
            if(skillData != null && skillData.skillVFX != null && target != null)
                SpawnVFX(skillData.skillVFX, target.transform.position);
        }

        private void HandleUnitDamaged(UnitCombatBase unit, float currentHealth, float healthBeforeDamaged) {
            SpawnVFX(defaultDamagedVFX, unit.transform.position);
        }

        private void HandleUnitHealed(UnitCombatBase unit, float currentHealth, float healedAmount) {
            SpawnVFX(healedVFX, unit.transform.position);
        }

        private void HandleUnitMPChanged(UnitCombatBase unit, float currentMP, float mpBeforeChange) {
            if(currentMP <= mpBeforeChange)
                return;

            SpawnVFX(mpGainedVFX, unit.transform.position);
        }
    }
}