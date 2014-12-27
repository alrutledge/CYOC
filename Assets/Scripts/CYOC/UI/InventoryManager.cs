using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ChoiceEngine;
using System.Collections.Generic;
using UnityEditor.VersionControl;

namespace Assets.Scripts.CYOC.UI
{
    public class InventoryManager : MonoBehaviour
    {
        private Dictionary<string, Item> m_currentInventory;
        private bool m_inventoryChanged = false;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<ProcessInventoryCommand>(MessageSystem.ServiceContext, OnProcessInventoryCommand);
            MessageSystem.SubscribeMessage<InventoryItemAdded>(MessageSystem.ServiceContext, OnInventoryItemAdded);
            MessageSystem.SubscribeMessage<InventoryItemRemoved>(MessageSystem.ServiceContext, OnInventoryItemRemoved);
            MessageSystem.SubscribeMessage<LoadActCommand>(MessageSystem.ServiceContext, OnLoadActCommand);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<ProcessInventoryCommand>(MessageSystem.ServiceContext, OnProcessInventoryCommand);
            MessageSystem.UnsubscribeMessage<InventoryItemAdded>(MessageSystem.ServiceContext, OnInventoryItemAdded);
            MessageSystem.UnsubscribeMessage<InventoryItemRemoved>(MessageSystem.ServiceContext, OnInventoryItemRemoved);
            MessageSystem.UnsubscribeMessage<LoadActCommand>(MessageSystem.ServiceContext, OnLoadActCommand);
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
                // TODO: process all the current inventory and place it in the UI
                m_inventoryChanged = false;
            }
        }
    }
}
