namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class RemoveFlagCommand
    {
        public string Name { get; set; }

        public RemoveFlagCommand(string name)
        {
            Name = name;
        }
    }
}
