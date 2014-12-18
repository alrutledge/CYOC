using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
    public class EntryTextMessaging : MonoBehaviour
    {
        private Text EntryText;

        private void Awake()
        {
            EntryText = gameObject.GetComponent<Text>();
            MessageSystem.SubscribeMessage<EntryLoadedMessage>(MessageSystem.ServiceContext, OnEntryLoaded);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<EntryLoadedMessage>(MessageSystem.ServiceContext, OnEntryLoaded);
        }

        private void OnEntryLoaded(EntryLoadedMessage message)
        {
            EntryText.text = message.LoadedEntry.Text;
        }
    }
}
