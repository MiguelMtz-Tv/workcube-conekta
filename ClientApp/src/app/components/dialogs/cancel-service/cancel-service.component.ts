import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ServiciosService } from 'src/app/services/servicios.service';

@Component({
  selector: 'app-cancel-service',
  templateUrl: './cancel-service.component.html',
  styleUrls: ['./cancel-service.component.css']
})
export class CancelServiceComponent {
  idCliente = localStorage.getItem('IdCliente')
  
  constructor(private serviciosService: ServiciosService, @Inject(MAT_DIALOG_DATA) public data: any){ }

  confirmCancel(){
    this.serviciosService.cancelService(this.data).subscribe(res => console.log(res))
    console.log(this.data)
  }
}
