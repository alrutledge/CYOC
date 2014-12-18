using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    public struct RotateCommand
    {
        public Vector3 RotationVector;
        public float Angle;

        public RotateCommand(Vector3 rotationVector, float angle)
        {
            RotationVector = rotationVector;
            Angle = angle;
        }
    }
}