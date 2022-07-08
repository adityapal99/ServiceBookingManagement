import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { user } from './register.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  apiUrl = environment.ConnectedServices.User;
  constructor(private http:HttpClient){}
  addUser(data: user)
  {
    return this.http.post(this.apiUrl + "user/", data)
  }
}
