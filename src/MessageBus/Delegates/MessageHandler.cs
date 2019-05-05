namespace Isc.MessageBus
{
    public delegate bool MessageHandler<in TMessage>(TMessage message);
}
