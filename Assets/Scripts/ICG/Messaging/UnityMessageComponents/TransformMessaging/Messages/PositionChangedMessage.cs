using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    public struct PositionChangedMessage
    {
        public Vector3 NewPosition;
        public PositionChangedMessage(Vector3 newPosition)
        {
            NewPosition = newPosition;
        }
    }
}
