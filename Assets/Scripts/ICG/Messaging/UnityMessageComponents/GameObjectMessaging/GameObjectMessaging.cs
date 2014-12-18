using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.GameObjectMessaging
{
    public class GameObjectMessaging : MonoBehaviour
    {
        private GameObject m_gameObject;

        private void Awake()
        {
            m_gameObject = gameObject;
            MessageSystem.SubscribeMessage<SetActiveCommand>(gameObject, OnSetActiveCommand);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<SetActiveCommand>(gameObject, OnSetActiveCommand);
        }

        private void OnSetActiveCommand(SetActiveCommand command)
        {
            m_gameObject.SetActive(command.Active);
        }
    }
}
