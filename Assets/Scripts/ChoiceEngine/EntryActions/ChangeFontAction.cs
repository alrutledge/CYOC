using Assets.Scripts.ChoiceEngine.ChoiceActions;
using Assets.Scripts.ChoiceEngine.EntryActions;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine
{
	class ChangeFontAction : EntryAction
	{
		private string m_name;

		public ChangeFontAction (string name)
		{
			m_name = name;
		}

		public override void PerformAction()
		{
			MessageSystem.BroadcastMessage(new ChangeFontCommand(m_name));
		}
		
		public override bool AlwaysRun()
		{
			return true;
		}
	}

}
