using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine;

namespace Assets.Scripts.CYOC.UI
{
    public class FontChangeListener : MonoBehaviour
    {
        private Text TextToChange;

        private void Awake()
        {
            TextToChange = gameObject.GetComponent<Text>();
            MessageSystem.SubscribeMessage<ChangeFontCommand>(MessageSystem.ServiceContext, OnChangeFontCommand);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<ChangeFontCommand>(MessageSystem.ServiceContext, OnChangeFontCommand);
        }

        private void OnChangeFontCommand(ChangeFontCommand message)
        {
            Font toUse = Resources.Load(message.Name, typeof(Font)) as Font;
            TextToChange.font = toUse;
        }
    }
}