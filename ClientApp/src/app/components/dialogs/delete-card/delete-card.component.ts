import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PaymentCardComponent } from '../../payment-card/payment-card.component';
import { TarjetasService } from 'src/app/services/tarjetas.service';

@Component({
  selector: 'app-delete-card',
  templateUrl: './delete-card.component.html',
  styleUrls: ['./delete-card.component.css']
})
export class DeleteCardComponent {
  constructor(
    public dialogRef: MatDialogRef<PaymentCardComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: any,
    private tarjetasService: TarjetasService,
    ){}

  deleteCard(){
    this.tarjetasService.deleteCard(this.data)
      .subscribe(res => console.log(res))
  }

}
