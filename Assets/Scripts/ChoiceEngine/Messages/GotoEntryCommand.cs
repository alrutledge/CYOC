using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class GotoEntryCommand
    {
        public int ID;

        public GotoEntryCommand(int id)
        {
            ID = id;
        }
    }
}
