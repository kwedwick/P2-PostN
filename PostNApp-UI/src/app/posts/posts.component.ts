import { Component, Input, OnInit } from '@angular/core';
import { Post } from '../interfaces/post';
import { AuthService } from '../auth.service';
import { PostService } from '../post.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';




@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  panelOpenState = false;
  addCommentOpenState = false;
  
  @Input() post?: Post;

  commentCount = this.post?.comments.length;

  constructor(public authService: AuthService, private postService: PostService,
    private route: ActivatedRoute,
    private location: Location) { }

  ngOnInit(): void {
  }

  deletePost():void {
    const id = Number(this.post?.id);

    const postObserver ={
      next: (x: any) => 
      {
        alert('Post deleted!');
      },
      error: (err:any) =>
      {
        console.log(err);
        alert("Unable to delete post. Please try again.");
      }
    };
    this.postService.deletePostById(id).subscribe(postObserver)
  }
  reloadCurrentPage() {
    window.location.reload();
   }
 
}