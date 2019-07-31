import { Component, OnInit, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Post } from '../app.models';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  posts: Post[];

  constructor(private httpClient: HttpClient, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const userId = params.get('id');
      this.getPosts(userId);
    });
  }

  getPosts(userId: string) {
    console.log('Get posts for userId ' + userId);
    this.httpClient.get<Post[]>('https://jsonplaceholder.typicode.com/posts?userId=' + userId).subscribe(posts => {
      this.posts = posts;
    });
  }
}
