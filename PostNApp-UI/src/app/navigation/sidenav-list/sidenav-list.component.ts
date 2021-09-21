import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from 'src/app/auth.service';

@Component({
  selector: 'app-sidenav-list',
  templateUrl: './sidenav-list.component.html',
  styleUrls: ['./sidenav-list.component.css']
})
export class SidenavListComponent implements OnInit {
  @Output() sidenavClose = new EventEmitter();
  constructor(public authService: AuthService) { }

  ngOnInit(): void {
  }

  public onSidenavClose = () => 
  {
    this.sidenavClose.emit();
  }

  logout() {
    this.authService.logout();
    alert("You have logged out. Come back soon!");
  }

  isAdmin(): boolean {
    return this.authService.currentUser.role == "Administrator" ? true : false;
  }
  isUser(): boolean {
    return this.authService.currentUser.role == "User" ? true : false;
  }
}

//*ngIf="!authService.loggedIn()"