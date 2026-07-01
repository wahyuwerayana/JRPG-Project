using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay {
    public enum ItemType {
        Normal,
        Usable,
    }
    
  [CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Item"), Serializable]
    public class ItemSO : ScriptableObject {
        public string Name;
        public string Description;
        public Sprite Icon;
        public ItemType Type;
        
        [Header("Usable Item Only")]
        [SerializeReference] public List<Effect> Effects;

        private void OnValidate() {
            Name = name;
        }
    }
}