using UnityEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ChoiceEngine.EntryActions;
using Assets.Scripts.CYOC.UI.Messages;

namespace Assets.Scripts.ChoiceEngine
{
    public class GameManager : MonoBehaviour
    {
        private Act m_currentAct;
        private DelayedGotoEntryCommand m_delayedGotoEntry;
        private int m_entriesLoaded = 0;
        private bool m_entriesSurpressed = false;
        private Entry m_queuedEntry;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
            MessageSystem.SubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnGotoEntryCommand);
            MessageSystem.SubscribeMessage<DelayedGotoEntryCommand>(MessageSystem.ServiceContext, OnDelayedGotoEntryCommand);
            MessageSystem.SubscribeMessage<SupressEntriesCommand>(MessageSystem.ServiceContext, OnSupressEntriesCommand);
            MessageSystem.SubscribeQuery<GetCurrentActReply, GetCurrentActQuery>(MessageSystem.ServiceContext, OnGetCurrentAct);
        }
                
        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
            MessageSystem.UnsubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnGotoEntryCommand);
            MessageSystem.UnsubscribeMessage<DelayedGotoEntryCommand>(MessageSystem.ServiceContext, OnDelayedGotoEntryCommand);
            MessageSystem.UnsubscribeMessage<SupressEntriesCommand>(MessageSystem.ServiceContext, OnSupressEntriesCommand);
            MessageSystem.UnsubscribeQuery<GetCurrentActReply, GetCurrentActQuery>(MessageSystem.ServiceContext, OnGetCurrentAct);
        }

        private GetCurrentActReply OnGetCurrentAct(GetCurrentActQuery message)
        {
            return new GetCurrentActReply(m_currentAct);
        }

        private void OnDelayedGotoEntryCommand(DelayedGotoEntryCommand message)
        {
            m_delayedGotoEntry = message;
        }

        private void OnGotoEntryCommand(GotoEntryCommand message)
        {
            LoadEntry(m_currentAct.Entries[message.ID]);
        }

        private void OnActLoaded(ActLoadedMessage message)
        {
            m_currentAct = message.CurrentAct;
            LoadEntry(message.FirstEntry, false);
        }

        private void OnSupressEntriesCommand(SupressEntriesCommand message)
        {
            m_entriesSurpressed = message.Active;
            if (!m_entriesSurpressed)
            {
                MessageSystem.BroadcastMessage(new EntryLoadedMessage(m_queuedEntry));
            }
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
            if (!m_entriesSurpressed)
            {
                MessageSystem.BroadcastMessage(new EntryLoadedMessage(entry));
            }
            else
            {
                m_queuedEntry = entry;
            }
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
