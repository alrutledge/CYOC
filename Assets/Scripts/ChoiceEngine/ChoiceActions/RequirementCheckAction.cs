using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.ChoiceActions
{
    public class RequirementCheckAction : ChoiceAction
    {
        private int m_value;
        private ChoiceRequirementType m_requirementToCheck;
        public ChoiceAction SuccessAction { get; set; }
        public ChoiceAction FailureAction { get; set; }

        public RequirementCheckAction(ChoiceRequirementType requirementToCheck, int value)
        {
            m_requirementToCheck = requirementToCheck;
            m_value = value;
        }

        public override void PerformAction()
        {
            ChoiceRequirement requirement = new ChoiceRequirement(m_requirementToCheck, m_value);
            RequirementReply reply = MessageSystem.BroadcastQuery<RequirementReply, RequirementQuery>(new RequirementQuery(requirement));
            if (reply.RequirementMet && SuccessAction != null)
            {
                SuccessAction.PerformAction();
            }
            else if (!reply.RequirementMet && FailureAction != null)
            {
                FailureAction.PerformAction();
            }
        }
    }
}
