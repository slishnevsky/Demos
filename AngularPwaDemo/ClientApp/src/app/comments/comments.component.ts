import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Comment } from '../app.models';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {
  comments: Comment[];

  constructor(private httpClient: HttpClient, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const postId = params.get('id');
      this.getComments(postId);
    });
  }

  getComments(postId: string) {
    console.log('Get comments for postId ' + postId);
    this.httpClient.get<Comment[]>('https://jsonplaceholder.typicode.com/comments?postId=' + postId).subscribe(comments => {
      this.comments = comments;
    });
  }
}
