using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

namespace Assets.Scripts.ChoiceEngine
{
    public class GameManager : MonoBehaviour
    {
        private Act CurrentAct;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
            MessageSystem.SubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnGotoEntryCommand);
        }
        
        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
            MessageSystem.UnsubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnGotoEntryCommand);
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
            MessageSystem.BroadcastMessage(new EntryLoadedMessage(entry));
        }
    }
}
