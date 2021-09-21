import { Component, OnInit, Input } from '@angular/core';
import { IFriend } from '../interfaces/friend';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FriendService } from '../friend.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {

  @Input() friends?: IFriend[] = [];
  constructor(
    private route: ActivatedRoute,
    private friendService: FriendService,
    private location: Location
  ) { }
  

  ngOnInit(): void {

  }

}
