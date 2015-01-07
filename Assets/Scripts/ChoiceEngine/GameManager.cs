using UnityEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ChoiceEngine.EntryActions;
using Assets.Scripts.CYOC.UI.Messages;

namespace Assets.Scripts.ChoiceEngine
{
    public class GameManager : MonoBehaviour
    {
        private Act CurrentAct;
        private DelayedGotoEntryCommand m_delayedGotoEntry;
        private int m_entriesLoaded = 0;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
            MessageSystem.SubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnGotoEntryCommand);
            MessageSystem.SubscribeMessage<DelayedGotoEntryCommand>(MessageSystem.ServiceContext, OnDelayedGotoEntryCommand);
        }
        
        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
            MessageSystem.UnsubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnGotoEntryCommand);
            MessageSystem.UnsubscribeMessage<DelayedGotoEntryCommand>(MessageSystem.ServiceContext, OnDelayedGotoEntryCommand);
        }

        private void OnDelayedGotoEntryCommand(DelayedGotoEntryCommand message)
        {
            m_delayedGotoEntry = message;
        }

        private void OnGotoEntryCommand(GotoEntryCommand message)
        {
            LoadEntry(CurrentAct.Entries[message.ID]);
        }

        private void OnActLoaded(ActLoadedMessage message)
        {
            CurrentAct = message.CurrentAct;
            LoadEntry(message.FirstEntry, false);
        }

        private void LoadEntry(Entry entry, bool runActions = true)
        {
            m_entriesLoaded++;
            if (m_entriesLoaded >= 8)
            {
                m_entriesLoaded = 0;
                MessageSystem.BroadcastMessage(new DisplayAdCommand());
            }
            foreach (EntryAction action in entry.Actions)
            {
                if (runActions || action.AlwaysRun())
                {
                    action.PerformAction();
                }
            }
            MessageSystem.BroadcastMessage(new EntryLoadedMessage(entry));
        }

        private void Update()
        {
            if (m_delayedGotoEntry != null)
            {
                MessageSystem.BroadcastMessage(new GotoEntryCommand(m_delayedGotoEntry.ID));
                m_delayedGotoEntry = null;
            }
        }
    }
}
