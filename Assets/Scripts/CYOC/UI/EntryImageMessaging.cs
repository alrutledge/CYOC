using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.CYOC.UI
{
    public class EntryImageMessaging : MonoBehaviour
    {
        private Image EntryImage;
     
        private void Awake()
        {
            EntryImage = gameObject.GetComponent<Image>();
            MessageSystem.SubscribeMessage<EntryLoadedMessage>(MessageSystem.ServiceContext, OnEntryLoaded);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<EntryLoadedMessage>(MessageSystem.ServiceContext, OnEntryLoaded);
        }

        private void OnEntryLoaded(EntryLoadedMessage message)
        {
            if (message.LoadedEntry.ImageResource != null)
            {
                gameObject.SetActive(true);
                EntryImage.sprite = Resources.Load(message.LoadedEntry.ImageResource, typeof(Sprite)) as Sprite;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
