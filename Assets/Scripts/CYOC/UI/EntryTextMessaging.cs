using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Assets.Scripts.ChoiceEngine;

namespace Assets.Scripts.CYOC.UI
{
    public class EntryTextMessaging : MonoBehaviour
    {
        public bool WithImageText;
        private Text EntryText;
        private List<PlayerStatChangedMessage> m_statChangesCache = new List<PlayerStatChangedMessage>();

        private void Awake()
        {
            EntryText = gameObject.GetComponent<Text>();
            MessageSystem.SubscribeMessage<EntryLoadedMessage>(MessageSystem.ServiceContext, OnEntryLoaded);
            MessageSystem.SubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnPlayerStatChangedMessage);
			MessageSystem.SubscribeMessage<ChangeFontCommand>(MessageSystem.ServiceContext, OnChangeFontCommand);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<EntryLoadedMessage>(MessageSystem.ServiceContext, OnEntryLoaded);
            MessageSystem.UnsubscribeMessage<PlayerStatChangedMessage>(MessageSystem.ServiceContext, OnPlayerStatChangedMessage);
        }

		private void OnChangeFontCommand (ChangeFontCommand message)
		{
			Font toUse = Resources.Load(message.Name, typeof(Font))as Font;
			EntryText.font = toUse;
		}

        private void OnPlayerStatChangedMessage(PlayerStatChangedMessage message)
        {
            if (message.Delta == 0)
            {
                return;
            }

            m_statChangesCache.Add(message);
        }

        private void OnEntryLoaded(EntryLoadedMessage message)
        {
            if ((message.LoadedEntry.ImageResource != null && WithImageText)||
                (message.LoadedEntry.ImageResource == null && !WithImageText))
            {
                EntryText.text = message.LoadedEntry.Text;
                
                if (m_statChangesCache.Count > 0)
                {
                    EntryText.text += "\n";
                    foreach (PlayerStatChangedMessage statMessage in m_statChangesCache)
                    {
                        string stat = "";
                        if (statMessage.StatChanged == ChoiceEngine.PlayerStat.CURRENT_MENTAL)
                        {
                            stat = "Sanity";
                        }
                        else
                        {
                            stat = "Health";
                        }
                        if (statMessage.Delta > 0)
                        {
                            EntryText.text += "<color=green>" + stat + " raised " + statMessage.Delta + "%</color>\n";
                        }
                        else
                        {
                            EntryText.text += "<color=red>" + stat + " lowered " + (Math.Abs(statMessage.Delta)) + "%</color>\n";
                        }
                    }

                    m_statChangesCache.Clear();
                }
            }
            else
            {
                EntryText.text = "";
            }
        }
    }
}
