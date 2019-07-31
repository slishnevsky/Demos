using NServiceBus;

namespace Shared
{
    public class OrderBilled : IEvent
    {
        public string OrderId { get; set; }
    }
}