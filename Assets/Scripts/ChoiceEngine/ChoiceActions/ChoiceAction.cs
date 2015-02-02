namespace Assets.Scripts.ChoiceEngine.ChoiceActions
{
    public enum ChoiceActionType
    {
        GOTO,
        MODIFY_ATTRIBUTE,
        GRANT_GEAR,
        REMOVE_GEAR,
        REQUIREMENT_CHECK,
        LOAD_ACT
    }

    public abstract class ChoiceAction
    {
        public ChoiceActionType Type { get;  set; }

        public abstract void PerformAction();
    }
}
