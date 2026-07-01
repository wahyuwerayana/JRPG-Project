using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneController {
    public static AsyncOperation LoadScene(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single) {
        return SceneManager.LoadSceneAsync(scene.Path, mode);
    }
    
    public static AsyncOperation UnloadScene(SceneReference scene) {
        return SceneManager.UnloadSceneAsync(scene.Path);
    }
    
    public static async Awaitable LoadSceneAndSetActive(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single) {
        AsyncOperation loadOperation = LoadScene(scene, mode);

        await loadOperation;
        
        SetActiveScene(scene);
    }
    
    public static void SetActiveScene(SceneReference scene) {
        SceneManager.SetActiveScene(scene.LoadedScene);
    }
    
    public static SceneReference GetCurrentActiveScene() {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneReference sceneReference = SceneReference.FromScenePath(activeScene.path);

        return sceneReference;
    }
}
