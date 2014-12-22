﻿using Assets.Scripts.ChoiceEngine.Messages;
using Assets.Scripts.ICG.Messaging;

namespace Assets.Scripts.ChoiceEngine.ChoiceActions
{
    public class ModifyAttributeAction : ChoiceAction
    {
        public PlayerStat PlayerStat {get; set;}
        public int Delta { get; set; }

        public ModifyAttributeAction(PlayerStat playerStat, int delta)
        {
            PlayerStat = playerStat;
            Delta = delta;
        }

        public override void PerformAction()
        {
            MessageSystem.BroadcastMessage(new ModifyAttributeCommand(PlayerStat, Delta));
        }        
    }
}
