using Assets.Scripts.ChoiceEngine.ChoiceActions;

namespace Assets.Scripts.ChoiceEngine
{
    public class ActionFactory
    {
        public static ChoiceAction ParseAction(string[] choiceParts)
        {
            ChoiceAction action;
            

            ChoiceActionType type = (ChoiceActionType)System.Enum.Parse(typeof(ChoiceActionType), choiceParts[1]);

            switch (type)
            {
                case ChoiceActionType.GOTO:
                    action = new GotoAction(System.Int32.Parse(choiceParts[2]));
                    break;
                case ChoiceActionType.REQUIREMENT_CHECK:
                    action = new RequirementCheckAction((ChoiceRequirementType)System.Enum.Parse(typeof(ChoiceRequirementType), choiceParts[2]),
                        System.Int32.Parse(choiceParts[3]));
                    break;
                default:
                    action = null;
                    break;
            }

            if (action != null)
            {
                action.Type = type;
            }
            return action;
        }
    }
}
