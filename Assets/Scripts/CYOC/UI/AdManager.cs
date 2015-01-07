using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ICG.Messaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
    [System.Serializable]
    public class AdStatus
    {
        public bool AdsOn { get; set; }
    }

    public class AdManager : MonoBehaviour
    {
        private bool m_adsOn = true;
        private FileStream m_file;
        private BinaryFormatter m_formatter = new BinaryFormatter();

        private void Start()
        {
            Advertisement.Initialize("22508");

            FileInfo info = new FileInfo(Application.persistentDataPath + "/savegame01.dat");
            if (info != null && info.Exists)
            {
                m_file = File.Open(Application.persistentDataPath + "/savegame01.dat", FileMode.Open);
                AdStatus adStatus = (AdStatus)m_formatter.Deserialize(m_file);
                m_adsOn = adStatus.AdsOn;
                GameObject.Find("TurnOffAdsButton").GetComponent<Button>().interactable = false;
                m_file.Close();
            }
        }

        private void Awake()
        {
            MessageSystem.SubscribeMessage<DisplayAdCommand>(MessageSystem.ServiceContext, OnDisplayAdCommand);
            MessageSystem.SubscribeMessage<TurnOffAdsCommand>(MessageSystem.ServiceContext, OnTurnOffAdsCommand);

        }
        
        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<DisplayAdCommand>(MessageSystem.ServiceContext, OnDisplayAdCommand);
            MessageSystem.UnsubscribeMessage<TurnOffAdsCommand>(MessageSystem.ServiceContext, OnTurnOffAdsCommand);
        }

        private void OnDisplayAdCommand(DisplayAdCommand message)
        {
            if (Advertisement.isReady() && m_adsOn)
            {
                Advertisement.Show();
            }
        }

        private void OnTurnOffAdsCommand(TurnOffAdsCommand message)
        {
            m_adsOn = false;

            m_file = File.Open(Application.persistentDataPath + "/savegame01.dat", FileMode.OpenOrCreate);
            AdStatus adStatus = new AdStatus();
            adStatus.AdsOn = false;
            m_formatter.Serialize(m_file, adStatus);
            m_file.Close();
        }
    }
}
