﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using System.IO;
//using Newtonsoft.Json;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


namespace Assets.Scripts.ChoiceEngine
{
    public class PlayerManager : MonoBehaviour
    {

        private Player m_player;
        private Text m_description;
        private Text m_age;
        private Text m_profession;
        private Text m_name;
        private BinaryFormatter m_formatter = new BinaryFormatter();
        private FileStream m_file;

        private void Awake()
        {
            m_name = GameObject.Find("CharacterPanelName").GetComponent<Text>();
            m_age = GameObject.Find("CharacterPanelAge").GetComponent<Text>();
            m_description = GameObject.Find("CharacterPanelDescription").GetComponent<Text>();
            m_profession = GameObject.Find("CharacterPanelProfession").GetComponent<Text>();
            MessageSystem.SubscribeMessage<CharacterSelectedMessage>(MessageSystem.ServiceContext, OnCharacterSelected);
            MessageSystem.SubscribeMessage<LoadGameCommand>(MessageSystem.ServiceContext, OnLoadGame);
            MessageSystem.SubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnEntryLoaded);
            MessageSystem.SubscribeMessage<ModifyAttributeCommand>(MessageSystem.ServiceContext, OnModifyAttributeCommand);
            MessageSystem.SubscribeMessage<AddFlagCommand>(MessageSystem.ServiceContext, OnAddFlag);
            MessageSystem.SubscribeMessage<RemoveFlagCommand>(MessageSystem.ServiceContext, OnRemoveFlag);
            MessageSystem.SubscribeQuery<SaveGameAnswer, SaveGameQuery>(gameObject, OnSaveGameQuery);
            MessageSystem.SubscribeQuery<RequirementReply, RequirementQuery>(gameObject, OnRequirementQuery);
            MessageSystem.SubscribeQuery<GetInventoryReply, GetInventoryQuery>(MessageSystem.ServiceContext, OnGetInventoryQuery);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<CharacterSelectedMessage>(MessageSystem.ServiceContext, OnCharacterSelected);
            MessageSystem.UnsubscribeMessage<LoadGameCommand>(MessageSystem.ServiceContext, OnLoadGame);
            MessageSystem.UnsubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnEntryLoaded);
            MessageSystem.UnsubscribeMessage<ModifyAttributeCommand>(MessageSystem.ServiceContext, OnModifyAttributeCommand);
            MessageSystem.UnsubscribeMessage<AddFlagCommand>(MessageSystem.ServiceContext, OnAddFlag);
            MessageSystem.UnsubscribeMessage<RemoveFlagCommand>(MessageSystem.ServiceContext, OnRemoveFlag);
            MessageSystem.UnsubscribeQuery<SaveGameAnswer, SaveGameQuery>(gameObject, OnSaveGameQuery);
            MessageSystem.UnsubscribeQuery<RequirementReply, RequirementQuery>(gameObject, OnRequirementQuery);
            MessageSystem.UnsubscribeQuery<GetInventoryReply, GetInventoryQuery>(MessageSystem.ServiceContext, OnGetInventoryQuery);
        }

        private void OnRemoveFlag(RemoveFlagCommand message)
        {
            if (m_player.Flags.ContainsKey(message.Name))
            {
                m_player.Flags.Remove(message.Name);
            }
        }

        private void OnAddFlag(AddFlagCommand message)
        {
            if (m_player.Flags.ContainsKey(message.Name)) return;
            m_player.Flags.Add(message.Name, true);
        }

        private void OnModifyAttributeCommand(ModifyAttributeCommand command)
        {
            int initialStat = m_player.Stats[command.PlayerStat];
            m_player.Stats[command.PlayerStat] = m_player.Stats[command.PlayerStat] + command.Delta;

            if (command.PlayerStat == PlayerStat.CURRENT_MENTAL) 

            {
                if (m_player.Stats[command.PlayerStat] > m_player.Stats[PlayerStat.MAX_MENTAL])
                {
                    m_player.Stats[command.PlayerStat] = m_player.Stats[PlayerStat.MAX_MENTAL];
                }

                // check for insanity
                if (m_player.Stats[PlayerStat.CURRENT_MENTAL] <=0)
                {
                    MessageSystem.BroadcastMessage(new DelayedGotoEntryCommand(-1));
                }
            }
            else if (command.PlayerStat == PlayerStat.CURRENT_PHYSICAL)
            {
                if (m_player.Stats[command.PlayerStat] > m_player.Stats[PlayerStat.MAX_PHYSICAL])
                {
                    m_player.Stats[command.PlayerStat] = m_player.Stats[PlayerStat.MAX_PHYSICAL];
                }
                // check for death
                if (m_player.Stats[PlayerStat.CURRENT_PHYSICAL] <= 0)
                {
                    MessageSystem.BroadcastMessage(new DelayedGotoEntryCommand(-2));
                }
            }
            else if (command.PlayerStat == PlayerStat.CURRENT_SOCIAL)
            {
                if (m_player.Stats[command.PlayerStat] > m_player.Stats[PlayerStat.MAX_SOCIAL])
                {
                    m_player.Stats[command.PlayerStat] = m_player.Stats[PlayerStat.MAX_SOCIAL];
                }
            }

            if (initialStat != m_player.Stats[command.PlayerStat])
            {
                MessageSystem.BroadcastMessage(new PlayerStatChangedMessage(command.PlayerStat, m_player.Stats[command.PlayerStat]));
            }

        }

        private RequirementReply OnRequirementQuery(RequirementQuery message)
        {
            RequirementReply reply = new RequirementReply();
            reply.RequirementMet = false;
            switch (message.Requirement.Type)
            {
                case ChoiceRequirementType.ATTRIBUTE_CURRENT_MENTAL:
                    if (m_player.Stats[PlayerStat.CURRENT_MENTAL] >= System.Int32.Parse(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;

                case ChoiceRequirementType.ATTRIBUTE_CURRENT_PHYSICAL:
                    if (m_player.Stats[PlayerStat.CURRENT_PHYSICAL] >= System.Int32.Parse(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;

                case ChoiceRequirementType.ATTRIBUTE_CURRENT_SOCIAL:
                    if (m_player.Stats[PlayerStat.CURRENT_SOCIAL] >= System.Int32.Parse(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;

                case ChoiceRequirementType.ATTRIBUTE_MAX_MENTAL:
                    if (m_player.Stats[PlayerStat.MAX_MENTAL] >= System.Int32.Parse(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;

                case ChoiceRequirementType.ATTRIBUTE_MAX_PHYSICAL:
                    if (m_player.Stats[PlayerStat.MAX_PHYSICAL] >= System.Int32.Parse(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;

                case ChoiceRequirementType.ATTRIBUTE_MAX_SOCIAL:
                    if (m_player.Stats[PlayerStat.MAX_SOCIAL] >= System.Int32.Parse(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;

                case ChoiceRequirementType.ATTRIBUTE_MYTHOS_KNOWLEDGE:
                    if (m_player.Stats[PlayerStat.MYTHOS_KNOWLEDGE] >= System.Int32.Parse(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;

                case ChoiceRequirementType.HAVE_ITEM:
                    if (m_player.Inventory.ContainsKey(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;

                case ChoiceRequirementType.NOT_HAVE_ITEM:
                    if (!m_player.Inventory.ContainsKey(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;
                case ChoiceRequirementType.HAVE_FLAG:
                    if (m_player.Flags.ContainsKey(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;

                case ChoiceRequirementType.NOT_HAVE_FLAG:
                    if (!m_player.Flags.ContainsKey(message.Requirement.Requirement))
                    {
                        reply.RequirementMet = true;
                    }
                    break;
            }
            return reply;
        }

        private void OnEntryLoaded(GotoEntryCommand message)
        {
            m_player.CurrentEntry = message.ID;
            SerializePlayer();
        }

        private void OnCharacterSelected(CharacterSelectedMessage message)
        {
            m_player = new Player();
            m_player.Age = message.Age;
            m_player.Description = message.Description;
            m_player.Profession = message.Profession;
            m_player.Name = message.Name;
            m_player.Stats = message.Stats;
            m_player.Inventory = message.Inventory;
            m_player.CurrentAct = 0;
            m_player.CurrentEntry = 0;
            m_player.Flags = new Dictionary<string, bool>();
            SetPlayerDescriptors();
            BroadcastStats();
            SerializePlayer();
        }

        private GetInventoryReply OnGetInventoryQuery(GetInventoryQuery message)
        {
            return new GetInventoryReply(m_player.Inventory);
        }

        private void SerializePlayer()
        {
            m_file = File.Open(Application.persistentDataPath + "/savegame.dat", FileMode.OpenOrCreate);
            m_formatter.Serialize(m_file, m_player);
            m_file.Close();
        }

        private void OnLoadGame(LoadGameCommand command)
        {
            m_file = File.Open(Application.persistentDataPath + "/savegame.dat", FileMode.Open);
            m_player = (Player) m_formatter.Deserialize(m_file);
            m_file.Close();
            SetPlayerDescriptors();
            BroadcastStats();
            MessageSystem.BroadcastMessage(new LoadActCommand("Act" + m_player.CurrentAct.ToString(), m_player.CurrentEntry));
        }

        private void SetPlayerDescriptors()
        {
            m_age.text = m_player.Age.ToString();
            m_description.text = m_player.Description;
            m_profession.text = m_player.Profession;
            m_name.text = m_player.Name;
        }

        private void BroadcastStats()
        {
            foreach(KeyValuePair<PlayerStat, int> kvp in m_player.Stats)
            {
                MessageSystem.BroadcastMessage(new PlayerStatChangedMessage(kvp.Key, kvp.Value));
            }
        }

        private SaveGameAnswer OnSaveGameQuery(SaveGameQuery message)
        {
            FileInfo info = new FileInfo(Application.persistentDataPath + "/savegame.dat");
            if (info == null || info.Exists == false)
            {
                return new SaveGameAnswer(false);
            }
            return new SaveGameAnswer(true);
        }
    }
}
