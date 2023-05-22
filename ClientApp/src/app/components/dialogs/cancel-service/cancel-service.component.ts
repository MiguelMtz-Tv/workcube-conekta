import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HotToastService } from '@ngneat/hot-toast';
import { ServiciosService } from 'src/app/services/servicios.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-cancel-service',
  templateUrl: './cancel-service.component.html',
  styleUrls: ['./cancel-service.component.css']
})
export class CancelServiceComponent {
  idCliente = localStorage.getItem('IdCliente')
  
  constructor(private serviciosService: ServiciosService, @Inject(MAT_DIALOG_DATA) public data: any, private toast: HotToastService){ }

  confirmCancel(){
    this.serviciosService.cancelService(this.data).
    pipe(
      catchError(error => {
        this.toast.error('No fue posible cancelar este servicio. Por favor contacte a los administradores de Workcube',{
          style: {
            margin: '100px 20px',
            padding: '15px'
          },
          position: 'top-right'
        })
        return throwError(error)
      })
    )
    .subscribe(
      res =>{
        this.toast.success('Servicio cancelado', {
          style: {
            margin: '100px 20px',
            padding: '15px'
          },
          iconTheme: {
            primary: '#FFFF00'
          },
          position: 'top-right'
        })
      }
    )
  }
}
