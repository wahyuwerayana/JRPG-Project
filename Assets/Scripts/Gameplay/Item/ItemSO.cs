using UnityEngine;

namespace Game.Gameplay {
    [CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Item", order = 0)]
    public class ItemSO : ScriptableObject {
        public string Name;
        public string Description;
        public Sprite Icon;
    }
}