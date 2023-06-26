import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Server } from '../libraries/server';
import { Sessions } from '../applicationConfig/application-service';
import id from 'date-fns/locale/id/index';

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

  getPdfFile(idFile : number){
    return this.http.get<any>(this.baseUrl+'api/pagos/file/'+idFile)
  }
}
