namespace Assets.Scripts.ChoiceEngine
{
    public enum ChoiceActionType
    {
        GOTO,
        RAISE_ATTRIBUTE,
        LOWER_ATTRIBUTE,
        GRANT_GEAR,
        REMOVE_GEAR
    }

    [System.Serializable]
    public class ChoiceAction
    {
        public ChoiceActionType Type { get;  set; }
        public int ID { get;  set; }
    }
}
