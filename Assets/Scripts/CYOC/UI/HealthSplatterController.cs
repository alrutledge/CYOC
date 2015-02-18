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
		public Color flashColor;
        public Color clearColor;
		public Image FlashImage;
		public float flashSpeed = 10f;
		private bool damaged = false;
		
		private void Awake()
		{
			MessageSystem.SubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}

		private void Update()
		{
			if (damaged) 
			{

                FlashImage.color = flashColor;
				damaged = false;
                //damaged = false;
				//m_splat.color = flashColor;

                //seconds -= 1 * Time.deltaTime;
                //if (seconds >= 0)
                //{
                //    m_color = m_splat.color;
                //    m_color.a = m_color.a - ((1f / seconds) * Time.deltaTime);
                //    m_splat.color = m_color;
                //}
			}
			else
			{
                FlashImage.color = Color.Lerp(FlashImage.color, clearColor, flashSpeed * Time.deltaTime);
			    //m_splat.color = Color.Lerp (m_splat.color, Color.clear, flashSpeed *  Time.deltaTime);
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
            //    m_color = m_splat.color;
            //    m_color.a = 0f;
            //    m_splat.color = m_color;
            //}

			if (message.StatChanged == PlayerStat.CURRENT_PHYSICAL && message.Delta < 0) 
			{
				damaged =  true;
                //m_color = m_splat.color;
                //m_color.a = 1.0f;
                //m_splat.color = m_color;
                FlashImage.color = flashColor;
			}
		}
	}
}