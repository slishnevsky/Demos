using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Lib.Net.Http.WebPush;

namespace AngularPwaDemo.Models
{
    public class PushNotificationModel
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public Notification Notification { get; set; }

        public PushMessage ToPushMessage(string topic = null, int? timeToLive = null, PushMessageUrgency urgency = PushMessageUrgency.Normal)
        {
            return new PushMessage(JsonConvert.SerializeObject(this, _jsonSerializerSettings))
            {
                Topic = topic,
                TimeToLive = timeToLive,
                Urgency = urgency
            };
        }
    }

    public class Notification
    {
        public class NotificationAction
        {
            public NotificationAction(string action, string title)
            {
                Action = action;
                Title = title;
            }
            
            public string Action { get; }
            public string Title { get; }
        }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Image { get; set; }

        public string Icon { get; set; }

        public IList<int> Vibrate { get; set; } = new List<int>();

        public IDictionary<string, object> Data { get; set; }

        public IList<NotificationAction> Actions { get; set; } = new List<NotificationAction>();
    }
}
