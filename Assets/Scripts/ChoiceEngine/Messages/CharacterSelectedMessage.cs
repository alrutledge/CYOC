using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class CharacterSelectedMessage
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public Dictionary<PlayerStat, int> Stats { get; set; }
    }
}
