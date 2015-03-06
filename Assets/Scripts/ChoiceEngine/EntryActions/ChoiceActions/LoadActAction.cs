using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.ChoiceActions
{
    public class LoadActAction: ChoiceAction
    {
        public int ActNumber { get; set; }

        public LoadActAction(int actNumber)
        {
            ActNumber = actNumber;
        }

        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new LoadActCommand("Act" + ActNumber.ToString(), 0));
        }
    }
}
