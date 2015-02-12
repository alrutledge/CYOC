namespace Assets.Scripts.CYOC.UI.Messages
{
    public class InAppPurchaseMessage
    {
        public string SKU { get; set; }
        public InAppPurchaseMessage(string sku)
        {
            SKU = sku;
        }
    }
}
