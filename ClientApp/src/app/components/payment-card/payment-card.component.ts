import { Component, Input, ViewChild, ElementRef } from '@angular/core';
import { DataService } from 'src/app/services/data.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HotToastService } from '@ngneat/hot-toast';

@Component({
  selector: 'app-payment-card',
  templateUrl: './payment-card.component.html',
  styleUrls: ['./payment-card.component.css']
})
export class PaymentCardComponent {

  @Input() finishedIn: string;
  @Input() owner: string;
  @Input() expiration: string;
  @Input() id : string;

  @ViewChild('editModal') editModal: ElementRef | undefined;
  @ViewChild('deleteModal') deleteModal: ElementRef | undefined;

  constructor(private dataService: DataService, private toast: HotToastService){
    this.finishedIn = '';
    this.owner = '';
    this.expiration = '';
    this.id = '';
  }

  //open modals to edit and delete
  openEditModal(){
    this.editModal?.nativeElement.click();
  }
  openDeleteModal(){
    this.deleteModal?.nativeElement.click();
  }
  //

  closeModal(){
    this.dataService.setModalClose('close')
  }

  deleteCard(id : any){
    this.dataService.setPaymentCardId(id);
    this.toast.success('Tarjeta eliminada',{
      style: {
        margin: '90px'
      },
      iconTheme: {
        primary: '#E60B5A',
        secondary: '#fff',
      }
    })
  }

  //update form:
  form = new FormGroup({
     /* owner: new FormControl('', [Validators.required]), */
     expiration: new FormControl('', [Validators.required])
  })

  onSubmit(id: any){
    let data = {
      id: id,
      /* owner: this.form.value.owner, */
      expiration: this.form.value.expiration
    } 
    this.dataService.setPaymentCardNewData(data)
    this.dataService.setModalClose('close')
    this.toast.success('Tarjeta actualizada',{
      style: {
        margin: '90px'
      },
      iconTheme: {
        primary: '#5e15c2',
        secondary: '#fff',
      }
    })
  }
  //
}
