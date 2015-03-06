using UnityEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.CYOC.UI.Messages;


namespace Assets.Scripts.CYOC.UI
{
    public class ActResultScreen : MonoBehaviour
    {

        public void OnContinueClicked()
        {
            MessageSystem.BroadcastMessage(new ExitToMainMenuCommand());
        }
    }
}