using UnityEngine;

namespace Assets.Scripts.ChoiceEngine
{
    [System.Serializable]
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SmallImage { get; set; }
        public string LargeImage { get; set; }

        public Item(string name, string description, string smallImageName, string largeImageName)
        {
            Name = name;
            Description = description;
            SmallImage = smallImageName;
            LargeImage = largeImageName;
        }
    }
}
