using UnityEngine;
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
            MessageSystem.SubscribeQuery<SaveGameAnswer, SaveGameQuery>(gameObject, OnSaveGameQuery);
        }

        private void Start()
        {
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<CharacterSelectedMessage>(MessageSystem.ServiceContext, OnCharacterSelected);
            MessageSystem.UnsubscribeMessage<LoadGameCommand>(MessageSystem.ServiceContext, OnLoadGame);
            MessageSystem.UnsubscribeMessage<GotoEntryCommand>(MessageSystem.ServiceContext, OnEntryLoaded);
            MessageSystem.UnsubscribeQuery<SaveGameAnswer, SaveGameQuery>(gameObject, OnSaveGameQuery);
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
            m_player.CurrentAct = 0;
            m_player.CurrentEntry = 0;
            SetPlayerDescriptors();
            BroadcastStats();
            SerializePlayer();
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
