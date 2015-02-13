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
        public int ActNumber;
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
            bool purchased = false;
            bool act2Purchased = false;
            bool act3Purchased = false;
            foreach(GooglePurchaseTemplate purchase in message.Inventory)
            {
                if (SKUToListenFor.Contains(purchase.SKU))
                {
                    purchased = true;
                }
                if (purchase.SKU == "com.incharactergames.cyoc.act2")
                {
                    act2Purchased = true;
                }
                else if (purchase.SKU == "com.incharactergames.cyoc.act3")
                {
                    act3Purchased = true;
                }
            }
            if (ActNumber == 0 && act2Purchased && act3Purchased) // Special case for buy all acts.
            {
                purchased = true;
            }
            if (purchased)
            {
                LinkedTextBox.text = "Owned";
            }
        }

    }
}
