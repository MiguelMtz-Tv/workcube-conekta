import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private modalCloser = new Subject<any>()
  private paymentCardId = new Subject<any>()
  private paymentCardNewData = new Subject<any>()
  private cancelService = new Subject<any>()
  private statusColor = new Subject<any>()
  private paymentCardData = new Subject<any>()

  constructor(){}

  //global modal closer :)
  setModalClose(value: any){
    this.modalCloser.next(value)
  }
  getModalClose(){
    return this.modalCloser.asObservable()
  }

  //to get a card id
  setPaymentCardId(value: any){
    this.paymentCardId.next(value);
  }
  getPaymentCardId(){
    return this.paymentCardId.asObservable()
  }

  //to get the data of edit payment card
  setPaymentCardNewData(value: any){
    this.paymentCardNewData.next(value)
  }
  getPaymentCardNewData(){
    return this.paymentCardNewData.asObservable()
  }

  //to get data of services card
  setCancelService(value: any){
    this.cancelService.next(value)
  }
  getCancelService(){
    return this.cancelService.asObservable()
  }

  //to get data of add card in payment
  setCardDataService(value: any){
    this.paymentCardData.next(value)
  }
  getCardDataService(){
    return this.paymentCardData.asObservable()
  }
}
