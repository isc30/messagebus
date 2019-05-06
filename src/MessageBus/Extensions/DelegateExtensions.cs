using System;

namespace Isc.MessageBus
{
    public static class DelegateExtensions
    {
        public static TDelegate AddHandler<TDelegate>(
            this TDelegate source,
            TDelegate handler)
            where TDelegate : Delegate
        {
            source = RemoveHandler(source, handler);

            return (TDelegate)Delegate.Combine(source, handler);
        }

        public static TDelegate RemoveHandler<TDelegate>(
            this TDelegate source,
            TDelegate handler)
            where TDelegate : Delegate
        {
            return (TDelegate)Delegate.RemoveAll(source, handler);
        }
    }
}
