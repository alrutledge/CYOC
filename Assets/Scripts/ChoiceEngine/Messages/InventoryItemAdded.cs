namespace Assets.Scripts.ChoiceEngine.Messages
{
    class InventoryItemAdded
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }

        public InventoryItemAdded(string name, string description, string smallImage, string largeImage)
        {
            Name = name;
            Description = description;
            SmallImage = smallImage;
            LargeImage = largeImage;
        }
    }
}
