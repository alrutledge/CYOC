﻿namespace Assets.Scripts.ChoiceEngine.EntryActions
{
    public enum EntryActionType
    {
        MODIFY_ATTRIBUTE,
        GRANT_ITEM,
        REMOVE_ITEM,
        CHANGE_PICTURE,
        ADD_FLAG,
        REMOVE_FLAG
    }

    public abstract class EntryAction
    {
        public EntryActionType Type { get; set; }

        public abstract void PerformAction();
    }
}