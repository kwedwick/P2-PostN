import { Injectable } from '@angular/core';
import { Post } from './interfaces/post';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { baseUrl } from 'src/environments/environment';
import { Comment } from './interfaces/comment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json'})
  };
  private url = `${baseUrl}posts`;

  constructor(private http: HttpClient) { }

  getPosts(): Observable<Post[]>
  {
    return this.http.get<Post[]>(this.url)
    .pipe(
      //tap(_ => this.log('fetched users')),
      catchError(this.handleError<Post[]>('getPosts', [])
    ));
  }

  getPostById(id: number)
  {
    const url = `${this.url}/${id}`;
    return this.http.get<Post>(url)
            .pipe(
              //tap(_ => this.log(`fetched post id=${id}`)),
              catchError(this.handleError<Post>(`getPostById id=${id}`))
            );
  }

  getFriendPosts(id: number)
  {
    const url = `${this.url}/${id}/GetFriendPosts/friends`;
    return this.http.get<Post[]>(url)
            .pipe(
              //tap(_ => this.log(`fetched post id=${id}`)),
              catchError(this.handleError<Post[]>(`getPostById id=${id}`))
            );
  }

   /** POST: add a new post to the server */
   addPost(post: any): Observable<Post> {
    return this.http.post<Post>(this.url, post, this.httpOptions).pipe(
      //tap((newPost: Post) => this.log(`added post w/ id=${newPost.id}`)),
      catchError(this.handleError<Post>('addPost'))
    );
  }

  /** POST: add a new Comment to the server */
  addComment(postId: number, comment: Comment): Observable<any> {
    const url = `${this.url}/${postId}/comment`;
    return this.http.post<Comment>(url, comment, this.httpOptions).pipe(
      //tap((newComment: Comment) => this.log(`added comment w/ id=${newComment.id}`)),
      catchError(this.handleError<Comment>('addComment'))
    );
  }

   /** PUT: update the user on the server */
   updatePost(postId: number, post: Post): Observable<any> {
    const url = `${this.url}/${postId}`;
    return this.http.put<Post>(url, post, this.httpOptions).pipe(
      //tap(_ => this.log(`updated user id=${post.id}`)),
      catchError(this.handleError<any>('updatePost'))
    );
  }

  /** PUT: add a new Comment to the server */
    updateComment(postId: number, commentId: number, comment: Comment): Observable<any> {
    const url = `${this.url}/${postId}/comment/${commentId}`;
    return this.http.put<Comment>(url, comment, this.httpOptions).pipe(
      //tap((newComment: Comment) => this.log(`added comment w/ id=${newComment.id}`)),
      catchError(this.handleError<Comment>('updateComment'))
    );
  }

      /** DELETE: delete the post from the server */
  deletePostById(id: number): Observable<Post> {
    const url = `${this.url}/${id}`;
    return this.http.delete<Post>(url, this.httpOptions).pipe(
      //tap(_ => this.log(`deleted post id=${id}`)),
      catchError(this.handleError<Post>('deletePost'))
    );
  }

  /** DELETE: add a new Comment to the server */
  deleteComment(postId: number, commentId: number): Observable<any> {
    const url = `${this.url}/${postId}/comment/${commentId}`;
    return this.http.delete<Comment>(url, this.httpOptions).pipe(
      //tap(_ => this.log(`deleted comment id=${commentId}`)),
      catchError(this.handleError<Comment>('updateComment'))
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
