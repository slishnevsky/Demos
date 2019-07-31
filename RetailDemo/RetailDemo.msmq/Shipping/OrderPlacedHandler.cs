using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace Shipping
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            lock (Console.Out) { Console.BackgroundColor = ConsoleColor.DarkGreen; log.Info($"Received OrderPlaced, OrderId = {message.OrderId}"); Console.ResetColor(); }

            return Task.CompletedTask;
        }
    }
}