using Eflatun.SceneReference;
using Game.Managers;
using Game.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game.Gameplay {
    public class EnemyPointInteractable : MonoBehaviour, IInteractable {
        [Header("References")]
        [SerializeField] private SceneReference battleScene;
        [SerializeField] private BattleData battleData;

        [SerializeField] private UnityEvent<BattleData> OnPlayerWin;
        
        [Header("Global Context")]
        [SerializeField] private BattleContextSO battleContext;
        [SerializeField] private FadeOverlayHandler fader;

        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnEnded += HandleBattleEnd;
            GameEventManager.Instance.BattleEvent.OnWin += HandlePlayerWin;
        }

        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnEnded -= HandleBattleEnd;
            GameEventManager.Instance.BattleEvent.OnWin -= HandlePlayerWin;
        }

        private void HandlePlayerWin(BattleData winBattleData) {
            if (winBattleData != battleData)
                return;
            
            OnPlayerWin?.Invoke(winBattleData);
            
            Destroy(gameObject);
        }

        private void HandleBattleEnd() {
            GameEventManager.Instance.PlayerEvent.RaiseOnInteractEnded();
        }

        public void Interact() {
            if (battleContext != null)
                battleContext.CurrentBattleData = battleData;
                
            _ = SceneController.LoadSceneWithFade(battleScene, fader, mode: LoadSceneMode.Additive);
        }
        
        public bool IsAvailableForInteract() {
            return battleScene != null && battleData != null;
        }
    }
}