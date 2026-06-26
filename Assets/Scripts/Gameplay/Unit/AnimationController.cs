using Game.Utils;
using UnityEngine;

namespace Game.Gameplay {
    public class AnimationController : MonoBehaviour {
        private Animator animator;

        private CountdownTimer animationTimer;

        private readonly int locomotionHash = Animator.StringToHash("Locomotion");
        private readonly int speedHash = Animator.StringToHash("Speed");

        private void Start() {
            animator = GetComponentInChildren<Animator>();
            
            animationTimer = new CountdownTimer(0f);
        }

        public void PlayOneShot(AnimationClip clip) {
            if (clip == null || animator == null)
                return;

            animationTimer.OnTimerStop = () => animator.CrossFade(locomotionHash, 0.1f);
            animationTimer.Reset(clip.length);
            animationTimer.Start();
            animator.CrossFade(clip.name, 0.1f);
        }
    }
}