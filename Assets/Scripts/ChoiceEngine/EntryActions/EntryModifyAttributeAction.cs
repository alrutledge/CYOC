using Assets.Scripts.ChoiceEngine.EntryActions;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.EntryActions
{
    public class EntryModifyAttributeAction : EntryAction
    {
        public PlayerStat PlayerStat { get; set; }
        public int Delta { get; set; }

        public EntryModifyAttributeAction(PlayerStat playerStat, int delta)
        {
            PlayerStat = playerStat;
            Delta = delta;
        }

        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new ModifyAttributeCommand(PlayerStat, Delta));
        }
    }
}
