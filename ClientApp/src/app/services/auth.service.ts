import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _authenticated: boolean
  private baseUrl: string = getBaseUrl();


  constructor(private http: HttpClient, private router: Router) {
    this._authenticated = false
  }

  login(objUser: any) {
    return this.http.post<any>(this.baseUrl+'api/auth/login', objUser /*{UserName, Password}*/);
  }

  storeData(token: string, id: string, NombreCompleto: string, IdCliente: string){
    localStorage.setItem('token', token)
    localStorage.setItem('Id', id)
    localStorage.setItem('NombreCompleto', NombreCompleto),
    localStorage.setItem('IdCliente', IdCliente)
  }

  removeToken(){
    localStorage.removeItem('token')
    localStorage.removeItem('Id')
    localStorage.removeItem('NombreCompleto')
    localStorage.removeItem('IdCliente')
  }

  getToken(){
    return localStorage.getItem('token')
  }

  getId(){
    return localStorage.getItem('id')
  }

  getNombreCompleto(){
    return localStorage.getItem('nombreCompleto')
  }

  areLogin() : boolean{
    return !!localStorage.getItem('token')
  }
}
