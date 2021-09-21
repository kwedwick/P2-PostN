import { Component, OnInit, Input } from '@angular/core';
import { UserService } from '../user.service';
import { User } from '../interfaces/user';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../auth.service';
@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css']
})
export class UserCardComponent implements OnInit {

  @Input() user?: User;
  
  constructor(private userService: UserService, private route: ActivatedRoute, private authServiceService: AuthService) { }

  ngOnInit(): void {
    //this.getUser();
  }
  // getUser(): void {
  //   const id = this.authServiceService.currentUser.id;
  //   this.userService.getUser(id)
  //     .subscribe(user => this.user = user);
  // }

}
