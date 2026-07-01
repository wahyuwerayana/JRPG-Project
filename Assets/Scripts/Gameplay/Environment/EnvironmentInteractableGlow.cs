using DG.Tweening;
using UnityEngine;

namespace Gameplay.Environment {
    public class EnvironmentInteractableGlow : MonoBehaviour {
        [SerializeField] private Color glowColor;
        
        private void Start() {
            GetComponent<Renderer>().material.DOColor(glowColor, "_BaseColor", 1f)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}