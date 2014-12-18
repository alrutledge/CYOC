using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    struct ParentReply
    {
        public GameObject Parent;
        public ParentReply(GameObject parent)
        {
            Parent = parent;
        }
    }
}
