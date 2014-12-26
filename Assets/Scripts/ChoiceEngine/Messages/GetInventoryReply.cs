using System.Collections.Generic;
namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class GetInventoryReply
    {
        public List<Item> Items { get; set; }
        public GetInventoryReply(List<Item> items)
        {
            Items = items;
        }
    }
}
