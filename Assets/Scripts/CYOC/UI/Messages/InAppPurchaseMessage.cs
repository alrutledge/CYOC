using System.Collections.Generic;
namespace Assets.Scripts.CYOC.UI.Messages
{
    public class InAppPurchaseMessage
    {
        public List<GooglePurchaseTemplate> Inventory { get; set; }
        public InAppPurchaseMessage(List<GooglePurchaseTemplate> inventory)
        {
            Inventory = inventory;
        }
    }
}
