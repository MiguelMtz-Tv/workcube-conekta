import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TarjetasService } from 'src/app/services/tarjetas.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { DataService } from 'src/app/services/data.service';
import { HotToastService } from '@ngneat/hot-toast';
@Component({
  selector: 'app-update-card',
  templateUrl: './update-card.component.html',
  styleUrls: ['./update-card.component.css']
})
export class UpdateCardComponent implements OnInit {
  date = new Date()
  dateInvalid: boolean = false
  expYear: any
  expMonth: any

  currentMonth = this.date.getMonth()
  currentYear= String(this.date.getFullYear())

  buttonLoading: boolean = false
  
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private tarjetasService: TarjetasService,
    private thisDialog: MatDialogRef<UpdateCardComponent>,
    private spinner: NgxSpinnerService,
    private dataService: DataService,
    private toast: HotToastService,
  ){}

  ngOnInit(){
    this.spinner.show('update-card')
    this.tarjetasService.getCard({
      IdCliente: this.data.idCliente,
      CardStripeId: this.data.cardId,
    })
    .subscribe(res => {
      this.form.setValue({
        name:     res.result.name || '',
        expMonth: res.result.expMonth || '',
        expYear:  res.result.expYear || '',
      })
      this.spinner.hide('update-card')
    })
  }

  form = new FormGroup({
    name:     new FormControl('', Validators['required']),
    expMonth: new FormControl('', Validators['required']),
    expYear:  new FormControl('', Validators['required']),
  })

  onSubmit(){
   this.buttonLoading = true
    this.tarjetasService.updateCard({
      IdCliente: this.data.idCliente,
      CardStripeId: this.data.cardId,
      Name: this.form.value.name,
      ExpMonth: this.form.value.expMonth,
      ExpYear: this.form.value.expYear,
    })
    .subscribe(res => {
      this.thisDialog.close()
      this.dataService.updateData('');
              this.toast.success('Metodo de pago actualizado', {
                style: {
                  border: '1px solid #3F51B5',
                  margin: '100px 20px',
                  padding: '15px'
                },
                iconTheme: {
                  primary: '#3F51B5'
                },
                position: 'top-right'
              });
    })
  }

  //verificacion de fecha
  onDateChange(){
    let date = new Date(this.expYear+'-'+this.expMonth+'-'+'02')
    let currentDate = new Date()

    if(date < currentDate || !isNaN(+date) == false){
      this.dateInvalid = true
    }else{
      this.dateInvalid = false
    }

  }
}
