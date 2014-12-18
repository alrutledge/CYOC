using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging.UI.Messages;
using UnityEngine.UI;

namespace Assets.Scripts.ICG.Messaging.UnityMessageComponents.UI
{
    public class InputFieldMessaging : MonoBehaviour
    {
        private InputField mInputField;

        private void Awake()
        {
            MessageSystem.SubscribeQuery<InputReply, InputQuery>(gameObject, OnInputQuery);
            mInputField = GetComponent<InputField>();
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeQuery<InputReply, InputQuery>(gameObject, OnInputQuery);
        }

        private InputReply OnInputQuery(InputQuery query)
        {
            return new InputReply(mInputField.text);
        }

    }
}
