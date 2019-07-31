# AngularPwaDemo
Sample project for demonstrating capabilities of Progressive Web App, such as automatic updates, offline functionality and push notifications, based on Push API and Web Push Protocol in ASP.NET Core powered Angular application.

Originated from:
https://www.telerik.com/blogs/push-notifications-in-asp-net-core-with-angular
https://github.com/tpeczek/Demo.AspNetCore.Angular.PushNotifications

Workaround for handling **notificationclick** event when PuchNotification is received by the ServiceWorker.
Currently its not supported in Angular, but there is a workaround.
Add the code below to ngsw-worker.js after the line this.scope.addEventListener('push', (event) => this.onPush(event));

```
this.scope.addEventListener('notificationclick', (event) => {
  console.log('[Service Worker] Notification click Received. event', event);
  event.notification.close();
  if (clients.openWindow && event.notification.data.url) {
    event.waitUntil(clients.openWindow(event.notification.data.url));
  }
});
```

Then you can specify the URL in the "notification.data.url".
