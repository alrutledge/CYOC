using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ICG.Messaging;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
    public class InAppPurchaseListener : MonoBehaviour
    {
        public Text LinkedTextBox;
        public Button LinkedButton;
        public int ActNumber;
        public MainMenu Menu;
        public List<string> SKUToListenFor;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<InAppPurchaseMessage>(MessageSystem.ServiceContext, OnInAppPurchaseMessage);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<InAppPurchaseMessage>(MessageSystem.ServiceContext, OnInAppPurchaseMessage);
        }

        private void OnInAppPurchaseMessage(InAppPurchaseMessage message)
        {
            if (SKUToListenFor.Contains(message.SKU))
            {
                LinkedTextBox.text = "Owned";
                LinkedButton.onClick.RemoveAllListeners();
                LinkedButton.onClick.AddListener(() => ActPressed());
                MessageSystem.UnsubscribeMessage<InAppPurchaseMessage>(MessageSystem.ServiceContext, OnInAppPurchaseMessage);
            }
        }

        private void ActPressed()
        {
            Menu.NewPressed(ActNumber);
        }
    }
}
