using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine
{
    public class Choice
    {
        public string Text { get;  set; }
        public List<ChoiceAction> Actions { get;  set; }
    }
}
