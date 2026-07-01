using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI {
    [RequireComponent(typeof(Button))]
    public class UIButtonSound : MonoBehaviour {
        private void Start() {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(PlaySound);
        }
        
        private void PlaySound() {
            AudioManager.Instance.PlaySFX(Const.Audio.SFX_BUTTON_CLICK);
        }
    }
}