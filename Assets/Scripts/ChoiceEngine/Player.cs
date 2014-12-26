using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine
{
    public enum PlayerStat
    {
        MAX_MENTAL,
        MAX_PHYSICAL,
        MAX_SOCIAL,
        CURRENT_MENTAL,
        CURRENT_PHYSICAL,
        CURRENT_SOCIAL,
        MYTHOS_KNOWLEDGE
    }

    [System.Serializable]
    public class Player
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }

        public Dictionary<PlayerStat, int> Stats { get; set; }
        public List<Item> Inventory { get; set; }

        public int CurrentAct { get; set; }
        public int CurrentEntry { get; set; }
    }
}
