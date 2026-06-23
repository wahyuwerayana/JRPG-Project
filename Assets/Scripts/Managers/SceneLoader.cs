using Game.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }
        
        Instance = this;
        
        DontDestroyOnLoad(this);
    }

    public void LoadScene(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single) {
        SceneManager.LoadScene(scene, mode);
    }

    public void LoadSceneAsync(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single) {
        SceneManager.LoadSceneAsync(scene, mode);
    }
}
