using UnityEngine;

namespace Assets.Scripts.CYOC.UI
{
    public class InGameExitPanel : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
