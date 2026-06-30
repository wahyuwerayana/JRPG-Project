using Game.Gameplay;
using Game.Managers;
using UnityEngine;

namespace Game.UI {
    public class ItemUIHandler : UIHandler {
        [Header("References")]
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private RectTransform itemContainer;
        [SerializeField] private PlayerInventorySO playerInventory;

        protected override void OnEnable() {
            base.OnEnable();
            
            GameEventManager.Instance.PlayerEvent.OnInventoryChanged += RefreshItems;
        }

        protected override void OnDisable() {
            base.OnDisable();
            
            GameEventManager.Instance.PlayerEvent.OnInventoryChanged -= RefreshItems;
        }

        private void Start() {
            RefreshItems();
        }

        private void RefreshItems() {
            foreach(Transform child in itemContainer) {
                Destroy(child.gameObject);
            }

            foreach(ItemSO item in playerInventory.items) {
                CreateItemUI(item);
            }
        }

        private void CreateItemUI(ItemSO item) {
            ItemUI itemUI = Instantiate(itemPrefab, itemContainer).GetComponent<ItemUI>();
            itemUI.Init(item);
        }
    }
}