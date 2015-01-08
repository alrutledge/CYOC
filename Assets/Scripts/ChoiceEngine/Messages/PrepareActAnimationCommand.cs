using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class PrepareActAnimationCommand
    {
        public string Name { get; set; }

        public PrepareActAnimationCommand(string name)
        {
            Name = name;
        }
    }
}
