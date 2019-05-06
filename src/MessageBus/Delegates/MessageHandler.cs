namespace Isc.MessageBus
{
    public delegate void MessageHandler<in TMessage>(TMessage message);
}
