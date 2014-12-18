using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    struct ForwardChangedMessage
    {
        public Vector3 NewForward;
        public ForwardChangedMessage(Vector3 newForward)
        {
            NewForward = newForward;
        }
    }
}
