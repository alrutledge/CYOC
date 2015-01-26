using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.EntryActions
{
    class PlayActAnimationAction : EntryAction
    {
        public string Name { get; set; }

        public PlayActAnimationAction(string name)
        {
            Name = name;
        }

        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new SupressEntriesCommand(true));
            MessageSystem.BroadcastMessage(new PrepareActAnimationCommand(Name));
        }

        public override bool AlwaysRun()
        {
            return true;
        }
    }
}
