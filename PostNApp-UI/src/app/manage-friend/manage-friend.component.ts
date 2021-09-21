import { Component, Input, OnInit } from '@angular/core';
import { FriendService } from '../friend.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { AuthService } from '../auth.service';
import { IFriend } from '../interfaces/friend';

@Component({
  selector: 'app-manage-friend',
  templateUrl: './manage-friend.component.html',
  styleUrls: ['./manage-friend.component.css']
})
export class ManageFriendComponent implements OnInit {
  isFriend = false;
  currentuserId = this.authService.currentUser.id;

  @Input() otherUserId?: any;
  constructor(public authService: AuthService, private friendService: FriendService,
    private route: ActivatedRoute,
    private location: Location) { }

  ngOnInit(): void {
    this.checkFollower();
    this.route.params.subscribe(routeParams => {
      this.checkFollower();
    });
  }

  checkFollower() {
    this.friendService.checkFriendship(this.currentuserId, this.otherUserId)
    .subscribe(response => this.isFriend = response);
  }

  addFriend(){
    this.friendService.addFriend(this.currentuserId, this.otherUserId)
    .subscribe(response => this.isFriend = response);
  }

  deleteFriend(){
    this.friendService.deleteFriend(this.currentuserId, this.otherUserId)
    .subscribe(response => this.isFriend = response);
  }
}
