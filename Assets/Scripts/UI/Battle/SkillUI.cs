using Game.Gameplay;
using Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class SkillUI : MonoBehaviour {
        [SerializeField] private Button skillButton;
        [SerializeField] private TMP_Text skillNameText;
        
        private SkillDataSO skillData;

        public void Init(SkillDataSO skill) {
            skillData = skill;
            skillNameText.text = skill.Name;
            
            skillButton.onClick.AddListener(OnSkillButtonClick);
        }

        private void OnSkillButtonClick() {
            Debug.Log($"Skill {skillData.Name} selected.");
            GameEventManager.Instance.BattleEvent.RaiseOnPlayerActionSelected(skillData);
        }
    }
}