using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.ChoiceEngine;
using Assets.Scripts.CYOC.UI.Messages;

namespace Assets.Scripts.CYOC.UI
{
    public class MainMenu : MonoBehaviour
    {
        private GameObject m_main;
        private GameObject m_credits;
        private Animator m_creditAnimations;
        private Button m_loadGameButon;

        private Dictionary<PlayerStat, int> m_playersStats;
        private Dictionary<string, Item> m_startingInventory;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<ExitToMainMenuCommand>(MessageSystem.ServiceContext, OnExitToMainMenuCommand);
        }
        
        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<ExitToMainMenuCommand>(MessageSystem.ServiceContext, OnExitToMainMenuCommand);
        }

        void Start()
        {
            m_main = GameObject.Find("Main");
            m_credits = GameObject.Find("CreditContainer");
            m_credits.SetActive(false);
            m_creditAnimations = GameObject.Find("MainMenu").GetComponent<Animator>();
            m_loadGameButon = GameObject.Find("LoadGameButton").GetComponent<Button>();

            SaveGameAnswer answer = MessageSystem.BroadcastQuery<SaveGameAnswer, SaveGameQuery>(new SaveGameQuery());
            if (!answer.Exists)
            {
                m_loadGameButon.interactable = false;
            }
            else
            {
                m_loadGameButon.interactable = true;
            }
        }

        public void LoadPressed()
        {
            MessageSystem.BroadcastMessage(new LoadGameCommand());
        }

        public void NewPressed(int actNumber)
        {

            m_playersStats = new Dictionary<PlayerStat, int>();
            m_playersStats[PlayerStat.MAX_MENTAL] = 100;
            m_playersStats[PlayerStat.MAX_PHYSICAL] = 100;
            m_startingInventory = new Dictionary<string, Item>();


            m_loadGameButon.interactable = true;
            CharacterSelectedMessage message = new CharacterSelectedMessage();
            
            m_playersStats[PlayerStat.CURRENT_MENTAL] = m_playersStats[PlayerStat.MAX_MENTAL];
            m_playersStats[PlayerStat.CURRENT_PHYSICAL] = m_playersStats[PlayerStat.MAX_PHYSICAL];
            m_playersStats[PlayerStat.MYTHOS_KNOWLEDGE] = 0;

            message.Stats = m_playersStats;
            message.Inventory = m_startingInventory;

            MessageSystem.BroadcastMessage(message);
            m_main.SetActive(false);
            MessageSystem.BroadcastMessage(new LoadActCommand("Act" + actNumber.ToString()));
        }


  
            
        public void SelectCharacterPressed()
        {
            m_loadGameButon.interactable = true;
            CharacterSelectedMessage message = new CharacterSelectedMessage();


            m_playersStats[PlayerStat.CURRENT_MENTAL] = m_playersStats[PlayerStat.MAX_MENTAL];
            m_playersStats[PlayerStat.CURRENT_PHYSICAL] = m_playersStats[PlayerStat.MAX_PHYSICAL];
            m_playersStats[PlayerStat.MYTHOS_KNOWLEDGE] = 0;

            message.Stats = m_playersStats;
            message.Inventory = m_startingInventory;

            MessageSystem.BroadcastMessage(message);
            MessageSystem.BroadcastMessage(new LoadActCommand("Act1"));
        }

        public void OnExitClicked()
        {
            if (!m_main.activeInHierarchy)
            {
                m_main.SetActive(true);
            }
        }

        private void OnExitToMainMenuCommand(ExitToMainMenuCommand message)
        {
            m_main.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!m_main.activeInHierarchy)
                {
                    OnExitClicked();
                }
            }
        }

        public void CreditsPressed()
        {
            m_main.SetActive(false);
            m_credits.SetActive(true);
            m_creditAnimations.SetTrigger("RollCredits");
        }

        public void CreditsFinished()
        {
            m_main.SetActive(true);
            m_credits.SetActive(false);
            m_creditAnimations.ResetTrigger("RollCredits");
        }
    }
}
