using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine
{
    public class Entry
    {
        public int ID { get;  set; }
        public string Text { get;  set; }
        public List<Choice> Choices { get;  set; }

        public Entry(int id, string text, List<Choice> choices)
        {
            ID = id;
            Text = text;
            Choices = choices;
        }
    }
}
