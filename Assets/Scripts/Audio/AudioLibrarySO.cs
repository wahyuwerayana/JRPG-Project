using System;
using System.Collections.Generic;
using Eflatun.SceneReference;
using UnityEngine;

namespace Game.Audio {
    [CreateAssetMenu(fileName = "New Audio Library", menuName = "Scriptable Object/Audio/Audio Library")]
    public class AudioLibrarySO : ScriptableObject {
        [field: SerializeField] public List<BGMAudioPair> BGMAudioPairs { get; private set; }
        [field: SerializeField] public List<SFXAudioPair> SFXAudioPairs { get; private set; }
    }


    [Serializable]
    public class SFXAudioPair {
        [field: SerializeField] public string AudioKey { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
    }
    
    [Serializable]
    public class BGMAudioPair {
        [field: SerializeField] public SceneReference SceneRef { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
    }
}