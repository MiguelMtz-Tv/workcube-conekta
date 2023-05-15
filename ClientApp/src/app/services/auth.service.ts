import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _authenticated: boolean
  private baseUrl: string = getBaseUrl();


  constructor(private _httpClient: HttpClient) {
    this._authenticated = false
  }

  //setter anf getter for acces token 

  login(objUser: any) {
    this._httpClient.post<any>(`${this.baseUrl}api/auth/login`, objUser /*{UserName, Password}*/).subscribe(res => this.storeToken(res.token));
  }

  //store token 
  storeToken(token : string){
    localStorage.setItem('token', token)
  }

  //get token
  getToken(){
    return localStorage.getItem('token')
  }

  //remove token (close session)
  removeToken(){
    return localStorage.removeItem('token')
  }

  //verufy token
  areLogin() : boolean{
    return !!localStorage.getItem('token')
  }
}
