using Fungus;
using Game.Managers;
using UnityEngine;

namespace Game.Utils.FungusCommand {
    public class SetPlayerInputCommand : Command {
        [SerializeField] private bool isEnabled;

        public override void OnEnter() {
            InputManager.Instance.SetPlayerInput(isEnabled);
            Continue();
        }
    }
}