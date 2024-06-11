using System;
using System.Collections.Generic;

namespace Wallet.Utils
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, object> Entries = new();
    
        public interface IEntry<out T>
        {
            public event Action<T> Event;
        }
    
        private class Entry<T> : IEntry<T>
        {
            public event Action<T> Event;
        
            public void Push(T eventData)
            {
                Event?.Invoke(eventData);
            }
        }

        private static Entry<T> GetEntryInner<T>()
        {
            var type = typeof(T);
            Entry<T> result;
            if (Entries.ContainsKey(type))
            {
                result = (Entry<T>) Entries[type];
            }
            else
            {
                result = new Entry<T>();
                Entries.Add(type, result);
            }

            return result;
        }

        public static IEntry<T> GetEntry<T>() => GetEntryInner<T>();

        public static void Push<T>() where T : new() => Push(new T());
        public static void Push<T>(T eventData)
        {
            var inner = GetEntryInner<T>();
            inner.Push(eventData);
        }

    }
}