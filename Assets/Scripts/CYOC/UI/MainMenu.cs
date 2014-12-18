using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
    public class MainMenu : MonoBehaviour
    {
        private GameObject m_main;
        private GameObject m_actSelect;
        private GameObject m_characterSelect;
        private GameObject m_characterInformation;

        private Text m_characterName;
        private Text m_characterProfession;
        private Text m_characterDescription;
        private Text m_characterAge;
        private Image m_image;

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
            m_image = GameObject.Find("CharacterImage").GetComponent<Image>();
            m_characterInformation.SetActive(false);
            m_actSelect.SetActive(false);
            m_characterSelect.SetActive(false);
        }

        public void LoadPressed()
        {
            // load the save game
            MessageSystem.BroadcastMessage(new LoadActCommand("Act0"));
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
        }

        public void StudentPressed()
        {
            m_characterName.text = "Anne Delent";
            m_characterProfession.text = "Student";
            m_characterDescription.text = "The Student with the mostest...";
            m_characterAge.text = "22";
            m_characterInformation.SetActive(true);
        }

        public void DilettantePressed()
        {
            m_characterName.text = "Stephen Mallory III";
            m_characterProfession.text = "Unemployed";
            m_characterDescription.text = "The bachelor with the mostest...";
            m_characterAge.text = "23";
            m_characterInformation.SetActive(true);
        }

        public void ReporterPressed()
        {
            m_characterName.text = "Megan Ash";
            m_characterProfession.text = "Reporter";
            m_characterDescription.text = "The reporter with the mostest...";
            m_characterAge.text = "31";
            m_characterInformation.SetActive(true);
        }
            
        public void SelectCharacterPressed()
        {

            m_characterSelect.SetActive(false);
            MessageSystem.BroadcastMessage(new LoadActCommand("Act0"));
        }
    }
}
