using Assets.Scripts.ChoiceEngine.EntryActions;
using System.Collections.Generic;

namespace Assets.Scripts.ChoiceEngine
{
    public class Entry
    {
        public int ID { get;  set; }
        public string Text { get;  set; }
        public List<Choice> Choices { get; set; }
        public List<EntryAction> Actions { get; set; }
        public string ImageResource { get; set; }

        public Entry(int id)
        {
            ID = id;
            Choices = new List<Choice>();
            Actions = new List<EntryAction>();
        }

    }
}
