using System.Collections.Generic;


namespace Assets.Scripts.ChoiceEngine
{
    [System.Serializable]
    public class Act
    {
        public List<Entry> EntryList { get; set; }
        public Dictionary<int, Entry> Entries { get; set; }
        public string Name { get; set; }

        public Act(string name)
        {
            Name = name;
            Entries = new Dictionary<int, Entry>();
        }
    }
}
