import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PostService } from '../post.service';
import { Comment } from '../interfaces/comment';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css']
})
export class AddCommentComponent implements OnInit {
  mySubscription: any;
  @Input() postId?: any;

  userId = this.authService.currentUser.id;
  username = this.authService.currentUser.username;

  formGroup = new FormGroup({
    userId: new FormControl(this.userId, [Validators.required]),
    username: new FormControl(this.username, [Validators.required]),
    commentBody: new FormControl('', [Validators.required]),
  })


  constructor(
    public authService: AuthService, 
    private fb: FormBuilder, 
    private postService: PostService,
    
    ) { }
    


  ngOnInit(): void {
  }

  newComment() {
    if (this.formGroup.valid)
    {
      const commentObserver ={
        next: (x: any) => 
        {
          alert('Comment added!');
        },
        error: (err:any) =>
        {
          console.log(err);
          alert("Unable to post. Please try again.");
        }
      };
      this.postService.addComment(this.postId, this.formGroup.value)
        .subscribe(commentObserver)
    } else {
      console.log(this.formGroup);
      alert("Missing comment information!");
    }
  }
  reloadCurrentPage() {
    window.location.reload();
   }

}
