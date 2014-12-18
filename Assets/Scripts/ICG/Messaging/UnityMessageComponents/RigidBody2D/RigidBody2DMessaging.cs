using Assets.Scripts.ICG.Messaging.UnityMessageComponents.RigidBody2D.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.RigidBody2D
{
    class RigidBody2DMessaging : MonoBehaviour
    {
        private Rigidbody2D m_rigidBody2D;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<SetVelocityCommand>(gameObject, OnSetVelocityCommand);
            MessageSystem.SubscribeMessage<ApplyForce2DCommand>(gameObject, OnApplyForceCommand);
        }

        private void Start()
        {
            m_rigidBody2D = GetComponent<Rigidbody2D>();
        }

        private void OnApplyForceCommand(ApplyForce2DCommand message)
        {
            m_rigidBody2D.AddForce(message.Force, message.Mode);
        }

        private void OnSetVelocityCommand(SetVelocityCommand message)
        {
            m_rigidBody2D.velocity = message.Velocity;
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<SetVelocityCommand>(gameObject, OnSetVelocityCommand);
        }
    }
}
