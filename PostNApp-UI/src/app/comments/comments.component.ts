import { Component, Input, OnInit } from '@angular/core';
import { Comment } from '../interfaces/comment';
import { PostService } from '../post.service';
import { AuthService } from '../auth.service';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {
  @Input() comment?: Comment;
  constructor(public authService: AuthService, private postService: PostService,
    private route: ActivatedRoute,
    private location: Location) { }

  ngOnInit(): void {
  }
  deleteComment(): void {
    const id = Number(this.comment?.id);
    const postId = Number(this.comment?.postId)

    const commentObserver = {
      next: (x: any) => {
        alert('Post deleted!');
      },
      error: (err: any) => {
        console.log(err);
        alert("Unable to delete post. Please try again.");
      }
    };
    this.postService.deleteComment(postId, id).subscribe(commentObserver)
  }
  reloadCurrentPage() {
    window.location.reload();
  }

}
