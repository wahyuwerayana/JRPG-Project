using Fungus;
using Game.Managers;

namespace Game.Utils.FungusCommand {
    [CommandInfo("Player", "Raise On Interact Started Command", "Raise OnInteractStarted event")]
    public class RaiseOnInteractStartedCommand : Command {
        public override void OnEnter() {
            GameEventManager.Instance.PlayerEvent.RaiseOnInteractStarted();
            Continue();
        }
    }
}