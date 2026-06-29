using Eflatun.SceneReference;
using Fungus;
using UnityEngine;

namespace Game.Utils.FungusCommand {
    [CommandInfo("Scene", "Load Scene", "Loads a scene.")]
    public class LoadSceneCommand : Command {
        [SerializeField] private SceneReference sceneToLoad;
        
        public override void OnEnter() {
            SceneController.LoadScene(sceneToLoad);
            Continue();
        }
    }
}