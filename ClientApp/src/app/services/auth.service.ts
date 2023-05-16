import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _authenticated: boolean
  private baseUrl: string = getBaseUrl();


  constructor(private http: HttpClient) {
    this._authenticated = false
  }

  //setter anf getter for acces token 

  login(objUser: any) {
    this.http.post<any>(this.baseUrl+'api/auth/login', objUser /*{UserName, Password}*/).subscribe(res => this.storeData(res.token, res.id, res.nombreCompleto));
  }

  //store token 
  storeData(token: string, id: string, NombreCompleto: string){
    localStorage.setItem('token', token)
    localStorage.setItem('Id', id)
    localStorage.setItem('NombreCompleto', NombreCompleto)
  }

  //get token
  getToken(){
    return localStorage.getItem('token')
  }

  //get Id
  getId(){
    return localStorage.getItem('id')
  }

  //get nombreCompleto
  getNombreCompleto(){
    return localStorage.getItem('nombreCompleto')
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
