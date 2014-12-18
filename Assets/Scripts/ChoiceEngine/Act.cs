using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine
{
    public class Act
    {
        public List<Entry> EntryList { get; set; }
        public Dictionary<int, Entry> Entries { get; set; }
        public string Name { get; set; }

        internal void ConvertEntries()
        {
            Entries = new Dictionary<int, Entry>();
            foreach (Entry entry in EntryList)
            {
                Entries.Add(entry.ID, entry);
            }
            EntryList = null;
        }
    }
}
