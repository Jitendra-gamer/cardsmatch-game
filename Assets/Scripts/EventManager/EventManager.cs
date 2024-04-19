using System;
using System.Collections.Generic;

namespace CardMatch
{
    public class EventManager
    {
        private static readonly Dictionary<string, Action> events = new Dictionary<string, Action>();

        public static void AddListener(string eventName, Action action)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName] -= action;
                events[eventName] += action;
            }
            else
            {
                Action eventAction = null;
                eventAction += action;
                events.Add(eventName, eventAction);
            }
        }

        public static void RemoveListener(string eventName, Action action)
        {
            if (events.ContainsKey(eventName))
                events[eventName] -= action;
        }

        public static void Dispatch(string eventName)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName]?.Invoke();
            }
        }
    }

    public class EventManager<T>
    {
        private static readonly Dictionary<string, Action<T>> eventsWithParam = new Dictionary<string, Action<T>>();

        public static void AddListener(string eventName, Action<T> action)
        {
            if (eventsWithParam.ContainsKey(eventName))
            {
                eventsWithParam[eventName] -= action;
                eventsWithParam[eventName] += action;
            }
            else
            {
                Action<T> eventAction = null;
                eventAction += action;
                eventsWithParam.Add(eventName, eventAction);
            }
        }

        public static void RemoveListener(string eventName, Action<T> action)
        {
            if (eventsWithParam.ContainsKey(eventName))
            {
                eventsWithParam[eventName] -= action;
            }
        }

        public static void Dispatch(string eventName, T data)
        {
            if (eventsWithParam.ContainsKey(eventName))
            {
                eventsWithParam[eventName]?.Invoke(data);
            }
        }
    }
}