using Shared;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace Billing
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            lock (Console.Out) { Console.BackgroundColor = ConsoleColor.DarkBlue; log.Info($"Received OrderPlaced, OrderId = {message.OrderId}"); Console.ResetColor(); }

            var orderBilled = new OrderBilled { OrderId = message.OrderId };

            return context.Publish(orderBilled);
        }
    }
}
