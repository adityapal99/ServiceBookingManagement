import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User, UserListResponse, UserResponse } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiUrl: string = environment.ConnectedServices.User;

  constructor(private httpClient: HttpClient, private toastr: ToastrService) { }

  getAll(): Observable<UserListResponse> {
    return this.httpClient.get(this.apiUrl + "user").pipe(
      catchError<any, any>(this.errorHandler)
    );
  }

  create(user: User): Observable<UserResponse> {
    return this.httpClient.post(this.apiUrl + "user", user).pipe(
      catchError<any, any>(this.errorHandler)
    );
  }

  find(id: number): Observable<UserResponse> {
    return this.httpClient.get(this.apiUrl + "user/" + id).pipe(
      catchError<any, any>(this.errorHandler)
    );
  }

  update(id: number, user: User): Observable<UserResponse> {
    return this.httpClient.put(this.apiUrl + "user/" + id, user).pipe(
      catchError<any, any>(this.errorHandler)
    );
  }

  delete(id: number): Observable<Response> {
    return this.httpClient.delete(this.apiUrl + "user/" + id).pipe(
      catchError<any, any>(this.errorHandler)
    );
  }

  errorHandler(error:any) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    this.toastr.error(errorMessage, 'Error');
    return throwError(() => new Error(errorMessage));
 }
}
