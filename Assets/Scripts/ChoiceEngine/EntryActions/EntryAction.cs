namespace Assets.Scripts.ChoiceEngine.EntryActions
{
    public enum EntryActionType
    {
        MODIFY_ATTRIBUTE,
        GRANT_ITEM,
        REMOVE_ITEM,
        CHANGE_PICTURE,
        CHANGE_FONT,
		ADD_FLAG,
        REMOVE_FLAG,
        PLAY_MUSIC,
        PLAY_SOUND,
        PLAY_ACT_ANIMATION
    }

    public abstract class EntryAction
    {
        public EntryActionType Type { get; set; }

        public abstract void PerformAction();
        public abstract bool AlwaysRun();
    }
}