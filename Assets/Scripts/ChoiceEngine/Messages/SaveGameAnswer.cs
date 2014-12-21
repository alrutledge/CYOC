using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class SaveGameAnswer
    {
        public bool Exists { get; set; }

        public SaveGameAnswer(bool exists)
        {
            Exists = exists;
        }
    }
}
