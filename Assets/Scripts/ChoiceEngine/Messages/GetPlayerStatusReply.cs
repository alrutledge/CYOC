using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ChoiceEngine.Messages
{
    public class GetPlayerStatusReply
    {
        public PlayerStatus Status { get; private set; }
        public GetPlayerStatusReply(PlayerStatus status)
        {
            Status = status;
        }
    }
}
