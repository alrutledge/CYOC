using Assets.Scripts.ChoiceEngine.ChoiceActions;
using Assets.Scripts.ChoiceEngine.EntryActions;

namespace Assets.Scripts.ChoiceEngine
{
    public class ActionFactory
    {
        public static ChoiceAction ParseChoiceAction(string[] choiceParts)
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
                        choiceParts[3]);
                    break;
                case ChoiceActionType.MODIFY_ATTRIBUTE:
                    action = new ChoiceModifyAttributeAction((PlayerStat)System.Enum.Parse(typeof(PlayerStat), choiceParts[2]), System.Int32.Parse(choiceParts[3]));
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

        public static EntryAction ParseEntryAction(string[] choiceParts)
        {
            EntryAction action;


            EntryActionType type = (EntryActionType)System.Enum.Parse(typeof(EntryActionType), choiceParts[1]);

            switch (type)
            {
                case EntryActionType.MODIFY_ATTRIBUTE:
                    action = new EntryModifyAttributeAction((PlayerStat)System.Enum.Parse(typeof(PlayerStat), choiceParts[2]), System.Int32.Parse(choiceParts[3]));
                    break;
                case EntryActionType.GRANT_ITEM:
                    action = new AddItemAction(choiceParts[2], choiceParts[5], choiceParts[3], choiceParts[4]);
                    break;
                case EntryActionType.REMOVE_ITEM:
                    action = new RemoveItemAction(choiceParts[2]);
                    break;
                case EntryActionType.ADD_FLAG:
                    action = new AddFlagAction(choiceParts[2]);
                    break;
                case EntryActionType.REMOVE_FLAG:
                    action = new RemoveFlagAction(choiceParts[2]);
                    break;
				case EntryActionType.PLAY_MUSIC:
					action = new PlayMusicAction(choiceParts[2]);
				    break;
				case EntryActionType.PLAY_SOUND:
				    action = new PlaySoundAction(choiceParts[2]);
				    break;
                case EntryActionType.PLAY_ACT_ANIMATION:
                    action = new PlayActAnimationAction(choiceParts[2]);
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
