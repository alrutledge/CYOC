using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.ChoiceActions
{
    public class EndGameAction : ChoiceAction
    {
        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new ClearSaveGameCommand());
            MessageSystem.BroadcastMessage(new ExitToMainMenuCommand());
        }
    }
}
