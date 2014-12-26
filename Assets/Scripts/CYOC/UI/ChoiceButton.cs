using UnityEngine;
using Assets.Scripts.ChoiceEngine;
using Assets.Scripts.ChoiceEngine.ChoiceActions;

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
