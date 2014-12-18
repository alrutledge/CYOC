using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.GameObjectMessaging
{
    struct SetActiveCommand
    {
        public bool Active;
        public SetActiveCommand(bool active)
        {
            Active = active;
        }
    }
}
