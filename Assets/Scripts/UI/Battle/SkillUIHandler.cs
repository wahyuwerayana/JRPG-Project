using Game.Gameplay;
using Game.Managers;
using UnityEngine;

namespace Game.UI {
    public class SkillUIHandler : UIHandler {
        [Header("Skill UI Reference")]
        [SerializeField] private GameObject skillUIPrefab;
        [SerializeField] private RectTransform skillUIContainer;

        protected override void OnEnable() {
            base.OnEnable();
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned += CreateSkillUI;
        }

        protected override void OnDisable() {
            base.OnDisable();
            GameEventManager.Instance.BattleEvent.OnPlayerSpawned -= CreateSkillUI;
        }

        private void CreateSkillUI(PlayerCombat player) {
            foreach(SkillDataSO skill in player.Stats.Skills) {
                SkillUI skillUI = Instantiate(skillUIPrefab, skillUIContainer).GetComponent<SkillUI>();
                skillUI.Init(skill);
            }
        }
    }
}