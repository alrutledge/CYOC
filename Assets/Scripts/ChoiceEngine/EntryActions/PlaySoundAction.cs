using System.Collections.Specialized;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.EntryActions
{
	public class PlaySoundAction : EntryAction
	{
		public string Name { get; set; }

		public PlaySoundAction(string name)
		{
			Name = name;
		}

		public override void PerformAction()
		{
			//MessageSystem.BroadcastMessage(new PlaySoundCommand(Name));
		}

        public override bool AlwaysRun()
        {
            return true;
        }
	}
}