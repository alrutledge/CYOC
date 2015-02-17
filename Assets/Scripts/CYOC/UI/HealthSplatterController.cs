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
		public Color flashColor = new Color (1f, 0f, 0f, 0.1f);
		public Image damageImage;
		public Image m_splat;
		public float flashSpeed = 10f;
		private Color m_color;
		private float seconds = 3;
		private bool damaged;
		
		private void Awake()
		{
			m_splat = GetComponent<Image> ();
			damageImage = GetComponent<Image> ();
			MessageSystem.SubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}

		private void Update()
		{
			if (damaged) 
			{

				//seconds -= 1 * Time.deltaTime;
				damageImage.color = flashColor;
				m_splat.color = flashColor;
				//if (seconds >= 0) 
				//{
				//	m_color = m_splat.color; 
				//	m_color.a = m_color.a - ((1f / seconds) * Time.deltaTime);
				//	m_splat.color = m_color;
				//}
			}
			else
			{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
			m_splat.color = Color.Lerp (m_splat.color, Color.clear, flashSpeed *  Time.deltaTime);

			}
			damaged = false;

		}

		private void OnDestroy()
		{
			MessageSystem.UnsubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}
		
		private void OnStatChanged(PlayerStatChangedMessage message)
		{
			if (message.StatChanged == PlayerStatistic && message.NewValue > TriggerValue)
			{
				m_color = m_splat.color;
				m_color.a = 0f;
				m_splat.color = m_color;
			}

			if (message.StatChanged == PlayerStatistic && message.NewValue <= TriggerValue) 
			{
				damaged =  true;
				m_color = m_splat.color;
				m_color.a = 1.0f;
				m_splat.color = m_color;
				damageImage.color = flashColor;
			}
		}
	}
}