using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class LoadActCommand
    {
        public string ActToLoad { get; set; }
        public int EntryToLoad { get; set; }

        public LoadActCommand(string actToLoad, int entryToLoad = 0)
        {
            ActToLoad = actToLoad;
            EntryToLoad = entryToLoad;
        }
    }
}
