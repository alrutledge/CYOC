using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    public struct TranslateCommand
    {
        public Vector3 TranslationVector;
        public Space TranslationSpace;

        public TranslateCommand(Vector3 newPosition, Space space)
        {
            TranslationVector = newPosition;
            TranslationSpace = space;
        }
    }
}
