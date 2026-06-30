using Game.Gameplay;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    public class ItemUI : MonoBehaviour {
        [SerializeField] private Image itemImage;
        [SerializeField] private Button itemButton;
        
        private ItemSO referencedItem;
        
        public void Init(ItemSO item) {
            referencedItem = item;
            itemImage.sprite = referencedItem.Icon;
            
            itemButton.onClick.AddListener(() => {
                GameEventManager.Instance.BattleEvent.RaiseOnPlayerItemSelected(referencedItem);
                Destroy(gameObject);
            });
        }
    }
}