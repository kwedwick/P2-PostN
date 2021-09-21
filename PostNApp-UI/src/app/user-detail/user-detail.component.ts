import { Component, OnInit, Input } from '@angular/core';
import { User } from '../interfaces/user';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { UserService } from '../user.service';


@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {


  @Input() user?: User;
  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getUser();
  }
  goBack(): void {
    this.location.back();
  }

  getUser(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.userService.getUser(id)
      .subscribe(user => this.user = user);
  }

 
  save(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.user && id) {
      this.userService.updateUser(id, this.user)
        .subscribe(() => this.goBack());
    }
  }

  delete(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
   
    //this.user = this.user.filter(u => u !== user);
    this.userService.deleteUser(id).subscribe(() => this.goBack());
  }

  
}
