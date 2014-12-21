using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.AnimationMessaging
{
    struct ResetTriggerCommand
    {
        public string Name;
        public ResetTriggerCommand(string name)
        {
            Name = name;
        }
    }
}
