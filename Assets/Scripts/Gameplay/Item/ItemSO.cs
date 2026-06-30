using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Item/Normal Item", order = 0)]
    public abstract class ItemSO : ScriptableObject {
        public string Name;
        public string Description;
        public Sprite Icon;

        private void OnValidate() {
            Name = name;
        }
    }
    
    [CreateAssetMenu(fileName = "New Normal Item", menuName = "Scriptable Object/Item/Normal Item")]
    public class NormalItemSO : ItemSO { }
    
    [CreateAssetMenu(fileName = "New Usable Item", menuName = "Scriptable Object/Item/Usable Item")]
    public class UsableItemSO : ItemSO {
        [SerializeReference] public List<Effect> itemEffects;
    }
}