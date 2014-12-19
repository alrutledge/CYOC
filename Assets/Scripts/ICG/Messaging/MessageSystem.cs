using UnityEngine;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.ICG.Messaging
{
    public delegate void MessageReceiver<T>(T message);
    public delegate R MessageReceiver<R, T>(T message);

    public class MessageSystem : MonoBehaviour
    {
        public static MessageSystem Instance;
        public static GameObject ServiceContext;
        
        public static void SubscribeMessage<MESSAGE>(GameObject context, MessageReceiver<MESSAGE> messageReceiver)
        {
            if (!Instance.m_messageSubscriptions.ContainsKey(typeof(MESSAGE)))
            {
                Instance.m_messageSubscriptions[typeof(MESSAGE)] = new MessageReceiverList<MESSAGE>();
            }
            MessageReceiverList<MESSAGE> list = (MessageReceiverList<MESSAGE>)Instance.m_messageSubscriptions[typeof(MESSAGE)];
            list.Add(context, messageReceiver);
        }

        public static void SubscribeQuery<REPLY, QUERY>(GameObject context, MessageReceiver<REPLY, QUERY> messageReceiver)
        {
            if (!Instance.m_querySubscriptions.ContainsKey(typeof(QUERY)))
            {
                Instance.m_querySubscriptions[typeof(QUERY)] = new MessageReceiverList<REPLY, QUERY>();
            }
            MessageReceiverList<REPLY, QUERY> list = (MessageReceiverList<REPLY, QUERY>)Instance.m_querySubscriptions[typeof(QUERY)];
            list.Add(context, messageReceiver);
        }

        public static void UnsubscribeMessage<MESSAGE>(GameObject context, MessageReceiver<MESSAGE> messageReceiver)
        {
            if (!Instance.m_messageSubscriptions.ContainsKey(typeof(MESSAGE)))
            {
                MessageReceiverList<MESSAGE> list = (MessageReceiverList<MESSAGE>)Instance.m_messageSubscriptions[typeof(MESSAGE)];
                list.Remove(context, messageReceiver);
            }
        }

        public static void UnsubscribeQuery<REPLY, QUERY>(GameObject context, MessageReceiver<REPLY, QUERY> messageReceiver)
        {
            if (!Instance.m_querySubscriptions.ContainsKey(typeof(QUERY)))
            {
                MessageReceiverList<REPLY, QUERY> list = (MessageReceiverList<REPLY, QUERY>)Instance.m_querySubscriptions[typeof(QUERY)];
                list.Remove(context, messageReceiver);
            }
        }

        public static void SendMessage<MESSAGE>(GameObject context, MESSAGE message)
        {
            if (Instance.m_messageSubscriptions.ContainsKey(typeof(MESSAGE)))
            {
                MessageReceiverList<MESSAGE> list = (MessageReceiverList<MESSAGE>)Instance.m_messageSubscriptions[typeof(MESSAGE)];
                list.SendMessage(context, message);
            }
        }

        public static REPLY SendQuery<REPLY, QUERY>(GameObject context, QUERY message)
        {
            if (Instance.m_querySubscriptions.ContainsKey(typeof(QUERY)))
            {
                MessageReceiverList<REPLY, QUERY> list = (MessageReceiverList<REPLY, QUERY>)Instance.m_querySubscriptions[typeof(QUERY)];
                return list.SendMessage(context, message);
            }
            return default(REPLY);
        }

        public static R BroadcastQuery<R, T>(T message)
        {
            if (Instance.m_querySubscriptions.ContainsKey(typeof(T)))
            {
                MessageReceiverList<R, T> list = (MessageReceiverList<R, T>)Instance.m_querySubscriptions[typeof(T)];
                return list.SendMessage(message); // dubious sends back only the first response
            }
            return default(R);
        }

        public static void BroadcastMessage<MESSAGE>(MESSAGE message)
        {
            if (Instance.m_messageSubscriptions.ContainsKey(typeof(MESSAGE)))
            {
                MessageReceiverList<MESSAGE> list = (MessageReceiverList<MESSAGE>)Instance.m_messageSubscriptions[typeof(MESSAGE)];
                list.SendMessage(ServiceContext, message);
            }
        }

        private Dictionary<Type, IMessageReceiverList> m_messageSubscriptions = new Dictionary<Type, IMessageReceiverList>();
        private Dictionary<Type, IMessageReceiverList> m_querySubscriptions = new Dictionary<Type, IMessageReceiverList>();

        private void Update()
        {
            foreach(IMessageReceiverList list in m_messageSubscriptions.Values)
            {
                list.Update();
            }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple instances of Message System!");
            }
            Instance = this;
            ServiceContext = gameObject;
        }
    }
}
