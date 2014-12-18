using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.RigidBody2D.Messages
{
    class SetVelocityCommand
    {
        public Vector3 Velocity;
        public SetVelocityCommand(Vector3 velocity)
        {
            Velocity = velocity;
        }
    }
}
