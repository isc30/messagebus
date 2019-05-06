using System;
using Isc.MessageBus.Tests.Fakes;
using Xunit;

namespace Isc.MessageBus.Tests
{
    public sealed class MessageBusTests
    {
        [Fact]
        public void MessageBus_Implementation_ImplementsIMessageBus()
        {
            // Arrange
            var bus = new MessageBus<Exception>();

            // Assert
            Assert.True(bus is IMessageBus<Exception>);
        }

        [Fact]
        public void MessageBus_Implementation_ImplementsIMessagePublisher()
        {
            // Arrange
            var bus = new MessageBus<Exception>();

            // Assert
            Assert.True(bus is IMessagePublisher<Exception>);
        }

        [Theory]
        [InlineData("isc")]
        [InlineData("")]
        [InlineData(null)]
        public void Publish_WithNoSubscriptions_DoesNothing(string message)
        {
            // Arrange
            IMessageBus<string> bus = new MessageBus<string>();

            // Act
            bus.Publish(message);
        }

        [Theory]
        [InlineData("isc")]
        [InlineData("")]
        [InlineData(null)]
        public void Publish_SingleHandler_ReceivesTheMessage(string message)
        {
            // Arrange
            IMessageBus<string> bus = new MessageBus<string>();
            var handler = new HandlerSpy<string>();

            bus.Subscribe<string>(handler.Delegate);

            // Act
            bus.Publish(message);

            // Assert
            Assert.Equal(message, handler.Messages[0]);
        }

        [Fact]
        public void Publish_SingleHandlerMultipleTimes_GetsCalledOnce()
        {
            // Arrange
            IMessageBus<string> bus = new MessageBus<string>();
            var handler = new HandlerSpy<string>();

            // Act
            bus.Subscribe<string>(handler.Delegate);
            bus.Subscribe<string>(handler.Delegate);
            bus.Subscribe<string>(handler.Delegate);

            bus.Publish(string.Empty);

            // Assert
            Assert.Equal(1, handler.Messages.Count);
        }

        [Theory]
        [InlineData("isc")]
        [InlineData("")]
        [InlineData(null)]
        public void Publish_MultipleHandlers_AllReceiveTheMessage(string message)
        {
            // Arrange
            var bus = MessageBus<string>.Empty;
            var handler0 = new HandlerSpy<string>();
            var handler1 = new HandlerSpy<string>();

            bus.Subscribe<string>(handler0.Delegate);
            bus.Subscribe<string>(handler1.Delegate);

            // Act
            bus.Publish(message);

            // Assert
            Assert.Equal(message, handler0.Messages[0]);
            Assert.Equal(message, handler1.Messages[0]);
        }
    }
}
