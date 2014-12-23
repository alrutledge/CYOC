using UnityEngine;
using System.Collections;

namespace Assets.Scripts.CYOC.UI
{
    public class CharacterButton : MonoBehaviour
    {

        private Animator m_animator;

        private void Awake()
        {
            m_animator = GameObject.Find("MainGamePlay").GetComponent<Animator>();
        }

        public void OnPressed()
        {
            m_animator.enabled = true;
            if (m_animator.GetBool("InventoryIsOffscreen"))
            {
                ToggleCharacter();
            }
        }

        private void ToggleCharacter()
        {
            bool IsOffscreen = m_animator.GetBool("CharacterIsOffscreen");
            m_animator.SetBool("CharacterIsOffscreen", !IsOffscreen);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !m_animator.GetBool("CharacterIsOffscreen"))
            {
                ToggleCharacter();
            }
        }
    }
}
