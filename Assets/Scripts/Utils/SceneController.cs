using Eflatun.SceneReference;
using Game.UI;
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

    public static async Awaitable LoadSceneWithFade(SceneReference scene, FadeOverlayHandler fader, bool setSceneAsActive = true,
                                                    LoadSceneMode mode = LoadSceneMode.Single) {
        AsyncOperation loadOperation = LoadScene(scene, mode);
        loadOperation.allowSceneActivation = false;
        await fader.FadeOutAsync();
        loadOperation.allowSceneActivation = true;
        await loadOperation;
        if (setSceneAsActive) {
            SetActiveScene(scene);
        }
    }
    
    public static async Awaitable UnloadSceneWithFade(SceneReference scene, FadeOverlayHandler fader) {
        AsyncOperation unloadOperation = UnloadScene(scene);
        await fader.FadeInAsync();
        await unloadOperation;
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
