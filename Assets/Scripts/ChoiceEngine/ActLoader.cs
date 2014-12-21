using UnityEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

namespace Assets.Scripts.ChoiceEngine
{
    public class ActLoader : MonoBehaviour
    {
        public Act LoadedAct { get; set; }
        private Entry m_currentEntry;
        private Choice m_currentChoice;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<LoadActCommand>(MessageSystem.ServiceContext, OnLoadActCommand);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<LoadActCommand>(MessageSystem.ServiceContext, OnLoadActCommand);
        }

        void OnLoadActCommand(LoadActCommand command)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("Assets\\" + command.ActToLoad + ".act");

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("ActName:"))
                {
                    LoadedAct = new Act(line.Substring(line.IndexOf(':') + 1));
                }
                else if (line.StartsWith("EntryID:"))
                {
                    m_currentEntry = new Entry(System.Int32.Parse(line.Substring(line.IndexOf(':') + 1)));
                    LoadedAct.Entries[m_currentEntry.ID] = m_currentEntry;
                }

                else if (line.StartsWith("EntryText:"))
                {
                    m_currentEntry.Text = line.Substring(line.IndexOf(':') + 1);
                }

                else if (line.StartsWith("Choice:"))
                {
                    m_currentChoice = new Choice();
                    m_currentChoice.Text = line.Substring(line.IndexOf(':') + 1);
                    m_currentEntry.Choices.Add(m_currentChoice);
                }
                else if (line.StartsWith("Action:"))
                {
                    ChoiceAction action = new ChoiceAction();
                    string[] choiceParts = line.Split(':');
                    action.Type = (ChoiceActionType)System.Enum.Parse(typeof(ChoiceActionType), choiceParts[1]);
                    action.ID = System.Int32.Parse(choiceParts[2]);
                    m_currentChoice.Actions.Add(action);
                }
                else if (line.StartsWith("Requirement:"))
                {
                    ChoiceRequirement requirement = new ChoiceRequirement();
                    string[] requirementParts = line.Split(':');
                    requirement.Type = (ChoiceRequirementType)System.Enum.Parse(typeof (ChoiceRequirementType), requirementParts[1]);
                    requirement.Requirement = System.Int32.Parse(requirementParts[2]);
                    m_currentChoice.Requirements.Add(requirement);
                }
            }

            file.Close();
            MessageSystem.BroadcastMessage(new ActLoadedMessage(LoadedAct.Entries[command.EntryToLoad], LoadedAct));

        }
    }
}
