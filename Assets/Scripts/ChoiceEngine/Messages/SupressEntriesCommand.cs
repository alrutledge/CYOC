using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class SupressEntriesCommand
    {
        public bool Active { get; set; }
        public SupressEntriesCommand(bool active)
        { 
            Active = active;
        }
    }
}
