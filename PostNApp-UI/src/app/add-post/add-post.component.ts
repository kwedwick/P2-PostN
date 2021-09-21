import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PostService } from '../post.service';
import { Post } from '../interfaces/post';

@Component({
  selector: 'app-add-post',
  templateUrl: './add-post.component.html',
  styleUrls: ['./add-post.component.css']
})
export class AddPostComponent implements OnInit {

  userId = this.authService.currentUser.id;
  username = this.authService.currentUser.username;
  formGroup = new FormGroup({
    userId: new FormControl(this.userId, [Validators.required]),
    title: new FormControl('', [Validators.required]),
    body: new FormControl('', [Validators.required]),
    username: new FormControl(this.username, [Validators.required])
  })

  constructor(public authService: AuthService, private fb: FormBuilder, private postService: PostService) { }


  ngOnInit(): void {
  }

  newPost() {
    if (this.formGroup.valid)
    {
      const postObserver ={
        next: (x: any) => 
        {
          alert('Post added!');
        },
        error: (err:any) =>
        {
          console.log(err);
          alert("Unable to post. Please try again.");
        }
      };
      this.postService.addPost(this.formGroup.value)
        .subscribe(postObserver)
    } else {
      alert("Missing post information!");
    }
  }
  reloadCurrentPage() {
    window.location.reload();
   }
   
   

}
