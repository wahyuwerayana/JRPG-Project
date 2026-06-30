using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gameplay {
    public class EnemyPointInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private SceneReference battleScene;
        [SerializeField] private BattleData battleData;

        [Header("Global Context")]
        [SerializeField] private BattleContextSO battleContext;
        
        public void Interact() {
            if (battleContext != null)
                battleContext.CurrentBattleData = battleData;
                
            SceneController.LoadScene(battleScene, LoadSceneMode.Additive);
        }
    }
}