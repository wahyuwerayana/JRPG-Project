using Fungus;
using Game.Managers;

namespace Game.Utils.FungusCommand {
    [CommandInfo("Player", "Raise On Interact Ended Command", "Raise OnInteractEnded event")]
    public class RaiseOnInteractEndedCommand : Command {
        public override void OnEnter() {
            GameEventManager.Instance.PlayerEvent.RaiseOnInteractEnded();
            Continue();
        }
    }
}