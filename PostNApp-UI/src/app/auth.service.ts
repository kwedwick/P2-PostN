import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { baseUrl } from 'src/environments/environment';
import { JwtHelperService } from "@auth0/angular-jwt";
import { map } from 'rxjs/operators';
import { IUser } from './interfaces/app-user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  helper = new JwtHelperService();

  decodedToken = this.helper.decodeToken(localStorage.getItem("jwt")!)
  // currentUser: IUser = {
  //   id: this.decodedToken.userId || null!,
  //   username: this.decodedToken.nameid || null!,
  //   email: this.decodedToken.email  || null!,
  //   role: this.decodedToken.role  || null!,
  // }
  currentUser: IUser = {
    id: null || this.decodedToken?.userId,
    username: null || this.decodedToken?.nameid,
    email: null ||  this.decodedToken?.email,
    role: null ||  this.decodedToken?.role,
  }

  
  constructor(private http: HttpClient, private router: Router) { }

  login(data: any): Observable<IUser> {
    return this.http.post(`${baseUrl}auth/login`, data)
      .pipe(
        map((response: any) => {
          const decodeToken = this.helper.decodeToken(response.token);
          this.currentUser.id = decodeToken.userId;
          this.currentUser.username = decodeToken.nameid;
          this.currentUser.email = decodeToken.email;
          this.currentUser.role = decodeToken.role;
          localStorage.setItem('jwt', response.token);
          return this.currentUser;
        })
      );
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('jwt')!;
    return !this.helper.isTokenExpired(token);
  }

  logout() {
    this.currentUser = {
      id: null!,
    username: null!,
    email: null!,
    role: null!,
    }
    localStorage.removeItem('jwt');
    this.router.navigate(['/login']);
  }

}
