﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.CYOC.UI.Messages;

public class MainFlow : MonoBehaviour 
{
    private GameObject m_mainMenu;
    private GameObject m_gamePlay;
    private GameObject m_splashScreen;
    private GameObject m_exitGame;
    private GameObject m_characterSelect;

	private void Awake () 
    {
        m_mainMenu = GameObject.Find("MainMenu");
        m_gamePlay = GameObject.Find("GamePlay");
        m_splashScreen = GameObject.Find("SplashScreen");
        m_exitGame = GameObject.Find("ConfirmGameExitPanel");
        m_characterSelect = GameObject.Find("CharacterSelect");
        MessageSystem.SubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
        MessageSystem.SubscribeMessage<ExitToMainMenuCommand>(MessageSystem.ServiceContext, OnExitToMainMenuCommand);
	}

    private void OnDestroy()
    {
        MessageSystem.UnsubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
        MessageSystem.UnsubscribeMessage<ExitToMainMenuCommand>(MessageSystem.ServiceContext, OnExitToMainMenuCommand);
    }


    private void Start()
    {
        m_mainMenu.SetActive(false);
        m_gamePlay.SetActive(false);
        m_exitGame.SetActive(false);
        StartCoroutine(RemoveSplashScreen(1.0f));
    }
	
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(m_characterSelect.activeInHierarchy)
            {

            }
            else if (m_gamePlay.activeInHierarchy)
            {

            }
            else if (!m_exitGame.activeInHierarchy)
            {
                OnExitClicked();
            }
            else
            {
                m_exitGame.SetActive(false);
            }
        }
    }

    public void OnExitClicked()
    {
        if (!m_exitGame.activeInHierarchy)
        {
            m_exitGame.SetActive(true);
        }
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

    private void OnExitToMainMenuCommand(ExitToMainMenuCommand message)
    {
        m_mainMenu.SetActive(true);
        m_gamePlay.SetActive(false);
    }

    public void OnNoClicked()
    {
        m_exitGame.SetActive(false);
    }

    public void OnYesClicked()
    {
        Application.Quit();
    }
}
