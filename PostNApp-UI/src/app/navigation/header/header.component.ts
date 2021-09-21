import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from 'src/app/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @Output() public sidenavToggle = new EventEmitter();
  constructor(public authService: AuthService) { }

  ngOnInit(): void {
  }

  public onToggleSidenav =() => {
    this.sidenavToggle.emit();
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
