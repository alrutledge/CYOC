using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

namespace Assets.Scripts.CYOC.UI
{
    public class AnimationManager : MonoBehaviour
    {
        private Animator m_animator;
        private GameObject m_actAnimation;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<PlayActAnimationCommand>(MessageSystem.ServiceContext, OnPlayActAnimationCommand);
            m_animator = gameObject.GetComponent<Animator>();
            m_actAnimation = GameObject.Find("ActAnimations");
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<PlayActAnimationCommand>(MessageSystem.ServiceContext, OnPlayActAnimationCommand);
        }

        private void Start()
        {
            m_actAnimation.SetActive(false);
        }

        private void OnPlayActAnimationCommand(PlayActAnimationCommand message)
        {
            m_actAnimation.SetActive(true);
            m_animator.SetTrigger(message.Name);
        }

        public void AnimationEnded()
        {
            m_actAnimation.SetActive(false);
            MessageSystem.BroadcastMessage(new ActAnimationCompletedMessage());
        }
    }
}
