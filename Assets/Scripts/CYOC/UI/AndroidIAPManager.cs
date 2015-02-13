using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ICG.Messaging;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
    class AndroidIAPManager : MonoBehaviour
    {
        private GameObject m_confirmAct1PurchasePanel;
        private Button m_act2Button;
        private Button m_act3Button;
        private Button m_allActsButton;
        public MainMenu Menu;

        private void Awake()
        {
            m_act2Button = GameObject.Find("Act2Button").GetComponent<Button>();
            m_act3Button = GameObject.Find("Act3Button").GetComponent<Button>();
            m_allActsButton = GameObject.Find("BuyAllActs").GetComponent<Button>();

            m_confirmAct1PurchasePanel = GameObject.Find("ConfirmPurchasePanel");
            //MessageSystem.SubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
        }

        private void OnDestroy()
        {
            //MessageSystem.UnsubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
        }

        private void Start()
        {
            m_confirmAct1PurchasePanel.SetActive(false);
            AndroidInAppPurchaseManager.ActionProductPurchased += OnProductPurchased;
            AndroidInAppPurchaseManager.ActionProductConsumed += OnProductConsumed;
            AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;
            AndroidInAppPurchaseManager.instance.loadStore();
        }

        private void OnBillingConnected(BillingResult result)
        {
            AndroidInAppPurchaseManager.ActionBillingSetupFinished -= OnBillingConnected;
            if (result.isSuccess)
            {
                AndroidInAppPurchaseManager.instance.retrieveProducDetails();
                AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += OnRetriveProductsFinised;
            }
        }

        private void OnRetriveProductsFinised(BillingResult result)
        {
            AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetriveProductsFinised;

            CheckPurchases(result);
        }

        private static void CheckPurchases(BillingResult result)
        {
            if (result.isSuccess)
            {
                MessageSystem.BroadcastMessage(new InAppPurchaseMessage(AndroidInAppPurchaseManager.instance.inventory.purchases));  
            }
        }

        private void OnProductConsumed(BillingResult result)
        {
            CheckPurchases(result);
        }

        private void OnProductPurchased(BillingResult result)
        {
            CheckPurchases(result);
            if (result.isSuccess)
            {
                CheckButtons();
            }
        }

        private void CheckButtons()
        {
            throw new System.NotImplementedException();
        }

        public void PurchaseAct2ButtonPressed()
        {
            bool purchased = false;
            foreach (GooglePurchaseTemplate purchase in AndroidInAppPurchaseManager.instance.inventory.purchases)
            {
                if (purchase.SKU == "com.incharactergames.cyoc.act2" ||
                    purchase.SKU == "com.incharactergames.cyoc.allacts")
                {
                    purchased = true;
                    break;
                }
            }
            if (purchased)
            {
                ActPressed(2);
            }
            else
            {
                AndroidInAppPurchaseManager.instance.purchase("com.incharactergames.cyoc.act2");
            }
        }

        public void PurchaseAct3ButtonPressed()
        {
            bool purchased = false;
            foreach (GooglePurchaseTemplate purchase in AndroidInAppPurchaseManager.instance.inventory.purchases)
            {
                if (purchase.SKU == "com.incharactergames.cyoc.act3" ||
                    purchase.SKU == "com.incharactergames.cyoc.allacts")
                {
                    purchased = true;
                    break;
                }
            }
            if (purchased)
            {
                ActPressed(2);
            }
            else
            {
                AndroidInAppPurchaseManager.instance.purchase("com.incharactergames.cyoc.act2");
            }
        }

        public void PurchaseAllActsButtonPressed()
        {
            bool purchased = false;
            bool act2Purchased = false;
            bool act3Purchased = false;
            foreach (GooglePurchaseTemplate purchase in AndroidInAppPurchaseManager.instance.inventory.purchases)
            {
                if (purchase.SKU == "com.incharactergames.cyoc.allacts")
                {
                    purchased = true;
                }
                else if (purchase.SKU == "com.incharactergames.cyoc.act2")
                {
                    act2Purchased = true;
                }
                else if (purchase.SKU == "com.incharactergames.cyoc.act3")
                {
                    act3Purchased = true;
                }
            }
            if (act2Purchased && act3Purchased)
            {
                purchased = true;
            }
            if (!purchased)
            {
                AndroidInAppPurchaseManager.instance.purchase("com.incharactergames.cyoc.allacts");
            }
        }

        public void ConsumeAllPurchases()
        {
            foreach (GooglePurchaseTemplate purchase in AndroidInAppPurchaseManager.instance.inventory.purchases)
            {
                AndroidInAppPurchaseManager.instance.consume(purchase.SKU);
            }
        }

        private void ActPressed(int act)
        {
            Menu.NewPressed(act);
        }
    }
}
