using UnityEngine;
using System.Collections;
using Assets.Scripts.ChoiceEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

namespace Assets.Scripts.CYOC.UI
{
    public class ChoiceButton : MonoBehaviour
    {
        public Choice CurrentChoice { get; set; }

        public void OnButtonClick()
        {
            foreach(ChoiceAction action in CurrentChoice.Actions)
            {
                action.PerformAction();
            }
        }
    }
}
