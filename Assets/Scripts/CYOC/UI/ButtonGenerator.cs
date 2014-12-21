using UnityEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ChoiceEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
    public class ButtonGenerator : MonoBehaviour
    {
        private GameObject[] m_buttons = new GameObject[4];

        private void Awake()
        {
            for (int i = 1; i <= 4; i++)
            {
                GameObject buttonObject = GameObject.Find("Choice" + i.ToString());
                m_buttons[i-1] = buttonObject;
            }
            MessageSystem.SubscribeMessage<EntryLoadedMessage>(MessageSystem.ServiceContext, OnEntryLoaded);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<EntryLoadedMessage>(MessageSystem.ServiceContext, OnEntryLoaded);
        }

        private void OnEntryLoaded(EntryLoadedMessage message)
        {
            int count = 0;
            foreach(Choice choice in message.LoadedEntry.Choices)
            {
                count++;
                bool meetsAllRequirements = true;
                foreach (ChoiceRequirement requirement in choice.Requirements)
                {
                    RequirementReply reply = MessageSystem.BroadcastQuery<RequirementReply, RequirementQuery>(new RequirementQuery(requirement));
                    if (!reply.RequirementMet)
                    {
                        meetsAllRequirements = false;
                        break;
                    }
                }
                m_buttons[count-1].SetActive(true);
                m_buttons[count - 1].GetComponent<Button>().interactable = meetsAllRequirements;
                Text text = m_buttons[count-1].GetComponentInChildren<Text>();
                text.text = choice.Text;
                ChoiceButton choiceButtonComponent = m_buttons[count - 1].GetComponent<ChoiceButton>();
                choiceButtonComponent.CurrentChoice = choice;
            }
            
            for(int i = count;i<4; i++)
            {
                count++;
                m_buttons[count-1].SetActive(false);
            }
        }
    }
}
