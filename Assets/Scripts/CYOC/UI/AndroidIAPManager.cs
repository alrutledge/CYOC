using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ICG.Messaging;
using UnityEngine;

namespace Assets.Scripts.CYOC.UI
{
    class AndroidIAPManager : MonoBehaviour
    {
        private GameObject m_confirmAct1PurchasePanel;

        private void Awake()
        {
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
                foreach (GooglePurchaseTemplate purchase in AndroidInAppPurchaseManager.instance.inventory.purchases)
                {
                    MessageSystem.BroadcastMessage(new InAppPurchaseMessage(purchase.SKU));
                }
            }
        }

        private void OnProductConsumed(BillingResult result)
        {
            CheckPurchases(result);
        }

        private void OnProductPurchased(BillingResult result)
        {
            CheckPurchases(result);
        }

        public void PurchaseAct2ButtonPressed()
        {
            AndroidInAppPurchaseManager.instance.purchase("com.incharactergames.cyoc.act2");
        }

        public void PurchaseAct3ButtonPressed()
        {
            AndroidInAppPurchaseManager.instance.purchase("com.incharactergames.cyoc.act3");
        }

        public void PurchaseAllActsButtonPressed()
        {
            AndroidInAppPurchaseManager.instance.purchase("com.incharactergames.cyoc.allacts");
        }

        public void ConsumeAllPurchases()
        {
            foreach (GooglePurchaseTemplate purchase in AndroidInAppPurchaseManager.instance.inventory.purchases)
            {
                AndroidInAppPurchaseManager.instance.consume(purchase.SKU);
            }
        }
    }
}
