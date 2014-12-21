using System.Collections.Generic;

namespace Assets.Scripts.ChoiceEngine
{
    public class Choice
    {
        public string Text { get;  set; }
        public List<ChoiceAction> Actions { get;  set; }
        public List<ChoiceRequirement> Requirements { get; set; }

        public Choice()
        {
            Actions = new List<ChoiceAction>();
            Requirements = new List<ChoiceRequirement>();
        }
    }
}
