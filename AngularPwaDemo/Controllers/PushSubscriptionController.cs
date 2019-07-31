using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AngularPwaDemo.Models;
using AngularPwaDemo.Services;
using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AngularPwaDemo.Controllers
{
    [ApiController]
    public class PushSubscriptionController : ControllerBase
    {
        private readonly PushNotificationOptions _options;
        private readonly IPushSubscriptionService _pushSubscriptionsService;
        private readonly PushNotificationService _pushNotificationService;
        public PushSubscriptionController(IOptions<PushNotificationOptions> options, IPushSubscriptionService pushSubscriptionsService, PushNotificationService pushNotificationService)
        {
            _options = options.Value;
            _pushSubscriptionsService = pushSubscriptionsService;
            _pushNotificationService = pushNotificationService;
        }

        [HttpGet("api/publickey")]
        public ContentResult GetPublicKey()
        {
            return Content(_options.PublicKey, "text/plain");
        }

        [HttpPost("api/subscribe")]
        public void Subscribe([FromBody] PushSubscription subscription)
        {
            _pushSubscriptionsService.Insert(subscription);
        }

        [HttpPost("api/unsubscribe")]
        public void Unsubscribe([FromBody] PushSubscription subscription)
        {
            _pushSubscriptionsService.Delete(subscription);
        }

        [HttpPost("api/broadcast")]
        public void Broadcast([FromBody] PushNotificationModel notification)
        {
            _pushNotificationService.Broadcast(notification, CancellationToken.None);
        }
    }
}
