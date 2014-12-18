using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    struct ForwardReply
    {
        public Vector3 Forward;
        public ForwardReply(Vector3 forward)
        {
            Forward = forward;
        }
    }
}
