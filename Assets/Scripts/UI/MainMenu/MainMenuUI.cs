using Eflatun.SceneReference;
using UnityEngine;

namespace UI.MainMenu {
    public class MainMenuUI : MonoBehaviour {
        [SerializeField] private SceneReference gameScene;
        
        public void PlayGame() {
            SceneController.LoadScene(gameScene);
        }
        
        public void QuitGame() {
            Application.Quit();
        }
    }
}