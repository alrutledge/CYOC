using UnityEngine;
using System.Collections;


namespace Assets.Scripts.CYOC.UI
{
    public class InventoryButton : MonoBehaviour
    {

        private Animator m_animator;

        private void Awake()
        {
            m_animator = GameObject.Find("MainGamePlay").GetComponent<Animator>();

        }
        public void OnPressed()
        {
            m_animator.enabled = true;
            if (m_animator.GetBool("CharacterIsOffscreen"))
            {
                bool IsOffscreen = m_animator.GetBool("InventoryIsOffscreen");
                m_animator.SetBool("InventoryIsOffscreen", !IsOffscreen);
            }
        }
    }
}
