import { Injectable } from '@angular/core';
import { getBaseUrl } from 'src/main';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ServiciosService {
  baseUrl: string = getBaseUrl()
  idUser: any = ''

  constructor(private http: HttpClient) { this.idUser = localStorage.getItem('IdCliente') }

  getUserServices(){
    return this.http.get(this.baseUrl+'api/servicio/clientservices/'+this.idUser)
    
  }
}
