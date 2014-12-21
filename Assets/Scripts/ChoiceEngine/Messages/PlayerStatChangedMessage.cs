using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class PlayerStatChangedMessage
    {

        public int NewValue { get; set; }
        public PlayerStat StatChanged { get; set; }

        public PlayerStatChangedMessage(PlayerStat statChanged, int newValue)
        {
            StatChanged = statChanged;
            NewValue = newValue;
        }
    }
}
