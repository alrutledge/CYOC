namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class PlayActAnimationCommand
    {
        public string Name { get; set; }

        public PlayActAnimationCommand(string name)
        {
            Name = name;
        }
    }
}
