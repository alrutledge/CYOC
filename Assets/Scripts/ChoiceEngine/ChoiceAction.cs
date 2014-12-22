namespace Assets.Scripts.ChoiceEngine
{
    public enum ChoiceActionType
    {
        GOTO,
        MODIFY_ATTRIBUTE,
        GRANT_GEAR,
        REMOVE_GEAR,
        REQUIREMENT_CHECK
    }

    public abstract class ChoiceAction
    {
        public ChoiceActionType Type { get;  set; }

        public abstract void PerformAction();
    }
}
