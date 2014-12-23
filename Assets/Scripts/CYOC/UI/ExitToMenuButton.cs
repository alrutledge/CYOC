using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ICG.Messaging;
using UnityEngine;

namespace Assets.Scripts.CYOC.UI
{
    public class ExitToMenuButton : MonoBehaviour
    {
        private GameObject m_exitGamePlay;
        private Animator m_animator;

        public void Start()
        {
            m_animator = GameObject.Find("MainGamePlay").GetComponent<Animator>();
            m_exitGamePlay = GameObject.Find("ConfirmExitPanel");
            m_exitGamePlay.SetActive(false);
        }
        public void OnExitClicked()
        {
            m_exitGamePlay.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && m_animator.GetBool("InventoryIsOffscreen") && m_animator.GetBool("CharacterIsOffscreen"))
            {
                if (!m_exitGamePlay.activeInHierarchy)
                {
                    m_exitGamePlay.SetActive(true);
                }
            }
        }

        public void OnNoClicked()
        {
            m_exitGamePlay.SetActive(false);
        }

        public void OnYesClicked()
        {
            m_exitGamePlay.SetActive(false);
            MessageSystem.BroadcastMessage(new ExitToMainMenuCommand());
        }
    }
}
