using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Isc.MessageBus.Tests
{
    public class MessageBusTests
    {
        [Fact]
        public void Publish_WithNoSubscriptions_DoesNothing()
        {
            // Arrange
            var bus = new MessageBus<string>();

            // Act
            bus.Publish("hello!");
        }

        public static IEnumerable<object[]> X() =>
            new List<object[]>
            {
                new object[] { new[] { 1, 2, 3 } },
                new object[] { new List<int> { 1, 2, 3 } },
            };

        [Theory]
        [InlineData(new[] { 1, 2, 3 })]
        //[MemberData(nameof(X))]
        //[InlineData("")]
        //[InlineData(null)]
        public void Publish_SingleHandler_ReceivesTheValue(ICollection<int> value)
        {
            // Arrange
            ICollection<int> stringHandler_value = null;
            int[] lHandler_value = null;

            var bus = new MessageBus<ICollection<int>>();
            bus.Subscribe<ICollection<int>>(v => { stringHandler_value = v; return false; });
            bus.Subscribe<int[]>(v => { lHandler_value = v; return false; });

            // Act
            bus.Publish(value);

            // Assert
            Assert.Same(stringHandler_value, value);
            Assert.Same(lHandler_value, value);
        }

        public bool NumberReceived0(int number)
        {
            Console.WriteLine($"[0] {number}");

            return false;
        }

        public bool NumberReceived1(int number)
        {
            Console.WriteLine($"[1] {number}");

            return false;
        }
    }
}
