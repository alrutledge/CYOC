using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    public struct RotationChangedMessage
    {
        public Quaternion NewRotation;
        public RotationChangedMessage(Quaternion newRotation)
        {
            NewRotation = newRotation;
        }
    }
}
