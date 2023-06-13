import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Server } from '../libraries/server';

@Injectable({
  providedIn: 'root'
})
export class CuponesService {
  baseUrl: string = ''
  idCliente: any
  constructor(private http: HttpClient) { this.baseUrl = Server.base(), this.idCliente = localStorage.getItem('IdCliente') }

  getCupones(){
    return this.http.post<any>(this.baseUrl + 'api/cupon/list', { IdCliente: Number(this.idCliente) })
  }

  getCupon(codigo : any){
    return this.http.post<any>(this.baseUrl + 'api/cupon/get', { IdCliente: Number(this.idCliente), Codigo: codigo})
  }

}
