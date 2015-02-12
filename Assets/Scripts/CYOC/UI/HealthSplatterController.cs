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
		public PlayerStat PlayerStatistic;
		public int TriggerValue;
		private Image m_image;
		private Color m_color;
		
		private void Awake()
		{
			m_image = GetComponent<Image> ();
			MessageSystem.SubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}
		
		private void OnDestroy()
		{
			MessageSystem.UnsubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}
		
		private void OnStatChanged(PlayerStatChangedMessage message)
		{
			if (message.StatChanged == PlayerStatistic && message.NewValue > TriggerValue)
			{
				m_color = m_image.color;
				m_color.a = 0f;
				m_image.color = m_color;
			}

			if (message.StatChanged == PlayerStatistic && message.NewValue <= TriggerValue)
			{
				m_color = m_image.color;
				m_color.a = 0.25f;
				m_image.color = m_color;
			}

		}
	}
}
