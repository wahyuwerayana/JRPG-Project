using Eflatun.SceneReference;
using Game.UI;
using UnityEngine;

namespace UI.MainMenu {
    public class MainMenuUI : MonoBehaviour {
        [SerializeField] private SceneReference gameScene;
        [SerializeField] private FadeOverlayHandler fader;
        
        public void PlayGame() {
            SceneController.LoadSceneWithFade(gameScene, fader);
        }
        
        public void QuitGame() {
            Application.Quit();
        }
    }
}