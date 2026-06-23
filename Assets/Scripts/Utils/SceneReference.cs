using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Utils {
    [Serializable]
    public class SceneReference {
        [SerializeField] private Object sceneAsset;
        [SerializeField] private string sceneName = "";

        public string SceneName => string.IsNullOrEmpty(sceneName) ? sceneAsset.name : sceneName;

        public static implicit operator string(SceneReference sceneReference) {
            return sceneReference.SceneName;
        }
    }
}

