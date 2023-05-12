import { Injectable, Inject } from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _authenticated: boolean

  constructor(@Inject(APP_BASE_HREF) private baseUrl: string, private _httpClient: HttpClient, ) { 
    this._authenticated = false
  }
  
  //setter anf getter for acces token 
}
