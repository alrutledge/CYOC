
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class ActLoadedMessage
    {
        public Entry FirstEntry;
        public Act CurrentAct;

        public ActLoadedMessage(Entry firstEntry, Act currentAct)
        {
            FirstEntry = firstEntry;
            CurrentAct = currentAct;
        }
    }
}
