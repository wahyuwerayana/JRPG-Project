using Fungus;
using UnityEngine;

namespace Game.Gameplay.NPC {
    public class NPCInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private Flowchart flowchart;
        
        public void Interact() {
            flowchart.gameObject.SetActive(true);
        }
        
        public bool IsAvailableForInteract() {
            return flowchart != null;
        }
    }
}