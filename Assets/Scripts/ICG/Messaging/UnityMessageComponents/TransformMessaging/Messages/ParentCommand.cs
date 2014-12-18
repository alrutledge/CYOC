using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    struct ParentCommand
    {
        public Transform Parent;
        public ParentCommand(Transform parent)
        {
            Parent = parent;
        }
    }
}
