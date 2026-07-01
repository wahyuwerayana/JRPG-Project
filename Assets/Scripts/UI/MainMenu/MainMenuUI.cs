using System;
using Eflatun.SceneReference;
using Game.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.MainMenu {
    public class MainMenuUI : MonoBehaviour {
        [Header("References")]
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;

        [Header("Settings")]
        [SerializeField] private AudioMixer mainMixer;
        [SerializeField] private SceneReference gameScene;
        [SerializeField] private FadeOverlayHandler fader;

        private void Start() {
            masterSlider.value = PlayerPrefs.GetFloat("MASTER_VOLUME", 100f);
            bgmSlider.value = PlayerPrefs.GetFloat("BGM_VOLUME", 100f);
            sfxSlider.value = PlayerPrefs.GetFloat("SFX_VOLUME", 100f);
            
            SetMasterVolume(masterSlider.value);
            SetBGMVolume(bgmSlider.value);
            SetSFXVolume(sfxSlider.value);
            
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        public void PlayGame() {
            _ = SceneController.LoadSceneWithFade(gameScene, fader);
        }
        
        public void Settings() {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
        
        public void QuitGame() {
            Application.Quit();
        }

        private void SetMasterVolume(float sliderValue) {
            float normalizedValue = sliderValue / 100f;
            
            float db = Mathf.Log10(Mathf.Max(normalizedValue, 0.0001f)) * 20f;
            mainMixer.SetFloat("masterVolume", db);
            PlayerPrefs.SetFloat("MASTER_VOLUME", sliderValue);
        }
        
        private void SetBGMVolume(float sliderValue) {
            float normalizedValue = sliderValue / 100f;
            
            float db = Mathf.Log10(Mathf.Max(normalizedValue, 0.0001f)) * 20f;
            mainMixer.SetFloat("bgmVolume", db);
            PlayerPrefs.SetFloat("BGM_VOLUME", sliderValue);
        }
        
        private void SetSFXVolume(float sliderValue) {
            float normalizedValue = sliderValue / 100f;
            
            float db = Mathf.Log10(Mathf.Max(normalizedValue, 0.0001f)) * 20f;
            mainMixer.SetFloat("sfxVolume", db);
            PlayerPrefs.SetFloat("SFX_VOLUME", sliderValue);
        }
    }
}