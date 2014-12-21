using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.AnimationMessaging
{
    struct SetTriggerCommand
    {
        public string Name;
        public SetTriggerCommand(string name)
        {
            Name = name;
        }
    }
}
