using Game.Gameplay;
using Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI {
    public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private Image itemImage;
        [SerializeField] private Button itemButton;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text itemDescriptionText;
        [SerializeField] private RectTransform itemDescriptionContainer;
        
        private ItemSO referencedItem;
        
        public void Init(ItemSO item) {
            referencedItem = item;
            itemImage.sprite = referencedItem.Icon;
            itemNameText.text = referencedItem.Name;
            itemDescriptionText.text = referencedItem.Description;
            
            itemButton.onClick.AddListener(() => {
                GameEventManager.Instance.PlayerEvent.RaiseOnPlayerItemSelected(referencedItem);
                Destroy(gameObject);
            });
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            itemDescriptionContainer.gameObject.SetActive(true);
        }
        
        public void OnPointerExit(PointerEventData eventData) {
            itemDescriptionContainer.gameObject.SetActive(false);
        }
    }
}