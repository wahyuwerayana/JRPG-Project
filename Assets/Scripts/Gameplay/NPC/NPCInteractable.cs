using Fungus;
using Game.Interface;
using UnityEngine;

namespace Game.Gameplay {
    public class NPCInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private Flowchart flowchart;
        
        public void Interact() {
            flowchart.gameObject.SetActive(true);
            flowchart = null;
        }
        
        public bool IsAvailableForInteract() {
            return flowchart != null;
        }
    }
}