using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine
{

    public enum PlayerStat
    {
        MaxMental,
        MaxPhysical,
        MaxSocial,
        CurrentMental,
        CurrentPhysical,
        CurrentSocial,
        MythosKnowledge
    }

    public class Player
    {
        public string Name { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }

        public Dictionary<PlayerStat, int> Stats { get; set; }

        public int CurrentAct { get; set; }
        public int CurrentEntry { get; set; }
    }
}
