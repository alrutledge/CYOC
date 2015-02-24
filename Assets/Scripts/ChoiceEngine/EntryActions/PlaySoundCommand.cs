using System.Collections.Specialized;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.EntryActions
{
	 class PlaySoundCommand
	{
		public string SoundClipName { get; set; }
		public PlaySoundCommand(string clipName)
		{
			SoundClipName = clipName;
		}
	}
}