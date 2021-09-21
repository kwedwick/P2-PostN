import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { PostService } from '../post.service';
import { Post } from '../interfaces/post';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{

  userId = this.authService.currentUser.id;
  //userId = JSON.parse(localStorage.getItem("userId")!);
  posts: Post[] = [];
  constructor(private jwtHelper: JwtHelperService, private router: Router, private postService: PostService, public authService: AuthService) {}



  isUserAuthenticated() {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {
      return false;
    }
  }
  ngOnInit(): void {
    this.getFriendPosts();
  }
  getFriendPosts(): void {
    
    this.postService.getFriendPosts(this.userId)
      .subscribe(posts => this.posts = posts);
  }

  
}