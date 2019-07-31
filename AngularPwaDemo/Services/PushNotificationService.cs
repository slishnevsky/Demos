using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AngularPwaDemo.Models;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AngularPwaDemo.Services
{
    public class PushNotificationService : BackgroundService
    {
        private const int NOTIFICATION_FREQUENCY = 10000;
        private readonly Random _random = new Random();
        private readonly IPushSubscriptionService _pushSubscriptionService;
        private readonly PushServiceClient _pushClient;

        public PushNotificationService(IOptions<PushNotificationOptions> options, IPushSubscriptionService pushSubscriptionService, PushServiceClient pushClient)
        {
            _pushSubscriptionService = pushSubscriptionService;
            _pushClient = pushClient;
            _pushClient.DefaultAuthentication = new VapidAuthentication(options.Value.PublicKey, options.Value.PrivateKey);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(NOTIFICATION_FREQUENCY, cancellationToken);

                var temperatureC = _random.Next(-20, 55);
                var notification = new PushNotificationModel()
                {
                    Notification = new Notification
                    {
                        Title = "New Weather Forecast",
                        Body = $"Temp. (C): {temperatureC} | Temp. (F): {32 + (int)(temperatureC / 0.5556)}",
                        Icon = "assets/icons/icon-96x96.png"
                    }
                };

                Broadcast(notification, cancellationToken);
            }
        }

        public void Broadcast(PushNotificationModel notification, CancellationToken cancellationToken)
        {
            var pushMessage = notification.ToPushMessage();
            foreach (PushSubscription pushSubscription in _pushSubscriptionService.GetAll())
            {
                _pushClient.RequestPushMessageDeliveryAsync(pushSubscription, pushMessage, cancellationToken);
            }
        }
    }
}
