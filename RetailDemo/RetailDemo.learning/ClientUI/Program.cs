using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace ClientUI
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "ClientUI";

            var endpointConfiguration = new EndpointConfiguration("ClientUI");

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            await SomeInput(endpointInstance).ConfigureAwait(false);

            await endpointInstance.Stop().ConfigureAwait(false);
        }

        static ILog log = LogManager.GetLogger<Program>();

        static async Task SomeInput(IEndpointInstance endpointInstance)
        {
            Console.WriteLine("Press Space to send a message or Enter to exit");
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) return;
                if (key.Key != ConsoleKey.Spacebar) continue;

                var command = new PlaceOrder { OrderId = Guid.NewGuid().ToString().Substring(0, 5).ToUpper() };

                lock (Console.Out) { Console.BackgroundColor = ConsoleColor.DarkRed; log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}"); Console.ResetColor(); }

                await endpointInstance.Send(command);
            }
        }

    }
}
