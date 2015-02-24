using Assets.Scripts.ChoiceEngine.ChoiceActions;
using Assets.Scripts.ChoiceEngine.EntryActions;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine
{
	class ChangeFontCommand
	{
		public string Name{ get; set; }
		public ChangeFontCommand (string name)
		{
			Name = name;
		}
	}
}
