using UnityEngine;
using Assets.Scripts.ChoiceEngine;
using UnityEngine.UI;

namespace Assets.Scripts.CYOC.UI
{
    public class ItemButton : MonoBehaviour
    {
        private GameObject m_itemName;
        private GameObject m_itemDescription;
        private GameObject m_itemImage;

        public Item LinkedItem { get; set; }

        private void Awake()
        {
            m_itemImage = GameObject.Find("ItemImage");
            m_itemDescription = GameObject.Find("ItemDescription");
            m_itemName = GameObject.Find("ItemName");
        }

        public void OnPressed()
        {
            if (LinkedItem != null)
            {
                m_itemName.SetActive(true);
                m_itemName.GetComponent<Text>().text = LinkedItem.Name;
                m_itemDescription.SetActive(true);
                m_itemDescription.GetComponent<Text>().text = LinkedItem.Description;
                m_itemImage.SetActive(true);
                m_itemImage.GetComponent<Image>().sprite =
                    Resources.Load(LinkedItem.LargeImage, typeof (Sprite)) as Sprite;
            }
        }

    }
}
