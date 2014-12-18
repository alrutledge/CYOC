using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    class PositionReply
    {
        public Vector3 Position;
        public PositionReply(Vector3 position)
        {
            Position = position;
        }
    }
}
