import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Server } from '../libraries/server';

@Injectable({
  providedIn: 'root'
})
export class TarjetasService {
  baseUrl = Server.base()
  constructor(private http: HttpClient) { }

  addCard(cardObj : any){
    return this.http.post<any>(this.baseUrl+'api/tarjetas', cardObj)
  }
}
