using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ICG.Messaging
{
    public class MessageReceiverList<REPLY, QUERY> : IMessageReceiverList
    {
        Dictionary<GameObject, List<MessageReceiver<REPLY, QUERY>>> m_receivers = new Dictionary<GameObject, List<MessageReceiver<REPLY, QUERY>>>();
        Dictionary<GameObject, List<MessageReceiver<REPLY, QUERY>>> m_receiversToRemove = new Dictionary<GameObject, List<MessageReceiver<REPLY, QUERY>>>();

        public void Add(GameObject context, MessageReceiver<REPLY, QUERY> messageReceiver)
        {
            if (!m_receivers.ContainsKey(context))
            {
                m_receivers[context] = new List<MessageReceiver<REPLY, QUERY>>();
            }
            m_receivers[context].Add(messageReceiver);
        }

        public REPLY SendMessage(GameObject context, QUERY message)
        {
            if (!m_receivers.ContainsKey(context))
            {
                return default(REPLY);
            }
            foreach (MessageReceiver<REPLY, QUERY> receiver in m_receivers[context])
            {
                return receiver(message);
            }
            return default(REPLY);
        }

        public REPLY SendMessage(QUERY message)
        {
            if (m_receivers.Count == 0)
            {
                return default(REPLY);
            }
            return m_receivers.ElementAt(0).Value[0](message);
        }

        public void Remove(GameObject context, MessageReceiver<REPLY, QUERY> messageReceiver)
        {
            if (!m_receiversToRemove.ContainsKey(context))
            {
                m_receiversToRemove[context] = new List<MessageReceiver<REPLY, QUERY>>();
            }
            m_receiversToRemove[context].Add(messageReceiver);
        }

        public void Update()
        {
            foreach (KeyValuePair<GameObject,List<MessageReceiver<REPLY, QUERY>>> receiverListKVP in m_receiversToRemove)
            {
                foreach (MessageReceiver<REPLY, QUERY> receiver in receiverListKVP.Value)
                {
                    m_receivers[receiverListKVP.Key].Remove(receiver);
                }
                
            }
        }
    }

    public class MessageReceiverList<MESSAGE> : IMessageReceiverList
    {
        Dictionary<GameObject, List<MessageReceiver<MESSAGE>>> m_receivers = new Dictionary<GameObject, List<MessageReceiver<MESSAGE>>>();
        Dictionary<GameObject, List<MessageReceiver<MESSAGE>>> m_receiversToRemove = new Dictionary<GameObject, List<MessageReceiver<MESSAGE>>>();

        public void Add(GameObject context, MessageReceiver<MESSAGE> messageReceiver)
        {
            if (!m_receivers.ContainsKey(context))
            {
                m_receivers[context] = new List<MessageReceiver<MESSAGE>>();
            }
            m_receivers[context].Add(messageReceiver);
        }

        public void SendMessage(GameObject context, MESSAGE message)
        {
            if (!m_receivers.ContainsKey(context))
            {
                return;
            }
            foreach (MessageReceiver<MESSAGE> receiver in m_receivers[context])
            {
                receiver(message);
            }
        }

        public void Remove(GameObject context, MessageReceiver<MESSAGE> messageReceiver)
        {
            if (!m_receiversToRemove.ContainsKey(context))
            {
                m_receiversToRemove[context] = new List<MessageReceiver<MESSAGE>>();
            }
            m_receiversToRemove[context].Add(messageReceiver);
        }

        public void Update()
        {
            foreach (KeyValuePair<GameObject, List<MessageReceiver<MESSAGE>>> receiverListKVP in m_receiversToRemove)
            {
                foreach (MessageReceiver<MESSAGE> receiver in receiverListKVP.Value)
                {
                    m_receivers[receiverListKVP.Key].Remove(receiver);
                }

            }
        }
    }
}
