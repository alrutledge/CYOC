using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.AnimationMessaging
{
    class CurrentStateReply
    {
        public AnimatorStateInfo CurrentState;
        public CurrentStateReply(AnimatorStateInfo currentState)
        {
            CurrentState = currentState;
        }
    }
}
