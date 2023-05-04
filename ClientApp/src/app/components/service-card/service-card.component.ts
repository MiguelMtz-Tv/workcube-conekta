import { Component, Input, OnInit, OnChanges, ViewChild, ElementRef, SimpleChanges } from '@angular/core';
import { add } from 'date-fns';
import differenceInDays from 'date-fns/differenceInDays';
import { DataService } from 'src/app/services/data.service';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-service-card',
  templateUrl: './service-card.component.html',
  styleUrls: ['./service-card.component.css']
})
export class ServiceCardComponent implements OnInit, OnChanges{
  @Input() name = ''
  @Input() status= ''
  @Input() description= ''
  @Input() date= ''
  @Input() cost= ''
  @Input() period= ''
  @Input() startDate= ''
  @Input() id = ''
  color = 'bg-gray-500'
  days= '0'
  canPay = false

  @ViewChild('cancelModal') cancelModal: ElementRef | undefined

  constructor(private dataService: DataService, private toast: HotToastService) { }

  //set status color
  setStatusColor(){
    switch(this.status){
      case 'Vigente':
        this.color = 'bg-green-500'
        break
      case 'Vencido':
        this.color = 'bg-red-500'
        break
      case 'Cancelado':
        this.color = 'bg-yellow-500'
        break
    } 
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.status && this.status === 'Cancelado') {
      this.setStatusColor()
      this.canPay = false
      this.toast.success('Servicio cancelado', {
        style: {
          margin: '90px'
        },
        iconTheme: {
          primary: '#5e15c2',
          secondary: '#fff',
        }
    })
    }
  }

  ngOnInit(){
    //get remaining days
    let closeDate = add(new Date(this.startDate), { days: 30 })
    let daysToDate = differenceInDays(closeDate, new Date())
    if(daysToDate > 0){
      this.days = 'Quedan '+daysToDate+' día(s)';
      this.status = 'Vigente';
    }else{
      this.days = 'Venció hace '+(daysToDate*-1)+' día(s)'
      this.status = 'Vencido'
    }
    //set initial status color
    this.setStatusColor()

    //set pyament button
    this.status != 'Vigente' ? this.canPay = true : null
  }

  /* to cancel service */
  openCancelModal(){
    this.cancelModal?.nativeElement.click()
  }
  cancelService(id: any){
    this.dataService.setCancelService(id)
  }

  closeModal(){
    this.dataService.setModalClose('close')
  }

}
