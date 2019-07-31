using System;
using System.Collections.Generic;
using Lib.Net.Http.WebPush;
using System.IO;
using Newtonsoft.Json;

namespace AngularPwaDemo.Services
{
    class PushSubscriptionService : IPushSubscriptionService
    {
        private List<PushSubscription> _subscriptions;

        public PushSubscriptionService()
        {
            _subscriptions = new List<PushSubscription>();
        }

        public IEnumerable<PushSubscription> GetAll()
        {
            return _subscriptions;
        }

        public void Insert(PushSubscription subscription)
        {
            _subscriptions.Add(subscription);
        }

        public void Delete(PushSubscription subscription)
        {
            var foundSubscription = _subscriptions.Find(sub => sub.Endpoint == subscription.Endpoint);
            if(foundSubscription != null) _subscriptions.Remove(foundSubscription);
        }
    }
}
