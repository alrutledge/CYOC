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
		private float seconds = 3;
		private bool renderSplat = false;
		
		private void Awake()
		{
			m_image = GetComponent<Image> ();
			MessageSystem.SubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}

		private void Update()
		{
			if (renderSplat) 
			{
				seconds -= 1 * Time.deltaTime;
				if (seconds >= 0) 
				{
					m_color = m_image.color; 
					m_color.a = m_color.a - ((1f/seconds) * Time.deltaTime);
					m_image.color = m_color;
					Debug.Log(m_color.a);
				}
			}
		}

		private void OnDestroy()
		{
			MessageSystem.UnsubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}
		
		private void OnStatChanged(PlayerStatChangedMessage message)
		{
			//if (message.StatChanged == PlayerStatistic && message.NewValue > TriggerValue)
			//{
				//m_color = m_image.color;
				//m_color.a = 0f;
				//m_image.color = m_color;
			//}

			if (message.StatChanged == PlayerStatistic && message.NewValue <= TriggerValue) 
			{
				renderSplat =  true;
				m_color = m_image.color;
				m_color.a = 1.0f;
				m_image.color = m_color;
			}
		}
	}
}