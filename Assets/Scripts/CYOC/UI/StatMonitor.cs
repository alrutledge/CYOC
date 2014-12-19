using UnityEngine;
using System.Collections;
using Assets.Scripts.ChoiceEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
    public class StatMonitor : MonoBehaviour
    {
        public PlayerStat StatToMonitor;
        private Text m_text;

        private void Awake()
        {
            m_text = gameObject.GetComponent<Text>();
            MessageSystem.SubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
        }

        private void OnStatChanged(PlayerStatChangedMessage message)
        {
            if (message.StatChanged == StatToMonitor)
            {
                m_text.text = message.NewValue.ToString();
            }
        }
    }
}
