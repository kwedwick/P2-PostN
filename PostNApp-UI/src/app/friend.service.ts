import { Injectable } from '@angular/core';
import { IFriend } from './interfaces/friend';
import { observable, Observable, of, throwError} from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { catchError, retry } from 'rxjs/operators';
import { Router } from '@angular/router';
import { baseUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FriendService {

  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  
  private friendsUrl = `${baseUrl}friends`;
 
  constructor(
    private http: HttpClient,
    private router: Router
    ) { }

  getFriends(id: number): Observable<IFriend[]>
  {
    return this.http.get<IFriend[]>(this.friendsUrl)
      .pipe(
        //tap(_ => this.log('fetched users')),
        catchError(this.handleError<IFriend[]>('getFriends', [])
      ));
  }

  getFriend(id: number): Observable<IFriend>
  {
    const url = `${this.friendsUrl}/${id}`;
    return this.http.get<IFriend>(url)
            .pipe(
              //tap(_ => this.log(`fetched user id=${id}`)),
              catchError(this.handleError<IFriend>(`getIFriend id=${id}`))
            );
    
  }

  checkFriendship(userId: number, friendId: number): Observable<boolean>
  {
    const url = `${this.friendsUrl}/CheckIfFriend/${userId}/${friendId}`;
    return this.http.get<boolean>(url)
            .pipe(
              //tap(_ => this.log(`fetched user id=${id}`)),
              catchError(this.handleError<boolean>(`getIFriend id=${friendId}`))
            );
  }

  /** POST: add a new user to the server */
  addFriend(userId: number, friendId: number): Observable<boolean>{
    const url = `${this.friendsUrl}/${userId}/${friendId}`;
    return this.http.post<boolean>(url, this.httpOptions).pipe(
      //tap((addFriend: IFriend) => this.log(`added friend w/ id=${IFriend.id}`)),
      catchError(this.handleError1));
  }
  
  handleError1(error: HttpErrorResponse){
    
   return throwError(error.error);
  }

  /** PUT: update the user on the server */
  updateFriend(userId: number, friendId: number): Observable<any> {
    const url = `${this.friendsUrl}/${userId}/${friendId}`;
    return this.http.put<any>(url, this.httpOptions).pipe(
      //tap(_ => this.log(`updated friend id=${IFriend.id}`)),
      catchError(this.handleError<any>('updateFriend'))
    );
  }

  /** DELETE: delete the user from the server */
deleteFriend(userId: number, friendId: number): Observable<boolean> {
  const url = `${this.friendsUrl}/${userId}/${friendId}`;

  return this.http.delete<boolean>(url, this.httpOptions).pipe(
    //tap(_ => this.log(`deleted user id=${id}`)),
    catchError(this.handleError<boolean>('deleteFriend'))
  );
}

    /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      //this.log(`${operation} failed: ${error.message}`);
      console.log(operation); //create message service

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  
}
