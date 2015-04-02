using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.CYOC.UI.Messages;
using UnityEngine.UI;

public class MainFlow : MonoBehaviour 
{
    private GameObject m_mainMenu;
    private GameObject m_gamePlay;
    private GameObject m_splashScreen;
    private GameObject m_exitGame;
    private GameObject m_endPanel;
    private GameObject m_mainPanel;
    private GameObject m_credits;

	private void Awake () 
    {
        m_mainMenu = GameObject.Find("MainMenu");
        m_gamePlay = GameObject.Find("GamePlay");
        m_endPanel = GameObject.Find("EndPanel");
        m_mainPanel = GameObject.Find("MainGamePanel");
        m_splashScreen = GameObject.Find("SplashScreen");
        m_exitGame = GameObject.Find("ConfirmGameExitPanel");
        m_credits = GameObject.Find("CreditContainer");
        MessageSystem.SubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
        MessageSystem.SubscribeMessage<ExitToMainMenuCommand>(MessageSystem.ServiceContext, OnExitToMainMenuCommand);
        MessageSystem.SubscribeMessage<PrepareActAnimationCommand>(MessageSystem.ServiceContext, OnPrepareActAnimationCommand);
        MessageSystem.SubscribeMessage<ActAnimationCompletedMessage>(MessageSystem.ServiceContext, OnActAnimationCompletedMessage);
        MessageSystem.SubscribeMessage<ShowActEndCommand>(MessageSystem.ServiceContext, OnShowActEndCommand);
	}

    private void OnDestroy()
    {
        MessageSystem.UnsubscribeMessage<ActLoadedMessage>(MessageSystem.ServiceContext, OnActLoaded);
        MessageSystem.UnsubscribeMessage<ExitToMainMenuCommand>(MessageSystem.ServiceContext, OnExitToMainMenuCommand);
        MessageSystem.UnsubscribeMessage<PrepareActAnimationCommand>(MessageSystem.ServiceContext, OnPrepareActAnimationCommand);
        MessageSystem.UnsubscribeMessage<ActAnimationCompletedMessage>(MessageSystem.ServiceContext, OnActAnimationCompletedMessage);
        MessageSystem.UnsubscribeMessage<ShowActEndCommand>(MessageSystem.ServiceContext, OnShowActEndCommand);
    }

    private void OnShowActEndCommand(ShowActEndCommand message)
    {
        m_endPanel.SetActive(true);
        m_mainPanel.SetActive(false);
    }
        
    private void Start()
    {
        m_mainMenu.SetActive(false);
        m_gamePlay.SetActive(false);
        m_exitGame.SetActive(false);
        m_endPanel.SetActive(false);
        StartCoroutine(RemoveSplashScreen(1.0f));
    }
	
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_gamePlay.activeInHierarchy || m_credits.activeInHierarchy)
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

    IEnumerator RemoveSplashScreen(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_mainMenu.SetActive(true);
        m_splashScreen.SetActive(false);
        MessageSystem.BroadcastMessage(new PlayMusicCommand("Awkward"));
    }

    public void OnExitClicked()
    {
        if (!m_exitGame.activeInHierarchy)
        {
            m_exitGame.SetActive(true);
        }
    }

    private void OnActAnimationCompletedMessage(ActAnimationCompletedMessage message)
    {
        m_gamePlay.SetActive(true);
        MessageSystem.BroadcastMessage(new SupressEntriesCommand(false));
    }

    private void OnPrepareActAnimationCommand(PrepareActAnimationCommand message)
    {
        m_gamePlay.SetActive(false);
        MessageSystem.BroadcastMessage(new PlayActAnimationCommand(message.Name));
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
        m_endPanel.SetActive(false);
        m_mainPanel.SetActive(true);
        MessageSystem.BroadcastMessage(new PlayMusicCommand("Awkward"));
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
