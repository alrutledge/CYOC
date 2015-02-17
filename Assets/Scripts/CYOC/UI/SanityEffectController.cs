using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.ChoiceEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
	public class SanityEffectController : MonoBehaviour
	{
		public PlayerStat PlayerStatistic;
		public int TriggerValue;
		public List <Text> ThingsToChange; 
		public Font FullSanity;
		public Font VeryInsane;

		
		private void Awake()
		{
			MessageSystem.SubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}
		
		private void Update()
		{
			
		}
		
		private void OnDestroy()
		{
			MessageSystem.UnsubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnStatChanged);
		}
		
		private void OnStatChanged(PlayerStatChangedMessage message)
		{
			if (message.StatChanged == PlayerStatistic && message.NewValue > TriggerValue)
			{
			}
			
			if (message.StatChanged == PlayerStatistic && message.NewValue <= TriggerValue) 
			{
				foreach(Text textToChange in ThingsToChange)
				{
					textToChange.font = VeryInsane;
				}
	
			}
		}	
	}
}