using UnityEngine;
using System.Collections;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

namespace Assets.Scripts.CYOC.UI
{
    public class ActButton : MonoBehaviour
    {
        public string ActToLoad;

        public void OnPress()
        {
            MessageSystem.BroadcastMessage(new LoadActCommand(ActToLoad));
        }
    }
}
