import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as moment from 'moment';
import jwt_decode from 'jwt-decode';
import { AuthorizationRequest, JwtDecoded, ResponseObject, Role } from './auth.model';
import { catchError, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  login(request: AuthorizationRequest) {
      const loginObserver =  this.http.post<ResponseObject>(environment.ConnectedServices.User + "user/login", {email: request.email, password: request.password}, {
          headers: {
              'Content-Type': 'application/json'
          }
      })
      loginObserver.subscribe(this.setSession.bind(this))
      return loginObserver.pipe(
        catchError((err) => {
          console.error(err)
          return of(null)
        }
      ));
  }

  private setSession(authResult: ResponseObject) {
      const jwtData: JwtDecoded = this.getDecodedAccessToken(authResult.payload.accessToken);

      console.log(authResult)
      console.log(jwtData)

      const expiresAt = moment().add(jwtData.exp - moment.now(), 'millisecond');

      localStorage.setItem('access_token', authResult.payload.accessToken);
      localStorage.setItem('id', jwtData.unique_name);
      localStorage.setItem('role', jwtData.role);
      localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()));

      this.toastr.success("Logged In Successfully")
  }

  private getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch(Error) {
      return null;
    }
  }

  logout() {
      localStorage.removeItem("access_token");
      localStorage.removeItem("expires_at");
  }

  public isLoggedIn() {
      return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
      return !this.isLoggedIn();
  }

  isAdmin() {
    return this.isLoggedIn() && localStorage.getItem("role") === Role.ADMIN;
  }

  getExpiration() {
    const expiration = localStorage.getItem("expires_at");
    const expiresAt = JSON.parse(expiration ?? "");
    return moment(expiresAt);
  }

}
