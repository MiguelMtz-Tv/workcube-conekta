import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Sessions } from '../applicationConfig/application-service';
import { APP_BASE_HREF } from "@angular/common";
import { Server } from '../libraries/server';

@Injectable({
  providedIn: 'root'
})
export class ServiciosService {
  idUser: any = ''
  baseUrl: string = ''

  constructor(private http: HttpClient) { this.idUser = localStorage.getItem('IdCliente'), this.baseUrl = Server.base() }

  getUserServices(){
    return this.http.post<any>(this.baseUrl+'api/servicio/list/', { idUser : this.idUser }, Sessions.header())
  }

  cancelService(serviceData : any){
    return this.http.put<any>(this.baseUrl+'api/servicio/cancel/', serviceData, Sessions.header())
  }
}

/* headers:{
  'Content-Type'      : 'application/json',
  'Authorization'     : 'Bearer ' + localStorage.getItem('token')
} */
