using System.Collections.Specialized;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.EntryActions
{
    public class RemoveItemAction : EntryAction
    {
        public string Name { get; set; }
        public RemoveItemAction(string name)
        {
            Name = name;
        }

        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new InventoryItemRemoved(Name));
        }

        public override bool AlwaysRun()
        {
            return false;
        }
    }
}
