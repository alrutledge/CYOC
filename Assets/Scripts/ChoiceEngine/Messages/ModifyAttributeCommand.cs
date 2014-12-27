namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class ModifyAttributeCommand
    {
        public PlayerStat PlayerStat {get; set;}
        public int Delta { get; set; }

        public ModifyAttributeCommand(PlayerStat playerStat, int delta)
        {
            PlayerStat = playerStat;
            Delta = delta;
        }
    }
}
