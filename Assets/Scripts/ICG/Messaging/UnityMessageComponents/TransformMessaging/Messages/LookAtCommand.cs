using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    struct LookAtCommand
    {
        public Vector3 LookAtPosition;
        public Vector3 Up;
        public LookAtCommand(Vector3 position, Vector3 up)
        {
            Up = up;
            LookAtPosition = position;
        }
    }
}
