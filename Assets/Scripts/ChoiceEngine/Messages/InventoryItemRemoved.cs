namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class InventoryItemRemoved
    {
        public string Name { get; set; }

        public InventoryItemRemoved(string name)
        {
            Name = name;
        }
    }
}
