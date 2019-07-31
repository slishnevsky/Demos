using Messages;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace Shipping
{
    public class OrderBilledHandler : IHandleMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<OrderBilledHandler>();

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            lock (Console.Out) { Console.BackgroundColor = ConsoleColor.DarkBlue; log.Info($"Received OrderBilled, OrderId = {message.OrderId}"); Console.ResetColor(); }

            return Task.CompletedTask;
        }
    }
}
