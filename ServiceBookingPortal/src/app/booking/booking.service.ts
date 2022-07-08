import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { ResponseObj } from './user-request';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl:string = "https://localhost:5001/api/service";

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private httpClient:HttpClient) { }

  getRequests():Observable<any>
  {
    return this.httpClient.get(this.apiUrl)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  deleteRequest(id:number):Observable<any>
  {
    return this.httpClient.delete(this.apiUrl+"/"+id , this.httpOptions)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  getRquestById(id:number):Observable<any>
  {
    return this.httpClient.get(this.apiUrl+"/"+id)
    .pipe(
      catchError(this.errorHandler)
    )
  }


  errorHandler(error:any) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
 }
}
