using UnityEngine;

namespace Assets.Scripts.ChoiceEngine
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Texture2D SmallImage { get; set; }
        public Texture2D LargeImage { get; set; }

        public Item(string name, string description, string smallImageName, string largeImageName)
        {
            Name = name;
            Description = description;
            SmallImage = Resources.Load(smallImageName) as Texture2D;
            LargeImage = Resources.Load(largeImageName) as Texture2D;
        }
    }
}
