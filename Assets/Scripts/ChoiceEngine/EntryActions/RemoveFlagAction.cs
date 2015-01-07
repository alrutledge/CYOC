using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

namespace Assets.Scripts.ChoiceEngine.EntryActions
{
    public class RemoveFlagAction : EntryAction
    {
        public string Name { get; set; }
        public RemoveFlagAction(string name)
        {
            Name = name;
        }

        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new RemoveFlagCommand(Name));
        }

        public override bool AlwaysRun()
        {
            return false;
        }
    }
}
