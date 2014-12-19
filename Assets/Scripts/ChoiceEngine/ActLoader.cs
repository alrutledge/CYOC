using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.ChoiceEngine.Messages;

namespace Assets.Scripts.ChoiceEngine
{
    public class ActLoader : MonoBehaviour
    {
        public Act LoadedAct { get; set; }
        public bool ActLoaded { get; set; }


        private void Awake()
        {
            ActLoaded = false;
            MessageSystem.SubscribeMessage<LoadActCommand>(MessageSystem.ServiceContext, OnLoadActCommand);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<LoadActCommand>(MessageSystem.ServiceContext, OnLoadActCommand);
        }

        void OnLoadActCommand(LoadActCommand command)
        {
            StreamReader re = new StreamReader("Assets\\" + command.ActToLoad + ".dat");
            JsonTextReader reader = new JsonTextReader(re);
            JsonSerializer se = new JsonSerializer();
            LoadedAct = se.Deserialize<Act>(reader);
            LoadedAct.ConvertEntries();
            ActLoaded = true;
            MessageSystem.BroadcastMessage(new ActLoadedMessage(LoadedAct.Entries[command.EntryToLoad], LoadedAct));
        }
    }
}
