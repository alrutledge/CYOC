using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

namespace Assets.Scripts.ChoiceEngine
{
    public class GameManager : MonoBehaviour
    {
        private Entry CurrentEntry;
        private Act CurrentAct;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
            MessageSystem.SubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnGotoEntryCommand);
            //MessageSystem.SubscribeQuery<CurrentStateReply, CurrentStateQuery>(gameObject, OnCurrentStateQuery);
        }
        
        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
            MessageSystem.UnsubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnGotoEntryCommand);
            //MessageSystem.UnsubscribeQuery<CurrentStateReply, CurrentStateQuery>(gameObject, OnCurrentStateQuery);
        }

        private void OnGotoEntryCommand(GotoEntryCommand message)
        {
            LoadEntry(CurrentAct.Entries[message.ID]);
        }

        private void OnActLoaded(ActLoadedMessage message)
        {
            LoadEntry(message.FirstEntry);
            CurrentAct = message.CurrentAct;
        }

        private void LoadEntry(Entry entry)
        {
            CurrentEntry = entry;
            MessageSystem.BroadcastMessage(new EntryLoadedMessage(entry));
        }
    }
}
