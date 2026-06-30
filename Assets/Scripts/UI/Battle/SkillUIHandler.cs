using System.Collections.Generic;
using Game.Gameplay;
using Game.Managers;
using UnityEngine;

namespace Game.UI {
    public class SkillUIHandler : UIHandler {
        [Header("Skill UI Reference")]
        [SerializeField] private GameObject skillUIPrefab;
        [SerializeField] private RectTransform skillUIContainer;
        
        private List<SkillUI> skillUIs = new List<SkillUI>();

        protected override void OnEnable() {
            base.OnEnable();
            
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned += CreateSkillUI;
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged += SetSkillsAvailability;
        }

        protected override void OnDisable() {
            base.OnDisable();
            
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned -= CreateSkillUI;
            GameEventManager.Instance.BattleEvent.OnUnitMPChanged -= SetSkillsAvailability;
        }

        private void CreateSkillUI(PlayerCombat player) {
            foreach(SkillDataSO skill in player.Stats.Skills) {
                SkillUI skillUI = Instantiate(skillUIPrefab, skillUIContainer).GetComponent<SkillUI>();
                skillUI.Init(skill);
                skillUIs.Add(skillUI);
            }
        }
        
        private void SetSkillsAvailability(UnitCombatBase unit, float currentMP, float mpUsed) {
            if (unit is not PlayerCombat)
                return;

            foreach(SkillUI skillUI in skillUIs) {
                skillUI.SetSkillAvailability(currentMP);
            }
        }
    }
}