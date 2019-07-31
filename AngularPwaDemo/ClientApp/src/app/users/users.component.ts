import { Component, OnInit } from '@angular/core';
import { User } from '../app.models';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: any;

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    console.log('Get users');
    this.httpClient.get<User[]>('https://jsonplaceholder.typicode.com/users').subscribe(users => {
      this.users = users;
    });
  }

}
