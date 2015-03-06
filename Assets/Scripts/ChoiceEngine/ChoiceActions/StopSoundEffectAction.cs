using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.ChoiceActions
{
    class StopSoundEffectAction : ChoiceAction
    {
        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new StopSoundEffectCommand());
        }
    }
}
