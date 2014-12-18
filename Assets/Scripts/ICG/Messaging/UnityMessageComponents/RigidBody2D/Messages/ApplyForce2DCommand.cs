using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.RigidBody2D.Messages
{
    class ApplyForce2DCommand
    {
        public Vector2 Force;
        public ForceMode2D Mode;


        public ApplyForce2DCommand(Vector2 force, ForceMode2D mode)
        {
            Mode = mode;
            Force = force;
        }
    }
}
