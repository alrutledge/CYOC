using System.Collections.Specialized;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.CYOC.UI.Messages;

namespace Assets.Scripts.ChoiceEngine.EntryActions
{
	public class PlayMusicAction : EntryAction
	{
		public string Name { get; set; }
		
		public PlayMusicAction(string name)
		{
			Name = name;
		}
		public override void PerformAction()
		{
			MessageSystem.BroadcastMessage(new PlayMusicCommand(Name));
		}
	}
}