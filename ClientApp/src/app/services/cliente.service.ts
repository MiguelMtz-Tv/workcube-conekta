import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Server } from '../libraries/server';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  baseUrl = Server.base()
  constructor(private http: HttpClient) { }

  addClient(ClientObj : any){
    return this.http.post<any>(this.baseUrl+'api/clientes', ClientObj)
  }
}
