import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {  Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Product, Sample } from './product';
import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private apiURL = environment.ConnectedServices.Product;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private httpClient: HttpClient, private toastr: ToastrService) { }

  getAll(): Observable<any> {

    return this.httpClient.get(this.apiURL + 'api/products/')

    .pipe(
      catchError(this.errorHandler)
    )
  }

  create(product:Sample): Observable<any> {

    return this.httpClient.post(this.apiURL + 'api/products/', product, this.httpOptions)

    .pipe(
      catchError(this.errorHandler)
    )
  }

  find(id: number): Observable<any> {

    return this.httpClient.get(this.apiURL + 'api/products/' + id)

    .pipe(
      catchError(this.errorHandler)
    )
  }

  update(id: number, product:Product): Observable<any> {

    return this.httpClient.put(this.apiURL + 'api/products/' + id, product, this.httpOptions)

    .pipe(
      catchError(this.errorHandler)
    )
  }

  delete(id:number){
    return this.httpClient.delete(this.apiURL + 'api/products/' + id, this.httpOptions)

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
}
