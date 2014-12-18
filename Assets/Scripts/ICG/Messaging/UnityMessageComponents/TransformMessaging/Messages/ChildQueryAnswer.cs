using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    public struct ChildQueryAnswer
    {
        public List<GameObject> Children;
        public ChildQueryAnswer(List<GameObject> children)
        {
            Children = children;
        }
    }
}
