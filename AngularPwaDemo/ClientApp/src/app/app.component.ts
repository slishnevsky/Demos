import { Component, OnInit, Inject } from '@angular/core';
import { SwUpdate, SwPush } from '@angular/service-worker';
import { HttpClient } from '@angular/common/http';

const notificationPayload = {
  notification: {
    title: 'Angular News',
    body: 'Angular newsletter is available!',
    icon: 'assets/icons/icon-96x96.png',
    image: 'assets/images/weather.jpg',
    vibrate: [100, 50, 100],
    data: {
      url: 'https://www.tpeczek.com/2017/12/push-notifications-and-aspnet-core-part.html'
    },
    actions: [{
      action: 'http://www.google.com',
      title: 'Google'
    }, {
      action: 'http://www.microsoft.com',
      title: 'Microsoft'
    }]
  }
};

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'AngularPwaDemo';
  subscription: PushSubscription;
  hostingService: string;

  constructor(private swUpdate: SwUpdate, private swPush: SwPush, private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {

    this.getHostingService();

    console.log(this.swUpdate.isEnabled ? 'Update is enabled' : 'Update is disabled');
    console.log(this.swPush.isEnabled ? 'Push is enabled' : 'Push is disabled');


    if (this.swUpdate.isEnabled) {
      this.swUpdate.available.subscribe(() => {
        if (confirm('Update is available. Install new version?')) {
          this.swUpdate.activateUpdate().then(() => document.location.reload());
        }
      });
    }
    if (this.swPush.isEnabled) {
      this.swPush.subscription.subscribe(subscription => {
        console.log('Subscription is retrieved from firebase messaging server');
        this.subscription = subscription;
      });
    }
  }

  getHostingService() {
    console.log('Get hosting service');
    this.httpClient.get(this.baseUrl + 'api/hostingservice', { responseType: 'text' }).subscribe((hostingService: string) => {
      this.hostingService = hostingService;
    });
  }

  subscribe() {
    if (this.swPush.isEnabled) {
      console.log('Subscription is requested');
      this.httpClient.get(this.baseUrl + 'api/publickey', { responseType: 'text' }).subscribe((publicKey: string) => {
        console.log('Public key received: ', publicKey);
        this.swPush.requestSubscription({ serverPublicKey: publicKey }).then(subcription => {
          console.log('Subscription is added to firebase messaging server');
          this.subscription = subcription;
          this.httpClient.post(this.baseUrl + 'api/subscribe', subcription).subscribe(() => {
            console.log('Subscription is added to application server');
          });
        });
      });
    }
  }

  unsubscribe() {
    if (this.swPush.isEnabled) {
      console.log('Unsubscription is requested');
      const subcription = this.subscription;
      this.swPush.unsubscribe().then(() => {
        console.log('Subscription is removed from firebase messaging server');
        this.httpClient.post(this.baseUrl + 'api/unsubscribe', subcription).subscribe(() => {
          console.log('Subscription is removed from application server');
        });
      });
    }
  }

  broadcast() {
    if (this.swPush.isEnabled) {
      console.log('Broadcasting notification: ', notificationPayload);
      this.httpClient.post(this.baseUrl + 'api/broadcast', notificationPayload).subscribe(() => {
        console.log('Notification has been broadcasted');
      });
    }
  }
}
