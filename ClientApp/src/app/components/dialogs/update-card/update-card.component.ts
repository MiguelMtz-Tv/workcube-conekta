import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TarjetasService } from 'src/app/services/tarjetas.service';

@Component({
  selector: 'app-update-card',
  templateUrl: './update-card.component.html',
  styleUrls: ['./update-card.component.css']
})
export class UpdateCardComponent {
  date = new Date()
  dateInvalid: boolean = false
  expYear: any
  expMonth: any

  currentMonth = this.date.getMonth() + 1
  currentYear= String(this.date.getFullYear())
  
  constructor(
    @Inject(MAT_DIALOG_DATA) public data : any,
    private tarjetasService: TarjetasService,
  ){}


  form = new FormGroup({
    name: new FormControl(''),
    expMonth: new FormControl(''),
    expYear: new FormControl(''),
  })

  onSubmit(){
    console.log(
      {IdCliente: this.data.idCliente,
      CardStripeId: this.data.cardId,
      Name: this.form.value.name,
      ExpMonth: this.form.value.expMonth,
      ExpYear: this.form.value.expYear,}
    )
    
    console.log(this.data)

    this.tarjetasService.updateCard({
      IdCliente: this.data.idCliente,
      CardStripeId: this.data.cardId,
      Name: this.form.value.name,
      ExpMonth: this.form.value.expMonth,
      ExpYear: this.form.value.expYear,
    }).subscribe(res => console.log(res))
  }

  //verificacion de fecha
  onDateChange(){
    if (this.expYear < this.currentYear || this.expMonth > 12) {
      this.dateInvalid = true;
    } else if (this.expYear === this.currentYear && this.expMonth <= this.currentMonth) {
      this.dateInvalid = true;
    } else {
      this.dateInvalid = false;
    }
  }
}
