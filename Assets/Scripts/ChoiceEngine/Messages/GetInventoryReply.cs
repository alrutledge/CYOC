using System.Collections.Generic;
namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class GetInventoryReply
    {
        public Dictionary<string, Item> Items { get; set; }
        public GetInventoryReply(Dictionary<string, Item> items)
        {
            Items = items;
        }
    }
}
