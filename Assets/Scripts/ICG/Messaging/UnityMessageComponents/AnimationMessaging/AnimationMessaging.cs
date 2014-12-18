using UnityEngine;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.AnimationMessaging
{
    class AnimationMessaging : MonoBehaviour
    {
        private Animator m_animator;

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            MessageSystem.SubscribeMessage<SetFloatCommand>(gameObject, OnSetFloatCommand);
            MessageSystem.SubscribeMessage<SetTriggerCommand>(gameObject, OnSetTriggerCommand);
            MessageSystem.SubscribeMessage<ResetTriggerCommand>(gameObject, OnResetTriggerCommand);
            MessageSystem.SubscribeQuery<CurrentStateReply, CurrentStateQuery>(gameObject, OnCurrentStateQuery);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<SetFloatCommand>(gameObject, OnSetFloatCommand);
            MessageSystem.UnsubscribeMessage<SetTriggerCommand>(gameObject, OnSetTriggerCommand);
            MessageSystem.UnsubscribeMessage<ResetTriggerCommand>(gameObject, OnResetTriggerCommand);
            MessageSystem.UnsubscribeQuery<CurrentStateReply, CurrentStateQuery>(gameObject, OnCurrentStateQuery);
        }

        private CurrentStateReply OnCurrentStateQuery(CurrentStateQuery message)
        {
            return new CurrentStateReply(m_animator.GetCurrentAnimatorStateInfo(0));
        }

        private void OnResetTriggerCommand(ResetTriggerCommand command)
        {
            m_animator.ResetTrigger(command.Name);
        }

        private void OnSetFloatCommand(SetFloatCommand command)
        {
            m_animator.SetFloat(command.Name, command.Value);
        }

        private void OnSetTriggerCommand(SetTriggerCommand command)
        {
            m_animator.SetTrigger(command.Name);
        }
    }
}
