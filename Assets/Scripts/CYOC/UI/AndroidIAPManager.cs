using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ICG.Messaging;
using UnityEngine;

namespace Assets.Scripts.CYOC.UI
{
    class AndroidIAPManager : MonoBehaviour
    {
        private void Awake()
        {
            //MessageSystem.SubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
        }

        private void OnDestroy()
        {
            //MessageSystem.UnsubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
        }

        private void Start()
        {
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

            Debug.Log("Connection Responce: " + result.response.ToString() + " " + result.message);
        }

        private void OnRetriveProductsFinised(BillingResult result)
        {
            AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetriveProductsFinised;

            if (result.isSuccess)
            {
                foreach (GooglePurchaseTemplate purchase in AndroidInAppPurchaseManager.instance.inventory.purchases)
                {
                    MessageSystem.BroadcastMessage(new InAppPurchaseMessage(purchase.SKU));
                }
            }
        }

        private void OnProductConsumed(BillingResult obj)
        {
            throw new System.NotImplementedException();
        }

        private void OnProductPurchased(BillingResult obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
