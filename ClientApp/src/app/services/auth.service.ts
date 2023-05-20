import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from '../../main';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl: string = getBaseUrl();
  constructor(private http: HttpClient) { }

  login(loginObj: any) {
    return this.http.post(`${this.baseUrl}api/auth/login`, loginObj)
  }
}
