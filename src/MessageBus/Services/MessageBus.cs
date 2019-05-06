using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Isc.MessageBus
{
    public sealed class MessageBus<TMessage>
        : IMessageBus<TMessage>
    {
        public static MessageBus<TMessage> Empty => new MessageBus<TMessage>();

        private ConcurrentDictionary<Type, Delegate> _handlers
            = new ConcurrentDictionary<Type, Delegate>();

        public void Publish<T>(T message)
            where T : TMessage
        {
            bool stopPropagation = false;

            foreach (var handlerTypes in _handlers.Where(h => h.Key.IsAssignableFrom(typeof(T))))
            {
                var typedDelegate = (MessageHandler<T>)handlerTypes.Value;

                typedDelegate?.Invoke(message);
            }

            if (!stopPropagation)
            {
                BubbleUp(message);
            }
        }

        public void Subscribe<T>(MessageHandler<T> handler)
            where T : TMessage
        {
            var messageType = typeof(T);

            lock (_handlers)
            {
                _handlers.GetOrAdd(messageType, (MessageHandler<T>)null);
                _handlers[messageType] = _handlers[messageType].AddHandler(handler);
            }
        }

        public void Unsubscribe<T>(MessageHandler<T> handler)
            where T : TMessage
        {
            var messageType = typeof(T);

            lock (_handlers)
            {
                _handlers.GetOrAdd(messageType, (MessageHandler<T>)null);
                _handlers[messageType] = _handlers[messageType].RemoveHandler(handler);
            }
        }

        private void BubbleUp<T>(T message)
            where T : TMessage
        {
            // throw new NotImplementedException();
        }
    }
}
