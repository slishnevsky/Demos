using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Shared;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Persistence.Sql;
using System.Configuration;

namespace ClientUI
{
    class Program
    {
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        static async Task Main()
        {
            Console.Title = "ClientUI";

            await Helper.DeleteAllQueues();
            await Helper.DeleteAllTables(connectionString);

            var endpointConfiguration = new EndpointConfiguration("ClientUI");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.EnableOutbox();

            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.ConnectionString(connectionString);

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(() => new SqlConnection(connectionString));

            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            await SomeInput(endpointInstance).ConfigureAwait(false);

            await endpointInstance.Stop().ConfigureAwait(false);

        }

        static ILog log = LogManager.GetLogger<Program>();

        static async Task SomeInput(IEndpointInstance endpointInstance)
        {
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
