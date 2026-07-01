using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Item/Normal Item"), Serializable]
    public abstract class ItemSO : ScriptableObject {
        public string Name;
        public string Description;
        public Sprite Icon;

        private void OnValidate() {
            Name = name;
        }
    }
    
    [CreateAssetMenu(fileName = "New Normal Item", menuName = "Scriptable Object/Item/Normal Item"), Serializable]
    public class NormalItemSO : ItemSO { }
    
    [CreateAssetMenu(fileName = "New Usable Item", menuName = "Scriptable Object/Item/Usable Item"), Serializable]
    public class UsableItemSO : ItemSO {
        [SerializeReference] public List<Effect> itemEffects;
    }
}