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
        private GameObject m_actSelect;
        private GameObject m_characterSelect;
        private GameObject m_characterInformation;
        private GameObject m_introDescription;

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
            m_introDescription = GameObject.Find("IntroInformation");
            //m_image = GameObject.Find("CharacterImage").GetComponent<Image>();
            m_loadGameButon = GameObject.Find("LoadGameButton").GetComponent<Button>();
            m_characterInformation.SetActive(false);
            m_actSelect.SetActive(false);
            m_characterSelect.SetActive(false);
            m_introDescription.SetActive(false);
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
            m_introDescription.SetActive(true);
            m_characterInformation.SetActive(false);
        }

        public void ProfessorPressed()
        {
            m_introDescription.SetActive(false);
            m_characterName.text = "Joseph Allred";
            m_characterProfession.text = "Professor";
            m_characterDescription.text = "  Professor Joseph Allred teaches history at Miskatonic University in Arkham, Massachusetts. He has been there a few months, having moved back to Arkham from being away for a long time.\n\n  A native of the town, Joseph left to get his degree, then travel abroad, and has only recently returned to settle down in his home town.\n\n  Joseph is highly intelligent, but prefers to spend his time looking back at what has come before him and not looking ahead at all the wonders the world has to offer.";
            m_characterAge.text = "48";
            m_characterInformation.SetActive(true);
            m_playersStats = new Dictionary<PlayerStat, int>();
            m_playersStats[PlayerStat.MAX_MENTAL] = 50;
            m_playersStats[PlayerStat.MAX_SOCIAL] = 40;
            m_playersStats[PlayerStat.MAX_PHYSICAL] = 30;
            m_characterPrimary.text = "Mental";
            m_characterSecondary.text = "Social";
            m_characterTertiary.text = "Physical";
            m_startingInventory = new Dictionary<string, Item>();
            Item startingItem = new Item("Local History Book", "This is a small tome on the history of the local area. Joseph picked this book up at the local bookstore, and has been carrying it around ever since. Carrying a book is comforting to Joseph, and he never knows when such knowledge about the area might prove useful.", "book64", "book256");
            m_startingInventory.Add(startingItem.Name, startingItem);
        }

        public void StudentPressed()
        {
            m_introDescription.SetActive(false);
            m_characterName.text = "Anne Delent";
            m_characterProfession.text = "Student";
            m_characterDescription.text = "  Anne Delent is a student at Miskatonic University in Arkham Massachesetts. She is not native to Arkham, having moved her just to go to school. Her family has ties to the area though. Her mother was from Arkham, before she moved to Boston.\n\n  Excited about her time at Miskatonic University, Anne is an avid student, and making good grades. She has to study hard to keep her grades up, but often partakes in campus social activities.\n\n  Anne is very fit, participating in Track and Field and Tennis activities in High School and now at Miskatonic University.";
            m_characterAge.text = "22";
            m_characterInformation.SetActive(true);
            m_playersStats = new Dictionary<PlayerStat, int>();
            m_playersStats[PlayerStat.MAX_MENTAL] = 30;
            m_playersStats[PlayerStat.MAX_SOCIAL] = 40;
            m_playersStats[PlayerStat.MAX_PHYSICAL] = 50;
            m_characterPrimary.text = "Physical";
            m_characterSecondary.text = "Social";
            m_characterTertiary.text = "Mental";
            m_startingInventory = new Dictionary<string, Item>();
            Item startingItem = new Item("A tattered notebook", "This is Anne's favorite school notebook, great for taking notes. She is an incessant note taker, always scribbling away in one of her many notebooks. Eventually the notebook will be full, but then she will just find another as note taking is just a part of who she is.", "notebook64", "notebook256");
            m_startingInventory.Add(startingItem.Name, startingItem);
        }

        public void DilettantePressed()
        {
            m_introDescription.SetActive(false);
            m_characterName.text = "Stephen Mallory III";
            m_characterProfession.text = "Unemployed";
            m_characterDescription.text = "  Stephen Mallory III was born into the influential and wealthy Mallory family. The Mallorys are old Arkham money, having property in Arkham, Massachusetts and Boston.\n\n  Like all of the Mallorys before him, Stephen is rich, but must keep up appearences and stick to protocols if he is to receive his monthly stipends.\n\n  Stephen is very much a socialite, and is often the life of the party at high end social gatherings in Arkham and Boston alike.";
            m_characterAge.text = "23";
            m_characterInformation.SetActive(true);
            m_playersStats = new Dictionary<PlayerStat, int>();
            m_playersStats[PlayerStat.MAX_MENTAL] = 30;
            m_playersStats[PlayerStat.MAX_SOCIAL] = 50;
            m_playersStats[PlayerStat.MAX_PHYSICAL] = 40;
            m_characterPrimary.text = "Social";
            m_characterSecondary.text = "Physical";
            m_characterTertiary.text = "Mental";
            m_startingInventory = new Dictionary<string, Item>();
            Item startingItem = new Item("A silver flask", "Stephen's father gave him this flask before he passed a few years ago. Stephen keeps it close to him always, ensuring that he has a reminder of his father with him, as well as a little liquid courage in case he needs a bit of a kick.", "smallItemSample", "smallItemSample");
            m_startingInventory.Add(startingItem.Name, startingItem);
        }

        public void ReporterPressed()
        {
            m_introDescription.SetActive(false);
            m_characterName.text = "Megan Ash";
            m_characterProfession.text = "Photographer";
            m_characterDescription.text = "  Megan Ash is a photographer, recently moved to Arkham Massachusetts to try to find out more about her family. Her father was from Arkham, but until just before his death he never talked about it. \n\n  Megan never goes anywhere without her camera. She loves her job, and freelances for both portrait photography and news photography when she can.\n\n  Back in Pittsburgh, where she grew up, Megan's work was becoming quite well known. If it hadn't been for her father's death, she would likely still be there, growing her clientele.";
            m_characterAge.text = "31";
            m_characterInformation.SetActive(true);
            m_playersStats = new Dictionary<PlayerStat, int>();
            m_playersStats[PlayerStat.MAX_MENTAL] = 40;
            m_playersStats[PlayerStat.MAX_SOCIAL] = 50;
            m_playersStats[PlayerStat.MAX_PHYSICAL] = 30;
            m_characterPrimary.text = "Social";
            m_characterSecondary.text = "Mental";
            m_characterTertiary.text = "Physical";
            m_startingInventory = new Dictionary<string, Item>();
            Item startingItem = new Item("Camera", "Megan's camera is a sturdy work horse of a camera. She takes it wherever she goes, and dotes on the thing like a pet or child. Afterall, her camera is her livelihood, and acquiring a replacement would not be cheap.", "smallItemSample", "smallItemSample");
            m_startingInventory.Add(startingItem.Name, startingItem);

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
            message.Inventory = m_startingInventory;

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
                else
                {
                    //m_exitGame.SetActive(false);
                }
            }
        }
    }
}
