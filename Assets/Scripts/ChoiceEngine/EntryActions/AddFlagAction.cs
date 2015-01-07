using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

namespace Assets.Scripts.ChoiceEngine.EntryActions
{
    public class AddFlagAction : EntryAction
    {
        public string Name { get; set; }
        public AddFlagAction(string name)
        {
            Name = name;
        }

        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new AddFlagCommand(Name));
        }

        public override bool AlwaysRun()
        {
            return false;
        }
    }
}
