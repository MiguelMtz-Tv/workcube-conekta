import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Server } from '../libraries/server';

@Injectable({
  providedIn: 'root'
})
export class ServiciosService {
  idCliente: any
  baseUrl: string = ''

  constructor(private http: HttpClient) { 
    this.idCliente = localStorage.getItem('IdCliente'), this.baseUrl = Server.base() 
  }

  getServiceDetails(IdService : any){
    return this.http.post<any>(this.baseUrl + 'api/servicio/', {IdCliente: this.idCliente, IdServicio: IdService})
  }

  getUserServices(){
    return this.http.post<any>(this.baseUrl+'api/servicio/list/', this.idCliente)
  }

  cancelService(serviceData : any){
    return this.http.put<any>(this.baseUrl+'api/servicio/cancel/', serviceData)
  }
}