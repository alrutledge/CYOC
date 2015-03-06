using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class GetCurrentActReply
    {
        public Act CurrentAct { get; set; }
        public GetCurrentActReply(Act currentAct)
        {
            CurrentAct = currentAct;
        }
    }
}
