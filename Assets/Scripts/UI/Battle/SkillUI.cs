using Game.Gameplay;
using Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class SkillUI : MonoBehaviour {
        [SerializeField] private Button skillButton;
        [SerializeField] private TMP_Text skillNameText;
        [SerializeField] private TMP_Text skillCostText;
        
        private SkillDataSO skillData;

        public void Init(SkillDataSO skill) {
            skillData = skill;
            skillNameText.text = skill.Name;
            skillCostText.text = $"MP: {skill.MPCost}";
            
            skillButton.onClick.AddListener(OnSkillButtonClick);
        }

        private void OnSkillButtonClick() {
            GameEventManager.Instance.PlayerEvent.RaiseOnPlayerActionSelected(skillData);
        }
        
        public void SetSkillAvailability(float currentMP) {
            skillButton.interactable = currentMP >= skillData.MPCost;
        }
    }
}