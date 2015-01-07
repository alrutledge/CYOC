using UnityEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ChoiceEngine;
using UnityEngine.UI;
using Assets.Scripts.ChoiceEngine.ChoiceActions;

namespace Assets.Scripts.CYOC.UI
{
    public class ButtonGenerator : MonoBehaviour
    {
        private GameObject[] m_buttons = new GameObject[4];
        private int m_currentEntry = 0;

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
            m_currentEntry = message.LoadedEntry.ID;
            if (m_currentEntry >= 0)
            {
                GetPlayerStatusReply statusReply = MessageSystem.BroadcastQuery<GetPlayerStatusReply, GetPlayerStatusQuery>(new GetPlayerStatusQuery());
                if (statusReply == null) return;

                if (statusReply.Status == PlayerStatus.GOOD)
                {
                    foreach (Choice choice in message.LoadedEntry.Choices)
                    {
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
                        if (meetsAllRequirements)
                        {
                            count++;
                            m_buttons[count - 1].SetActive(true);
                            Text text = m_buttons[count - 1].GetComponentInChildren<Text>();
                            text.text = choice.Text;
                            ChoiceButton choiceButtonComponent = m_buttons[count - 1].GetComponent<ChoiceButton>();
                            choiceButtonComponent.CurrentChoice = choice;
                        }
                        //m_buttons[count - 1].GetComponent<Button>().interactable = meetsAllRequirements;
                    }
                }
                else if (statusReply.Status == PlayerStatus.INSANE)
                {
                    Choice choice = new Choice();
                    choice.Actions.Add(new GotoAction(-1));

                    count++;
                    m_buttons[count - 1].SetActive(true);
                    Text text = m_buttons[count - 1].GetComponentInChildren<Text>();
                    text.text = "Things go black...";
                    ChoiceButton choiceButtonComponent = m_buttons[count - 1].GetComponent<ChoiceButton>();
                    choiceButtonComponent.CurrentChoice = choice;

                }
                else if (statusReply.Status == PlayerStatus.DEAD)
                {

                    Choice choice = new Choice();
                    choice.Actions.Add(new GotoAction(-2));

                    count++;
                    m_buttons[count - 1].SetActive(true);
                    Text text = m_buttons[count - 1].GetComponentInChildren<Text>();
                    text.text = "Things go black...";
                    ChoiceButton choiceButtonComponent = m_buttons[count - 1].GetComponent<ChoiceButton>();
                    choiceButtonComponent.CurrentChoice = choice;
                }
            }
            for (int i = count; i < 4; i++)
            {
                count++;
                m_buttons[count - 1].SetActive(false);
            }
        }
    }
}
