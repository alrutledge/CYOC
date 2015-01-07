namespace Assets.Scripts.CYOC.UI.Messages
{
	public class PlayMusicCommand
	{
		public string ClipName { get; set; }
		public PlayMusicCommand(string clipName)
		{
			ClipName = clipName;
		}
	}
}
