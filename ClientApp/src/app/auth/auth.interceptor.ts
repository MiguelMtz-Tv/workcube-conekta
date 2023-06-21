import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private auth: AuthService) {}

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    const token = this.auth.getToken() //localStorage.getItem('Token');

    if (token) {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
      });

      const authReq = req.clone({ headers });

      return next.handle(authReq).pipe(
        catchError(this.handleError)
      );
    }
    
    return next.handle(req);
  }

  handleError(error: HttpErrorResponse){
    console.log(error);
    return throwError(error);
  }
}
