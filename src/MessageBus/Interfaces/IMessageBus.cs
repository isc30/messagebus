namespace Isc.MessageBus
{
    public interface IMessageBus<in TMessage> : IMessagePublisher<TMessage>
    {
        void Subscribe<T>(MessageHandler<T> handler)
            where T : TMessage;

        void Unsubscribe<T>(MessageHandler<T> handler)
            where T : TMessage;
    }
}
