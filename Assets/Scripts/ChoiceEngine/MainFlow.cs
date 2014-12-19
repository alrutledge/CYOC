using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

public class MainFlow : MonoBehaviour 
{
    private GameObject m_mainMenu;
    private GameObject m_gamePlay;
    private GameObject m_splashScreen;

	private void Awake () 
    {
        m_mainMenu = GameObject.Find("MainMenu");
        m_gamePlay = GameObject.Find("GamePlay");
        m_splashScreen = GameObject.Find("SplashScreen");
        MessageSystem.SubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
	}

    private void OnDestroy()
    {
        MessageSystem.UnsubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
    }

    private void Start()
    {
        m_mainMenu.SetActive(false);
        m_gamePlay.SetActive(false);
        StartCoroutine(RemoveSplashScreen(1.0f));
    }
	
    IEnumerator RemoveSplashScreen(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_mainMenu.SetActive(true);
        m_splashScreen.SetActive(false);
    }

    private void OnActLoaded(ActLoadedMessage message)
    {
        m_mainMenu.SetActive(false);
        m_gamePlay.SetActive(true);
    }
}
