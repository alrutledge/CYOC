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
		public int TriggerValueOne;
		public List <Text> ThingsToChange; 
		public Font FullSanity;
		public Font SanityLossOne;
		public Font SanityLossTwo;
		public Font SanityLossThree;
		public Font Insane;

		
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
			if (message.StatChanged == PlayerStatistic && message.NewValue > TriggerValueOne)
			{
			
			}
			
			if (message.StatChanged == PlayerStatistic && message.NewValue <= TriggerValueOne/100) 
			{
				foreach (Text textToChange in ThingsToChange) 
				{
					textToChange.font = Insane;
				}
			}
			else if (message.StatChanged == PlayerStatistic && message.NewValue < TriggerValueOne/3) 
			{
				foreach (Text textToChange in ThingsToChange) 
				{
					textToChange.font = SanityLossOne;
				}
			}
            else if (message.StatChanged == PlayerStatistic && message.NewValue <= TriggerValueOne / 2)
            {
                foreach (Text textToChange in ThingsToChange)
                {
                    textToChange.font = SanityLossTwo;
                }
            }
            else
            {
                foreach (Text textToChange in ThingsToChange)
                {
                    textToChange.font = FullSanity;
                }
            }
		}	
	}
}