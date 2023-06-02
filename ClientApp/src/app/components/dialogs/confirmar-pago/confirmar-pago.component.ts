import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PagarComponent } from 'src/app/pages/pagar/pagar.component';
import { PagosService } from 'src/app/services/pagos.service';

@Component({
  selector: 'app-confirmar-pago',
  templateUrl: './confirmar-pago.component.html',
  styleUrls: ['./confirmar-pago.component.css']
})
export class ConfirmarPagoComponent {
  constructor(
    public dialogRef: MatDialogRef<PagarComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private pagosService: PagosService,
    ){}

  confirmPayment(){
    console.log(this.data)

    this.pagosService.confirmPayment(this.data)
      .subscribe(res => {
        console.log(res)
      })
  }
}
