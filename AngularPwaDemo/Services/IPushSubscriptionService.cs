using System.Collections.Generic;
using Lib.Net.Http.WebPush;

namespace AngularPwaDemo.Services
{
    public interface IPushSubscriptionService
    {
        IEnumerable<PushSubscription> GetAll();

        void Insert(PushSubscription subscription);

        void Delete(PushSubscription subscription);
    }
}
