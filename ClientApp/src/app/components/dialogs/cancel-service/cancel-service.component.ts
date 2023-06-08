import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HotToastService } from '@ngneat/hot-toast';
import { ServiciosService } from 'src/app/services/servicios.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-cancel-service',
  templateUrl: './cancel-service.component.html',
  styleUrls: ['./cancel-service.component.css']
})
export class CancelServiceComponent {
  idCliente = localStorage.getItem('IdCliente')
  
  constructor(
    private serviciosService: ServiciosService, 
    @Inject(MAT_DIALOG_DATA) public data: any, 
    private toast: HotToastService,
    private spinner: NgxSpinnerService,
    private thisDialog: MatDialogRef<CancelServiceComponent>,
    private dataService: DataService,
  ){ }

  confirmCancel(){
    this.spinner.show('cancel-service')
    this.serviciosService.cancelService(this.data)
      .subscribe(
        res =>{
          this.spinner.hide('cancel-service')
          this.toast.success('Servicio cancelado', {
            style: {
              border: '1px solid #FFFF00',
              margin: '100px 20px',
              padding: '15px'
            },
            iconTheme: {
              primary: '#FFFF00'
            },
            position: 'top-right'
          })
          this.dataService.updateData('cancel-service')
          this.thisDialog.close()
        },
        error => {
          this.toast.error('No fue posible cancelar este servicio. Por favor contacte a los administradores de Workcube',{
            style: {
              margin: '100px 20px',
              padding: '15px'
            },
            position: 'top-right'
          })
        }
      )
  }
}
