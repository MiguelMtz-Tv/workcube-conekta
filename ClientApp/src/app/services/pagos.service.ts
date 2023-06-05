import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Server } from '../libraries/server';

@Injectable({
  providedIn: 'root'
})
export class PagosService {
  baseUrl = Server.base()
  constructor(private http: HttpClient) { }

  confirmPayment(paymentObj : any){
    return this.http.post<any>(this.baseUrl+'api/pagos', paymentObj)
  }

  getPaymentsList(idServicio : number){
    return this.http.post<any>(this.baseUrl+'api/pagos/list', idServicio)
  }
}
