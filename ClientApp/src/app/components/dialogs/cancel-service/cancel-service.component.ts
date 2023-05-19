import { Component, Inject } from '@angular/core';
import { ServiciosService } from 'src/app/services/servicios.service';

@Component({
  selector: 'app-cancel-service',
  templateUrl: './cancel-service.component.html',
  styleUrls: ['./cancel-service.component.css']
})
export class CancelServiceComponent {
  
  constructor(private serviciosService: ServiciosService, @Inject('id') public id: any){ }

  confirmCancel(){
    this.serviciosService.cancelService(this.id)
  }
}
