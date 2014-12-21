using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class EntryLoadedMessage
    {
        public Entry LoadedEntry { get; set; }

        public EntryLoadedMessage(Entry entry)
        {
            LoadedEntry = entry;
        }
    }
}
