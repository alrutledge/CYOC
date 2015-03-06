using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;
using System.Collections.Generic;

namespace Assets.Scripts.ChoiceEngine.ChoiceActions
{
    public class RequirementCheckAction : ChoiceAction
    {
        private string m_value;
        private ChoiceRequirementType m_requirementToCheck;
        public List<ChoiceAction> SuccessActions { get; set; }
        public List<ChoiceAction> FailureActions { get; set; }

        public RequirementCheckAction(ChoiceRequirementType requirementToCheck, string value)
        {
            m_requirementToCheck = requirementToCheck;
            m_value = value;
            SuccessActions = new List<ChoiceAction>();
            FailureActions = new List<ChoiceAction>();
        }

        public override void PerformAction()
        {
            ChoiceRequirement requirement = new ChoiceRequirement(m_requirementToCheck, m_value);
            RequirementReply reply = MessageSystem.BroadcastQuery<RequirementReply, RequirementQuery>(new RequirementQuery(requirement));
            if (reply.RequirementMet && SuccessActions != null)
            {
                foreach (ChoiceAction action in SuccessActions)
                {
                    action.PerformAction();
                }
            }
            else if (!reply.RequirementMet && FailureActions != null)
            {
                foreach (ChoiceAction action in FailureActions)
                {
                    action.PerformAction();
                }
            }
        }
    }
}
