using UnityEngine;
using System.Collections;
using Assets.Scripts.ChoiceEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
	public class HealthSplatterController : MonoBehaviour
	{
		public float TriggerValue;
		private Color m_color;
		private Image bloodSplatOne;

		
		private void Awake()
		{
			MessageSystem.SubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
            bloodSplatOne = gameObject.GetComponent<Image>();
		}

        private void Start()
        {
            m_color = bloodSplatOne.color;
            m_color.a = 0.0f;
            bloodSplatOne.color = m_color;
        }

		private void OnDestroy()
		{
			MessageSystem.UnsubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}
		
		private void OnStatChanged(PlayerStatChangedMessage message)
		{
			if (message.StatChanged == PlayerStat.CURRENT_PHYSICAL && message.NewValue <= TriggerValue)
			{
			    m_color = bloodSplatOne.color;
			    m_color.a = 1.0f;
			    bloodSplatOne.color = m_color;
			}

			else if (message.StatChanged == PlayerStat.CURRENT_PHYSICAL && message.NewValue > TriggerValue)
			{
				m_color = bloodSplatOne.color;
				m_color.a = 0.0f;
				bloodSplatOne.color = m_color;
			}

	}
}
}
