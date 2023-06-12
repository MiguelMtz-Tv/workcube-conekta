import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PaymentCardComponent } from '../../payment-card/payment-card.component';
import { TarjetasService } from 'src/app/services/tarjetas.service';
import { DataService } from 'src/app/services/data.service';
import { DialogRef } from '@angular/cdk/dialog';

@Component({
  selector: 'app-delete-card',
  templateUrl: './delete-card.component.html',
  styleUrls: ['./delete-card.component.css']
})
export class DeleteCardComponent {
  isDeleting: boolean = false

  constructor(
    public dialogRef: MatDialogRef<PaymentCardComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: any,
    private tarjetasService: TarjetasService,
    private dataService: DataService,
    private thisDialog: DialogRef<DeleteCardComponent>
    ){}

  deleteCard(){
    this.isDeleting = true
    this.tarjetasService.deleteCard(this.data)
      .subscribe(res => {
        this.thisDialog.close()
        this.dataService.updateData('data')
        this.isDeleting = false
      })
  }

}
