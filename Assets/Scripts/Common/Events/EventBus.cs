using System;
using System.Collections.Generic;

namespace Common.Events
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<object>> _eventHandlersMap = new();

        public void Fire<TEvent>()
        {
            var emptyEvent = default(TEvent);
            Fire(emptyEvent);
        }

        public void Fire<TEvent>(TEvent e)
        {
            List<object> handlers = GetHandlers<TEvent>();

            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                object handler = handlers[i];
                if (handler is Action action)
                {
                    action.Invoke();
                }
                else if (handler is Action<TEvent> actionEvent)
                {
                    actionEvent.Invoke(e);
                }
            }
        }

        public void Subscribe<TEvent>(Action listener)
        {
            List<object> handlers = GetHandlers<TEvent>();
            handlers.Add(listener);
        }

        public void Subscribe<TEvent>(Action<TEvent> listener)
        {
            List<object> handlers = GetHandlers<TEvent>();
            handlers.Add(listener);
        }

        public void Unsubscribe<TEvent>(Action listener)
        {
            UnsubscribeInternal<TEvent>(listener);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> listener)
        {
            UnsubscribeInternal<TEvent>(listener);
        }

        private List<object> GetHandlers<TEvent>()
        {
            Type eventType = typeof(TEvent);
            if (!_eventHandlersMap.TryGetValue(eventType, out List<object> handlers))
            {
                handlers = new List<object>();
                _eventHandlersMap.Add(eventType, handlers);
            }

            return handlers;
        }

        private void UnsubscribeInternal<TEvent>(object listener)
        {
            List<object> handlers = GetHandlers<TEvent>();
            handlers.Remove(listener);
        }
    }
}