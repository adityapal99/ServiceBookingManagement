import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>,
              next: HttpHandler): Observable<HttpEvent<any>> {

        const idToken = localStorage.getItem("access_token");
        if (idToken) {
            const cloned = req.clone({
                headers: new HttpHeaders({
                    "Authorization": "Bearer " + idToken
                  })
            });

            return next.handle(cloned);
        }
        else {
            return next.handle(req);
        }
    }
}
