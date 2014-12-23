using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.ChoiceEngine;

namespace Assets.Scripts.CYOC.UI
{
    public class MainMenu : MonoBehaviour
    {
        private GameObject m_main;
        private GameObject m_actSelect;
        private GameObject m_characterSelect;
        private GameObject m_characterInformation;
        private Button m_loadGameButon;

        private Text m_characterName;
        private Text m_characterProfession;
        private Text m_characterDescription;
        private Text m_characterAge;
        private Text m_characterPrimary;
        private Text m_characterSecondary;
        private Text m_characterTertiary;
        private Image m_image;

        private Dictionary<PlayerStat, int> m_playersStats;

        void Start()
        {
            m_main = GameObject.Find("Main");
            m_actSelect = GameObject.Find("ActSelect");
            m_characterSelect = GameObject.Find("CharacterSelect");
            m_characterInformation = GameObject.Find("CharacterInformation");
            m_characterName = GameObject.Find("CharacterName").GetComponent<Text>();
            m_characterProfession = GameObject.Find("CharacterProfession").GetComponent<Text>();
            m_characterDescription = GameObject.Find("CharacterDescription").GetComponent<Text>();
            m_characterAge = GameObject.Find("CharacterAge").GetComponent<Text>();
            m_characterPrimary = GameObject.Find("CharacterPrimary").GetComponent<Text>();
            m_characterSecondary = GameObject.Find("CharacterSecondary").GetComponent<Text>();
            m_characterTertiary = GameObject.Find("CharacterTertiary").GetComponent<Text>();
            m_image = GameObject.Find("CharacterImage").GetComponent<Image>();
            m_loadGameButon = GameObject.Find("LoadGameButton").GetComponent<Button>();
            m_characterInformation.SetActive(false);
            m_actSelect.SetActive(false);
            m_characterSelect.SetActive(false);
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

        public void NewPressed()
        {
            m_characterSelect.SetActive(true);
            m_main.SetActive(false);
        }

        public void ProfessorPressed()
        {
            m_characterName.text = "Joseph Allred";
            m_characterProfession.text = "Professor";
            m_characterDescription.text = "The Professor with the mostest...";
            m_characterAge.text = "48";
            m_characterInformation.SetActive(true);
            m_playersStats = new Dictionary<PlayerStat, int>();
            m_playersStats[PlayerStat.MAX_MENTAL] = 50;
            m_playersStats[PlayerStat.MAX_SOCIAL] = 40;
            m_playersStats[PlayerStat.MAX_PHYSICAL] = 30;
            m_characterPrimary.text = "Mental";
            m_characterSecondary.text = "Social";
            m_characterTertiary.text = "Physical";
        }

        public void StudentPressed()
        {
            m_characterName.text = "Anne Delent";
            m_characterProfession.text = "Student";
            m_characterDescription.text = "The Student with the mostest...";
            m_characterAge.text = "22";
            m_characterInformation.SetActive(true);
            m_playersStats = new Dictionary<PlayerStat, int>();
            m_playersStats[PlayerStat.MAX_MENTAL] = 30;
            m_playersStats[PlayerStat.MAX_SOCIAL] = 40;
            m_playersStats[PlayerStat.MAX_PHYSICAL] = 50;
            m_characterPrimary.text = "Physical";
            m_characterSecondary.text = "Social";
            m_characterTertiary.text = "Mental";
        }

        public void DilettantePressed()
        {
            m_characterName.text = "Stephen Mallory III";
            m_characterProfession.text = "Unemployed";
            m_characterDescription.text = "The bachelor with the mostest...";
            m_characterAge.text = "23";
            m_characterInformation.SetActive(true);
            m_playersStats = new Dictionary<PlayerStat, int>();
            m_playersStats[PlayerStat.MAX_MENTAL] = 30;
            m_playersStats[PlayerStat.MAX_SOCIAL] = 50;
            m_playersStats[PlayerStat.MAX_PHYSICAL] = 40;
            m_characterPrimary.text = "Social";
            m_characterSecondary.text = "Physical";
            m_characterTertiary.text = "Mental";
        }

        public void ReporterPressed()
        {
            m_characterName.text = "Megan Ash";
            m_characterProfession.text = "Reporter";
            m_characterDescription.text = "The reporter with the mostest...";
            m_characterAge.text = "31";
            m_characterInformation.SetActive(true);
            m_playersStats = new Dictionary<PlayerStat, int>();
            m_playersStats[PlayerStat.MAX_MENTAL] = 40;
            m_playersStats[PlayerStat.MAX_SOCIAL] = 50;
            m_playersStats[PlayerStat.MAX_PHYSICAL] = 30;
            m_characterPrimary.text = "Social";
            m_characterSecondary.text = "Mental";
            m_characterTertiary.text = "Physical";

        }
            
        public void SelectCharacterPressed()
        {
            m_loadGameButon.interactable = true;
            CharacterSelectedMessage message = new CharacterSelectedMessage();

            message.Name = m_characterName.text;
            message.Profession = m_characterProfession.text;
            message.Description = m_characterDescription.text;
            message.Age = System.Int32.Parse(m_characterAge.text);

            m_playersStats[PlayerStat.CURRENT_MENTAL] = m_playersStats[PlayerStat.MAX_MENTAL];
            m_playersStats[PlayerStat.CURRENT_PHYSICAL] = m_playersStats[PlayerStat.MAX_PHYSICAL];
            m_playersStats[PlayerStat.CURRENT_SOCIAL] = m_playersStats[PlayerStat.MAX_SOCIAL];
            m_playersStats[PlayerStat.MYTHOS_KNOWLEDGE] = 0;

            message.Stats = m_playersStats;

            MessageSystem.BroadcastMessage(message);
            m_characterSelect.SetActive(false);
            MessageSystem.BroadcastMessage(new LoadActCommand("Act0"));
        }

        public void OnExitClicked()
        {
            if (!m_main.activeInHierarchy)
            {
                m_characterSelect.SetActive(false);
                m_main.SetActive(true);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!m_main.activeInHierarchy)
                {
                    OnExitClicked();
                }
                else
                {
                    //m_exitGame.SetActive(false);
                }
            }
        }
    }
}
