namespace Isc.MessageBus
{
    public interface IMessagePublisher<in TMessage>
    {
        void Publish<T>(T message)
            where T : TMessage;
    }
}
