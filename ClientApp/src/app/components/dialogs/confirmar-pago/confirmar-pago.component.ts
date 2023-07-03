import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PagarComponent } from 'src/app/pages/pagar/pagar.component';
import { PagosService } from 'src/app/services/pagos.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { catchError, throwError } from 'rxjs';
import { HotToastService } from '@ngneat/hot-toast';
import { Router } from '@angular/router';

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
    private spinner: NgxSpinnerService,
    private toast: HotToastService,
    private thisDialog: MatDialogRef<ConfirmarPagoComponent>,
    private router: Router,
    ){}

  confirmPayment(){
    this.spinner.show()
    this.pagosService.confirmPayment(this.data)
      .pipe(
        catchError((e) => {
          this.spinner.hide()
          this.toast.error('No se pudo procesar el pago, intentelo con otro metodo de pago o contacte a un administrador', {
            style: {
              border: '1px solid #FF0000',
              margin: '100px 20px',
              padding:'15px'
            },
            position: 'top-right'
          })

          return throwError(e)
        })
      )
      .subscribe(res => {
        this.spinner.hide()
        this.thisDialog.close()
        if(res.action){
          this.router.navigateByUrl('/pago-confirmado')
        }else{
          console.log(res.message)
          this.toast.error(res.message, {
            style: {
              border: '1px solid #FF0000',
              margin: '100px 20px',
              padding:'15px'
            },
            position: 'top-right'
          })
        }
      })
  }
}
