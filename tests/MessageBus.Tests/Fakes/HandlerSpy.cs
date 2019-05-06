using System.Collections.Generic;

namespace Isc.MessageBus.Tests.Fakes
{
    public class HandlerSpy<T>
    {
        private readonly List<T> _messages = new List<T>();

        public IReadOnlyList<T> Messages => _messages;

        public void Delegate(T message)
        {
            _messages.Add(message);
        }
    }
}
