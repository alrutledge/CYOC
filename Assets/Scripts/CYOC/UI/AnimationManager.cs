using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

namespace Assets.Scripts.CYOC.UI
{
    public class AnimationManager : MonoBehaviour
    {
        private Animator m_animator;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<PlayActAnimationCommand>(MessageSystem.ServiceContext, OnPlayActAnimationCommand);
            m_animator = gameObject.GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<PlayActAnimationCommand>(MessageSystem.ServiceContext, OnPlayActAnimationCommand);
        }

        private void Start()
        {
        }

        private void OnPlayActAnimationCommand(PlayActAnimationCommand message)
        {
            m_animator.SetTrigger(message.Name);
        }

        public void AnimationEnded()
        {
            MessageSystem.BroadcastMessage(new ActAnimationCompletedMessage());
        }
    }
}
