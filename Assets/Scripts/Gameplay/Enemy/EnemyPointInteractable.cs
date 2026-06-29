using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gameplay {
    public class EnemyPointInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private SceneReference battleScene;
        [SerializeField] private BattleData battleData;
        
        public void Interact() {
            SceneController.LoadScene(battleScene, LoadSceneMode.Additive);
        }
    }
}