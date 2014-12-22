using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.ChoiceActions
{
    public class GotoAction : ChoiceAction
    {
        public int ID { get; set; }

        public GotoAction(int id)
        {
            ID = id;
        }

        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new GotoEntryCommand(ID));
        }
    }
}
