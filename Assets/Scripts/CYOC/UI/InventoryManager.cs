using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ChoiceEngine;
using System.Collections.Generic;

namespace Assets.Scripts.CYOC.UI
{
    public class InventoryManager : MonoBehaviour
    {
        private List<Item> m_currentInventory;
        private bool m_inventoryChanged = false;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<ProcessInventoryCommand>(MessageSystem.ServiceContext, OnProcessInventoryCommand);
            MessageSystem.SubscribeMessage<InventoryItemAdded>(MessageSystem.ServiceContext, OnInventoryItemAdded);
            MessageSystem.SubscribeMessage<InventoryItemRemoved>(MessageSystem.ServiceContext, OnInventoryItemRemoved);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<ProcessInventoryCommand>(MessageSystem.ServiceContext, OnProcessInventoryCommand);
            MessageSystem.UnsubscribeMessage<InventoryItemAdded>(MessageSystem.ServiceContext, OnInventoryItemAdded);
            MessageSystem.UnsubscribeMessage<InventoryItemRemoved>(MessageSystem.ServiceContext, OnInventoryItemRemoved);
        }

        private void Start()
        {
            GetInventoryReply inventoryMessage = MessageSystem.BroadcastQuery<GetInventoryReply, GetInventoryQuery>(new GetInventoryQuery());
            m_currentInventory = inventoryMessage.Items;
            m_inventoryChanged = true;
        }

        private void OnInventoryItemAdded(InventoryItemAdded message)
        {
            // TODO: add the item to the inventory
            m_inventoryChanged = true;
        }

        private void OnInventoryItemRemoved(InventoryItemRemoved message)
        {
            // TODO: remove the item from the inventory
            m_inventoryChanged = true;
        }

        private void OnProcessInventoryCommand(ProcessInventoryCommand message)
        {
            if (m_inventoryChanged)
            {
                // TODO: process all the current inventory and place it in the UI
                m_inventoryChanged = false;
            }
        }
    }
}
