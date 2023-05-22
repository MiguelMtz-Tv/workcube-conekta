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
    return this.http.post<any>(this.baseUrl+'api/servicio/list/', { idUser : this.idUser }, {
      headers:{
        'Content-Type'      : 'application/json',
        'Authorization'     : 'Bearer ' + localStorage.getItem('token')
      }
    })
  }

  cancelService(serviceData : any){
    return this.http.put<any>(this.baseUrl+'api/servicio/cancel/', serviceData, Sessions.header())
  }
}

/* headers:{
  'Content-Type'      : 'application/json',
  'Authorization'     : 'Bearer ' + localStorage.getItem('token')
} */

/* eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzdWFyaW8xIiwiSWQiOiI1NTQ4MDEwNy03MDc1LTQyM2YtOTcwNS02MDZhMTBlNTFiMmYiLCJOb21icmUiOiJNQVJUSEEgQVZBTE9TIElTSURSTyIsImp0aSI6IjE5YjQ1YzVhLTk3ODAtNGJlZC1iYWU0LWZlZDBlMTNkNjVjZiIsIm5iZiI6MTY4NDc5NjU2OSwiZXhwIjoxNjg0ODAwMTY5LCJpYXQiOjE2ODQ3OTY1Njl9.GJrtvoGn9s0GGhgvsGAXpMb0UymRMal_meptawAQmoc */