using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public class ChoiceAction
    {
        public ChoiceActionType Type { get;  set; }
        public int ID { get;  set; }
    }
}
