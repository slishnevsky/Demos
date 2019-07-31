import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  url = 'https://localhost:5001/hub';
  hubConnection: HubConnection;
  // Use accessToken when JWT-based authentication is enabled
  // accessToken = '85883011-d3fd-4a90-a908-311284f2af81';
  title = 'AngularSignalRDemo';
  events: string[] = [];
  messages: string[] = [];
  name: string;

  ngOnInit() {
    console.log(this.name);

    // Configure signalr connection (without authentication)
    this.events.push('Configuring hub connection at ' + this.url);
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.url)
      .build();

    // Configure signalr connection (with authentication)
    // this.hubConnection = new HubConnectionBuilder()
    //   .withUrl(this.url, {
    //     accessTokenFactory: () => this.accessToken
    //   })
    //   .build();

    // Subscribe to signalr events
    this.events.push('Subscribing to events');
    this.hubConnection.on('ReceiveMessage', (name: string, message: string) => {
      const text = `${name}: ${message}`;
      this.messages.push(text);
    });

    this.hubConnection.on('UserConnected', (connectionId: string) => {
      const text = `User [${connectionId}] connected`;
      this.events.push(text);
    });

    this.hubConnection.on('UserDisconnected', (connectionId: string) => {
      const text = `User [${connectionId}] disconnected`;
      this.events.push(text);
    });

    // Establish signalr connection (connect)
    this.events.push('Establishing connection');
    this.hubConnection.start()
      .then(() => this.events.push('Connection started'))
      .catch(err => this.events.push('Error while establishing connection'));
  }

  onEnter(message: string) {
    if (message === '') { return; }
    if (!this.name) { this.name = 'Anonymous'; }
    this.hubConnection.send('SendMessageToAll', this.name, message)
      .catch((err) => this.messages.push(err.message));
  }
}
