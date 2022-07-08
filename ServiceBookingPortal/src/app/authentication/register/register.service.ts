import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { user } from './register.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  url="";
  constructor(private http:HttpClient){}
  addUser(data:user)
  {
    return this.http.post(this.url,data)
  }
}
