using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.EntryActions
{
    public class AddItemAction : EntryAction
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }

        public AddItemAction(string name, string description, string smallImage, string largeImage)
        {
            Name = name;
            Description = description;
            SmallImage = smallImage;
            LargeImage = largeImage;
        }

        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new InventoryItemAdded(Name, Description, SmallImage, LargeImage));
        }

        public override bool AlwaysRun()
        {
            return false;
        }
    }
}
