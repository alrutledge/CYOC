using UnityEngine;
using System.Collections;
using Assets.Scripts.ChoiceEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
	public class HealthFlashController : MonoBehaviour
	{
		public Color flashColor;
        public Color clearColor;
		public float flashSpeed = 10f;
		private bool damaged = false;
		private Image FlashImage;

		
		private void Awake()
		{
			MessageSystem.SubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
            FlashImage = gameObject.GetComponent<Image>();
		}


		private void Update()
		{
			if (damaged) 
			{

                FlashImage.color = flashColor;
				damaged = false;
			}
			else
			{
                FlashImage.color = Color.Lerp(FlashImage.color, clearColor, flashSpeed * Time.deltaTime);
			}

		}

		private void OnDestroy()
		{
			MessageSystem.UnsubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}
		
		private void OnStatChanged(PlayerStatChangedMessage message)
		{
			if (message.StatChanged == PlayerStat.CURRENT_PHYSICAL && message.Delta < 0) 
			{
				damaged =  true;
                FlashImage.color = flashColor;
			}
	}
}
}
