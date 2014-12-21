using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.AnimationMessaging
{
    struct SetFloatCommand
    {
        public string Name;
        public float Value;
        public SetFloatCommand(string name, float value)
        {
            Name = name;
            Value = value;
        }
    }
}
