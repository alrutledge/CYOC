namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class AddFlagCommand
    {
        public string Name { get; set; }

        public AddFlagCommand(string name)
        {
            Name = name;
        }
    }
}
