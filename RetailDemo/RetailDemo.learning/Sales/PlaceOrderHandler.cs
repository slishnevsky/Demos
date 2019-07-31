using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Sales
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            lock (Console.Out) { Console.BackgroundColor = ConsoleColor.DarkGreen; log.Info($"Received PlaceOrder, OrderId = {message.OrderId}"); Console.ResetColor(); }

            var orderPlaced = new OrderPlaced { OrderId = message.OrderId };

            return context.Publish(orderPlaced);
        }
    }
}

