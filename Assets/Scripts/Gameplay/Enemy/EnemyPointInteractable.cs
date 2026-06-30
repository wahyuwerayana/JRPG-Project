using Eflatun.SceneReference;
using Game.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game.Gameplay {
    public class EnemyPointInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private SceneReference battleScene;
        [SerializeField] private BattleData battleData;

        [SerializeField] private UnityEvent<BattleData> OnPlayerWin;
        
        [Header("Global Context")]
        [SerializeField] private BattleContextSO battleContext;

        private void OnEnable() {
            GameEventManager.Instance.BattleEvent.OnWin += HandlePlayerWin;
        }

        private void OnDisable() {
            GameEventManager.Instance.BattleEvent.OnWin -= HandlePlayerWin;
        }

        private void HandlePlayerWin(BattleData winBattleData) {
            if (winBattleData != battleData)
                return;
            
            OnPlayerWin?.Invoke(winBattleData);
            
            Destroy(gameObject);
        }

        public void Interact() {
            if (battleContext != null)
                battleContext.CurrentBattleData = battleData;
                
            _ = SceneController.LoadSceneAndSetActive(battleScene, LoadSceneMode.Additive);
        }
    }
}