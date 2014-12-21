using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine
{
    [System.Serializable]
    public class Choice
    {
        public string Text { get;  set; }
        public List<ChoiceAction> Actions { get;  set; }

        public Choice()
        {
            Actions = new List<ChoiceAction>();
        }
    }
}
