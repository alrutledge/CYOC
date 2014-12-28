using UnityEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ChoiceEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
    public class InventoryManager : MonoBehaviour
    {
        private Dictionary<string, Item> m_currentInventory;
        private bool m_inventoryChanged = false;
        private GameObject m_itemName;
        private GameObject m_itemDescription;
        private GameObject m_itemImage;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<ProcessInventoryCommand>(MessageSystem.ServiceContext, OnProcessInventoryCommand);
            MessageSystem.SubscribeMessage<InventoryItemAdded>(MessageSystem.ServiceContext, OnInventoryItemAdded);
            MessageSystem.SubscribeMessage<InventoryItemRemoved>(MessageSystem.ServiceContext, OnInventoryItemRemoved);
            MessageSystem.SubscribeMessage<LoadActCommand>(MessageSystem.ServiceContext, OnLoadActCommand);
            m_itemImage = GameObject.Find("ItemImage");
            m_itemDescription = GameObject.Find("ItemDescription");
            m_itemName = GameObject.Find("ItemName");
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<ProcessInventoryCommand>(MessageSystem.ServiceContext, OnProcessInventoryCommand);
            MessageSystem.UnsubscribeMessage<InventoryItemAdded>(MessageSystem.ServiceContext, OnInventoryItemAdded);
            MessageSystem.UnsubscribeMessage<InventoryItemRemoved>(MessageSystem.ServiceContext, OnInventoryItemRemoved);
            MessageSystem.UnsubscribeMessage<LoadActCommand>(MessageSystem.ServiceContext, OnLoadActCommand);
        }

        private void Start()
        {
            ResetItemDetails();
        }

        private void ResetItemDetails()
        {
            m_itemName.SetActive(false);
            m_itemDescription.SetActive(false);
            m_itemImage.SetActive(false);
        }

        private void OnLoadActCommand(LoadActCommand command)
        {
            GetInventoryReply inventoryMessage = MessageSystem.BroadcastQuery<GetInventoryReply, GetInventoryQuery>(new GetInventoryQuery());

            m_currentInventory = inventoryMessage.Items;

            m_inventoryChanged = true;
        }

        private void OnInventoryItemAdded(InventoryItemAdded message)
        {
            if (m_currentInventory.ContainsKey(message.Name)) return;
            m_currentInventory.Add(message.Name, new Item(message.Name, message.Description, message.SmallImage, message.LargeImage));
            m_inventoryChanged = true;
        }

        private void OnInventoryItemRemoved(InventoryItemRemoved message)
        {
            if (m_currentInventory.ContainsKey(message.Name))
            {
                m_currentInventory.Remove(message.Name);
                m_inventoryChanged = true;
            }
        }

        private void OnProcessInventoryCommand(ProcessInventoryCommand message)
        {
            if (m_inventoryChanged)
            {
                ResetItemDetails();

                int slot = 1;
                foreach (Item item in m_currentInventory.Values)
                {
                    GameObject slotGO = GameObject.Find("InventorySlot" + slot.ToString());
                    ItemButton itemButton = slotGO.GetComponent<ItemButton>();
                    itemButton.LinkedItem = item;
                    slotGO.GetComponent<Button>().interactable = true;
                    Image image = slotGO.GetComponent<Image>();
                    image.sprite = Resources.Load(item.SmallImage, typeof(Sprite)) as Sprite;
                    slot++;
                }
                for (int i = slot; i <= 10; i++)
                {
                    GameObject slotGO = GameObject.Find("InventorySlot" + i.ToString());
                    ItemButton itemButton = slotGO.GetComponent<ItemButton>();
                    itemButton.LinkedItem = null;
                    slotGO.GetComponent<Button>().interactable = false;
                    Image image = slotGO.GetComponent<Image>();
                    image.sprite = Resources.Load("paper64", typeof(Sprite)) as Sprite;
                }
                m_inventoryChanged = false;
            }
        }
    }
}
