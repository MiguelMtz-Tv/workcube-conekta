import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Server } from '../libraries/server';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class TarjetasService {
  baseUrl = Server.base()
  constructor(private http: HttpClient, private auth: AuthService) { }

  addCard(cardObj : any){
    return this.http.post<any>(this.baseUrl+'api/tarjetas', cardObj)
  }

  listCards(){
    return this.http.post<any>(this.baseUrl+'api/tarjetas/list', this.auth.getClientId())
  }

  deleteCard(cardObj : any){
    return this.http.post<any>(this.baseUrl+'api/tarjetas/delete', cardObj)
  }

  updateCard(cardObj : any){
    return this.http.post<any>(this.baseUrl+'api/tarjetas/update', cardObj)
  }

  getCard(cardObj : any){
    return this.http.post<any>(this.baseUrl+'api/tarjetas/get', cardObj)
  }
}
