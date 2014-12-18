using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.TransformMessaging
{
    public class TransformMessaging : MonoBehaviour
    {
        private Transform m_transform;
        private Vector3 m_oldPosition;
        private Quaternion m_oldRotation;
        private Vector3 m_oldForward;

        private void Awake()
        {
            m_transform = GetComponent<Transform>();
            MessageSystem.SubscribeMessage<TranslateCommand>(gameObject, OnTranslateCommand);
            MessageSystem.SubscribeMessage<RotateCommand>(gameObject, OnRotateCommand);
            MessageSystem.SubscribeMessage<LookAtCommand>(gameObject, OnLookAtCommand);
            MessageSystem.SubscribeMessage<ParentCommand>(gameObject, OnParentCommand);
            MessageSystem.SubscribeQuery<ChildQueryAnswer, ChildQuery>(gameObject, OnChildQuery);
            MessageSystem.SubscribeQuery<ParentReply, ParentQuery>(gameObject, OnParentQuery);
            MessageSystem.SubscribeQuery<PositionReply, PositionQuery>(gameObject, OnPositionQuery);
            MessageSystem.SubscribeQuery<ForwardReply, ForwardQuery>(gameObject, OnForwardQuery);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<TranslateCommand>(gameObject, OnTranslateCommand);
            MessageSystem.UnsubscribeMessage<RotateCommand>(gameObject, OnRotateCommand);
            MessageSystem.UnsubscribeMessage<LookAtCommand>(gameObject, OnLookAtCommand);
            MessageSystem.UnsubscribeMessage<ParentCommand>(gameObject, OnParentCommand);
            MessageSystem.UnsubscribeQuery<ChildQueryAnswer, ChildQuery>(gameObject, OnChildQuery);
            MessageSystem.UnsubscribeQuery<ParentReply, ParentQuery>(gameObject, OnParentQuery);
            MessageSystem.UnsubscribeQuery<PositionReply, PositionQuery>(gameObject, OnPositionQuery);
            MessageSystem.UnsubscribeQuery<ForwardReply, ForwardQuery>(gameObject, OnForwardQuery);
        }

        private void OnLookAtCommand(LookAtCommand command)
        {
            m_transform.LookAt(command.LookAtPosition, command.Up);
        }

        private void OnTranslateCommand(TranslateCommand command)
        {
            m_transform.Translate(command.TranslationVector, command.TranslationSpace);
        }

        private void OnRotateCommand(RotateCommand command)
        {
            m_transform.Rotate(command.RotationVector, command.Angle);
        }

        private void OnParentCommand(ParentCommand command)
        {
            m_transform.parent = command.Parent;
        }

        private ForwardReply OnForwardQuery(ForwardQuery query)
        {
            return new ForwardReply(m_transform.forward);
        }

        private ChildQueryAnswer OnChildQuery(ChildQuery query)
        {
            ChildQueryAnswer answer = new ChildQueryAnswer(new List<GameObject>());
            foreach (Transform transform in m_transform)
            {
                answer.Children.Add(transform.gameObject);
            }
            return answer;
        }

        private ParentReply OnParentQuery(ParentQuery query)
        {
            return new ParentReply(m_transform.parent.gameObject);
        }

        private PositionReply OnPositionQuery(PositionQuery query)
        {
            return new PositionReply(m_transform.position);
        }

        private void Update()
        {
            if (m_oldPosition != m_transform.position)
            {
                MessageSystem.SendMessage(gameObject, new PositionChangedMessage(m_transform.position));
                m_oldPosition = m_transform.position;
            }
            if (m_oldRotation != m_transform.rotation)
            {
                MessageSystem.SendMessage(gameObject, new RotationChangedMessage(m_transform.rotation));
                m_oldRotation = m_transform.rotation;
            }
            if (m_oldForward != m_transform.forward)
            {
                MessageSystem.SendMessage(gameObject, new ForwardChangedMessage(m_transform.forward));
                m_oldForward = m_transform.forward;
            }
        }
    }
}
