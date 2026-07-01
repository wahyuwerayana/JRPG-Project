using System.Collections;
using System.Collections.Generic;
using Eflatun.SceneReference;
using Game.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Managers {
    public class AudioManager : MonoBehaviour {
        public static AudioManager Instance { get; private set; }
        
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioSource sfxSource;
        
        [SerializeField] private AudioLibrarySO audioLibrary;
        
        private Dictionary<SceneReference, AudioClip> bgmClips = new Dictionary<SceneReference, AudioClip>();
        private Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

        private AudioClip previousBGM;
        private Coroutine activeFadeRoutine;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach(BGMAudioPair bgm in audioLibrary.BGMAudioPairs) {
                bgmClips.Add(bgm.SceneRef, bgm.AudioClip);
            }

            foreach(SFXAudioPair sfx in audioLibrary.SFXAudioPairs) {
                sfxClips.Add(sfx.AudioKey, sfx.AudioClip);
            }
        }

        private void OnEnable() {
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        private void OnDisable() {
            SceneManager.sceneLoaded -= HandleSceneLoaded;
        }

        public void PlayBGM(AudioClip clip, bool isFade = true) {
            if (bgmSource.clip == clip)
                return;

            if (bgmSource.clip != null)
                previousBGM = bgmSource.clip;
            
            if(activeFadeRoutine != null)
                StopCoroutine(activeFadeRoutine);

            if (isFade) {
                activeFadeRoutine = StartCoroutine(FadeBGM(clip));
            }
            else {
                bgmSource.volume = 1f;
                bgmSource.clip = clip;
                bgmSource.Play();
            }
        }

        private IEnumerator FadeBGM(AudioClip newClip) {
            const float FADE_DURATION = 1.5f;
            float startVolume = bgmSource.volume;

            if (bgmSource.isPlaying) {
                float timeElapsed = 0f;
                while (timeElapsed < FADE_DURATION) {
                    timeElapsed += Time.deltaTime;
                    bgmSource.volume = Mathf.Lerp(startVolume, 0f, timeElapsed / FADE_DURATION);
                    yield return null;
                }
            }

            bgmSource.clip = newClip;

            if (newClip != null) {
                bgmSource.Play();

                float timeElapsed = 0f;
                while (timeElapsed < FADE_DURATION) {
                    timeElapsed += Time.deltaTime;
                    bgmSource.volume = Mathf.Lerp(0f, 1f, timeElapsed / FADE_DURATION);
                    yield return null;
                }

                bgmSource.volume = 1f;
            }

            activeFadeRoutine = null;
        }

        public void ResumePreviousBGM(bool isFade = true) {
            if (previousBGM != null) {
                PlayBGM(previousBGM, isFade);
            }
        }

        public void PlaySFX(AudioClip clip) {
            sfxSource.PlayOneShot(clip);
        }
        
        public void PlaySFX(string key) {
            if(sfxClips.TryGetValue(key, out AudioClip clip)) {
                PlaySFX(clip);
            }
        }
        
        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode) {
            SceneReference sceneReference = SceneReference.FromScenePath(scene.path);
            
            if(bgmClips.TryGetValue(sceneReference, out AudioClip clip)) {
                PlayBGM(clip);
            }
        }
        
        public void StopBGM() {
            if (activeFadeRoutine != null) {
                StopCoroutine(activeFadeRoutine);
            }
            
            bgmSource.Stop();
        }
    }
}