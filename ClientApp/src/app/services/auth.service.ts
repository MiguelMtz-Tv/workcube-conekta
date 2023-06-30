import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getBaseUrl } from 'src/main';
import { Router } from '@angular/router'; 
import * as jwt from 'jose';

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

  storeData(token: string, id: string, nombreCompleto: string, idCliente: string, email: string, stripePk: string){
    localStorage.setItem('Token',           token)
    localStorage.setItem('Id',              id)
    localStorage.setItem('NombreCompleto',  nombreCompleto)
    localStorage.setItem('IdCliente',       idCliente)
    localStorage.setItem('Email',           email)
    localStorage.setItem('Spk',             stripePk)
  }

  removeData(){
    localStorage.clear()
    window.location.reload()
  }

  getSPK(){
    return localStorage.getItem('Spk') || ''
  }

  getToken(){
    return localStorage.getItem('Token')
  }

  getUserId(){
    return localStorage.getItem('Id') || ''
  }

  getClientId(){
    return localStorage.getItem('IdCliente')
  }

  getNombreCompleto(){
    return localStorage.getItem('NombreCompleto')
  }

  areLogin() : boolean{
    return !!localStorage.getItem('Token')
  }

  isTokenExpired(){
    const authToken = this.getToken();

    if (authToken) {
      try {
        const tokenPayload = jwt.decodeJwt(authToken) as any;
        const expirationDate = new Date(tokenPayload.exp * 1000); // El campo 'exp' debe contener la fecha de expiración en segundos

        return expirationDate < new Date(); // Devuelve true si el token ha expirado
      } catch (error) {
        console.error('Error al decodificar el token:', error);
      }
    }
    return false; // Devuelve false si el token no existe o no ha expirado
  } 
}
