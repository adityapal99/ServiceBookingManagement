import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Sample, SampleReport, UserReport, UserRequest } from './user-request';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl:string = environment.ConnectedServices.ServiceBooking;
 private apiUrl2:string= this.apiUrl+"/report";

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private httpClient: HttpClient, private toastr: ToastrService) { }

  getRequests():Observable<any>
  {
    return this.httpClient.get(this.apiUrl)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  deleteRequest(id:number):Observable<any>
  {
    return this.httpClient.delete(this.apiUrl + id , this.httpOptions)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  getRquestById(id:number):Observable<any>
  {
    return this.httpClient.get(this.apiUrl + id)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  addRequest(request:Sample):Observable<any>
  {
    return this.httpClient.post(this.apiUrl, JSON.stringify(request),this.httpOptions)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  updateRequest(id:number, request:UserRequest): Observable<any> {

    return this.httpClient.put(this.apiUrl + id, JSON.stringify(request), this.httpOptions)

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
    this.toastr.error(errorMessage, 'Error');
    return throwError(() => new Error(errorMessage));
 }


getReport():Observable<any>
  {
    return this.httpClient.get(this.apiUrl2)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  deleteReport(id:number):Observable<any>
  {
    return this.httpClient.delete(this.apiUrl2 + id , this.httpOptions)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  getReportById(id:number):Observable<any>
  {
    return this.httpClient.get(this.apiUrl2 + id)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  addReport(report:SampleReport):Observable<any>
  {
    return this.httpClient.post(this.apiUrl2, JSON.stringify(report),this.httpOptions)
    .pipe(
      catchError(this.errorHandler)
    )
  }

  updateReport(id:number, report:UserReport): Observable<any> {

    return this.httpClient.put(this.apiUrl2 + id, JSON.stringify(report), this.httpOptions)

    .pipe(
      catchError(this.errorHandler)
    )
  }

}
