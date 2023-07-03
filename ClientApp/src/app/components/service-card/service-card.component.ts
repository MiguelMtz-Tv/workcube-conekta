import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { add } from 'date-fns';
import differenceInDays from 'date-fns/differenceInDays';
import { HotToastService } from '@ngneat/hot-toast';
import { MatDialog } from '@angular/material/dialog';
import { CancelServiceComponent } from '../dialogs/cancel-service/cancel-service.component';

@Component({
  selector: 'app-service-card',
  templateUrl: './service-card.component.html',
  styleUrls: ['./service-card.component.css']
})
export class ServiceCardComponent implements OnInit, OnChanges{
  @Input() name = ''
  @Input() description= ''
  @Input() date= ''
  @Input() cost= ''
  @Input() period= ''
  @Input() startDate= ''
  @Input() id = ''
  @Input() isVigente: boolean = false
  @Input() isCanceled: boolean = false

  color = 'bg-gray-500' //on error default
  days= '0'
  canPay = false
  status = ''

  constructor(private toast: HotToastService, private dialog: MatDialog) { }

  //set status color
  setStatusColor(){
    if(this.isCanceled){
      this.color = 'bg-yellow-500'
      this.status = 'Cancelado'
    }else if(this.isVigente){
      this.color = 'bg-green-500'
      this.status = 'Vigente'
    }else{
      this.color = 'bg-red-500'
      this.status = 'Vencido'
    }
  }

  setPaymentButton(){
    if(this.isCanceled){
      this.canPay = false
    }else if(this.isVigente){
      this.canPay = false
    }else{this.canPay = true}
  }

  ngOnInit(){
    //get remaining days
    let closeDate = add(new Date(this.startDate), { days: 30 })
    let daysToDate = differenceInDays(closeDate, new Date())
    if(daysToDate > 0){
      this.days = 'Quedan '+ daysToDate +' día(s)';
    }else{
      this.days = 'Venció hace '+ (daysToDate * -1) +' día(s)'
    }
    //set initial status color
    this.setStatusColor()
    this.setPaymentButton()
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.status && this.isCanceled) {
      this.setStatusColor()
      this.setPaymentButton()
    }
  }

  /* to cancel service */
  openCancelModal(enterAnimationDuration: string, exitAnimationDuration: string , idServicio: any){
    this.dialog.open(CancelServiceComponent,{
      width: '90%',
      maxWidth: '500px',
      enterAnimationDuration,
      exitAnimationDuration,
      data: {
        IdServicio: idServicio,
        IdCliente: localStorage.getItem('IdCliente')
      }
    })
  }

  /* to request reactivation */
  requestReactivation(){
    console.log('proxima actualización :p')
  }

}
